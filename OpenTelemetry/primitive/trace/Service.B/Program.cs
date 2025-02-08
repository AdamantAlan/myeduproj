using OpenTelemetry;
using OpenTelemetry.Trace;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Activity.DefaultIdFormat = ActivityIdFormat.W3C;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();


builder.Services.AddOpenTelemetry()
        //Подключает все
       //.UseOtlpExporter(OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf, new("http://localhost:4318"))
    .WithTracing(builder =>
    {
        builder.AddAspNetCoreInstrumentation();
        builder.AddHttpClientInstrumentation();
        builder.AddConsoleExporter();

        builder.AddOtlpExporter("OLTP_TRACES", oltp =>
        {
            oltp.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
            oltp.Endpoint = new("http://localhost:4318/v1/traces");
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
