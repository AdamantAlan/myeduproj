using NRedisStack.DataTypes;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

namespace RedisStackLearn.Modules
{
    //хранения временных меток с числовыми значениями,

    //агрегации (среднее, максимум, сумма и т.д.),

    //уплотнения (downsampling),

    //автоматической retention(удаление старых данных).

    [Obsolete("Старая апишка временных рядов, нужно глянуть новую")]
    public class TimeSerieService
    {
        public async Task Example()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
            var db = redis.GetDatabase();
            var ts = db.TS();

            // 1. Создание ряда
            await ts.CreateAsync("sensor:temp", retentionTime: 36000);

            // 2. Добавление точки (время: сейчас, значение: 21.5°C)
            await ts.AddAsync("sensor:temp", DateTime.UtcNow, 21.5);

            // 3. Получение последнего значения
            var latest = await ts.GetAsync("sensor:temp");
            Console.WriteLine($"Время: {latest.Time}, Значение: {latest.Val}");

            // 4. Получение диапазона значений за последние 10 минут
            var now = DateTime.UtcNow;
            var data = await ts.RangeAsync("sensor:temp", now.AddMinutes(-10), now);
            foreach (var point in data)
                Console.WriteLine($"{point.Time}: {point.Val}");

            await ts.CreateAsync("sensor:temp", retentionTime: 36000, labels: new[] {
                new TimeSeriesLabel("sensor", "office"),
                new TimeSeriesLabel("type", "temperature") 
            });

            var dataRange = await ts.MRangeAsync(
                now.AddMinutes(-10),
                now,
                new[] { "sensor=office" });

            // Создаём исходный ряд
            await ts.CreateAsync("cpu:raw", retentionTime: 36000);

            // Создаём агрегированный ряд
            await ts.CreateAsync("cpu:avg:10s", retentionTime: 36000);

            // Создаём правило агрегации: среднее значение за каждые 10 секунд
            await ts.CreateRuleAsync("cpu:raw", new TimeSeriesRule("cpu:avg:10s", 10, NRedisStack.Literals.Enums.TsAggregation.Avg));
        }
    }
}
