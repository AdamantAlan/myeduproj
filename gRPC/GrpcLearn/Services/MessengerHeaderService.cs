using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace GrpcLearn.Services;

public class MessengerHeaderService : HeaderRpc.Messenger.MessengerBase
{
    [Authorize]
    public override async Task<HeaderRpc.Response> SendMessage(HeaderRpc.Request request, ServerCallContext context)
    {
        foreach (var h in context.RequestHeaders)
        {
            Console.WriteLine($"{h.Key}: {h.Value}"); 
        }

        var customHeaders = new Metadata();
        customHeaders.Add("username", context.RequestHeaders.GetValue("username"));
        customHeaders.Add("token", context.RequestHeaders.GetValue("authorization"));
        await context.WriteResponseHeadersAsync(customHeaders);
        
        return await Task.FromResult(new HeaderRpc.Response());
    }
}