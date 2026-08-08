using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Options;
using S3Learn.Services;
using S3Learn.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .AddOptions<S3Settings>()
    .Bind(builder.Configuration.GetSection(S3Settings.SectionName))
    .ValidateOnStart();

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<S3Settings>>().Value;

    var credentials = new BasicAWSCredentials(settings.AccessKey, settings.PrivateKey);

    var config = new AmazonS3Config
    {
        ServiceURL = settings.Address,
        AuthenticationRegion = settings.Region,
        ForcePathStyle = true
    };

    return new AmazonS3Client(credentials, config);
});

builder.Services.AddScoped<BucketService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
