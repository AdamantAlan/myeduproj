using Redis.OM.Modeling;

namespace RedisStackLearn.OM
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "product" })]
    public class Product
    {
        [RedisIdField]
        public Ulid Id { get; set; } = Ulid.NewUlid();

        [Indexed]
        public string Name { get; set; }

        [Indexed(Sortable = true)]
        public double Price { get; set; }

        public string Description { get; set; }

        [Expiration]
        public TimeSpan? ExpireAfter { get; set; }

        [GeoIndexed]
        public GeoLoc Location { get; set; }
    }
}
