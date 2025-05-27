using NRedisStack.RedisStackCommands;
using RedisStackLearn.OM;
using StackExchange.Redis;
using System.Collections.Generic;

namespace RedisStackLearn.Modules
{
    //Count-Min Sketch — это вероятностная структура данных, которая:
    //Хранит приближённые счётчики частоты для элементов
    //Использует мало памяти
    //Может немного переврать счёт, но никогда не занижает его
    //Это идеальный выбор, если тебе надо оценить, как часто что-то встречается,
    //и точность не критична(например, популярность запросов, IP-адресов, продуктов и т.п.)

    //Где CMS полезен:
    //Сценарий Почему это удобно
    //Подсчёт частоты использования Счёт на миллионы уникальных значений
    //Подсчёт кликов, просмотров  Быстро, дешево, без ключей
    //Ограничение API на редкие IP Можно делать rate limit по популярности
    //Логирование действий Без хранения всех данных
    public class CmsService
    {
        public async Task Example()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
            var db = redis.GetDatabase();
            var cms = db.CMS();

            // 1. Создать Count-Min Sketch с размерами 2000 x 5

            await cms.InitByDimAsync("cms:views1", width: 2000, depth: 5);
            await cms.InitByProbAsync("cms:views2", error: 0.001, probability: 0.99);
            // 2. Увеличить счётчик для "product:123" на 1
            await cms.IncrByAsync("cms:views1", "product:123", 1);

            var result = await cms.QueryAsync("cms:views1", "product:123", "product:456");

            Console.WriteLine($"product:123 seen ≈ {result[0]} times");
            Console.WriteLine($"product:456 seen ≈ {result[1]} times");
        }
    }
}
