using Amazon.S3.Model;
using Amazon.S3;

namespace S3Learn.Services
{
    public class LyfecycleService(IAmazonS3 s3Client)
    {
        public Task EnableLifeCycleAsync(string bucketName, string lifecycleRuleId, CancellationToken cancellationToken = default)
        {
            return s3Client.PutLifecycleConfigurationAsync(
                new PutLifecycleConfigurationRequest
                {
                    BucketName = bucketName,

                    Configuration = new LifecycleConfiguration
                    {
                        Rules =
                        [
                            new LifecycleRule
                           {
                               Id = lifecycleRuleId,

                               Status = LifecycleRuleStatus.Enabled,

                               Filter = new LifecycleFilter
                               {
                                   LifecycleFilterPredicate =
                                       new LifecyclePrefixPredicate
                                       {
                                           Prefix = "temp/"
                                       },
                                   Tag = new Tag
                                       {
                                           Key = "temporary",
                                           Value = "true"
                                       }
                               },

                               Expiration = new LifecycleRuleExpiration
                               {
                                   Days = 30
                               },
                               NoncurrentVersionExpiration =
                               new LifecycleRuleNoncurrentVersionExpiration
                               {
                                   NoncurrentDays = 365
                               },
                               AbortIncompleteMultipartUpload =
                               new LifecycleRuleAbortIncompleteMultipartUpload
                               {
                                   DaysAfterInitiation = 7
                               }
                           }
                        ]
                    }
                }, 
                cancellationToken);
        }

        public Task GetLifeCycleAsync(string bucketName, CancellationToken cancellationToken = default)
        {
            return s3Client.GetLifecycleConfigurationAsync(
                new GetLifecycleConfigurationRequest
                {
                    BucketName = bucketName
                },
                cancellationToken);
        }

        public Task DeleteLifeCycleAsync(string bucketName, CancellationToken cancellationToken = default)
        {
            return s3Client.DeleteLifecycleConfigurationAsync(
                new DeleteLifecycleConfigurationRequest
                {
                    BucketName = bucketName
                },
                cancellationToken);
        }
    }
}
