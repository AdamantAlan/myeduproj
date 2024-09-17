using System.Text.Json;

namespace EventLearn
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        //public int TemperatureF => 32 + (int)(TemperatureC / 0.5556); â Func

        public string? Summary { get; set; }

        private event EventHandler<WeatherForecastEventArgs> onWeatherNow;

        private event Func<object, ConvertTemperatureEventArgs, string> onConvertTemperature;

        public void SubscribeToWeatherNow(EventHandler<WeatherForecastEventArgs> action) => onWeatherNow += action;

        public void UnsubscribToWeatherNow(EventHandler<WeatherForecastEventArgs> action) => onWeatherNow -= action;

        public void SubscribeToConvertTemperature(Func<object, ConvertTemperatureEventArgs, string> func) => onConvertTemperature += func;

        public void UnsubscribToConvertTemperature(Func<object, ConvertTemperatureEventArgs, string> func) => onConvertTemperature -= func;


        public string ToSerialize(string city, string metter)
        {
            onWeatherNow(this, new WeatherForecastEventArgs(city));
            var temp = onConvertTemperature(this, new ConvertTemperatureEventArgs(metter));
            return JsonSerializer.Serialize(this)[..^1] + $",\"AlternativeTemperature\": \"{temp}\"}}";
        }
    }

    public class WeatherForecastEventArgs : EventArgs
    {
        public WeatherForecastEventArgs(string city) => City = city;

        public string City { get; set; }
    }

    public class ConvertTemperatureEventArgs : EventArgs
    {
        public ConvertTemperatureEventArgs(string metter) => Metter = metter;

        public string Metter { get; set; }
    }

}
