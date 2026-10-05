using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using QA.Order.App;

namespace QA.Order.Test.Integration
{
    public class OldIntegrationFixture : IAsyncLifetime
    {
        public WebApplicationFactory<Program> Factory { get; private set; } = null!;
        public HttpClient Client { get; private set; } = null!;

        public async Task InitializeAsync()
        {
            IRefundService refundMock = Mock.Of<IRefundService>(x => x.Refund(It.IsAny<long>()) == true);

            // Выполняется один раз перед тестами класса
            Factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(service =>
                    {
                        service.RemoveAll<IRefundService>();
                        service.AddScoped(_ => refundMock);
                    });
                });

            Client = Factory.CreateClient();

            await Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            // Выполняется один раз после всех тестов класса
            Client.Dispose();
            await Factory.DisposeAsync();
        }
    }
}
