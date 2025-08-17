using Microsoft.EntityFrameworkCore;
using Pg.Easy.Sharding.Db;
using Npgsql;

namespace Pg.Easy.Sharding.Shard
{
    public interface IShardedDbContextFactory
    {
        AppDbContext CreateForKey(long shardKey);
        AppDbContext CreateForShard(int shardId);
    }

    public sealed class ShardedDbContextFactory(IShardRegistry reg) : IShardedDbContextFactory
    {
        public AppDbContext CreateForKey(long shardKey)
        {
            var shardId = reg.ResolveShardId(shardKey);
            return CreateForShard(shardId);
        }

        public AppDbContext CreateForShard(int shardId)
        {
            var cs = reg.GetConnectionString(shardId);
            var opts = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(cs, npg => npg.EnableRetryOnFailure())
                .Options;
            return new AppDbContext(opts);
        }
    }
}
