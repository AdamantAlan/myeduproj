using Confluent.Kafka;
using QA.Order.Domain;
using QA.Order.Infra;
using System.Text.Json;

namespace QA.Order.App
{
    public sealed class OrderEventProducer : IOrderEventProducer
    {
        private readonly IProducer<string, string> _producer;

        public OrderEventProducer(
            IProducer<string, string> producer)
        {
            _producer = producer;
        }

        public async Task PublishAsync(
            OrderCreated message,
            CancellationToken cancellationToken = default)
        {
            var kafkaMessage = new Message<string, string>
            {
                Key = message.OrderId.ToString(),

                Value = JsonSerializer.Serialize(message)
            };

            await _producer.ProduceAsync(
                KafkaTopics.OrderCreated,
                kafkaMessage,
                cancellationToken);
        }
    }
}
