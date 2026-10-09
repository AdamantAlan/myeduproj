using XDE.DocumentReportService.Domain;

namespace XDE.DocumentReportService.Application.Abstractions;

/// <summary>
/// Представляет генератор печатных форм документов.
/// </summary>
public interface IPrintFormGenerator
{
    /// <summary>
    /// Генерирует печатную форму документа.
    /// </summary>
    /// <param name="document">
    /// Документ, для которого нужно сгенерировать печатную форму.
    /// </param>
    /// <returns>
    /// Печатная форма документа (PDF, сериализованный в массив байт).
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Возникает в том случае, если эта реализация не умеет генерировать
    /// печатную форму для переданного типа документов.
    /// </exception>
    byte[] GeneratePrintForm(Document document);
}