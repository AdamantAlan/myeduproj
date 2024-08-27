using Camunda.Worker;
using Camunda.Worker.Client;
using NLog.Web;

namespace CamundaLearn;

public class Program
{
    public static void Main(string[] args)
    {
        var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
        logger.Debug("init main");
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();
                
                
            builder.Services.AddHttpClient<CamundaClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8080");
            });
            
            builder.Services.AddExternalTaskClient(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8080/engine-rest");
            });

            builder.Services.AddCamundaWorker("sampleWorker")
                .AddHandler<MyTaskHandler>()
                .ConfigurePipeline(pipeline =>
                {
                    pipeline.Use(next => async context =>
                    {
                        var logger = context.ServiceProvider.GetRequiredService<ILogger<Program>>();
                        logger.LogInformation("Started processing of task {Id}", context.Task.Id);
                        await next(context);
                        logger.LogInformation("Finished processing of task {Id}", context.Task.Id);
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
        }
        catch (Exception e)
        {
            logger.Error(e, "Stopped program because of exception");
            throw;
        }
        finally
        {
            NLog.LogManager.Shutdown();
        }
    }
}