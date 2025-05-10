using NRedisStack;
using StackExchange.Redis;

namespace RedisStackLearn.Services
{
    public class RedisKeyService(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase(0);

        public Task SetAsync(string key, string value)
        {
            return db.StringSetAsync(key, value);
        }

        public async Task<string> GetAsync(string key)
        {
            string? value = await db.StringGetAsync(key);
            return value!;
        }

        public async Task<string> PipelineAsync(string key)
        {
            var pipe = new Pipeline(db);

            for (int i = 0; i < 5; i++)
            {
#pragma warning disable CS4014 
                pipe.Db.StringSetAsync($"seat:{i}", $"#{i}");
#pragma warning restore CS4014 
            }

            pipe.Execute();

        }
    }


}
