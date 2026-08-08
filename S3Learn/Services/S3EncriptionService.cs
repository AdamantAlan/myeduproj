using Amazon.S3;
using Amazon.S3.Model;

namespace S3Learn.Services
{
    public class S3EncriptionService(IAmazonS3 s3Client)
    {
        public Task<GetBucketEncryptionResponse> GetEncryptionAsync(string bucketName)
        {
            return s3Client.GetBucketEncryptionAsync(
                new GetBucketEncryptionRequest
                {
                    BucketName = bucketName
                });
        }

        public async Task<GetBucketEncryptionResponse> ChangeEncryptionAsync(string bucketName)
        {
            var currentEncryption = await s3Client.GetBucketEncryptionAsync(
                new GetBucketEncryptionRequest
                {
                    BucketName = bucketName
                });

            if(currentEncryption.ServerSideEncryptionConfiguration is null)
            {
                await EnableEncryptionAsync(bucketName);
            }
            else
            {
                await DisableEncryptionAsync(bucketName);
            }

            return await s3Client.GetBucketEncryptionAsync(
                new GetBucketEncryptionRequest
                {
                    BucketName = bucketName
                });
        }

        private async Task EnableEncryptionAsync(string bucketName)
        {
            var request = new PutBucketEncryptionRequest
            {
                BucketName = bucketName,
                ServerSideEncryptionConfiguration =
                    new ServerSideEncryptionConfiguration
                    {
                        ServerSideEncryptionRules =
                        [
                            new ServerSideEncryptionRule
                    {
                        ServerSideEncryptionByDefault =
                            new ServerSideEncryptionByDefault
                            {
                                ServerSideEncryptionAlgorithm =
                                    ServerSideEncryptionMethod.AES256
                            }
                    }
                        ]
                    }
            };

            await s3Client.PutBucketEncryptionAsync(request);
        }

        private async Task DisableEncryptionAsync(string bucketName)
        {
            await s3Client.DeleteBucketEncryptionAsync(
                new DeleteBucketEncryptionRequest
                {
                    BucketName = bucketName
                });
        }
    }
}
