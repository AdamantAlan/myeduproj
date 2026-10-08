namespace XDE.DocumentReportService.Application.Dto
{
    /// <summary>
    /// Модель запроса на генерацию форм документа.
    /// </summary>
    public sealed class GenerateFormsCommand
    {
        /// <summary>
        /// Возвращает или задает идентификатор документа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает идентификатор пакета документов.
        /// </summary>
        /// <remarks>
        public int? PackageId { get; set; }

        /// <summary>
        /// Возвращает или задает название документа.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Возвращает или задает тип документа.
        /// </summary>
        public string DocumentType { get; set; } = null!;
    }
}
