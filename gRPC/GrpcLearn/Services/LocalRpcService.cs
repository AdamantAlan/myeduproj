using Grpc.Core;

namespace GrpcLearn.Services;

public class LocalRpcService : LocalRpc.LocalRpcBase
{
    public LocalRpcService()
    {
        
    }

    public override Task<Response> GetLocalSignal(Request request, ServerCallContext context)
    {
        try
        {
            if (request.Signal == -1)
                throw new Exception("Exception 1");
            return Task.FromResult(new Response() { OutMessage = request.GetHashCode().ToString(), ErrorId = 0});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult(new Response() { OutMessage = null, ErrorId = 500});       
        }
        
    }
}