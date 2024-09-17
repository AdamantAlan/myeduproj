using Microsoft.AspNetCore.Mvc;

namespace EventLearn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get(string city, string metter)
        {
             var forecast = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
            
            forecast.SubscribeToWeatherNow(WriteWeather!);
            forecast.SubscribeToConvertTemperature(ConvertToFahrenheit);

            return forecast.ToSerialize(city, metter);
        }

        internal void WriteWeather(object sender, WeatherForecastEventArgs args)
        {
            var forecast = (WeatherForecast)sender;
            if (forecast.TemperatureC < 0)
                Console.WriteLine($"Сегодня в {args.City} холодно!");
        }

        internal string ConvertToFahrenheit(object sender, ConvertTemperatureEventArgs args)
        {
            var forecast = (WeatherForecast)sender;
            var res = (32 + (int)(forecast.TemperatureC / 0.5556)).ToString() + "F";
            return res;
        }
    }
}
