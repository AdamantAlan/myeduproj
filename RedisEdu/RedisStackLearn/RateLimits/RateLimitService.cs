using StackExchange.Redis;

namespace RedisStackLearn.RateLimits
{
    public class RateLimitService(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase();

        public async Task<bool> IsRateLimitedFixedWindowCounterAsync(string key, int limit, TimeSpan period)
        {
            var count = await db.StringIncrementAsync($"rate:{key}");

            if (count == 1) await db.KeyExpireAsync(key, period);

            return count > limit; 
        }
        
        public async Task<bool> IsRateLimitedSlidingWindowLogAsync(string key, int limit, TimeSpan period)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            await db.ListLeftPushAsync(key, now);
            await db.KeyExpireAsync(key, period);

            var timespans = await db.ListRangeAsync(key);

            var windowStart = now - (long)period.TotalMilliseconds;

            int count = timespans.Count(ts => (long)ts >= windowStart);

            return count > limit;
        }
    }
}
