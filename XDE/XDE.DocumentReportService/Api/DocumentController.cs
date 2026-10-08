using Microsoft.AspNetCore.Mvc;
using XDE.DocumentReportService.Application.Dtos;
using XDE.DocumentReportService.Application.Abstractions;

namespace XDE.DocumentReportService.Api;

[ApiController]
[Route("[controller]")]
public class DocumentController(IFormGenerationHandler formGenerationHandler) : ControllerBase
{
    [HttpPost("forms")]
    public IReadOnlyCollection<GenerateFormsResponse> GenerateFormsAsync(
        IReadOnlyCollection<GenerateFormsCommand> request)
    {
        return formGenerationHandler.HandleAsync(request);
    }
}

