//YandexBackuper
//├── Program.cs
//├── YandexDiskClient.cs
//├── BackupService.cs
//├── DownloadService.cs
//└── Models.cs
//Сначала зарегистрируй OAuth-приложение через Яндекс OAuth и
//предоставь ему разрешение на чтение Диска (cloud_api:disk.read). Получи токен для своего аккаунта.
//https://oauth.yandex.ru/authorize?response_type=code&client_id=ТВОЙ_CLIENT_ID
//запустить Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass и . .\asses_token.ps1
//.\YandexBackuper.exe "D:\YandexBackup"

using YandexBackuper;

var token = Environment.GetEnvironmentVariable("YANDEX_DISK_TOKEN");

if (string.IsNullOrWhiteSpace(token))
{
    Console.WriteLine(
        "Set YANDEX_DISK_TOKEN environment variable.");
    return;
}

var directory = args.Length > 0
    ? args[0]
    : Path.Combine(
        Environment.GetFolderPath(
            Environment.SpecialFolder.UserProfile),
        "YandexBackup");

using var cts = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

using var apiHttp = new HttpClient
{
    Timeout = TimeSpan.FromMinutes(2)
};

using var downloadHttp = new HttpClient
{
    Timeout = Timeout.InfiniteTimeSpan
};

var disk = new YandexDiskClient(apiHttp, token);
var downloader = new DownloadService(downloadHttp, disk);
var backup = new BackupService(disk, downloader, parallelism: 5);

try
{
    await backup.RunAsync(directory, cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Backup cancelled.");
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Fatal error: {ex.Message}");
    Environment.ExitCode = 1;
}