using Kafka.Cluster.Consumer.Properties;
using Microsoft.Extensions.Options;

namespace Kafka.Cluster.Consumer
{
    public class ClaimsConsumer : KafkaConsumerBase
    {
        public ClaimsConsumer(IOptions<KafkaSettings> settings, ILogger<ClaimsConsumer> logger)
            : base(settings, logger, "claims-topic", "claims-group") { }

        protected override Task HandleMessageAsync(string message, CancellationToken token)
        {
            Console.WriteLine($"[Claims] {message}");
            return Task.CompletedTask;
        }
    }

    public class PolicyConsumer : KafkaConsumerBase
    {
        public PolicyConsumer(IOptions<KafkaSettings> settings, ILogger<PolicyConsumer> logger)
            : base(settings, logger, "policy-topic", "policy-group") { }

        protected override Task HandleMessageAsync(string message, CancellationToken token)
        {
            Console.WriteLine($"[Policy] {message}");
            return Task.CompletedTask;
        }
    }

    public class PaymentsConsumer : KafkaConsumerBase
    {
        public PaymentsConsumer(IOptions<KafkaSettings> settings, ILogger<PaymentsConsumer> logger)
            : base(settings, logger, "payments-topic", "payments-group") { }

        protected override Task HandleMessageAsync(string message, CancellationToken token)
        {
            Console.WriteLine($"[Payments] {message}");
            return Task.CompletedTask;
        }
    }
}
