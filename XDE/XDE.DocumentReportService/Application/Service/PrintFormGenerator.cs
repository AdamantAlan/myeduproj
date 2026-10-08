using XDE.DocumentReportService.Domain;

namespace XDE.DocumentReportService.Application.Service;

/// <summary>
/// Представляет генератор печатных форм документов.
/// </summary>
public sealed class PrintFormGenerator
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

