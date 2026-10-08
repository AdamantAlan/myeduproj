using Microsoft.AspNetCore.Mvc;
using XDE.DocumentReportService.Application.Dto;
using XDE.DocumentReportService.Application.Interface;

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

