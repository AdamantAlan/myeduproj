using Microsoft.EntityFrameworkCore;

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

        public MigrationMiddleware(
            RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using var scope = context.RequestServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await db.Database.MigrateAsync();

            await next(context);
        }
    }
}
