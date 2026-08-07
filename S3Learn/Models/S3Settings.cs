namespace S3Learn.Settings
{
    public class S3Settings
    {
        public const string SectionName = "S3";

        public string Address { get; init; } = null!;

        public string AccessKey { get; init; } = null!;

        public string PrivateKey { get; init; } = null!;

        public string Region { get; init; } = null!;
    }
}
