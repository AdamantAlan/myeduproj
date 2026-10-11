namespace YandexBackuper;

public sealed class BackupService
{
    private readonly YandexDiskClient disk;
    private readonly DownloadService downloader;
    private readonly int parallelism;

    public BackupService(
        YandexDiskClient disk,
        DownloadService downloader,
        int parallelism = 5)
    {
        this.disk = disk;
        this.downloader = downloader;
        this.parallelism = parallelism;
    }

    public async Task RunAsync(
        string directory,
        CancellationToken ct = default)
    {
        directory = Path.GetFullPath(directory);
        Directory.CreateDirectory(directory);

        var files = new List<DiskResource>();

        Console.WriteLine("Scanning Yandex Disk...");

        await ScanAsync("disk:/", directory, files, ct);

        var totalBytes = files.Sum(x => x.Size ?? 0);
        var completed = 0;
        var downloaded = 0;
        var skipped = 0;
        var failed = 0;
        long processedBytes = 0;

        Console.WriteLine(
            $"Found {files.Count} files, " +
            $"{totalBytes / 1024d / 1024 / 1024:F2} GB");

        await Parallel.ForEachAsync(
            files,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = parallelism,
                CancellationToken = ct
            },
            async (file, token) =>
            {
                var relative = GetRelativePath(file.Path);
                var destination = GetSafePath(directory, relative);

                try
                {
                    using var timeoutCts =
                        CancellationTokenSource.CreateLinkedTokenSource(token);

                    timeoutCts.CancelAfter(TimeSpan.FromSeconds(8));

                    var wasDownloaded = await downloader.DownloadAsync(
                        file, destination, timeoutCts.Token);

                    if (wasDownloaded)
                        Interlocked.Increment(ref downloaded);
                    else
                        Interlocked.Increment(ref skipped);

                    Interlocked.Add(
                        ref processedBytes, file.Size ?? 0);
                }
                catch (OperationCanceledException) when (!token.IsCancellationRequested)
                {
                    Interlocked.Increment(ref failed);

                    Console.WriteLine($"TIMEOUT (5 sec): {file.Path}");
                }
                catch (OperationCanceledException) when (token.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Interlocked.Increment(ref failed);

                    Console.WriteLine(
                        $"ERROR: {file.Path}: {ex.Message}");
                }
                finally
                {
                    var done = Interlocked.Increment(ref completed);
                    var bytes = Interlocked.Read(ref processedBytes);

                    Console.WriteLine(
                        $"[{done}/{files.Count}] " +
                        $"{bytes / 1024d / 1024 / 1024:F2} GB " +
                        $"| Downloaded: {downloaded} " +
                        $"| Skipped: {skipped} " +
                        $"| Failed: {failed}");
                }
            });

        Console.WriteLine();
        Console.WriteLine("Backup finished.");
        Console.WriteLine($"Downloaded: {downloaded}");
        Console.WriteLine($"Skipped: {skipped}");
        Console.WriteLine($"Failed: {failed}");

        if (failed > 0)
            Console.WriteLine("Some files failed. Run again to retry.");
    }

    private async Task ScanAsync(
        string remotePath,
        string root,
        List<DiskResource> files,
        CancellationToken ct)
    {
        var children = await disk.GetChildrenAsync(remotePath, ct);

        foreach (var item in children)
        {
            ct.ThrowIfCancellationRequested();

            var relative = GetRelativePath(item.Path);
            var local = GetSafePath(root, relative);

            if (item.Type == "dir")
            {
                Directory.CreateDirectory(local);
                await ScanAsync(item.Path, root, files, ct);
            }
            else if (item.Type == "file")
            {
                files.Add(item);
            }
        }
    }

    private static string GetRelativePath(string remotePath)
    {
        const string prefix = "disk:/";

        if (!remotePath.StartsWith(
            prefix, StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException("Invalid disk path");

        return remotePath[prefix.Length..]
            .Replace('/', Path.DirectorySeparatorChar);
    }

    private static string GetSafePath(string root, string relative)
    {
        var fullRoot = Path.GetFullPath(root);
        var fullPath = Path.GetFullPath(
            Path.Combine(fullRoot, relative));

        var rootPrefix = Path.TrimEndingDirectorySeparator(fullRoot)
            + Path.DirectorySeparatorChar;

        if (!fullPath.StartsWith(
            rootPrefix,
            OperatingSystem.IsWindows()
                ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal))
        {
            throw new InvalidDataException("Unsafe resource path");
        }

        return fullPath;
    }
}