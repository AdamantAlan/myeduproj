using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace FluentValidationLearn
{
    public class ResponseGetWeatherForecast
    {
        public WeatherForecast WeatherForecast { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }


    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public class WeatherForecastValidation : AbstractValidator<WeatherForecast>
    {
        public WeatherForecastValidation()
        {
            RuleFor(x => x.Date).GreaterThan(DateTime.MinValue);
            RuleFor(x => x.TemperatureC).NotEqual(x => x.TemperatureF);
            RuleFor(x => x.TemperatureF).NotEqual(x => x.TemperatureC);
            RuleFor(x => x.Summary).NotEmpty().WithMessage("какое-то суммари должно быть не нулл и не пустое!");
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class ValidationAttribute : Attribute
    {
    }
}
