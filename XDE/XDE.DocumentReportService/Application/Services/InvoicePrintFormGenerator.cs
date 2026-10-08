using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Application.Attributes;
using XDE.DocumentReportService.Domain;

namespace XDE.DocumentReportService.Application.Services;

[DocumentType("INVOICE")]
internal sealed class InvoicePrintFormGenerator : IPrintFormGenerator
{
    public byte[] GeneratePrintForm(Document document)
    {
        return new byte[0];
    }
}

