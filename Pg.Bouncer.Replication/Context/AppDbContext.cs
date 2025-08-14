using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pg.Easy.Replication.Infrastructure;
using Pg.Easy.Replication.Model;

namespace Pg.Easy.Replication.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IDataSourceSelector _selector;

        [ActivatorUtilitiesConstructor]
        public AppDbContext(DbContextOptions<AppDbContext> options, IDataSourceSelector selector): base(options)
        {
            _selector = selector;
        }

        // Используется в design-time фабрике (без selector)
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            _selector = null;
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Если опции уже сконфигурированы (DesignTimeFactory вызывает .UseNpgsql) — ничего не делаем
            if (optionsBuilder.IsConfigured) return;

            // В рантайме, когда DI дал селектор — настраиваем провайдер по текущему источнику
            if (_selector is not null)
                optionsBuilder.UseNpgsql(_selector.Current());
        }

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Product>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.Price).HasColumnType("numeric(12,2)");
                e.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
            });
        }
    }
}
