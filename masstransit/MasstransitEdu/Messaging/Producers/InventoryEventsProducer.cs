using MassTransit;
using MasstransitEdu.Messaging.Contracts;

namespace MasstransitEdu.Messaging.Producers;

public class InventoryEventsProducer(IPublishEndpoint publishEndpoint)
{
    public async Task<Guid> ChangeInventory(
        string sku,
        int quantityDelta,
        string warehouseCode,
        CancellationToken cancellationToken)
    {
        var eventId = NewId.NextGuid();

        await publishEndpoint.Publish(new InventoryChanged
        {
            EventId = eventId,
            Sku = sku,
            QuantityDelta = quantityDelta,
            WarehouseCode = warehouseCode,
            ChangedAt = DateTimeOffset.UtcNow
        }, context => context.SetRoutingKey($"inventory.{warehouseCode.ToLowerInvariant()}.changed"), cancellationToken);

        return eventId;
    }
}
