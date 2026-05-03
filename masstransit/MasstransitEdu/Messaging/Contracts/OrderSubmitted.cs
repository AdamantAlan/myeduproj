namespace MasstransitEdu.Messaging.Contracts;

public record OrderSubmitted
{
    public Guid OrderId { get; init; }

    public string CustomerName { get; init; } = string.Empty;

    public decimal Total { get; init; }

    public DateTimeOffset SubmittedAt { get; init; }
}
