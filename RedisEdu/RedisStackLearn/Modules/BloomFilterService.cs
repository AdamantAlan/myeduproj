using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

namespace RedisStackLearn.Modules
{
    public class BloomFilterService
    {
        //Кеш-антидубликат Быстрая проверка «было ли это»
        //Защита от повторной загрузки    Проверка перед запросом к API - идемпотентность
        //Предфильтрация для БД   Уменьшение запросов к SQL/NoSQL
        //Фильтрация спама/ботов/чёрных списков   Лёгкая, быстрая проверка
        public async Task Example()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
            var db = redis.GetDatabase();
            var bf = db.BF();

            // 1. Создаём фильтр с вероятностью ложного срабатывания 1% и ожидаемым объёмом 10_000
            await bf.ReserveAsync("mybloom", errorRate: 0.01, capacity: 10_000);

            // 2. Добавляем значение
            await bf.AddAsync("mybloom", "user:123");

            // 3. Проверяем наличие
            bool exists = await bf.ExistsAsync("mybloom", "user:123"); // true
            bool notExists = await bf.ExistsAsync("mybloom", "user:999"); // false

            Console.WriteLine($"User 123 exists? {exists}");
            Console.WriteLine($"User 999 exists? {notExists}");
        }
    }
}
