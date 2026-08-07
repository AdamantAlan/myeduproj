using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using S3Learn.Settings;
using System.Net;

namespace S3Learn.Services
{
    public class BucketService(IAmazonS3 s3Client)
    {
        public async Task<IReadOnlyList<S3BucketResponse>> GetAllBucketsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var buckets = (await s3Client.ListBucketsAsync(cancellationToken)).Buckets;

                if (buckets is null) throw new Exception("Бакетов нет");
                var bucketResponses = new List<S3BucketResponse>();

                foreach (var bucket in buckets)
                {
                    try
                    {
                        var taggingResponse = await s3Client.GetBucketTaggingAsync(new() { BucketName = bucket.BucketName }, cancellationToken);

                        bucketResponses.Add(new S3BucketResponse()
                        {
                            BucketName = bucket.BucketName,
                            CreationDate = bucket.CreationDate,
                            Region = bucket.BucketRegion, 
                            Tags = taggingResponse.TagSet.ToDictionary(x => x.Key,x => x.Value)
                        });
                    }
                    catch (AmazonS3Exception ex) when (ex.ErrorCode == "NoSuchTagSet")
                    {
                        bucketResponses.Add(new S3BucketResponse()
                        {
                            BucketName = bucket.BucketName,
                            CreationDate = bucket.CreationDate,
                            Region = bucket.BucketRegion,
                            Tags = null!
                        });
                    }
                }

                return bucketResponses;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default)
        {
            await ValidateBucketNotExists(bucketName);

            try
            {
                var createBucketRequest = new PutBucketRequest { BucketName = bucketName };
                var createBucketResponse = await s3Client.PutBucketAsync(createBucketRequest, cancellationToken);

                if(createBucketResponse.HttpStatusCode is not HttpStatusCode.OK)
                {
                    throw new Exception($"Не удалось создать бакет {bucketName}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default)
        {
            await ValidateBucketExists(bucketName);

            try
            {
                var createBucketRequest = new DeleteBucketRequest { BucketName = bucketName };
                var createBucketResponse = await s3Client.DeleteBucketAsync(createBucketRequest, cancellationToken);

                if (createBucketResponse.HttpStatusCode is not HttpStatusCode.NoContent)
                {
                    throw new Exception($"Не удалось удалить бакет {bucketName}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task SetBucketTagsAsync(string bucketName, Dictionary<string, string> tags, CancellationToken cancellationToken = default)
        {
            await ValidateBucketExists(bucketName);
            
            try
            {
                ArgumentNullException.ThrowIfNull(tags);

                var addedTagsResponse = await s3Client.PutBucketTaggingAsync(
                new PutBucketTaggingRequest
                {
                    BucketName = bucketName,
                    TagSet = tags.Select(t => new Tag { Key = t.Key, Value = t.Value }).ToList()
                },
                cancellationToken);

                if (addedTagsResponse.HttpStatusCode is not HttpStatusCode.OK)
                {
                    throw new Exception($"Не удалось добавить тэги к бакету {bucketName}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private async Task<bool> ValidateBucketNotExists(string bucketName)
        {
            return await IsBucketExistAsync(bucketName) 
                ? throw new Exception($"Бакет {bucketName} уже существует")
                : false;
        }

        private async Task<bool> ValidateBucketExists(string bucketName)
        {
            return await IsBucketExistAsync(bucketName)
                ? true
                : throw new Exception($"Бакет {bucketName} не существует");
        }

        private Task<bool> IsBucketExistAsync(string bucketName)
        {
            try
            {
                return AmazonS3Util.DoesS3BucketExistV2Async(s3Client, bucketName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
