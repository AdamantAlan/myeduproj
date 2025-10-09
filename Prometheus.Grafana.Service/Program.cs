using Prometheus;
using Prometheus.Grafana.Edu;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMetricsService, MetricsService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpMetrics();
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapMetrics();
app.MapControllers();

app.Run();
