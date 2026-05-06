using MassTransit;
using MasstransitEdu.Messaging.Consumers;
using MasstransitEdu.Messaging.Contracts;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MasstransitEdu.Messaging.Definition
{
    public sealed class InventoryChangedConsumerDefinition
        : ConsumerDefinition<InventoryChangedConsumer>
    {
        private readonly RabbitMqOptions _rabbitMqOptions;

        public InventoryChangedConsumerDefinition(IOptions<RabbitMqOptions> options)
        {
            _rabbitMqOptions = options.Value;

            EndpointName = "inventory-changed";
            
            ConcurrentMessageLimit = 16;
        }

        protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpoint,
        IConsumerConfigurator<InventoryChangedConsumer> consumer,
        IRegistrationContext context)
        {
            if (endpoint is not IRabbitMqReceiveEndpointConfigurator rabbit)
                throw new InvalidOperationException("Endpoint is not RabbitMQ endpoint");

            ConfigureDeadLetterQueue(rabbit, _rabbitMqOptions);

            rabbit.ConfigureConsumeTopology = false;
            rabbit.PrefetchCount = 32;
            rabbit.SetQueueArgument("x-queue-type", "quorum");
            rabbit.Bind<InventoryChanged>(binding =>
            {
                binding.ExchangeType = ExchangeType.Topic;
                binding.RoutingKey = "inventory.*.changed";
            });
        }

        private static void ConfigureDeadLetterQueue(
        IRabbitMqReceiveEndpointConfigurator endpoint,
        RabbitMqOptions options)
        {
            endpoint.DeadLetterExchange = options.DeadLetterExchange;
            endpoint.BindDeadLetterQueue(options.DeadLetterExchange, options.DeadLetterQueue, _ => { });
        }
    }
}
