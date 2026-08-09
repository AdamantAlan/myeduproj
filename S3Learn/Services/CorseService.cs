using Amazon.S3;
using Amazon.S3.Model;

namespace S3Learn.Services
{
    public class CorseService(IAmazonS3 s3Client)
    {
        public async Task SetCorsAsync(string bucketName, CancellationToken cancellationToken)
        {
            var request = new PutCORSConfigurationRequest
            {
                BucketName = bucketName,

                Configuration = new CORSConfiguration
                {
                    Rules =
                    [
                new CORSRule
                {
                    Id = "frontend",

                    AllowedOrigins =
                    [
                            "http://localhost:5173",
                            "https://app.example.com"
                    ],

                    AllowedMethods =
                    [
                        "GET",
                        "PUT",
                        "HEAD"
                    ],

                    AllowedHeaders =
                    [
                        "*"
                    ],

                    ExposeHeaders =
                    [
                        "ETag"
                    ],

                    MaxAgeSeconds = 3600
                }
                    ]
                }
            };

            await s3Client.PutCORSConfigurationAsync(request, cancellationToken);
        }

        public async Task<CORSConfiguration> GetCorsAsync(string bucketName, CancellationToken cancellationToken)
        {
            var response =
                await s3Client.GetCORSConfigurationAsync(
                    new GetCORSConfigurationRequest
                    {
                        BucketName = bucketName
                    }, cancellationToken);

            return response.Configuration;
        }

        public Task DeleteCorsAsync(string bucketName, CancellationToken cancellationToken)
        {
            return s3Client.DeleteCORSConfigurationAsync(
                new DeleteCORSConfigurationRequest
                {
                    BucketName = bucketName
                }, cancellationToken);
        }
    }
}
