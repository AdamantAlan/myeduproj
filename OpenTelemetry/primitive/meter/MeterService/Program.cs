using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOpenTelemetry()
    .WithMetrics(builder =>
    {
        builder.AddAspNetCoreInstrumentation();
        builder.AddRuntimeInstrumentation();
        builder.AddHttpClientInstrumentation();
        builder.AddConsoleExporter();
        //builder.AddPrometheusExporter();
        builder.AddOtlpExporter();
    });

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

app.UseAuthorization();

//app.MapPrometheusScrapingEndpoint();
app.MapControllers();

app.Run();
