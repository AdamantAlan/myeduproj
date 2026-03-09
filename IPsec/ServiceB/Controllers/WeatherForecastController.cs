using Microsoft.AspNetCore.Mvc;

namespace ServiceB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, HttpClient httpClient, IConfiguration configuration) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetFromServiceA()
        {
            logger.LogInformation("Запрос на serviceA");

            var url = configuration["SERVICE_A"];
            var result = await httpClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>($"{url}/weatherforecast/get");

            return result!;
        }
    }
}
