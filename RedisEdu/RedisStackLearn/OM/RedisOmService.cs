using Redis.OM;
using Redis.OM.Modeling;
using System.Linq.Expressions;

namespace RedisStackLearn.OM
{
    public class RedisOmService
    {
        static RedisConnectionProvider provider = new RedisConnectionProvider("redis://localhost:6379");

        public Task CreateIndex(Type collectionType) => provider.Connection.CreateIndexAsync(collectionType);

        public Task<string> AddProduct(Product product = null)
        {
            var products = provider.RedisCollection<Product>();

            product ??= new Product
            {
                Name = "Water Bottle",
                Price = 2.99,
                Description = "Plastic bottle, 1L",
                ExpireAfter = TimeSpan.FromSeconds(30),
                Location = new GeoLoc(41.0082, 28.9784)
            };

            return products.InsertAsync(product);
        }

        public ICollection<Product> GetAllProducts(Ulid productId, Expression<Func<Product, bool>> predicate) 
        {
            return provider.RedisCollection<Product>().Where(predicate).OrderBy(p => p.Price).ToList();
        }

        public Task DeleteProducts(Ulid? productId, Product product)
        {
            return productId is not null
                ? provider.Connection.UnlinkAsync(productId.Value.ToString())
                : provider.RedisCollection<Product>().DeleteAsync(product);
        }
    }
}
