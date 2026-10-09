using Microsoft.OpenApi;
using XDE.DocumentReportService.Application.Handlers;
using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Application.Services;
using XDE.DocumentReportService.Infrastructure;
using XDE.DocumentReportService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISendExternalSystemHandler, SendExternalSystemHandler>();

builder.Services.AddSingleton<PrintFormGenerator>();
builder.Services.AddSingleton<InvoicePrintFormGenerator>();
builder.Services.AddSingleton<PrintFormGeneratorContext>();
builder.Services.AddSingleton<ExternalSystemConnector>();

builder.Services.AddDocumentQueue();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "XDE", Version = "v1" });
});


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "XDE v1");
});

app.MapControllers();

app.Run();