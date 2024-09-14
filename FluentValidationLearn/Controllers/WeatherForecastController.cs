using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationLearn.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IValidator<WeatherForecast> _validator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IValidator<WeatherForecast> validator)
        {
            _logger = logger;
            _validator = validator;
        }

        [HttpPost]
        public IActionResult GetWithValidation([Validation] WeatherForecast weather)
        {
            return Ok(new ResponseGetWeatherForecast { WeatherForecast = weather });
        }

        [HttpPost]
        public IActionResult Get(WeatherForecast weather)
        {
            return Ok(new ResponseGetWeatherForecast { WeatherForecast = weather });
        }
    }
}
