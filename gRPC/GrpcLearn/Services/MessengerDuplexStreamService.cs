using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace GrpcLearn.Services;

[Authorize]
public class MessengerDuplexStreamService : DuplexStream.Messenger.MessengerBase
{
    string[] messages = { "Привет", "Норм", "...", "Нет", "пока" };

    public override async Task DataStream(IAsyncStreamReader<DuplexStream.Request> streamReader,
        IServerStreamWriter<DuplexStream.Response> streamWriter,
        ServerCallContext context)
    {
        var reading = Task.Run(async () =>
        {
            await foreach (var m in streamReader.ReadAllAsync())
            {
                Console.WriteLine(m.Content);
            }
        });

        foreach (var m in messages)
        {
            await streamWriter.WriteAsync(new DuplexStream.Response {Content = m});
            Console.WriteLine(m);
        }

        for (int i = 0; i < 1000; i++)
        {
            await streamWriter.WriteAsync(new DuplexStream.Response {Content = $"SPAM {i}"});
        }

        await reading;
    }
}