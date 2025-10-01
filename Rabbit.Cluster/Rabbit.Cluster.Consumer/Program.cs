using EasyNetQ;
using Rabbit.Cluster.Consumer;
using Rabbit.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<ConsumerWorker>();
builder.Services.AddEasyNetQ("host=localhost:5672,host=localhost:5673,host=localhost:5674;username=app;password=secret;timeout=60").UseSystemTextJson();

builder.Services.AddSingleton<ITypeNameSerializer, SimpleTypeNameSerializer>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
