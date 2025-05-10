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

        public Task<long> IncrAsync(string key) => db.StringIncrementAsync(key);

        public Task<long> IncrbyAsync(string key, long count) => db.StringIncrementAsync(key, count);

        public Task<long> DecrAsync(string key) => db.StringDecrementAsync(key);

        public Task<long> DecrByAsync(string key, long diff) => db.StringDecrementAsync(key, diff);

        public Task PipelineAsync(string key)
        {
            var pipe = new Pipeline(db);

            for (int i = 0; i < 5; i++)
            {
                #pragma warning disable CS4014 
                pipe.Db.StringSetAsync($"seat:{i}", $"#{i}");
                #pragma warning restore CS4014 
            }

            pipe.Execute();

            var resp4 = db.StringGet("counter:1");
            Console.WriteLine(resp4);

            var resp5 = db.StringGet("counter:2");
            Console.WriteLine(resp5);

            var resp6 = db.StringGet("counter:3");
            Console.WriteLine(resp6);

            return Task.CompletedTask;
        }

        public Task TransactionAsync(string key)
        {
            var transaction = new Transaction(db);

            transaction.AddCondition(Condition.StringNotEqual("test:134", "aaa/bbb"));

            for (int i = 0; i < 5; i++)
            {
                #pragma warning disable CS4014
                transaction.Db.StringSetAsync($"seat:{i}", $"#{i}");
                #pragma warning restore CS4014
            }

            transaction.Execute();

            var resp4 = db.StringGet("counter:1");
            Console.WriteLine(resp4);

            var resp5 = db.StringGet("counter:2");
            Console.WriteLine(resp5);

            var resp6 = db.StringGet("counter:3");
            Console.WriteLine(resp6);

            return Task.CompletedTask;
        }

        public async Task WhenConditionAsync()
        {
            //good work
            await db.HashSetAsync("Details", "SerialNumber", "12345");

            //it doesn't change values into hashSet, return false
            await db.HashSetAsync("Details", "SerialNumber", "12345A", When.NotExists);
        }
    }
}
