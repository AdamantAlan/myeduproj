using QA.Order.Domain;

namespace QA.Order.App
{
    public interface IOrderEventProducer
    {
        Task PublishAsync(
            OrderCreated message,
            CancellationToken cancellationToken = default);
    }
}
