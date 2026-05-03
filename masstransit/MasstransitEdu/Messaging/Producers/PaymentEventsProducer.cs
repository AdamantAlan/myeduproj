using MassTransit;
using MasstransitEdu.Messaging.Contracts;

namespace MasstransitEdu.Messaging.Producers;

public class PaymentEventsProducer(IPublishEndpoint publishEndpoint)
{
    public async Task<Guid> CapturePayment(
        Guid orderId,
        decimal amount,
        string provider,
        CancellationToken cancellationToken)
    {
        var paymentId = NewId.NextGuid();

        await publishEndpoint.Publish(new PaymentCaptured
        {
            PaymentId = paymentId,
            OrderId = orderId,
            Amount = amount,
            Provider = provider,
            CapturedAt = DateTimeOffset.UtcNow
        }, context => context.SetRoutingKey("payment.captured"), cancellationToken);

        return paymentId;
    }
}
