using StackExchange.Redis;

namespace RedisStackLearn.Services
{
    public class RedisBitmapService(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase(0);

        public Task<bool> SetBitAsync(string key, long offset, bool bit) => db.StringSetBitAsync(key, offset, bit);

        public Task<bool> GetBitAsync(string key, long offset) => db.StringGetBitAsync(key, offset);

        public Task<long> GetBitPositionAsync(string key, bool bit, long start, long end) => 
            db.StringBitPositionAsync(key, bit, start, end);

        public Task<long> CountBitAsync(string key, long start, long end) => db.StringBitCountAsync(key, start, end);

        public Task BitOperationAsync(RedisKey[] keys, string destKey, Bitwise bitwise = Bitwise.Xor) =>
            db.StringBitOperationAsync(bitwise, destKey, keys);

    }
}
