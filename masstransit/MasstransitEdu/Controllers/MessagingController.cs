using MassTransit;
using MasstransitEdu.Messaging.Contracts;
using MasstransitEdu.Messaging.Producers;
using Microsoft.AspNetCore.Mvc;

namespace MasstransitEdu.Controllers;

[ApiController]
[Route("api/messaging")]
public class MessagingController(
    OrderEventsProducer orderEventsProducer,
    PaymentEventsProducer paymentEventsProducer,
    InventoryEventsProducer inventoryEventsProducer,
    IRequestClient<GetOrderStatus> orderStatusClient) : ControllerBase
{
    [HttpPost("orders")]
    public async Task<ActionResult<PublishedMessageResponse>> SubmitOrder(
        SubmitOrderRequest request,
        CancellationToken cancellationToken)
    {
        var orderId = await orderEventsProducer.SubmitOrder(
            request.CustomerName,
            request.Total,
            cancellationToken);

        return Accepted(new PublishedMessageResponse(orderId));
    }

    [HttpPost("payments")]
    public async Task<ActionResult<PublishedMessageResponse>> CapturePayment(
        CapturePaymentRequest request,
        CancellationToken cancellationToken)
    {
        var paymentId = await paymentEventsProducer.CapturePayment(
            request.OrderId,
            request.Amount,
            request.Provider,
            cancellationToken);

        return Accepted(new PublishedMessageResponse(paymentId));
    }

    [HttpPost("inventory")]
    public async Task<ActionResult<PublishedMessageResponse>> ChangeInventory(
        ChangeInventoryRequest request,
        CancellationToken cancellationToken)
    {
        var eventId = await inventoryEventsProducer.ChangeInventory(
            request.Sku,
            request.QuantityDelta,
            request.WarehouseCode,
            cancellationToken);

        return Accepted(new PublishedMessageResponse(eventId));
    }

    [HttpPost("orders/status")]
    public async Task<ActionResult<OrderStatusResult>> GetOrderStatus(
        GetOrderStatusRequest request,
        CancellationToken cancellationToken)
    {
        var response = await orderStatusClient.GetResponse<OrderStatusResult>(new GetOrderStatus
        {
            OrderId = request.OrderId
        }, cancellationToken);

        return Ok(response.Message);
    }
}

public record SubmitOrderRequest(string CustomerName, decimal Total);

public record CapturePaymentRequest(Guid OrderId, decimal Amount, string Provider);

public record ChangeInventoryRequest(string Sku, int QuantityDelta, string WarehouseCode);

public record GetOrderStatusRequest(Guid OrderId);

public record PublishedMessageResponse(Guid Id);
