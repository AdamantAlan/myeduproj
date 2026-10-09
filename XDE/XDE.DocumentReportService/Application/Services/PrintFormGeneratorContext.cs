using System.Reflection;
using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Application.Attributes;

namespace XDE.DocumentReportService.Application.Services;

/// <summary>
/// Контекст генераторов печатных форм.
/// Обеспечивает поиск генератора по типу документа.
/// </summary>
/// <remarks>
/// Соответствие между типом документа и генератором определяется
/// атрибутом <see cref="DocumentTypeAttribute"/>,
/// указанным на классе реализации <see cref="IPrintFormGenerator"/>.
/// </remarks>
internal sealed class PrintFormGeneratorContext
{
    private readonly Dictionary<string, IPrintFormGenerator> generators;

    public PrintFormGeneratorContext(IEnumerable<IPrintFormGenerator> formGenerators)
    {
        generators = formGenerators.ToDictionary(generator =>
            generator.GetType().GetCustomAttribute<DocumentTypeAttribute>()?
            .DocumentType ?? throw new InvalidOperationException("Generator has no document type."),
            generator => generator);
    }

    /// <summary>
    /// Возвращает генератор печатной формы для указанного типа документа.
    /// </summary>
    /// <param name="documentType">
    /// Тип документа, для которого требуется генератор.
    /// </param>
    /// <returns>
    /// Генератор печатной формы или <see langword="null"/>,
    /// если генератор для указанного типа не зарегистрирован.
    /// </returns>
    internal IPrintFormGenerator? GetOrDefault(string documentType)
        => generators.GetValueOrDefault(documentType);
}

