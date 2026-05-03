using MassTransit;
using MasstransitEdu.Messaging.Contracts;

namespace MasstransitEdu.Messaging.Consumers;

public class OrderSubmittedConsumer(ILogger<OrderSubmittedConsumer> logger) : IConsumer<OrderSubmitted>
{
    public Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        logger.LogInformation(
            "Order submitted: {OrderId}, customer: {CustomerName}, total: {Total}",
            context.Message.OrderId,
            context.Message.CustomerName,
            context.Message.Total);

        return Task.CompletedTask;
    }
}
