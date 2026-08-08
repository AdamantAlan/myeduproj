using Amazon.S3;
using Amazon.S3.Model;
using S3Learn.Models;
using System;

namespace S3Learn.Services
{
    public class FileService(IAmazonS3 s3Client)
    {
        public async Task UploadFileAsync(string bucketName, string key, IFormFile file, CancellationToken cancellationToken = default)
        {
            await using var stream = file.OpenReadStream();

            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType
            };

            var response = await s3Client.PutObjectAsync(request, cancellationToken);
        }

        public Task<GetObjectResponse> GetFileAsync(string bucketName, string key, CancellationToken cancellationToken = default)
        {
            return s3Client.GetObjectAsync(new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = key
                }, 
                cancellationToken);
        }

        public async Task<GetObjectResponse> GetFileVersionAsync(string bucketName, string key, string versionId, CancellationToken cancellationToken = default)
        {
            return await s3Client.GetObjectAsync(
                new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    VersionId = versionId
                },
                cancellationToken);
        }

        public async Task<IReadOnlyList<S3FileVersion>> GetFileVersionsAsync(string bucketName, string key, CancellationToken cancellationToken = default)
        {
            var response = await s3Client.ListVersionsAsync(
                new ListVersionsRequest
                {
                    BucketName = bucketName,
                    Prefix = key
                },
                cancellationToken);

            return response.Versions
                    .Where(x => x.Key == key)
                    .OrderBy(x => x.LastModified)
                    .Select((x, index) => new S3FileVersion
                    {
                        Number = index + 1,
                        VersionId = x.VersionId,
                        LastModified = x.LastModified,
                        IsLatest = x.IsLatest ?? false
                    })
                    .OrderByDescending(x => x.Number)
                    .ToList();
        }

        public async Task DeleteFileAsync(string bucketName, string key, string versionId, CancellationToken cancellationToken = default)
        {
            await s3Client.DeleteObjectAsync(
                new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    VersionId = versionId
                },
                cancellationToken);
        }
    }
}
