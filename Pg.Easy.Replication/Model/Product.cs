namespace Pg.Easy.Replication.Model
{
    public sealed class Product
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
