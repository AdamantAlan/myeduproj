using System.Reflection;
using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Application.Attributes;

namespace XDE.DocumentReportService.Application.Services;

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

    internal IPrintFormGenerator? GetOrDefault(string documentType)
        => generators.GetValueOrDefault(documentType);
}

