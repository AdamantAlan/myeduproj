using StackExchange.Redis;

namespace RedisStackLearn.Services
{
    public class RedisSetService(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase(0);

        public Task AddAsync(string key, RedisValue value) => db.SetAddAsync(key, value);

        public Task AddRangeAsync(string key, RedisValue[] values) => db.SetAddAsync(key, values);

        public Task<RedisValue[]> GetAllAsync(string key) => db.SetMembersAsync(key);

        public Task<RedisValue> PopAsync(string key) => db.SetPopAsync(key);

        public Task<bool> RemoveAsync(string key, RedisValue value) => db.SetRemoveAsync(key, value);

        public Task<bool> IsSetContainsAsync(string key, RedisValue value) => db.SetContainsAsync(key, value);

        public Task GetLengthAsync(string key) => db.SetLengthAsync(key);

        public Task<bool> MoveAsync(string sourceKey, string destKey, RedisValue value) => db.SetMoveAsync(sourceKey, destKey, value);

        public Task<RedisValue[]> SetCombineAsync(string key1, string key2, SetOperation operation) => 
            db.SetCombineAsync(operation, key1, key2);

    }
}