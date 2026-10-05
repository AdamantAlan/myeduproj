using FluentAssertions;
using Grpc.Core;
using System.Net.Http.Json;
using Tests.Grpc;

namespace QA.Order.Test.Integration
{
    public class SystemTests : IClassFixture<SystemFixture>
    {
        private readonly SystemFixture _systemFixture;

        public SystemTests(SystemFixture systemFixture)
        {
            _systemFixture = systemFixture;
        }

        [Theory]
        [InlineData(123)]
        public async Task Order_GetOrder_Success(long orderId)
        {
            var cts = new CancellationTokenSource();
            var response = await _systemFixture.HttpClient.GetAsync($"/orders/{orderId}", cts.Token);

            response.EnsureSuccessStatusCode();

            var order = await response.Content.ReadFromJsonAsync<Order>();

            order.Should().NotBeNull();
            order.Description.Should().NotBeNull();
            order.Id.Should().BeGreaterThan(0);
            order.Name.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(123)]
        public async Task Order_GetOrderFromChannel_Success(long orderId)
        {
            var cts = new CancellationTokenSource();
            try
            {
                var order = await _systemFixture.OrderClient.GetOrderAsync(new GetOrderRequest { }, cancellationToken: cts.Token);

                order.Should().NotBeNull();
                order.Id.Should().BeGreaterThan(0);
                order.Status.Should().NotBeEmpty();
            }
            catch (RpcException e)
            {
                Assert.Fail(e.Message);
            }
        }

        class Order
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
