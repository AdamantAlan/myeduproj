
using Microsoft.Extensions.DependencyInjection;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;
using System.Net;

namespace RedisStackLearn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(new ConfigurationOptions() 
                {
                    EndPoints = { "localhost:6379" },
                });
            });

            //Äëÿ RedLock
            builder.Services.AddSingleton(RedLockFactory.Create(new List<RedLockEndPoint>
            {       
                new RedLockEndPoint
                {
                    EndPoint = new DnsEndPoint("localhost", 6379)
                }
             }));

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
