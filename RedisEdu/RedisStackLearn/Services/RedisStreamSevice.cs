using StackExchange.Redis;

namespace RedisStackLearn.Services;

public class RedisStreamSevice(IConnectionMultiplexer connection)
{
    private IDatabase db { get; set; } = connection.GetDatabase(0);

    public Task<RedisValue> AddEntry(string stream, NameValueEntry[] entry) => db.StreamAddAsync(stream, entry);

    //+ until the end
    public Task<StreamEntry[]> GetRangeAsync(string stream, string minId, int length) => db.StreamRangeAsync(stream, minId, StreamPosition.NewMessages, length);

    public Task<StreamEntry[]> ReadAsync(string stream, int position, int length) => db.StreamReadAsync(stream, StreamPosition.Beginning, length);

    public Task<long> LengthAsync(string key) => db.StreamLengthAsync(key);

    public Task<bool> CreateConsumerGroupAsync(string key, string groupName, string position = null) => 
        db.StreamCreateConsumerGroupAsync(key, groupName, position ?? "$");

    public Task<StreamEntry[]> StreamReadGroupAsync(string key, string groupName, string consumerName,
        RedisValue position, int count) => db.StreamReadGroupAsync(key, groupName, consumerName, position, count);

    public Task<long> AscAsync(string key, string groupName, string consumerName,
        RedisValue position) => db.StreamAcknowledgeAsync(key, groupName,position);

}