using Grpc.Core;

namespace GrpcLearn.Services;

public class MessengerHeaderService : HeaderRpc.Messenger.MessengerBase
{
    public override async Task<HeaderRpc.Response> SendMessage(HeaderRpc.Request request, ServerCallContext context)
    {
        foreach (var h in context.RequestHeaders)
        {
            Console.WriteLine($"{h.Key}: {h.Value}"); 
        }

        var customHeaders = new Metadata();
        customHeaders.Add("username", context.RequestHeaders.GetValue("username"));
        customHeaders.Add("token", context.RequestHeaders.GetValue("token"));
        await context.WriteResponseHeadersAsync(customHeaders);
        
        return await Task.FromResult(new HeaderRpc.Response());
    }
}