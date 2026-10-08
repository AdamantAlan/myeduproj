namespace XDE.DocumentReportService.Domain;

/// <summary>
/// Представляет модель документа в пакете.
/// </summary>
public sealed class DocumentInPackage
{
    /// <summary>
    /// Возвращает или задает идентификатор документа.
    /// </summary>
    public int DocumentId { get; set; }

    /// <summary>
    /// Возвращает или задает название документа.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Возвращает или задает тип документа.
    /// </summary>
    public string DocumentType { get; set; } = null!;

    /// <summary>
    /// Возвращает или задает содержимое печатной формы документа.
    /// </summary>
    public byte[] PrintForm { get; set; } = null!;
}