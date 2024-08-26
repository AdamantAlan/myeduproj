using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleintGrpcLern.ClientStream;
using CleintGrpcLern.Measure;
using CleintGrpcLern.ServerStream;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.IdentityModel.Tokens;

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
                var customHeaders = new Metadata();
                customHeaders.Add("Authorization", $"Bearer {GetJwtToken()}");
                Console.Write("Введите имя: ");
                string? name = Console.ReadLine();
                var reply = await client.SayHelloAsync(new HelloRequest { Name = name }, customHeaders);
                Console.WriteLine($"Ответ сервера: {reply.Message}");
            }

            if (chanellNumber == "2")
            {
                var client = new LocalRpc.LocalRpcClient(channel);
                var customHeaders = new Metadata();
                customHeaders.Add("Authorization", $"Bearer {GetJwtToken()}");
                Console.Write("Введите сигнал: ");
                string signal = Console.ReadLine();
                Console.Write("Введите описание: ");
                var desc = Console.ReadLine();
                var reply = await client.GetLocalSignalAsync(new Request()
                {
                    Signal = int.Parse(signal),
                    Desc = desc
                }, customHeaders);
                var result = reply.ErrorId == 500 ? "Error" + reply.ErrorId : reply.OutMessage;
                Console.WriteLine($"Ответ сервера: {result}");
            }

            if (chanellNumber == "3")
            {
                var client = new MeasureManager.MeasureManagerClient(channel);
                var customHeaders = new Metadata();
                customHeaders.Add("Authorization", $"Bearer {GetJwtToken()}");
                Console.Write("Введите имя: ");
                var name = Console.ReadLine();
                var reply = await client.InviteAsync(new CreateMeasureWriteDto
                {
                    Name = name
                }, customHeaders);
                var eventInvitation = reply.Invitation;
                var eventDateTime = reply.Start.ToDateTime();
                var eventDuration = reply.Duration.ToTimeSpan();
                Console.WriteLine(eventInvitation);
                Console.WriteLine(
                    $"Начало: {eventDateTime.ToString("dd.MM HH:mm")}   Длительность: {eventDuration.TotalHours} часа");
            }

            if (chanellNumber == "4")
            {
                var client = new Messenger.MessengerClient(channel);
                var customHeaders = new Metadata();
                customHeaders.Add("username", "Dmitry");
                customHeaders.Add("Authorization", $"Bearer {GetJwtToken()}");
                using var reply = client.ServerDataStream(new ServerStream.Request(), customHeaders);
                var stream = reply.ResponseStream;

                await foreach (var r in stream.ReadAllAsync())
                {
                    Console.WriteLine(r.Content);
                }

                var headers = await reply.ResponseHeadersAsync;
            }

            if (chanellNumber == "5")
            {
                string[] messages = { "Привет", "Как дела?", "Че молчишь?", "Ты че, спишь?", "Ну пока" };
                var client = new MessengerClient.MessengerClientClient(channel);
                var customHeaders = new Metadata();
                customHeaders.Add("username", "Dmitry");
                customHeaders.Add("Authorization", $"Bearer {GetJwtToken()}");
                
                using var call = client.ClientDataStream(headers: customHeaders);
                foreach (var m in messages)
                {
                    await call.RequestStream.WriteAsync(new ClientStream.Request() { Content = m });
                }

                await call.RequestStream.CompleteAsync();
                
                var response = await call.ResponseAsync;
                Console.WriteLine($"Ответ сервера: {response.Content}");
            }

            if (chanellNumber == "6")
            {
                string[] messages = { "Привет", "Как дела?", "Че молчишь?", "Ты че, спишь?", "Ну пока" };
                var client = new DuplexStream.Messenger.MessengerClient(channel);
                var customHeaders = new Metadata();
                customHeaders.Add("username", "Dmitry");
                customHeaders.Add("Authorization", $"Bearer {GetJwtToken()}");
                using var call = client.DataStream(headers: customHeaders);
                var reading = Task.Run(async () =>
                {
                    await foreach (var r in call.ResponseStream.ReadAllAsync())
                    {
                        Console.WriteLine($"Server: {r.Content}");
                    }
                });

                foreach (var m in messages)
                {
                    await call.RequestStream.WriteAsync(new DuplexStream.Request() { Content = m });
                    Console.WriteLine(m);
                }

                await Task.WhenAll(call.RequestStream.CompleteAsync(), reading);
            }

            if (chanellNumber == "7")
            {
                var client = new HeaderRpc.Messenger.MessengerClient(channel);
                var customHeaders = new Metadata();
                customHeaders.Add("username", "Dmitry");
                customHeaders.Add("Authorization", $"Bearer {GetJwtToken()}");
                using var call = client.SendMessageAsync(new HeaderRpc.Request(), customHeaders);
                
                var response = await call;
                Console.WriteLine($"Response: {response.Content}");
                
                var headers = await call.ResponseHeadersAsync;
                foreach (var h in headers)
                {
                    Console.WriteLine($"{h.Key}: {h.Value}");
                }
            }
        }
    }
    
    private class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }

    private static string GetJwtToken()
    {
        var claims = new List<Claim> {new Claim(ClaimTypes.Name, "Dmitry Myagkov") };
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}