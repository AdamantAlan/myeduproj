using System.Net;
using OpenSearch.Client;
using OpenSearch.Net;

namespace OpenSearchLearn;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        var config = new ConnectionSettings(new Uri("https://localhost:9200"));
        config.ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) =>
        {
            return true;
        }).BasicAuthentication("admin", "myStrongPassword#$%dsf_asdf0");
        builder.Services.AddSingleton(new OpenSearchClient(config));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.MapControllers();
        app.Run();
    }
}