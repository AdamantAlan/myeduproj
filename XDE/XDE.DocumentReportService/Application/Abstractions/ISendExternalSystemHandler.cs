using XDE.DocumentReportService.Application.Dtos;

namespace XDE.DocumentReportService.Application.Abstractions;

/// <summary>
/// Обработчик запросов на формирование пакетов и отправку документов
/// во внешнюю систему.
/// </summary>
public interface ISendExternalSystemHandler
{
    /// <summary>
    /// Обрабатывает документы: группирует их по пакетам,
    /// генерирует печатные формы и ставит документы в очередь
    /// на отправку во внешнюю систему.
    /// </summary>
    /// <param name="sendExternalSystemCommand">
    /// Коллекция документов для обработки и отправки.
    /// </param>
    /// <returns>
    /// Коллекция результатов обработки, содержащих идентификаторы
    /// пакетов и входящих в них документов.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Коллекция документов для обработки равна null.
    /// </exception>
    IReadOnlyCollection<SendExternalSystemResponse> Handle(IReadOnlyCollection<SendExternalSystemCommand> sendExternalSystemCommand);
}