namespace XDE.DocumentReportService.Application.Attributes;

/// <summary>
/// Атрибут для указания типа документа.
/// </summary>
/// <param name="documentType">Тип документа.</param>
[AttributeUsage(AttributeTargets.Class)]
internal sealed class DocumentTypeAttribute(string documentType) : Attribute
{
    public string DocumentType { get; } = documentType;
}

