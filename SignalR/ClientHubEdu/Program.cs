using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5054/chat")
    .WithAutomaticReconnect(new[] { TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5) })
    .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Information))
    .Build();

connection.On<string, string>("Receive", 
    (user, message) => 
    {
        var newMessage = $"{user}: {message}";
        Console.WriteLine(newMessage);
    });

await connection.StartAsync();

Console.WriteLine("Вы вошли в чат");
Console.WriteLine("Введите логин");
var login = Console.ReadLine();

while (true)
{
    Console.WriteLine("Введите сообщение");
    var message = Console.ReadLine();

    await connection.InvokeAsync("Send", login, message);
}

//Закроется по таймауту(1м)
await connection.StopAsync();