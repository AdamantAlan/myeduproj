using StackExchange.Redis;
using System.Text.Json;

namespace RedisStackLearn.Bus
{
    public class Consumer(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase();

        public async Task ConsumeAsync<T>(CancellationToken token) where T : class
        {
            var eventBus = connection.GetSubscriber();

            var channel = new RedisChannel(nameof(T), RedisChannel.PatternMode.Literal);

            await eventBus.SubscribeAsync(channel, (channel, message) =>
            {
                if (message.IsNullOrEmpty) return;

                var @object = JsonSerializer.Deserialize<T>(message);
            });

            await Task.Delay(Timeout.Infinite, token);
        }
    }
}
