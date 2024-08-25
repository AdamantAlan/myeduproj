using Grpc.Net.Client;

namespace CleintGrpcLern;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello,It's the gRpc client!!");
        using var channel = GrpcChannel.ForAddress("http://localhost:5120");

        while (true)
        {
            Console.Write("Введите номер канала: ");
            var chanellNumber = Console.ReadLine();
            if (chanellNumber == "1")
            {
                var client = new Greeter.GreeterClient(channel);
                Console.Write("Введите имя: ");
                string? name = Console.ReadLine();
                var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
                Console.WriteLine($"Ответ сервера: {reply.Message}");
            }

            if (chanellNumber == "2")
            {
                var client = new LocalRpc.LocalRpcClient(channel);
                Console.Write("Введите сигнал: ");
                string signal = Console.ReadLine();
                Console.Write("Введите описание: ");
                var desc = Console.ReadLine();
                var reply = await client.GetLocalSignalAsync(new Request()
                {
                    Signal = int.Parse(signal), 
                    Desc = desc
                });
                var result = reply.OutMessage is null ? reply.ErrorId.ToString() : reply.OutMessage;
                Console.WriteLine($"Ответ сервера: {result}");
            }   
        }
    }
}