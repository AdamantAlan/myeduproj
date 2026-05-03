namespace MasstransitEdu.Messaging.Contracts;

public record OrderStatusResult
{
    public Guid OrderId { get; init; }

    public string Status { get; init; } = string.Empty;

    public DateTimeOffset CheckedAt { get; init; }
}
