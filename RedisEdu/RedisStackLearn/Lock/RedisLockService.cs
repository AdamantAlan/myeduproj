using Medallion.Threading.Redis;
using StackExchange.Redis;

namespace RedisStackLearn.Lock
{
    public class RedisLockService(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase();

        public RedisDistributedLock GetLock(string resource) => new RedisDistributedLock(resource, db);

        public async Task Exemple(TimeSpan? timeout = null)
        {
            var @lock = GetLock("lock:s3:policies");
            RedisDistributedLockHandle? handle = null;
            var startTime = DateTime.UtcNow;
            timeout ??= TimeSpan.FromSeconds(10);

            while (DateTime.UtcNow - startTime < timeout)
            {
                handle = await @lock.TryAcquireAsync(TimeSpan.FromSeconds(4));
                if (handle != null) break;

                await Task.Delay(250);
            }

            if (handle != null)
            {
                using (handle)
                {
                    Console.WriteLine("🔐 Lock acquired! Working...");
                    await Task.Delay(3000);
                }
            }
        }
    }
}
