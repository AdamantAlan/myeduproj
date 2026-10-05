using FluentAssertions;
using System.Net.Http.Json;

namespace QA.Order.Test.Integration
{
    public class Weather : IClassFixture<OldIntegrationFixture>
    {
        private readonly OldIntegrationFixture _fixture;

        public Weather(OldIntegrationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Weather_GetAll_AllWeathers()
        {
            var response = await _fixture.Client.GetAsync("WeatherForecast/Get");

            response.EnsureSuccessStatusCode();

            var orders = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<WeatherForecast>>();

            orders.Should().HaveCount(10);
            orders.Should().OnlyContain(o => o.TemperatureF > -272);
        }
    }
}
