using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5054/stream")
    .AddMessagePackProtocol()
    .Build();

await connection.StartAsync();
Console.WriteLine("Подключено! Получаем поток данных...\n");

var stream = connection.StreamAsync<int>("Counter", 10, 500);

await foreach (var number in stream)
{
    Console.WriteLine($"Получено: {number}");
}

Console.WriteLine("Поток завершён.");
await connection.DisposeAsync();

Console.ReadKey();