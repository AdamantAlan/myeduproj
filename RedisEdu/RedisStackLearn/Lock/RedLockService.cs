using RedLockNet.SERedis;

namespace RedisStackLearn.Lock
{
    public class RedLockService(RedLockFactory lockFactory)
    {
        public async Task ExampleReadLock(string resource)
        {
            resource ??= "lock:my:critical:task";

            var expiry = TimeSpan.FromSeconds(10);
            var wait = TimeSpan.FromSeconds(5);
            var retry = TimeSpan.FromMilliseconds(200);

            using var redlock = await lockFactory.CreateLockAsync(resource, expiry, wait, retry);

            if(!redlock.IsAcquired) return;

            //Do something

            return;
        }
    }
}
