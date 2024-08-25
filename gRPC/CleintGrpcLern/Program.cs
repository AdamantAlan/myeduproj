using CleintGrpcLern.ClientStream;
using CleintGrpcLern.Measure;
using CleintGrpcLern.ServerStream;
using Grpc.Core;
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
                var result = reply.ErrorId == 500 ? "Error" + reply.ErrorId : reply.OutMessage;
                Console.WriteLine($"Ответ сервера: {result}");
            }

            if (chanellNumber == "3")
            {
                var client = new MeasureManager.MeasureManagerClient(channel);
                Console.Write("Введите имя: ");
                var name = Console.ReadLine();
                var reply = await client.InviteAsync(new CreateMeasureWriteDto
                {
                    Name = name
                });
                var eventInvitation = reply.Invitation;
                var eventDateTime = reply.Start.ToDateTime();
                var eventDuration = reply.Duration.ToTimeSpan();
                Console.WriteLine(eventInvitation);
                Console.WriteLine($"Начало: {eventDateTime.ToString("dd.MM HH:mm")}   Длительность: {eventDuration.TotalHours} часа");
            }
            
            if (chanellNumber == "4")
            {
                var client = new Messenger.MessengerClient(channel);
                using var reply = client.ServerDataStream(new ServerStream.Request());
                var stream = reply.ResponseStream;
                
                await foreach (var r in stream.ReadAllAsync())
                {
                    Console.WriteLine(r.Content);
                }
            }
            
            if (chanellNumber == "5")
            {
                string[] messages = { "Привет", "Как дела?", "Че молчишь?", "Ты че, спишь?", "Ну пока" };
                var client = new MessengerClient.MessengerClientClient(channel);
                using var call = client.ClientDataStream();
                foreach (var m in messages)
                {
                    await call.RequestStream.WriteAsync(new ClientStream.Request() {Content = m});
                }

                await call.RequestStream.CompleteAsync();
                var response = await call.ResponseAsync;
                Console.WriteLine($"Ответ сервера: {response.Content}");
            }
        }
    }
}