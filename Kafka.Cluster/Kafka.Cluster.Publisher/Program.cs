using Kafka.Cluster.Publisher.Properties;

namespace Kafka.Cluster.Publisher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));
            builder.Services.AddSingleton<ClaimsProducer>();
            builder.Services.AddSingleton<PolicyProducer>();
            builder.Services.AddSingleton<PaymentsProducer>();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
