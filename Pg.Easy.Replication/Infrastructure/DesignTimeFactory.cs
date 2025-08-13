using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Pg.Easy.Replication.Context;

namespace Pg.Easy.Replication.Infrastructure
{
    public sealed class DesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var cfg = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(cfg.GetConnectionString("Primary")!)
                .Options;

            return new AppDbContext(options);
        }
    }
}
