using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

namespace RedisStackLearn.Modules
{
    public class TopKService
    {
        //Top-K фильтр — это вероятностная структура данных, которая:

        //Хранит K самых часто встречающихся элементов
        //Обновляется автоматически при добавлении новых
        //Используется, когда нужно знать самые популярные значения, но нет памяти считать всё
        //Типичный use case: топ-запросы, топ-товары, самые частые ошибки и т.д.

        //Применения Top-K:
        //Сценарий Почему использовать Top-K
        //Самые частые поисковые запросы Нет необходимости считать всё
        //Популярные товары Быстро, без сортировки
        //Частые ошибки/исключения Можно отслеживать и реагировать
        //Аналитика по пользователям/IP Минимум памяти, максимум пользы
        public async Task Example()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
            var db = redis.GetDatabase();
            var topk = db.TOPK();

            // 1. Создание Top-K фильтра на 3 элемента, с глубиной 50 и 3 счётчиками
            await topk.ReserveAsync("top:products", topk: 3, width: 50, depth: 3);

            // 2. Добавление данных
            await topk.AddAsync("top:products", "apple");
            await topk.AddAsync("top:products", "banana");
            await topk.AddAsync("top:products", "banana");
            await topk.AddAsync("top:products", "cherry");
            await topk.AddAsync("top:products", "banana");
            await topk.AddAsync("top:products", "apple");
            await topk.AddAsync("top:products", "cherry");
            await topk.AddAsync("top:products", "kiwi");

            // 3. Получение текущего топа
            var list = await topk.ListAsync("top:products");
            Console.WriteLine("Top 3:");
            foreach (var item in list)
                Console.WriteLine(item);

            // 4. Проверка, входит ли "banana" в топ
            var exists = await topk.QueryAsync("top:products", "banana");
            Console.WriteLine($"Is banana in top? {exists}");

            // 5. Количество появлений
            var counts = await topk.CountAsync("top:products", "banana", "apple");
            Console.WriteLine($"banana ≈ {counts[0]}, apple ≈ {counts[1]}");
        }
    }
}
