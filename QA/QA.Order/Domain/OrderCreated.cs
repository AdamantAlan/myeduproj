namespace QA.Order.Domain
{
    public record OrderCreated(
        Guid OrderId,
        string Product,
        decimal Price);
}
