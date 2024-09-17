using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using WebServer;
using WebServer.Services;

namespace e2e.Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<WebServer.Program>>
    {
        private readonly WebApplicationFactory<WebServer.Program> _factory;
        private readonly HttpClient _client;

        public UnitTest1(WebApplicationFactory<WebServer.Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetValues_ReturnsOK()
        {
            var response = await _client.GetAsync("/WeatherForecast");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            
            var manager = scope.ServiceProvider.GetRequiredService<IGuidManager>();
            var guid = manager.GetNewGuid();
            var content = await response.Content.ReadAsStringAsync();

            var values = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);

        }
    }
}