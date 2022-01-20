using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace RedisEdu
{
    public abstract record IRedisManager(IDistributedCache cache)
    {
       public abstract Task<string> TestRedisAsync();
    }

    public record RedisManager(IDistributedCache cache) : IRedisManager(cache)
    {

        public override async Task<string> TestRedisAsync()
        {
            var data_cache = await cache.GetStringAsync("test2");

            if (data_cache is null)
            {
                var cacheOpt = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(10)
                };
                data_cache = "Hello World2!";
                await cache.SetStringAsync("test", data_cache, cacheOpt);
            }

            return data_cache;
        }
    }
}
