using StackExchange.Redis;

namespace RedisStackLearn.Services
{
    public class RedisHyperLogLogService(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase(0);

        public Task<bool> AddAsync(string key, RedisValue value) => db.HyperLogLogAddAsync(key, value);

        public Task<long> CountUniqueAsync(string key, RedisValue value) => db.HyperLogLogLengthAsync(key);

        public Task CountUniqueAsync(string sourceKey, string secondKey, string destKey) => 
            db.HyperLogLogMergeAsync(destKey, sourceKey, secondKey);
    }
}
