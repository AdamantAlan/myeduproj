using StackExchange.Redis;

namespace Redis.MasterSlave.Replication
{
    public interface ICacheService
    {
        Task SetAsync(string key, string value, TimeSpan ttl);
        Task<string?> GetAsync(string key);            // обычное чтение
        Task<string?> GetFromReplicaAsync(string key); // чтение с реплики
    }

    public sealed class CacheService : ICacheService
    {
        private readonly IDatabase _db;
        public CacheService(IDatabase db) => _db = db;

        public Task SetAsync(string key, string value, TimeSpan ttl) =>
            _db.StringSetAsync(key, value, ttl); // запись -> мастер

        public async Task<string?> GetAsync(string key) =>
            await _db.StringGetAsync(key); // без флагов: библиотека сама выберет (обычно мастер)

        public async Task<string?> GetFromReplicaAsync(string key) =>
            await _db.StringGetAsync(key, CommandFlags.PreferReplica); // предпочесть реплики
    }
}
