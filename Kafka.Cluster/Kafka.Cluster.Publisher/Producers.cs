using Kafka.Cluster.Publisher.Properties;
using Microsoft.Extensions.Options;

namespace Kafka.Cluster.Publisher
{
    public class ClaimsProducer : KafkaProducerBase
    {
        public ClaimsProducer(IOptions<KafkaSettings> settings, ILogger<ClaimsProducer> logger)
            : base(settings, logger, "claims-topic") { }

        public Task SendClaimAsync(string claimJson, CancellationToken token = default)
            => ProduceAsync(claimJson, token);
    }

    public class PolicyProducer : KafkaProducerBase
    {
        public PolicyProducer(IOptions<KafkaSettings> settings, ILogger<PolicyProducer> logger)
            : base(settings, logger, "policy-topic") { }

        public Task SendPolicyAsync(string policyJson, CancellationToken token = default)
            => ProduceAsync(policyJson, token);
    }

    public class PaymentsProducer : KafkaProducerBase
    {
        public PaymentsProducer(IOptions<KafkaSettings> settings, ILogger<PaymentsProducer> logger)
            : base(settings, logger, "payments-topic") { }

        public Task SendPaymentAsync(string paymentJson, CancellationToken token = default)
            => ProduceAsync(paymentJson, token);
    }
}
