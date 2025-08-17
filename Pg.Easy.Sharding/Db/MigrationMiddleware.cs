using Microsoft.EntityFrameworkCore;
using Pg.Easy.Sharding.Shard;

namespace Pg.Easy.Sharding.Db
{
    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MigrationMiddleware>();
        }
    }

    public sealed class MigrationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IShardRegistry shardRegistry;
        private readonly IShardedDbContextFactory appContextfactory;

        public MigrationMiddleware(
            RequestDelegate next,
            IShardRegistry reg,
            IShardedDbContextFactory factory)
        {
            this.next = next;
            this.shardRegistry = reg;
            this.appContextfactory = factory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var scope = context.RequestServices.CreateScope())
            {
                for (var shard = 0; shard < shardRegistry.ShardCount; shard++)
                {
                    using var db = appContextfactory.CreateForShard(shard);
                    db.Database.Migrate(); // применит миграции на конкретном инстансе
                }
            }

            await next(context);
        }
    }
}
