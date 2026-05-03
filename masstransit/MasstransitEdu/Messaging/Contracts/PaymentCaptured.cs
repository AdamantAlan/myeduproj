namespace MasstransitEdu.Messaging.Contracts;

public record PaymentCaptured
{
    public Guid PaymentId { get; init; }

    public Guid OrderId { get; init; }

    public decimal Amount { get; init; }

    public string Provider { get; init; } = string.Empty;

    public DateTimeOffset CapturedAt { get; init; }
}
