using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

var app = WebApplication.CreateBuilder(args).Build();

var rand = new Random();
float cpuLoad = rand.NextSingle() * 50f;      // от 0 до 50%
uint requestCount = (uint)rand.Next(1000, 2000);
double deltaValue = rand.NextDouble() * 5;

// Эндпоинт /metrics
app.MapGet("/metrics", () =>
{
    // имитация изменения метрик
    cpuLoad = Math.Clamp(cpuLoad + (rand.NextSingle() - 0.5f) * 5f, 0f, 100f);     
    requestCount += (uint)rand.Next(1, 50);                                       
    deltaValue = rand.NextDouble() * 10;                                          

    var metrics = new
    {
        cpu_load = Math.Round(cpuLoad, 2),
        requests = requestCount,
        delta = Math.Round(deltaValue, 3)
    };

    app.Logger.LogInformation(
         $"Returned metrics: {JsonSerializer.Serialize(metrics)}");

    return Results.Json(metrics);
});

app.Run("http://0.0.0.0:5000");