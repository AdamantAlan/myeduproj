namespace MasstransitEdu.Messaging.Contracts;

public record InventoryChanged
{
    public Guid EventId { get; init; }

    public string Sku { get; init; } = string.Empty;

    public int QuantityDelta { get; init; }

    public string WarehouseCode { get; init; } = string.Empty;

    public DateTimeOffset ChangedAt { get; init; }
}
