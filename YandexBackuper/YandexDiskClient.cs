using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;


namespace YandexBackuper;

public sealed class YandexDiskClient
{
    private const string BaseUrl =
        "https://cloud-api.yandex.net/v1/disk/resources";

    private readonly HttpClient http;

    public YandexDiskClient(HttpClient http, string token)
    {
        this.http = http;
        http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("OAuth", token);
    }

    public async Task<List<DiskResource>> GetChildrenAsync(
        string path,
        CancellationToken ct = default)
    {
        const int limit = 100;
        var offset = 0;
        var result = new List<DiskResource>();

        while (true)
        {
            var url = $"{BaseUrl}?path={Uri.EscapeDataString(path)}" +
                      $"&limit={limit}&offset={offset}";

            var resource = await GetAsync<DiskResource>(url, ct);

            var items = resource.Embedded?.Items
                ?? new List<DiskResource>();

            result.AddRange(items);

            if (items.Count < limit)
                break;

            offset += items.Count;
        }

        return result;
    }

    public async Task<string> GetDownloadUrlAsync(
        string path,
        CancellationToken ct = default)
    {
        var url = $"{BaseUrl}/download?path=" +
                  Uri.EscapeDataString(path);

        var link = await GetAsync<DownloadLink>(url, ct);

        return link.Href;
    }

    private async Task<T> GetAsync<T>(
        string url,
        CancellationToken ct)
    {
        for (var attempt = 0; ; attempt++)
        {
            try
            {
                using var response = await http.GetAsync(url, ct);

                if (IsTransient(response.StatusCode) && attempt < 5)
                {
                    await DelayAsync(attempt, response, ct);
                    continue;
                }

                response.EnsureSuccessStatusCode();

                return await response.Content
                    .ReadFromJsonAsync<T>(cancellationToken: ct)
                    ?? throw new InvalidDataException("Empty API response");
            }
            catch (HttpRequestException) when (attempt < 5)
            {
                await Task.Delay(Backoff(attempt), ct);
            }
        }
    }

    private static bool IsTransient(HttpStatusCode status) =>
        status == HttpStatusCode.TooManyRequests ||
        (int)status >= 500;

    private static TimeSpan Backoff(int attempt) =>
        TimeSpan.FromSeconds(Math.Min(60, Math.Pow(2, attempt + 1)));

    private static Task DelayAsync(
        int attempt,
        HttpResponseMessage response,
        CancellationToken ct)
    {
        var delay = response.Headers.RetryAfter?.Delta
            ?? Backoff(attempt);

        return Task.Delay(delay, ct);
    }
}
