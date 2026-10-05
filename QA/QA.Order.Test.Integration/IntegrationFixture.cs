using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Testcontainers.Kafka;
using Testcontainers.PostgreSql;

namespace QA.Order.Test.Integration
{
    public class IntegrationFixture : IAsyncLifetime
    {
        public WebApplicationFactory<Program> Factory { get; private set; } = null!;

        private readonly KafkaContainer _kafka = new KafkaBuilder()
            .WithImage("confluentinc/cp-kafka:7.9.0")
            .Build();
        public string BootstrapServers => _kafka.GetBootstrapAddress();

        public HttpClient Client { get; private set; } = null!;

        private readonly PostgreSqlContainer postgres = new PostgreSqlBuilder()
                .WithImage("postgres:16")
                .WithDatabase("test_db")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .Build();

        public async Task InitializeAsync()
        {
            Factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureAppConfiguration((_, config) =>
                    {
                        config.AddInMemoryCollection(new Dictionary<string, string?>
                        {
                            ["ConnectionStrings:Default"] = postgres.GetConnectionString(),

                            ["Kafka:BootstrapServers"] = BootstrapServers
                        });
                    });
                });

            

            await postgres.StartAsync();
            await _kafka.StartAsync();

            Client = Factory.CreateClient();
            //using var scope = Factory.Services.CreateScope()
            //    .ServiceProvider.GetRequiredService<AppDbContext>.DataBase.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            Factory?.Dispose();
            await postgres.StopAsync();
            await _kafka.DisposeAsync();
        }
    }
}
