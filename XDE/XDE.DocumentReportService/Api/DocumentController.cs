using Microsoft.AspNetCore.Mvc;
using XDE.DocumentReportService.Application.Dtos;
using XDE.DocumentReportService.Application.Abstractions;

namespace XDE.DocumentReportService.Api;


/// <summary>
/// Управление документами и их отправкой во внешние системы.
/// </summary>
[ApiController]
[Route("[controller]")]
public class DocumentController(ISendExternalSystemHandler sendExternalSystemHandler) : ControllerBase
{
    /// <summary>
    /// Генерирует печатные формы документов и ставит их в очередь
    /// на отправку во внешнюю систему.
    /// </summary>
    /// <response code="202">
    /// Документы приняты в обработку.
    /// </response>
    /// <response code="500">
    /// Внутренняя ошибка сервера.
    /// </response>
    [HttpPost("send/external-system")]
    [ProducesResponseType(typeof(IReadOnlyCollection<SendExternalSystemResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult SendExternalSystem(
        IReadOnlyCollection<SendExternalSystemCommand> request, CancellationToken cancellationToken)
    {
        return Accepted(sendExternalSystemHandler.Handle(request));
    }
}

