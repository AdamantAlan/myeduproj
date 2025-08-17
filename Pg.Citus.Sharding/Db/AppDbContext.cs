using Microsoft.EntityFrameworkCore;

namespace Pg.Easy.Sharding.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Order>(e =>
            {
                e.HasKey(x => new { x.CustomerId, x.Id });
                e.Property(x => x.Id).ValueGeneratedNever(); // Guid генерим в приложении

                e.HasIndex(x => new { x.CustomerId, x.CreatedAt });
            });
        }
    }
}
