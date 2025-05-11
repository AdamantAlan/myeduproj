using StackExchange.Redis;

namespace RedisStackLearn.Services
{
    public class RedisSortedSetService(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase(0);

        public Task<bool> AddAsync(string key, RedisValue value, int score) => db.SortedSetAddAsync(key, value, score);

        public Task<long> AddRangeAsync(string key, SortedSetEntry[] values = null) => db.SortedSetAddAsync(key, values ?? 
            new SortedSetEntry[]
            {
                new SortedSetEntry("Sam-Bodden", 8),
                new SortedSetEntry("Royce", 10),
                new SortedSetEntry("Ford", 6),
                new SortedSetEntry("Prickett", 14),
                new SortedSetEntry("Castilla", 12)
            });

        public Task<RedisValue[]> SortedSetRangeByRankAsync(string key) => db.SortedSetRangeByRankAsync(key, 0, -1, Order.Descending);
        
        public Task<SortedSetEntry[]> SortedSetRangeByRankWithScores(string key) => 
            db.SortedSetRangeByRankWithScoresAsync(key, 0, -1, Order.Descending);

        public Task<bool> SortedSetRemoveAsync(string key, string value) => db.SortedSetRemoveAsync(key, value);

        public Task<long> SortedSetRemoveRangeByScore(string key, long score) => db.SortedSetRemoveRangeByScoreAsync(key, 0, -1);

        public Task<long?> GetScoreAsync(string key, RedisValue value) => db.SortedSetRankAsync(key, value);
    }
}
