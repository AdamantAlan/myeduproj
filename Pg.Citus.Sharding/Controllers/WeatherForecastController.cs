using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pg.Easy.Sharding.Db;

namespace Pg.Easy.Sharding.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, AppDbContext context) : ControllerBase
    {
        [HttpGet(Name = "Order")]
        public async Task<IEnumerable<Order>> Get(long customerId)
        {
            var conn = context.Database.GetDbConnection();
            Console.WriteLine("Connection string: " + conn.ConnectionString);
            Console.WriteLine("Host: " + conn.DataSource);

            return await context.Orders.ToListAsync();
        }

        [HttpPost(Name = "Order")]
        public async Task<int> Post(Order order)
        {
            var conn = context.Database.GetDbConnection();
            Console.WriteLine("Connection string: " + conn.ConnectionString);
            Console.WriteLine("Host: " + conn.DataSource);

            await context.Orders.AddAsync(order);
            
            return await context.SaveChangesAsync();
        }
    }
}
