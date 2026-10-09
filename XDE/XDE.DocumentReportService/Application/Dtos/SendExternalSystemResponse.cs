namespace XDE.DocumentReportService.Application.Dtos;

/// <summary>
/// Результат обработки пакета документов для отправки
/// во внешнюю систему.
/// </summary>
public sealed class SendExternalSystemResponse
{
    /// <summary>
    /// Идентификатор пакета документов.
    /// </summary>
    public int PackageId { get; init; }

    /// <summary>
    /// Идентификаторы документов, входящих в пакет.
    /// </summary>
    public IEnumerable<int> DocumentIds { get; init; } = [];
}

