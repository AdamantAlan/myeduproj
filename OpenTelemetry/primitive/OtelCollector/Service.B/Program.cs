using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Activity.DefaultIdFormat = ActivityIdFormat.W3C;
var resource = ResourceBuilder.CreateDefault().AddService("srvice_b");
var metter = new Meter("b_metter");
var counter = metter.CreateCounter<long>("b_counter");
var gauge = metter.CreateGauge<long>("b_gauge");
var histogram = metter.CreateHistogram<long>("b_hist");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddOpenTelemetry()
    .WithTracing(builder =>
    {
        builder.AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddGrpcClientInstrumentation()
        .AddConsoleExporter()
        .AddOtlpExporter();
    })
    .WithLogging(builder =>
    {
        builder.AddConsoleExporter();
    })
    .WithMetrics(builder =>
    {
        builder.SetResourceBuilder(resource);
        builder.AddConsoleExporter();
        builder.AddMeter(metter.Name);
        builder.AddRuntimeInstrumentation();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", async (context) =>
{
    counter.Add(1);
    gauge.Record(150);
    histogram.Record(50);
    await context.Response.WriteAsync("HELLO!");
});


app.MapControllers();

app.Run();
