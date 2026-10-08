using System.Collections.ObjectModel;
using XDE.DocumentReportService.Application.Dto;

namespace XDE.DocumentReportService.Application.Interface;

public interface IFormGenerationHandler
{
    IReadOnlyCollection<GenerateFormsResponse> HandleAsync(IReadOnlyCollection<GenerateFormsCommand> generateFormsCommand);
}