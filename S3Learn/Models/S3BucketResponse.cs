namespace S3Learn.Settings
{
    public class S3BucketResponse
    {
        public string BucketName { get; init; }

        public DateTime? CreationDate { get; init; }

        public string Region { get; init; }

        public IReadOnlyDictionary<string, string> Tags { get; init; } = new Dictionary<string, string>();
    }
}
