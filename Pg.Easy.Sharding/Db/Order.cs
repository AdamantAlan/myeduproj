namespace Pg.Easy.Sharding.Db
{
    public sealed record Order
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public long CustomerId { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        public decimal Amount { get; init; }
        public string? Note { get; init; }
    }
}
