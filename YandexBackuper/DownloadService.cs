using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YandexBackuper;

public sealed class DownloadService
{
    private readonly HttpClient http;
    private readonly YandexDiskClient disk;

    public DownloadService(HttpClient http, YandexDiskClient disk)
    {
        this.http = http;
        this.disk = disk;
    }

    public async Task<bool> DownloadAsync(
        DiskResource resource,
        string destination,
        CancellationToken ct)
    {
        Directory.CreateDirectory(
            Path.GetDirectoryName(destination)!);

        if (await IsValidAsync(destination, resource, ct))
            return false;

        var temp = destination + ".part";

        for (var attempt = 0; attempt < 6; attempt++)
        {
            ct.ThrowIfCancellationRequested();

            try
            {
                // Получаем новую ссылку при каждой попытке.
                var url = await disk.GetDownloadUrlAsync(resource.Path, ct);

                using var response = await http.GetAsync(
                    url,
                    HttpCompletionOption.ResponseHeadersRead,
                    ct);

                if (response.StatusCode == HttpStatusCode.TooManyRequests ||
                    (int)response.StatusCode >= 500)
                {
                    response.EnsureSuccessStatusCode();
                }

                response.EnsureSuccessStatusCode();

                await using (var source =
                    await response.Content.ReadAsStreamAsync(ct))
                await using (var target = new FileStream(
                    temp, FileMode.Create, FileAccess.Write,
                    FileShare.None, 81920, useAsync: true))
                {
                    await source.CopyToAsync(target, ct);
                }

                if (!await IsValidAsync(temp, resource, ct))
                    throw new InvalidDataException(
                        $"Checksum or size mismatch: {resource.Path}");

                File.Move(temp, destination, overwrite: true);

                return true;
            }
            catch (Exception ex) when (
                ex is HttpRequestException or IOException or
                      InvalidDataException or TaskCanceledException &&
                !ct.IsCancellationRequested)
            {
                if (attempt == 5)
                    throw;

                await Task.Delay(
                    TimeSpan.FromSeconds(Math.Min(60, Math.Pow(2, attempt + 1))),
                    ct);
            }
        }

        throw new IOException($"Failed: {resource.Path}");
    }

    private static async Task<bool> IsValidAsync(
        string path,
        DiskResource resource,
        CancellationToken ct)
    {
        if (!File.Exists(path))
            return false;

        var info = new FileInfo(path);

        if (resource.Size is not null &&
            info.Length != resource.Size.Value)
            return false;

        if (!string.IsNullOrWhiteSpace(resource.Md5))
        {
            await using var stream = File.OpenRead(path);
            var hash = await MD5.HashDataAsync(stream, ct);
            var actual = Convert.ToHexString(hash);

            return actual.Equals(
                resource.Md5,
                StringComparison.OrdinalIgnoreCase);
        }

        // Без MD5 проверяем размер.
        // Если нет ни MD5, ни размера, не считаем файл проверенным.
        return resource.Size is not null;
    }
}