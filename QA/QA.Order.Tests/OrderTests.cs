using FluentAssertions;

namespace QA.Order.Tests
{
    public class OrderTests : IDisposable
    {
        public OrderTests()
        {
            // BEFORE EACH
            Console.WriteLine("before tests");
        }

        [Fact]
        public void Order_TwicePay_Exception()
        {
            var order = new Domain.Order(Guid.NewGuid(), 700, "Moscow");

            order.Pay();

            var onPay = order.Pay;

            onPay.Should().Throw<InvalidOperationException>();
            Assert.Throws<InvalidOperationException>(order.Pay);
        }

        [Fact]
        public void Order_TwicePay_Exceptio1n()
        {
            int[] expected = [1, 2, 3];
            int[] actual = [1, 2, 3];

            expected.Should().BeEquivalentTo(actual);
            Assert.Equal(expected, actual);

            Assert.Contains(2, actual);

            actual.Should().NotContain(10);
            Assert.DoesNotContain(10, actual);

            actual.Should().Contain(x => x > 2);
            Assert.Contains(actual, x => x > 2);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(5, 5, 10)]
        [InlineData(-1, 1, 0)]
        [InlineData(10, 20, 30)]
        public void Sum_ShouldReturnExpected(
    int a,
    int b,
    int expected)
        {
            // Act
            var result = a + b;

            // Assert
            Assert.Equal(expected, result);
        }

        public class Order
        {
            public decimal Amount { get; set; }
            public bool IsPaid { get; set; }
        }

        public static IEnumerable<object[]> OrdersData()
        {
            yield return new object[]
            {
            new Order { Amount = 100, IsPaid = true },
            true
            };

            yield return new object[]
            {
            new Order { Amount = 200, IsPaid = false },
            false
            };

            yield return new object[]
            {
            new Order { Amount = 0, IsPaid = false },
            false
            };
        }

        [Theory]
        [MemberData(nameof(OrdersData))]
        public void Order_ShouldHaveExpectedPaidStatus(Order order, bool expected)
        {
            Assert.Equal(expected, order.IsPaid);
            expected.Should().Be(order.IsPaid);
        }

        public void Dispose()
        {
            // AFTER EACH
            Console.WriteLine("after tests");
        }
    }
}
