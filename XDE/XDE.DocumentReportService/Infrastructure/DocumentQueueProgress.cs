namespace XDE.DocumentReportService.Infrastructure;

/// <summary>
/// Информация о ходе отправки документов во внешнюю систему.
/// </summary>
/// <param name="SentCount">
/// Общее количество успешно отправленных документов.
/// </param>
/// <param name="PendingCount">
/// Количество документов, ожидающих обработки в очереди.
/// </param>
/// <param name="Message">
/// Информационное сообщение о результате обработки.
/// </param>
/// <param name="IsFailed">
/// Признак ошибки при отправке очередного пакета документов.
/// </param>
/// <param name="ErrorMessage">
/// Описание ошибки отправки или <see langword="null"/>,
/// если обработка завершилась успешно.
/// </param>
internal record DocumentQueueProgress(int SentCount,
    int PendingCount,
    string Message,
    bool IsFailed, 
    string ErrorMessage);

