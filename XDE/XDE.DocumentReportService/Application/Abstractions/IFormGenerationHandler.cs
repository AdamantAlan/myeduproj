using System.Collections.ObjectModel;
using XDE.DocumentReportService.Application.Dtos;

namespace XDE.DocumentReportService.Application.Abstractions;

public interface IFormGenerationHandler
{
    IReadOnlyCollection<GenerateFormsResponse> HandleAsync(IReadOnlyCollection<GenerateFormsCommand> generateFormsCommand);
}