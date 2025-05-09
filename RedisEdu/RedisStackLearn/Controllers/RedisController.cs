using Microsoft.AspNetCore.Mvc;

namespace RedisStackLearn.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class RedisController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Ok();
        }
    }
}
