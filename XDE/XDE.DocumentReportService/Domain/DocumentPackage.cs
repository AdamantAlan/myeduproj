namespace XDE.DocumentReportService.Domain;

/// <summary>
/// Определяет модель пакета документов.
/// </summary>
public sealed class DocumentPackage
{
    /// <summary>
    /// Возвращает или задает идентификатора пакета.
    /// </summary>
    public int? PackageId { get; set; }

    /// <summary>
    /// Возвращает или задает последовательность документов в пакете.
    /// </summary>
    public IEnumerable<DocumentInPackage> Documents { get; set; } = null!;
}