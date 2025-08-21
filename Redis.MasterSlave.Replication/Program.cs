using Redis.MasterSlave.Replication;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// endpoints: можно указать мастер (и/или несколько нод Ч клиент сам разберЄтс€)
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var cfg = ConfigurationOptions.Parse("localhost:6379,abortConnect=false");
    cfg.ConnectRetry = 5;
    cfg.ConnectTimeout = 5000;        // таймаут соединени€
    cfg.SyncTimeout = 5000;           // таймаут команд
    cfg.ReconnectRetryPolicy = new ExponentialRetry(5000);
    return ConnectionMultiplexer.Connect(cfg);
});

builder.Services.AddSingleton<IDatabase>(sp =>
    sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Redis MasterSlave Replication API", Version = "v1" });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "Redis MasterSlave Replication API");
        o.RoutePrefix = "swagger";
    });
}

app.MapControllers();

app.Run();