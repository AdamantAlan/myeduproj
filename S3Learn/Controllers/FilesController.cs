using Microsoft.AspNetCore.Mvc;
using S3Learn.Models;
using S3Learn.Services;
using S3Learn.Settings;

namespace S3Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController(FileService fileService) : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetFile(string bucketName, string key, CancellationToken cancellationToken)
        {
            var response = await fileService.GetFileAsync(
                bucketName,
                key,
                cancellationToken);

            return File(
                response.ResponseStream,
                response.Headers.ContentType ?? "application/octet-stream",
                Path.GetFileName(key));
        }

        [HttpGet("versions")]
        public Task<IReadOnlyList<S3FileVersion>> GetVersions(string bucketName, string key, CancellationToken cancellationToken)
        {
           return fileService.GetFileVersionsAsync(bucketName, key, cancellationToken);
        }

        [HttpGet("version")]
        public async Task<IActionResult> GetFile(string bucketName, string key, string? versionId, CancellationToken cancellationToken)
        {
            var response = await fileService.GetFileVersionAsync(
                bucketName,
                key,
                versionId,
                cancellationToken);

            return File(
                response.ResponseStream,
                response.Headers.ContentType ?? "application/octet-stream",
                Path.GetFileName(key));
        }

        [HttpPost]
        public Task Create(string bucketName, IFormFile file, CancellationToken cancellationToken)
        {
            return fileService.UploadFileAsync(bucketName, file.FileName, file, cancellationToken);
        }

        [HttpDelete]
        public Task Delete(string bucketName, string key, string? versionId, CancellationToken cancellationToken)
        {
            return fileService.DeleteFileAsync(bucketName, key, versionId!, cancellationToken);
        }

        [HttpGet("temp-url")]
        public IActionResult GetTempUrl(string bucketName, string key, TimeSpan lifeTime , CancellationToken cancellationToken)
        {
            return Ok(fileService.GetDownloadUrl(bucketName, key, lifeTime));
        }
    }
}
