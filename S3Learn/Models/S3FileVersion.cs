namespace S3Learn.Models
{
    public class S3FileVersion
    {
      public long Number { get; init; }
      public string VersionId { get; init; }
      public DateTime? LastModified { get; init; }
      public bool IsLatest { get; init; }
    }
}
