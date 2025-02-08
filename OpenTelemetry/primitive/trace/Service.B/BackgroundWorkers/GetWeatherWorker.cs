using Service.B.Telemetry;
using System.Text.Json;

namespace Service.B.BackgroundWorkers
{
    public class GetWeatherWorker(IHttpClientFactory httpClientFactory) : BackgroundService
    {
        private HttpClient httpClient { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(5000);
            httpClient = httpClientFactory.CreateClient();
            var requests = 0;
            while (requests <= 5)
            {
                using var activity = WorkerActivitySource.ActivitySource.StartActivity("GetWeather");

                var response = await httpClient.GetAsync("http://localhost:5100/WeatherForecast");

                if (!response.IsSuccessStatusCode)
                    activity?.SetTag("weeather", "GET500ERROR");
                else
                    activity?.SetTag("weeather", "GET200OK");

                activity?.Stop();

                var resultApi = await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();
                Console.WriteLine(JsonSerializer.Serialize(resultApi));
                requests++;
            }
        }
    }
}
