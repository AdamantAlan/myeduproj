using Microsoft.OpenApi;
using XDE.DocumentReportService.Application.Handlers;
using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IFormGenerationHandler, FormGenerationHandler>();

builder.Services.AddSingleton<PrintFormGenerator>();
builder.Services.AddSingleton<InvoicePrintFormGenerator>();
builder.Services.AddSingleton<PrintFormGeneratorContext>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "XDE", Version = "v1" });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    // 3. ”казываем, где брать JSON-спецификацию
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "XDE v1");
});

app.MapControllers();

app.Run();