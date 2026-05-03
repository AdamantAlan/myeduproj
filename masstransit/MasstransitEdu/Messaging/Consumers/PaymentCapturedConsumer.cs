using MassTransit;
using MasstransitEdu.Messaging.Contracts;

namespace MasstransitEdu.Messaging.Consumers;

public class PaymentCapturedConsumer(ILogger<PaymentCapturedConsumer> logger) : IConsumer<PaymentCaptured>
{
    public Task Consume(ConsumeContext<PaymentCaptured> context)
    {
        logger.LogInformation(
            "Payment captured: {PaymentId}, order: {OrderId}, amount: {Amount}, provider: {Provider}",
            context.Message.PaymentId,
            context.Message.OrderId,
            context.Message.Amount,
            context.Message.Provider);

        return Task.CompletedTask;
    }
}
