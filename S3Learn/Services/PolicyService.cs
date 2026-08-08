using Amazon.S3;
using Amazon.S3.Model;
using System.Text.Json;

namespace S3Learn.Services
{
    public class PolicyService(IAmazonS3 s3Client)
    {
        public async Task SetPublicReadPolicyAsync(string bucketName)
        {
            var policy = new
            {
                Version = "2012-10-17",

                Statement = new[]
                    {
                    new
                    {
                        Sid = "PublicRead",

                        Effect = "Allow",

                        Principal = "*",

                        Action = new string[] { "s3:GetObject", "s3:PutObject" },

                        Resource =
                            $"arn:aws:s3:::{bucketName}/*"
                    },
                    new
                    {
                        Sid = "NotDelete",

                        Effect = "Deny",

                        Principal = "*",

                        Action = new string[] { "s3:DeleteObject" },

                        Resource =
                            $"arn:aws:s3:::{bucketName}/*"
                    }
                }
            };

            var json = JsonSerializer.Serialize(policy);

            await s3Client.PutBucketPolicyAsync(
                new PutBucketPolicyRequest
                {
                    BucketName = bucketName,
                    Policy = json
                });
        }

        public async Task<string> GetPolicyAsync(string bucketName)
        {
            var response =
                await s3Client.GetBucketPolicyAsync(
                    new GetBucketPolicyRequest
                    {
                        BucketName = bucketName
                    });

            return response.Policy;
        }

        public async Task DeletePolicyAsync(string bucketName)
        {
            await s3Client.DeleteBucketPolicyAsync(
                new DeleteBucketPolicyRequest
                {
                    BucketName = bucketName
                });
        }

    }
}
