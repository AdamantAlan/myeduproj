using Confluent.Kafka;
using QA.Order.App;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddScoped<IRefundService, RefundService>();
builder.Services.AddSingleton<IProducer<string, string>>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    var bootstrapServers =
        configuration["Kafka:BootstrapServers"]
        ?? throw new InvalidOperationException(
            "Kafka:BootstrapServers is not configured.");

    var config = new ProducerConfig
    {
        BootstrapServers = bootstrapServers
    };

    return new ProducerBuilder<string, string>(config)
        .Build();
});

builder.Services.AddSingleton<IOrderEventProducer, OrderEventProducer>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }