using CoreWCF;
using CoreWCF.Configuration;
using SoapWcfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<MathService>();
    serviceBuilder.AddServiceEndpoint<MathService, IMathService>(
        new BasicHttpBinding(), "/MathService.svc"
    );
});

var metadata = app.Services.GetRequiredService<CoreWCF.Description.ServiceMetadataBehavior>();
metadata.HttpGetEnabled = true;

app.Run();