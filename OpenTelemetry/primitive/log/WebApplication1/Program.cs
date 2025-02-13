using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.OpenTelemetry(otel =>
    {
        otel.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.HttpProtobuf;
        otel.Endpoint = new("http://localhost:5341/ingest/otlp/v1/logs");
        otel.Headers = new Dictionary<string, string>()
        {
            ["X-Seq-ApiKey"] = "2iktIb01qDwoKOQzIHvH"
        };
        otel.ResourceAttributes = new Dictionary<string, object>()
        {
            ["env"] = builder.Environment.EnvironmentName,
            ["app"] = builder.Environment.ApplicationName
        };
    })
    .CreateLogger();

builder.Services.AddSerilog();
//builder.Logging.ClearProviders();
//builder.Logging.AddOpenTelemetry(otel =>
//{
//    otel.SetResourceBuilder(ResourceBuilder.CreateEmpty()
//        .AddService("WebApp1")
//        .AddAttributes(new Dictionary<string, object>
//        {
//            ["env"] = builder.Environment.EnvironmentName,
//            ["app"] = builder.Environment.ApplicationName
//        }));
//    otel.IncludeScopes = true;
//    otel.IncludeFormattedMessage = true;
//    otel.AddOtlpExporter(otlp =>
//    {
//        otlp.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
//        otlp.Endpoint = new("http://localhost:5341/ingest/otlp/v1/logs");
//        otlp.Headers = "X-Seq-ApiKey=2iktIb01qDwoKOQzIHvH";
//    });
//});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
