using Microsoft.AspNetCore.Mvc;
using QA.Order.App;
using QA.Order.Domain;

namespace QA.Order.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderEventProducer _producer;

        public OrdersController(IOrderEventProducer producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            var orderId = Guid.NewGuid();

            var @event = new OrderCreated(
                orderId,
                request.Product,
                request.Price);

            await _producer.PublishAsync(
                @event,
                cancellationToken);

            return Ok(new CreateOrderResponse(orderId));
        }
    }

    public record CreateOrderRequest(
        string Product,
        decimal Price);

    public record CreateOrderResponse(
        Guid OrderId);
}
