using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pg.Easy.Replication.Context;
using Pg.Easy.Replication.Infrastructure;
using Pg.Easy.Replication.Model;

namespace Pg.Easy.Replication.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public ProductsController(IDbContextFactory<AppDbContext> factory) => _factory = factory;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            await using var db = await _factory.CreateDbContextAsync(ct);

            var conn = db.Database.GetDbConnection();
            Console.WriteLine("Connection string: " + conn.ConnectionString);
            Console.WriteLine("Host: " + conn.DataSource); // зависит от провайдера


            var items = await db.Products.AsNoTracking().ToListAsync(ct);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string name, CancellationToken ct)
        {
            await using var db = await _factory.CreateDbContextAsync(ct);

            var conn = db.Database.GetDbConnection();
            Console.WriteLine("Connection string: " + conn.ConnectionString);
            Console.WriteLine("Host: " + conn.DataSource); // зависит от провайдера


            db.Products.Add(new Product { Name = name, Price = 0m });
            await db.SaveChangesAsync(ct);
            return NoContent();
        }
    }
}
