using StackExchange.Redis;

namespace RedisStackLearn.Services
{
    public class RedisKeyService(ConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase(0);

        public Task SetAsync(string key, string value)
        {
            return db.StringSetAsync(key, value);
        }

        public async Task<string> GetAsync(string key)
        {
            string? value = await db.StringGetAsync(key);
            return value!;
        }

        public Task HSetAsync(string prefix, string key, Dictionary<string,string> hash)
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
    }
}
