using Kafka.Cluster.Consumer.Properties;

namespace Kafka.Cluster.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));
            builder.Services.AddHostedService<ClaimsConsumer>();
            builder.Services.AddHostedService<PolicyConsumer>();
            builder.Services.AddHostedService<PaymentsConsumer>();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
