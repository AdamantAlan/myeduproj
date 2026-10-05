using FluentAssertions;
using Moq;
using Moq.AutoMock;
using QA.Order.Payments;

namespace QA.Order.Tests
{
    public class AutoMockTests
    {
    public async Task etetetet()
        {
            var orderId = Guid.NewGuid();
            var order = new QA.Order.Payments.Order
            {
                Id = orderId,
                Amount = 1000
            };

            var autoMocker = new AutoMocker();

            autoMocker
                   .GetMock<IOrderRepository>()
                   .Setup(x => x.GetByIdAsync(orderId))
                   .ReturnsAsync(order);

            autoMocker
                .GetMock<IPaymentService>()
                .Setup(x => x.PayAsync(orderId, 1000))
                .ReturnsAsync(true);

            var sut = autoMocker.CreateInstance<OrderService>();

            // Act
            await sut.PayAsync(orderId);

            // Assert
            order.IsPaid.Should().BeTrue();

            autoMocker.GetMock<IOrderRepository>()
                .Verify(x => x.SaveAsync(It.Is<QA.Order.Payments.Order>(g => g.Id == orderId && g.IsPaid)), Times.Once);
        }

    }
}
