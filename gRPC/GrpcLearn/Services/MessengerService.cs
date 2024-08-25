using Grpc.Core;
using GrpcLearn.ServerStream;
namespace GrpcLearn.Services;

public class MessengerService : Messenger.MessengerBase
{
    string[] messages = { "Привет", "Как дела?", "Че молчишь?", "Ты че, спишь?", "Ну пока" };
    
    public override async Task ServerDataStream(ServerStream.Request request, IServerStreamWriter<ServerStream.Response> responseStream,
        ServerCallContext context)
    {
        foreach (var m in messages)
        {
            await responseStream.WriteAsync(new ServerStream.Response {Content = m});
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}