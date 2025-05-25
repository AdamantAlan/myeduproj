using Redis.OM;
using Redis.OM.Modeling;
using StackExchange.Redis;
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

        public async Task Aggreagate()
        {
            var products = provider.RedisCollection<Product>();

            await products.InsertAsync(new Product { Name = "Pen", Price = 2 });
            await products.InsertAsync(new Product { Name = "Pen", Price = 3 });
            await products.InsertAsync(new Product { Name = "Notebook", Price = 6 });
            await products.InsertAsync(new Product { Name = "Notebook", Price = 14 });

            var raw = await provider.Connection.ExecuteAsync("FT.AGGREGATE",
           "idx:product", "*",
           "GROUPBY", "1", "@name",
           "REDUCE", "AVG", "1", "@price", "AS", "avg_price",
           "SORTBY", "2", "@avg_price", "DESC");

            var replyArray = raw.ToArray();

            //Первый элемент — количество групп
            int groups = replyArray[0];

            for (int i = 1; i <= groups; i++)
            {
                RedisReply[] row = replyArray[i].ToArray();

                string name = row[1];
                double avgPrice = row[3];

                Console.WriteLine($"Продукт: {name}, Средняя цена: {avgPrice}");
            }
        }
    }
}
