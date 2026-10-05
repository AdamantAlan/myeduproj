using Confluent.Kafka;
using FluentAssertions;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using QA.Order.Controllers;
using QA.Order.Domain;
using QA.Order.Infra;
using System.Net.Http.Json;
using System.Text.Json;
using Tests.Grpc;

namespace QA.Order.Test.Integration
{
    public class IntegrationTests : IClassFixture<IntegrationFixture>
    {
        private readonly IntegrationFixture fixture;

        private GrpcChannel _channel;
        private OrderService.OrderServiceClient _serviceClient;

        public IntegrationTests(IntegrationFixture system)
        {
            fixture = system;
            _channel = GrpcChannel.ForAddress(fixture.Client.BaseAddress,
                new GrpcChannelOptions
                {
                    HttpClient = fixture.Client
                });

            _serviceClient = new OrderService.OrderServiceClient(_channel);
        }

        [Fact]
        public async Task sdfsdf()
        {
            var response = await fixture.Client.GetAsync("WeatherForecast/Get");

            response.EnsureSuccessStatusCode();

            using var scope = fixture.Factory.Services.CreateScope();
            //var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            //var weather = db.Weathers.First();
        }

        [Fact]
        public async Task asdasdasd()
        {
            var cts = new CancellationTokenSource();

            try
            {
                var response = await _serviceClient.GetOrderAsync(new GetOrderRequest { }, 
                    deadline: DateTime.Now.AddMilliseconds(300), 
                    cancellationToken: cts.Token);
            }
            catch (RpcException rpcEx)
            {
                Assert.Fail($"{rpcEx.StatusCode}, {rpcEx.Message}");
            }
        }

        [Fact]
        public async Task CreateOrder_ShouldPublishOrderCreated()
        {
            //Arrange
            using var consumer = CreateConsumer();

            consumer.Subscribe(KafkaTopics.OrderCreated);

            var request = new CreateOrderRequest("RTX 5090", 200_000);

            //Act
            var response = await fixture.Client.PostAsJsonAsync("/orders", request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<CreateOrderResponse>();

            response.Should().NotBeNull();

            var message = WaitForOrderCreated(consumer, result!.OrderId, TimeSpan.FromSeconds(10));

            message.Should().NotBeNull("где сообщение, алло??");

            var orderMessage = message;

            //Assert
            orderMessage.Should().NotBeNull();
            orderMessage.OrderId.Should().Be(result!.OrderId);
            orderMessage.Product.Should().Be("RTX 5090");
            orderMessage.Price.Should().Be(200_000);
        }

        private IConsumer<string, string> CreateConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers =
                    fixture.BootstrapServers,

                GroupId =
                    $"integration-tests-{Guid.NewGuid()}",

                AutoOffsetReset =
                    AutoOffsetReset.Earliest
            };

            return new ConsumerBuilder<string, string>(config)
                .Build();
        }

        private static OrderCreated WaitForOrderCreated(
    IConsumer<string, string> consumer,
    Guid expectedOrderId,
    TimeSpan timeout)
        {
            var deadline = DateTime.UtcNow + timeout;

            while (DateTime.UtcNow < deadline)
            {
                var result = consumer.Consume(
                    TimeSpan.FromMilliseconds(500));

                if (result is null)
                    continue;

                var @event =
                    JsonSerializer.Deserialize<OrderCreated>(
                        result.Message.Value);

                if (@event?.OrderId == expectedOrderId)
                    return @event;
            }

            throw new TimeoutException(
                $"OrderCreated for {expectedOrderId} was not received.");
        }
    }
}
