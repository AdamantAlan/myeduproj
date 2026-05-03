namespace MasstransitEdu.Messaging.Contracts;

public record GetOrderStatus
{
    public Guid OrderId { get; init; }
}
