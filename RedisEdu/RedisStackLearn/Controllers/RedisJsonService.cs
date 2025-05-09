using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using NRedisStack.Search.Aggregation;
using NRedisStack.Search.Literals.Enums;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisStackLearn.Controllers
{
    public class RedisJsonService(ConnectionMultiplexer connection)
    {
        private const string JSON_KEY = "json";
        private IDatabase db { get; set; } = connection.GetDatabase(0);

        public Task<bool> SetJsonAsync(string prefix, string key, object @object) =>
            db.JSON().SetAsync($"{prefix}:{key}","$", JsonSerializer.Serialize(@object));

        public async Task<T?> GetJsonAsync<T>(string key, string? property = null) where T : class
        {
            var redisResult = await db.JSON().GetAsync(key);

            if (redisResult.IsNull) return null;

            return JsonSerializer.Deserialize<T>(redisResult.ToString());
        }

        public Task<bool> CreateFtIndexAsync()
        {
            return db.FT().CreateAsync(
                indexName: "idx:users",
                new FTCreateParams()
                    .On(IndexDataType.JSON)
                    .Prefix("user:"),
                new Schema()
                    .AddTextField(new FieldName("$.name", "name"))
                    .AddTagField(new FieldName("$.city", "city"))
                    .AddNumericField(new FieldName("$.age", "age")));
        }

        public async Task<IEnumerable<T>> SearchAsync<T>(string index, string query = "Paul @age:[30 40]") 
            where T : class
        {
            var searchResult = await db.FT().SearchAsync(
                indexName: index,
                new Query(query)); //Paul по всем текстовым полям

            return searchResult.Documents.Select(d => JsonSerializer.Deserialize<T>(d[JSON_KEY]!))!;
        }

        public async Task<IEnumerable<T>> SearchAndGetFieldAsync<T>(string index, string field, string query = "@name:Paul @age:[30 40]")
            where T : class
        {
            var searchResult = await db.FT().SearchAsync(
                indexName: index,
                new Query(query).ReturnFields(new FieldName($"$.{field}", $"{field}"))); //Paul по полю name

            return searchResult.Documents.Select(d => JsonSerializer.Deserialize<T>(d[field]!))!;
        }

        public async Task AggreagatedSearch()
        {
            var aggRequest = new AggregationRequest("*")
                      .GroupBy("@city", Reducers.Count().As("count"))
                      .SortBy("@count")
                      .Limit(0, 10);

            var aggResult = await db.FT().AggregateAsync("idx:users", aggRequest);

            for (int i = 0; i < aggResult.TotalResults; i++)
            {
                Console.WriteLine($"Город: {aggResult.GetRow(i)["city"]}, Количество: {aggResult.GetRow(i)["count"]}");
            }
        }
    }
}
