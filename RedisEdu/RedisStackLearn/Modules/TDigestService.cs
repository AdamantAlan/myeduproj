using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

namespace RedisStackLearn.Modules
{
    //Используется для анализа распределения числовых данных — например, латентность, суммы заказов, задержки, температуры и т.п.
    public class TDigestService
    {
        public async Task Example()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
            var db = redis.GetDatabase();
            var tdigest = db.TDIGEST();

            await tdigest.CreateAsync("latency:metrics", compression: 100);

            await tdigest.AddAsync("latency:metrics", 100, 150, 200, 300, 250, 120, 110);

            // 3. Получение 50-го (медиана), 90-го и 99-го процентилей
            var percentiles = await tdigest.QuantileAsync("latency:metrics", 0.5, 0.9, 0.99);

            var min = await tdigest.MinAsync("latency:metrics");
            var max = await tdigest.MaxAsync("latency:metrics");
        }
    }
}
