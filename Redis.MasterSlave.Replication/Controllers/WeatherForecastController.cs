using Microsoft.AspNetCore.Mvc;

namespace Redis.MasterSlave.Replication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ICacheService cacheService) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpPost("Set")]
        public async Task Set()
        {
            var i = 0;
            foreach (var summary in Summaries)
            {
               await cacheService.SetAsync("sum:1", summary, TimeSpan.FromSeconds(60));
                i++;
            }
        }

        [HttpGet("get-from-master")]
        public Task<string?> Get(string key)
        {
           return cacheService.GetAsync(key);
        }

        [HttpGet("get-from-replica")]
        public Task<string?> GetFromReplica(string key)
        {
            return cacheService.GetFromReplicaAsync(key);
        }
    }
}
