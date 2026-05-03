using MassTransit;
using MasstransitEdu.Messaging.Contracts;

namespace MasstransitEdu.Messaging.Consumers;

public class GetOrderStatusConsumer(ILogger<GetOrderStatusConsumer> logger) : IConsumer<GetOrderStatus>
{
    public async Task Consume(ConsumeContext<GetOrderStatus> context)
    {
        logger.LogInformation("Order status requested: {OrderId}", context.Message.OrderId);

        await context.RespondAsync(new OrderStatusResult
        {
            OrderId = context.Message.OrderId,
            Status = "Processing",
            CheckedAt = DateTimeOffset.UtcNow
        });
    }
}
