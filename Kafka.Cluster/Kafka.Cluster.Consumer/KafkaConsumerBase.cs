using Confluent.Kafka;
using Kafka.Cluster.Consumer.Properties;
using Microsoft.Extensions.Options;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Kafka.Cluster.Consumer
{
    public abstract class KafkaConsumerBase : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly KafkaSettings _settings;
        private readonly string _topic;
        private readonly string _groupId;

        protected KafkaConsumerBase(
            IOptions<KafkaSettings> settings,
            ILogger logger,
            string topic,
            string groupId)
        {
            _settings = settings.Value;
            _logger = logger;
            _topic = topic;
            _groupId = groupId;
        }

        protected abstract Task HandleMessageAsync(string message, CancellationToken token);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _settings.BootstrapServers,
                GroupId = _groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
                AllowAutoCreateTopics = true,
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_topic);

            _logger.LogInformation("Started consumer for topic '{Topic}'", _topic);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = consumer.Consume(stoppingToken);
                        if (result?.Message == null)
                            continue;

                        await HandleMessageAsync(result.Message.Value, stoppingToken);
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError(e, "Kafka consume error in topic {Topic}: {Reason}", _topic, e.Error.Reason);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Consumer for topic {Topic} stopping...", _topic);
            }
            finally
            {
                try
                {
                    consumer.Commit();
                    consumer.Close();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error during commit/close for {Topic}", _topic);
                }
            }
        }
    }
}
