using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testMass
{
    public class SendMessageConsumer : IConsumer<Message>
    {
        public Task Consume(ConsumeContext<Message> context)
        {
            var data = context.Message.Value;
            Console.WriteLine($"Receive message (PubSub) value: {data}");
            return Task.CompletedTask;
        }
    }

    public class SendMessageConsumer2 : IConsumer<Message3>
    {
        public async Task Consume(ConsumeContext<Message3> context)
        {
            var data = context.Message.Value;
            Console.WriteLine($"Receive message (GetResponse) value: {data}");
            await context.RespondAsync(new MessageResponse { Value = $"Receive message (GetResponse) value: {data}" });
        }
    }
}
