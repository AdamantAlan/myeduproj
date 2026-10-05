namespace QA.Order.Domain;

    public class Order
    {
        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public OrderStatus Status { get; private set; }

        public decimal TotalAmount { get; private set; }

        public string DeliveryAddress { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? PaidAt { get; private set; }

        public DateTime? CancelledAt { get; private set; }

        public Order(
            Guid userId,
            decimal totalAmount,
            string deliveryAddress)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User is required.");

            if (totalAmount <= 0)
                throw new ArgumentException("Total amount must be greater than zero.");

            if (string.IsNullOrWhiteSpace(deliveryAddress))
                throw new ArgumentException("Delivery address is required.");

            Id = Guid.NewGuid();
            UserId = userId;
            TotalAmount = totalAmount;
            DeliveryAddress = deliveryAddress;
            Status = OrderStatus.Created;
            CreatedAt = DateTime.UtcNow;
        }

        public void Pay()
        {
            if (Status != OrderStatus.Created)
                throw new InvalidOperationException(
                    "Only created order can be paid.");

            Status = OrderStatus.Paid;
            PaidAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Delivered)
                throw new InvalidOperationException(
                    "Delivered order cannot be cancelled.");

            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException(
                    "Order is already cancelled.");

            Status = OrderStatus.Cancelled;
            CancelledAt = DateTime.UtcNow;
        }
    }

    public enum OrderStatus
    {
        Created,
        Paid,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

public class OrderCalculator
{
    public decimal CalculateTotal(decimal[] prices)
    {
        if (prices == null)
            throw new ArgumentNullException(nameof(prices));

        if (prices.Any(x => x < 0))
            throw new ArgumentException("Price cannot be negative.");

        return prices.Sum();
    }
}