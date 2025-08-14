using Npgsql;
using Pg.Easy.Replication.Context;
using Pg.Easy.Replication.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyedSingleton<NpgsqlDataSource>("Primary", (sp,dp) =>
{
    var cs = builder.Configuration.GetConnectionString("Primary")!;
    return NpgsqlDataSource.Create(cs);
});

builder.Services.AddKeyedSingleton<NpgsqlDataSource>("Replica", (sp, dp) =>
{
    var cs = builder.Configuration.GetConnectionString("Replica")!;
    return NpgsqlDataSource.Create(cs);
});

builder.Services.AddSingleton<IDataSourceSelector, DataSourceSelector>();

builder.Services.AddDbContextFactory<AppDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Pg Easy Replication API", Version = "v1" });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "Pg Easy Replication API v1");
        o.RoutePrefix = "swagger";
    });
}

app.UseMiddleware<ReadWriteIntentMiddleware>();

app.MapControllers();

app.Run();
