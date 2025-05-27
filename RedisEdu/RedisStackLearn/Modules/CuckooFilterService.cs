using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

namespace RedisStackLearn.Modules
{
    public class CuckooFilterService
    {
        //это вероятностная структура данных, аналогичная Bloom Filter, но с рядом преимуществ
        //Удаление(DELETE)
        //Меньше ложных срабатываний
        //Поддерживает перемещение записей

        //Применения Cuckoo Filter:
        //Хранение временных токенов Удаляем, когда пользователь их использовал
        //Проверка спама Можно исключать старые адреса
        //Кэширование ключей API С возможностью удаления устаревших
        public async Task Example()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
            var db = redis.GetDatabase();
            var cf = db.CF();

            // 1. Создание фильтра с вместимостью на 1000 элементов
            await cf.ReserveAsync("mycuckoo", capacity: 1000);

            // 2. Добавляем элемент
            await cf.AddAsync("mycuckoo", "user:123");

            // 3. Проверка наличия
            bool exists = await cf.ExistsAsync("mycuckoo", "user:123");  // true
            bool notExists = await cf.ExistsAsync("mycuckoo", "user:999"); // false

            // 4. Удаление
            bool deleted = await db.DeleteCuckooItemAsync("mycuckoo", "user:123"); // true

            // 5. Проверка после удаления
            bool stillExists = await cf.ExistsAsync("mycuckoo", "user:123"); // false

            Console.WriteLine($"Deleted? {deleted}");
        }
    }

    public static class RedisCuckooExtensions
    {
        public static async Task<bool> DeleteCuckooItemAsync(this IDatabase db, string filterKey, string value)
        {
            var result = await db.ExecuteAsync("CF.DEL", filterKey, value);
            return (int)result == 1;
        }
    }
}
