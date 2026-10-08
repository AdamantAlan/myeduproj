using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Application.Attributes;
using XDE.DocumentReportService.Domain;

namespace XDE.DocumentReportService.Application.Services;

/// <summary>
/// Представляет генератор печатных форм документов.
/// </summary>
[DocumentType("ORDER")]
internal sealed class PrintFormGenerator : IPrintFormGenerator
{
    /// <summary>
    /// Генерирует печатную форму документа.
    /// </summary>
    /// <param name="document">
    /// Документ, для которого нужно сгенерировать печатную форму.
    /// </param>
    /// <returns>
    /// Печатная форма документа (PDF, сериализованный в массив байт).
    /// </returns>
    public byte[] GeneratePrintForm(Document document)
    {
        // тестовая реализация, просто вернем массив байт нулевой длины
        return new byte[0];
    }
}

