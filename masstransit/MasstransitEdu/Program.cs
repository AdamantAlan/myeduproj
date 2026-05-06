using MassTransit;
using MasstransitEdu.Messaging;
using MasstransitEdu.Messaging.Consumers;
using MasstransitEdu.Messaging.Contracts;
using MasstransitEdu.Messaging.Definition;
using MasstransitEdu.Messaging.Producers;
using RabbitMQ.Client;

namespace MasstransitEdu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));

            builder.Services.AddScoped<OrderEventsProducer>();
            builder.Services.AddScoped<PaymentEventsProducer>();
            builder.Services.AddScoped<InventoryEventsProducer>();
            builder.Services.AddHealthChecks();

            builder.Services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumer<OrderSubmittedConsumer>();
                x.AddConsumer<PaymentCapturedConsumer>();
                x.AddConsumer<InventoryChangedConsumer, InventoryChangedConsumerDefinition>();
                x.AddConsumer<GetOrderStatusConsumer>();
                x.AddRequestClient<GetOrderStatus>(new Uri("queue:order-status-requests"));

                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMqOptions = builder.Configuration
                        .GetSection("RabbitMq")
                        .Get<RabbitMqOptions>() ?? new RabbitMqOptions();

                    cfg.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, host =>
                    {
                        host.Username(rabbitMqOptions.Username);
                        host.Password(rabbitMqOptions.Password);

                        host.Heartbeat(TimeSpan.FromSeconds(15));
                        host.RequestedConnectionTimeout(TimeSpan.FromSeconds(10));
                    });

                    cfg.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
                    cfg.UseInMemoryOutbox(context);

                    cfg.Message<OrderSubmitted>(x =>
                    {
                        x.SetEntityName("order-submitted");
                    });

                    cfg.Publish<OrderSubmitted>(exchange =>
                    {
                        exchange.ExchangeType = ExchangeType.Fanout;
                    });

                    cfg.Publish<PaymentCaptured>(exchange =>
                    {
                        exchange.ExchangeType = ExchangeType.Direct;
                    });

                    cfg.Publish<InventoryChanged>(exchange =>
                    {
                        exchange.ExchangeType = ExchangeType.Topic;
                    });

                    cfg.Publish<GetOrderStatus>(exchange =>
                    {
                        exchange.ExchangeType = ExchangeType.Direct;
                    });

                    cfg.ReceiveEndpoint("orders-submitted-queue", endpoint =>
                    {
                        ConfigureDeadLetterQueue(endpoint, rabbitMqOptions);
                        endpoint.PrefetchCount = 16;
                        endpoint.ConcurrentMessageLimit = 8;
                        endpoint.ConfigureConsumer<OrderSubmittedConsumer>(context);
                        endpoint.SetQueueArgument("x-queue-type", "quorum");
                        endpoint.UseMessageRetry(opt =>
                        {
                            opt.Interval(3, TimeSpan.FromSeconds(5));
                        });

                        endpoint.UseDelayedRedelivery(r =>
                        {
                            r.Intervals(
                                TimeSpan.FromMinutes(1),
                                TimeSpan.FromMinutes(5),
                                TimeSpan.FromMinutes(15));
                        });
                    });

                    cfg.ReceiveEndpoint("payments-captured", endpoint =>
                    {
                        ConfigureDeadLetterQueue(endpoint, rabbitMqOptions);
                        endpoint.ConfigureConsumeTopology = false;
                        endpoint.PrefetchCount = 8;
                        endpoint.ConcurrentMessageLimit = 4;
                        endpoint.SetQueueArgument("x-queue-type", "quorum");
                        endpoint.Bind<PaymentCaptured>(binding =>
                        {
                            binding.ExchangeType = ExchangeType.Direct;
                            binding.RoutingKey = "payment.captured";
                        });

                        endpoint.ConfigureConsumer<PaymentCapturedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("order-status-requests", endpoint =>
                    {
                        ConfigureDeadLetterQueue(endpoint, rabbitMqOptions);
                        endpoint.ExchangeType = ExchangeType.Direct;
                        endpoint.PrefetchCount = 8;
                        endpoint.ConcurrentMessageLimit = 4;
                        endpoint.SetQueueArgument("x-queue-type", "quorum");
                        endpoint.ConfigureConsumer<GetOrderStatusConsumer>(context);
                    });
                });
            });

            builder.Services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    options.WaitUntilStarted = true;
                    options.StartTimeout = TimeSpan.FromSeconds(30);
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapHealthChecks("/health");

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureDeadLetterQueue(
            IRabbitMqReceiveEndpointConfigurator endpoint,
            RabbitMqOptions rabbitMqOptions)
        {
            endpoint.DeadLetterExchange = rabbitMqOptions.DeadLetterExchange;
            endpoint.BindDeadLetterQueue(rabbitMqOptions.DeadLetterExchange, rabbitMqOptions.DeadLetterQueue, _ => { });
        }
    }
}
