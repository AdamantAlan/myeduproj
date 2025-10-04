using Confluent.Kafka;
using Kafka.Cluster.Publisher.Properties;
using Microsoft.Extensions.Options;

namespace Kafka.Cluster.Publisher
{
    public abstract class KafkaProducerBase : IDisposable
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;
        private readonly ILogger _logger;

        protected KafkaProducerBase(
            IOptions<KafkaSettings> settings,
            ILogger logger,
            string topic)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = settings.Value.BootstrapServers,
                Acks = Acks.All,
                EnableIdempotence = true,
                MessageTimeoutMs = 5000
            };

            _producer = new ProducerBuilder<Null, string>(config)
                .SetErrorHandler((_, e) => logger.LogError("Producer error: {Reason}", e.Reason))
                .Build();

            _topic = topic;
            _logger = logger;
        }

        protected async Task ProduceAsync(string message, CancellationToken token = default)
        {
            try
            {
                var result = await _producer.ProduceAsync(
                    _topic,
                    new Message<Null, string> { Value = message },
                    token);

                _logger.LogInformation("Sent to {Topic} [Partition {Partition}, Offset {Offset}]",
                    result.Topic, result.Partition.Value, result.Offset.Value);
            }
            catch (ProduceException<Null, string> e)
            {
                _logger.LogError(e, "Kafka produce error for {Topic}: {Reason}", _topic, e.Error.Reason);
            }
        }

        public void Dispose() => _producer?.Dispose();
    }
}
