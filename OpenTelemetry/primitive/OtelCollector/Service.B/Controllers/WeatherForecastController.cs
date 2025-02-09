using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace Service.B.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            HttpContext.Response.Headers.Append("Request-Id", Activity.Current?.TraceId.ToString() ?? "");

            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("http://localhost:5150/WeatherForecast");
            response.EnsureSuccessStatusCode();
            var resultApi = await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();

            if (resultApi is not { Count: > 0 })
            {
                return [];
            }

            return resultApi;
        }
    }
}
