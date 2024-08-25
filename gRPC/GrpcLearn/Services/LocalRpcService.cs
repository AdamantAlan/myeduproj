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
            if (request.Signal is null)
                throw new Exception("Signal is null!");
            return Task.FromResult(new Response() { OutMessage = request.GetHashCode().ToString(), ErrorId = 0});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult(new Response() { OutMessage = null, ErrorId = 500});       
        }
        
    }
}