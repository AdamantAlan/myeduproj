using Microsoft.OpenApi;
using XDE.DocumentReportService.Application.Handlers;
using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Application.Services;
using XDE.DocumentReportService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFormGenerationHandler, FormGenerationHandler>();

builder.Services.AddSingleton<PrintFormGenerator>();
builder.Services.AddSingleton<InvoicePrintFormGenerator>();
builder.Services.AddSingleton<PrintFormGeneratorContext>();

builder.Services.AddSingleton<ExternalSystemConnector>();
builder.Services.AddSingleton<IProgress<DocumentQueueProgress>>(_ => 
    new Progress<DocumentQueueProgress>(progress =>
    {
        Console.WriteLine($"Sent: {progress.SentCount}, Pending: {progress.PendingCount}, Message: {progress.Message}");
    }));

builder.Services.AddSingleton<IDocumentsQueue>(sp => 
        new DocumentQueue(sp.GetRequiredService<ExternalSystemConnector>(),
        sp.GetRequiredService<IProgress<DocumentQueueProgress>>(),
        TimeSpan.FromSeconds(5)));

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