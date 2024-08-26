using Grpc.Core;
using GrpcLearn.ClientStream;
using Microsoft.AspNetCore.Authorization;

namespace GrpcLearn.Services;

[Authorize]
public class MessengerClientService : MessengerClient.MessengerClientBase
{
    public override async Task<ClientStream.Response> ClientDataStream(IAsyncStreamReader<ClientStream.Request> requestStream,
        ServerCallContext context)
    {
        await foreach (var r in requestStream.ReadAllAsync())
        {
            Console.WriteLine(r.Content);
        }
        
        return new ClientStream.Response { Content = "все данные получены" };
    }
}