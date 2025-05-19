using StackExchange.Redis;
using System.Text.Json;

namespace RedisStackLearn.Bus
{
    public class Publisher(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase();

        public async Task PublishAsync<T>(T @event) where T : class
        {
            var eventBus = connection.GetSubscriber();

            var channel = new RedisChannel(nameof(T), RedisChannel.PatternMode.Literal);
            await eventBus.PublishAsync(channel, JsonSerializer.Serialize(@event));
        }
    }
}
