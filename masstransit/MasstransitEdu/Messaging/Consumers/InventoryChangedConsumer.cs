using MassTransit;
using MasstransitEdu.Messaging.Contracts;

namespace MasstransitEdu.Messaging.Consumers;

public class InventoryChangedConsumer(ILogger<InventoryChangedConsumer> logger) : IConsumer<InventoryChanged>
{
    public Task Consume(ConsumeContext<InventoryChanged> context)
    {
        logger.LogInformation(
            "Inventory changed: {Sku}, delta: {QuantityDelta}, warehouse: {WarehouseCode}",
            context.Message.Sku,
            context.Message.QuantityDelta,
            context.Message.WarehouseCode);

        return Task.CompletedTask;
    }
}
