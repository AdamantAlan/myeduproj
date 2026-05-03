using MassTransit;
using MasstransitEdu.Messaging.Contracts;

namespace MasstransitEdu.Messaging.Producers;

public class OrderEventsProducer(IPublishEndpoint publishEndpoint)
{
    public async Task<Guid> SubmitOrder(string customerName, decimal total, CancellationToken cancellationToken)
    {
        var orderId = NewId.NextGuid();

        await publishEndpoint.Publish(new OrderSubmitted
        {
            OrderId = orderId,
            CustomerName = customerName,
            Total = total,
            SubmittedAt = DateTimeOffset.UtcNow
        }, cancellationToken);

        return orderId;
    }
}
