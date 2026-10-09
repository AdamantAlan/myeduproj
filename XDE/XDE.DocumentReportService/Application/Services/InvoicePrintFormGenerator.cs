using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Application.Attributes;
using XDE.DocumentReportService.Domain;

namespace XDE.DocumentReportService.Application.Services;

/// <summary>
/// Генератор форм для документа с типом INVOICE
/// </summary>
[DocumentType("INVOICE")]
internal sealed class InvoicePrintFormGenerator : IPrintFormGenerator
{
    public byte[] GeneratePrintForm(Document document)
    {
        return new byte[0];
    }
}

