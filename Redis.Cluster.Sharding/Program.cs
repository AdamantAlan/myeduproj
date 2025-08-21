using Redis.Cluster.Sharding;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var cfg = new ConfigurationOptions
    {
        AbortOnConnectFail = false,
        ConnectRetry = 5,
        ConnectTimeout = 5000,
        SyncTimeout = 5000
    };

    cfg.EndPoints.Add("localhost", 7001);
    cfg.EndPoints.Add("localhost", 7002);
    cfg.EndPoints.Add("localhost", 7003);
    cfg.EndPoints.Add("localhost", 7004);
    cfg.EndPoints.Add("localhost", 7005);
    cfg.EndPoints.Add("localhost", 7006);

    return ConnectionMultiplexer.Connect(cfg);
});

builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddSingleton<IDatabase>(sp =>
    sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Redis Claster API", Version = "v1" });
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "Redis Claster API");
        o.RoutePrefix = "swagger";
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
