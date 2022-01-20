using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace testMass.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MassController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IRequestClient<Message3> _client;

        public MassController(IBus bus, IRequestClient<Message3> client)
        {
            _bus = bus;
            _client = client;
        }

        [HttpGet("PubSub")]
        public async Task publishSubscribe()
        {
            await _bus.Publish<Message>(
               new
               {
                   Value = "123"
               });
        }

        [HttpGet("GetResponse")]
        public async Task<string> GetResponse()
        {
            CancellationTokenSource source = new();
            var token = source.Token;

            return (await _client.GetResponse<MessageResponse>(new
            {
                Value = "123"
            }, token)).Message.Value;
        }
    }
}
