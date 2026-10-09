namespace XDE.DocumentReportService.Infrastructure;

internal record DocumentQueueProgress(int SentCount, int PendingCount, string Message, bool IsFailed, string ErrorMessage);

