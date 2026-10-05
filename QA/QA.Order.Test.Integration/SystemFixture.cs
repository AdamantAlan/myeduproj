using Grpc.Net.Client;
using Tests.Grpc;

namespace QA.Order.Test.Integration
{
    public class SystemFixture : IAsyncLifetime
    {
        public HttpClient HttpClient { get; set; }

        public GrpcChannel Channel { get; set; }

        public OrderService.OrderServiceClient OrderClient { get; set; }

        public Task InitializeAsync()
        {
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://test-api.company.ru")
            };

            HttpClient.Timeout = TimeSpan.FromSeconds(10);

            Channel = GrpcChannel.ForAddress("https://test-orders.company.ru:5001");

            OrderClient = new OrderService.OrderServiceClient(Channel);

            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            HttpClient.Dispose();
            Channel.Dispose();
            return Task.CompletedTask;
        }


    }
}
