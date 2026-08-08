using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using S3Learn.Services;
using S3Learn.Settings;

namespace S3Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketsController(BucketService bucketService, S3EncriptionService encriptionService, PolicyService policyService) : ControllerBase
    {
        [HttpGet]
        public Task<IReadOnlyList<S3BucketResponse>> GetAll(CancellationToken cancellationToken)
        {
            return bucketService.GetAllBucketsAsync(cancellationToken);
        }

        [HttpPost]
        public Task Create(string bucketName, CancellationToken cancellationToken)
        {
            return bucketService.CreateBucketAsync(bucketName, cancellationToken);
        }

        [HttpPut("tags")]
        public Task AddTagsAll(string bucketName, Dictionary<string, string> tags, CancellationToken cancellationToken)
        {
            return bucketService.SetBucketTagsAsync(bucketName, tags, cancellationToken);
        }

        [HttpPut("versioning")]
        public Task ChangeVersioning(string bucketName, CancellationToken cancellationToken)
        {
            return bucketService.ChangeVersioningAsync(bucketName, cancellationToken);
        }

        [HttpDelete]
        public Task Delete(string bucketName, CancellationToken cancellationToken)
        {
            return bucketService.DeleteBucketAsync(bucketName, cancellationToken);
        }

        [HttpGet("{bucketName}/encryption")]
        public Task<GetBucketEncryptionResponse> GetEncription(string bucketName, CancellationToken cancellationToken)
        {
            return encriptionService.GetEncryptionAsync(bucketName);
        }

        [HttpPut("{bucketName}/encryption")]
        public Task<GetBucketEncryptionResponse> ChangeEncription(string bucketName, CancellationToken cancellationToken)
        {
            return encriptionService.ChangeEncryptionAsync(bucketName);
        }

        [HttpGet("{bucketName}/policy")]
        public Task<string> GetPolicies(string bucketName, CancellationToken cancellationToken)
        {
            return policyService.GetPolicyAsync(bucketName);
        }

        [HttpPost("{bucketName}/policy")]
        public Task CreatePolicy(string bucketName, CancellationToken cancellationToken)
        {
            return policyService.SetPublicReadPolicyAsync(bucketName);
        }

        [HttpDelete("{bucketName}/policy")]
        public Task DeletePolicy(string bucketName, CancellationToken cancellationToken)
        {
            return policyService.DeletePolicyAsync(bucketName);
        }
    }
}
