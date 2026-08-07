using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3Learn.Services;
using S3Learn.Settings;

namespace S3Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketController(BucketService s3Service) : ControllerBase
    {
        [HttpGet]
        public Task<IReadOnlyList<S3BucketResponse>> GetAll(CancellationToken cancellationToken)
        {
            return s3Service.GetAllBucketsAsync(cancellationToken);
        }

        [HttpPost]
        public Task Create(string bucketName, CancellationToken cancellationToken)
        {
            return s3Service.CreateBucketAsync(bucketName, cancellationToken);
        }

        [HttpPut("tags")]
        public Task AddTagsAll(string bucketName, Dictionary<string, string> tags, CancellationToken cancellationToken)
        {
            return s3Service.SetBucketTagsAsync(bucketName, tags, cancellationToken);
        }

        [HttpDelete]
        public Task Delete(string bucketName, CancellationToken cancellationToken)
        {
            return s3Service.DeleteBucketAsync(bucketName, cancellationToken);
        }
    }
}
