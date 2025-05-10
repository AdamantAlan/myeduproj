using NRedisStack.RedisStackCommands;
using NRedisStack.Search.Literals.Enums;
using NRedisStack.Search;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisStackLearn.Services
{
    public class RedisHashService(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase(0);

        public Task HSetAsync(string prefix, string key, Dictionary<string, string> hash)
        {
            var hashEntry = new List<HashEntry>();

            foreach (var hashKey in hash.Keys)
            {
                hashEntry.Add(new HashEntry(hashKey, hash[hashKey]));
            }

            return db.HashSetAsync($"{prefix}:{key}", hashEntry.ToArray());
        }

        public async Task<string> HGetAsync(string key, string property)
        {
            string? value = await db.HashGetAsync(key, property);
            return value!;
        }

        public async Task<Dictionary<string, string>> HGetAllAsync(string key)
        {
            HashEntry[] hashEntrys = await db.HashGetAllAsync(key);
            var relultHash = new Dictionary<string, string>();

            foreach (var hashEntry in hashEntrys)
            {
                relultHash.Add(hashEntry.Name!, hashEntry.Value!);
            }

            return relultHash;
        }

        public Task<bool> CreateFtIndexAsync()
        {
            return db.FT().CreateAsync(
                indexName: "h-idx:users",
                new FTCreateParams()
                    .On(IndexDataType.HASH)
                    .Prefix("huser:"),
                new Schema()
                    .AddTextField(new FieldName("name"))
                    .AddTagField(new FieldName("city"))
                    .AddNumericField(new FieldName("age")));
        }

        public async Task<IEnumerable<string>> SearchAsync(string index = "h-idx:users", string query = "Paul @age:[30 40]")
        {
            var searchResult = await db.FT().SearchAsync(
                indexName: index,
                new Query(query)); //Paul по всем текстовым полям

            var result = new string[searchResult.Documents.Count];
            foreach (var hash in searchResult.Documents)
            {
                var name = hash["name"];
                var age = hash["age"];
            }

            return result;
        }
    }
}
