using StackExchange.Redis;

namespace RedisStackLearn.Services
{
    public class RedisListService(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase(0);

        public Task LPushListAsync(string key, string value) => db.ListLeftPushAsync(key, value);

        public Task RPushListAsync(string key, string value) => db.ListRightPushAsync(key, value);

        public Task<RedisValue> LPopListAsync(string key) => db.ListLeftPopAsync(key);

        public Task<RedisValue> RPopListAsync(string key) => db.ListRightPopAsync(key);

        public Task<long> LenghtAsync(string key) => db.ListLengthAsync(key);

        public Task<RedisValue[]> GetRangeAsync(string key, long start, long end) => db.ListRangeAsync(key, start, end);

        public Task<RedisValue> MoveAsync(string sourceKey, string destKey) => 
            db.ListMoveAsync(sourceKey, destKey, ListSide.Left,ListSide.Left);

        public Task TrimAsync(string key, long start, long end) => db.ListTrimAsync(key, start, end);
    }
}
