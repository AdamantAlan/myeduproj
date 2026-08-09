using Amazon.S3;
using Amazon.S3.Model;

namespace S3Learn.Services
{
    public class MultipartUploadService(IAmazonS3 s3Client)
    {
        public async Task<string> InitiateAsync(string bucketNAme, string key, CancellationToken cancellationToken)
        {
            var initiate = await s3Client.InitiateMultipartUploadAsync(
                new InitiateMultipartUploadRequest
                {
                    BucketName = bucketNAme,
                    Key = key
                });

            return initiate.UploadId;
        }

        public Task<UploadPartResponse> UploadPartAsync(string bucketName, string key, 
            string uploadId, 
            int partNumber, 
            long filePosition, 
            long partSize, 
            CancellationToken cancellationToken)
        {
             return s3Client.UploadPartAsync(
                new UploadPartRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    UploadId = uploadId,
                    PartNumber = partNumber,
                    FilePosition = filePosition,
                    PartSize = partSize
                }, cancellationToken);
        }
 
    public Task<CompleteMultipartUploadResponse> CompleteMultipartUploadAsync(string bucketName, string key,
            string uploadId,
            List<PartETag> partETags,
            CancellationToken cancellationToken)
        {
            return s3Client.CompleteMultipartUploadAsync(
                new CompleteMultipartUploadRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    UploadId = uploadId,
                    PartETags = partETags
                }, cancellationToken);
        }

        public Task<AbortMultipartUploadResponse> AbortMultipartUploadAsync(string bucketName, string key,
            string uploadId,
            CancellationToken cancellationToken)
        {
            return s3Client.AbortMultipartUploadAsync(
                new AbortMultipartUploadRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    UploadId = uploadId
                }, cancellationToken);
        }
    }
}