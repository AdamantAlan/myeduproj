namespace XDE.DocumentReportService.Application.Dtos
{
    public sealed class GenerateFormsResponse
    {
        public int PackageId { get; set; }

        public IEnumerable<int> DocumentIds { get; set; } = [];
    }
}
