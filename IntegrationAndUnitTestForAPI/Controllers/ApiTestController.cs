using IntegrationAndUnitTestForAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAndUnitTestForAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiTestController : ControllerBase
    {

        private readonly ILogger<ApiTestController> _logger;
        private readonly ITestServices _service;

        public ApiTestController(ILogger<ApiTestController> logger, ITestServices service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("/user/{id}")]
        public bool Get(string id)
        {
            return _service.CheckId(id);
        }
    }
}
