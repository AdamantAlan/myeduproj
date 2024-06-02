using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace ElasticSearchLearn;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
            .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
            .Authentication(new BasicAuthentication("admin", "erfytftgjdghj5645FSD.G$"));

        var client = new ElasticsearchClient(settings);
        builder.Services.AddSingleton(client);
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


        app.MapControllers();

        app.Run();
    }
}