namespace XDE.DocumentReportService.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class DocumentTypeAttribute(string documentType) : Attribute
    {
        public string DocumentType { get; } = documentType;
    }
}
