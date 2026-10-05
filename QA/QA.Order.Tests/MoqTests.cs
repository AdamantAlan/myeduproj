using FluentAssertions;
using Moq;
using QA.Order.Payments;

namespace QA.Order.Payments
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task SaveAsync(Order order);
    }

    public interface IPaymentService
    {
        Task<bool> PayAsync(Guid orderId, decimal amount);
    }

    public class Order
    {
        public Guid Id { get; init; }
        public decimal Amount { get; init; }
        public bool IsPaid { get; set; }
    }

    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IPaymentService _paymentService;

        public OrderService(
            IOrderRepository repository,
            IPaymentService paymentService)
        {
            _repository = repository;
            _paymentService = paymentService;
        }

        public async Task PayAsync(Guid orderId)
        {
            var order = await _repository.GetByIdAsync(orderId);

            if (order is null)
                throw new InvalidOperationException("Order not found");

            var success = await _paymentService.PayAsync(
                order.Id,
                order.Amount);

            if (!success)
                throw new InvalidOperationException("Payment failed");

            order.IsPaid = true;

            await _repository.SaveAsync(order);
        }
    }
}

namespace QA.Order.Tests
{
    public class MoqTests : IClassFixture<MoqTests>
    {
        [Fact]
        public async Task Order_PaySuccessful_ShouldBePayed()
        {
            //Arrange
            var orderId = Guid.NewGuid();
            var order = new QA.Order.Payments.Order
            {
                Id = orderId,
                Amount = 1000
            };

            var repoMock = new Mock<IOrderRepository>();
            var paymentMock = new Mock<IPaymentService>();

            repoMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(order);

            paymentMock.Setup(x => x.PayAsync(It.Is<Guid>(id => id == orderId), It.IsAny<decimal>()))
                .ReturnsAsync(true);
            paymentMock.Setup(x => x.PayAsync(It.Is<Guid>(id => id != orderId),It.IsAny<decimal>()))
                .ThrowsAsync(new Exception());

            var sut = new OrderService(repoMock.Object, paymentMock.Object);

            //Act
            await sut.PayAsync(orderId);

            //Assert
            repoMock.Verify(x => x.SaveAsync(It.Is<Payments.Order>(o => o.IsPaid)), Times.Once);
            order.IsPaid.Should().BeTrue("sdsd");
        }
    }
}

//new Mock<T>()

//.Setup(...)
//.Returns(...)
//.ReturnsAsync(...)

//It.IsAny<T>()
//It.Is<T>(...)

//.Verify(...)
//Times.Once
//Times.Never

//.Throws(...)
//.ThrowsAsync(...)

//.Callback(...)

//.SetupSequence(...)

//MockBehavior.Strict