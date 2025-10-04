using Microsoft.AspNetCore.Mvc;

namespace Kafka.Cluster.Publisher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaPublishController : ControllerBase
    {
        private readonly ClaimsProducer _claimsProducer;
        private readonly PolicyProducer _policyProducer;
        private readonly PaymentsProducer _paymentsProducer;

        public KafkaPublishController(
            ClaimsProducer claimsProducer,
            PolicyProducer policyProducer,
            PaymentsProducer paymentsProducer)
        {
            _claimsProducer = claimsProducer;
            _policyProducer = policyProducer;
            _paymentsProducer = paymentsProducer;
        }

        [HttpPost("claims")]
        public async Task<IActionResult> PublishClaim([FromBody] string message)
        {
            await _claimsProducer.SendClaimAsync(message);
            return Ok(new { status = "sent", topic = "claims-topic" });
        }

        [HttpPost("policy")]
        public async Task<IActionResult> PublishPolicy([FromBody] string message)
        {
            await _policyProducer.SendPolicyAsync(message);
            return Ok(new { status = "sent", topic = "policy-topic" });
        }

        [HttpPost("payments")]
        public async Task<IActionResult> PublishPayment([FromBody] string message)
        {
            await _paymentsProducer.SendPaymentAsync(message);
            return Ok(new { status = "sent", topic = "payments-topic" });
        }
    }
}
