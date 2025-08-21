using StackExchange.Redis;

namespace Redis.Cluster.Sharding
{
    public interface ICacheService
    {
        Task SetAsync(string key, string value, TimeSpan ttl);
        Task<string?> GetAsync(string key);
        Task<string?> GetFromReplicaAsync(string key);
    }

    public sealed class CacheService : ICacheService
    {
        private readonly IDatabase _db;
        public CacheService(IDatabase db) => _db = db;

        public Task SetAsync(string key, string value, TimeSpan ttl) =>
            _db.StringSetAsync(key, value, ttl);

        public async Task<string?> GetAsync(string key) =>
           await _db.StringGetAsync(key);

        public async Task<string?> GetFromReplicaAsync(string key) =>
           await _db.StringGetAsync(key, flags: CommandFlags.PreferReplica);
    }
}
