namespace Pg.Easy.Sharding.Shard
{
    public interface IShardRegistry
    {
        int ShardCount { get; }

        string GetConnectionString(int shardId);

        int ResolveShardId(long shardKey);
    }

    public sealed class ModuloShardRegistry : IShardRegistry
    {
        private readonly string[] _conn;

        public int ShardCount => _conn.Length;

        public ModuloShardRegistry(IConfiguration cfg)
        {
            _conn =
            [
            cfg.GetConnectionString("pg1")!,
            cfg.GetConnectionString("pg2")!,
            cfg.GetConnectionString("pg3")!,
            ];
        }

        public string GetConnectionString(int shardId) => _conn[shardId];

        // простое шардирование по остатку — достаточно, если ребаланс не нужен
        public int ResolveShardId(long shardKey)
            => (int)(unchecked((ulong)shardKey) % (uint)ShardCount);
    }
}
