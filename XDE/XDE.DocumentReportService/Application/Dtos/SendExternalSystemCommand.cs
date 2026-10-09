namespace XDE.DocumentReportService.Application.Dtos;

/// <summary>
/// Модель запроса на отправку документа во внешнюю систему.
/// </summary>
public sealed class SendExternalSystemCommand
{
    /// <summary>
    /// Возвращает или задает идентификатор документа.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Возвращает или задает идентификатор пакета документов.
    /// </summary>
    public int? PackageId { get; init; }

    /// <summary>
    /// Возвращает или задает название документа.
    /// </summary>
    public string Title { get; init; } = null!;

    /// <summary>
    /// Возвращает или задает тип документа.
    /// </summary>
    public string DocumentType { get; init; } = null!;
}

