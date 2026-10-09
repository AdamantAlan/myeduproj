using System.Collections.Concurrent;
using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Domain;

namespace XDE.DocumentReportService.Infrastructure;

/// <summary>
/// Очередь документов для фоновой отправки во внешнюю систему.
/// </summary>
/// <remarks>
/// Документы накапливаются в очереди и отправляются пакетами
/// размером до 10 элементов с заданным интервалом.
/// 
/// Обработка выполняется в фоновой асинхронной задаче.
/// Информация о результатах отправки передаётся через
/// <see cref="IProgress{DocumentQueueProgress}"/>.
///
/// При освобождении ресурсов обработка останавливается.
/// Документы, оставшиеся в очереди, не отправляются.
/// </remarks>
internal sealed class DocumentQueue : IDocumentsQueue, IAsyncDisposable
{
    private const int BATCH_SIZE = 10;
    private readonly ConcurrentQueue<Document> queue = new();
    private readonly CancellationTokenSource cst = new();
    private readonly PeriodicTimer periodicTimer;
    private readonly Task processingTask;
    private readonly Lock sync = new();
    private int sentDocumentCount;
    private bool disposed;
    private Task? disposeTask;

    private readonly ExternalSystemConnector externalSystemConnector;
    private readonly IProgress<DocumentQueueProgress> progress;

    /// <summary>
    /// Создаёт очередь документов и запускает фоновую обработку.
    /// </summary>
    /// <param name="externalSystemConnector">
    /// Коннектор для отправки документов во внешнюю систему.
    /// </param>
    /// <param name="progress">
    /// Получатель уведомлений о ходе отправки документов.
    /// </param>
    /// <param name="tick">
    /// Интервал между попытками обработки очереди.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Коннектор или получатель уведомлений не задан.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Интервал обработки меньше либо равен нулю.
    /// </exception>
    internal DocumentQueue(ExternalSystemConnector externalSystemConnector,
        IProgress<DocumentQueueProgress> progress,
        TimeSpan tick)
    {
        ArgumentNullException.ThrowIfNull(externalSystemConnector);
        ArgumentNullException.ThrowIfNull(progress);

        if (tick <= TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(tick));

        this.externalSystemConnector = externalSystemConnector;
        this.progress = progress;
        periodicTimer = new PeriodicTimer(tick);

        processingTask = ProcessQueueAsync(cst.Token);
    }

    /// <summary>
    /// Добавляет документ в очередь для последующей отправки.
    /// </summary>
    /// <param name="document">
    /// Документ, который необходимо отправить.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Документ не задан.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Идентификатор пакета некорректен или тип документа не поддерживается.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Очередь закрыта и больше не принимает документы.
    /// </exception>
    public void Enqueue(Document document)
    {
        ValidateDocument(document);

        lock (sync)
        {
            ObjectDisposedException.ThrowIf(disposed, this);
            queue.Enqueue(document);
        }
    }

    /// <summary>
    /// Останавливает фоновую обработку очереди и освобождает ресурсы.
    /// </summary>
    /// <remarks>
    /// Отменяет выполняющуюся отправку и ожидает завершения
    /// фоновой задачи. Не гарантирует отправку оставшихся документов.
    /// Повторный вызов возвращает ожидание того же завершения.
    /// </remarks>
    /// <returns>
    /// Задача, представляющая асинхронное освобождение ресурсов.
    /// </returns>
    public ValueTask DisposeAsync()
    {
        lock (sync)
        {
            if (disposeTask is not null)
                return new ValueTask(disposeTask);

            disposed = true;

            disposeTask = Task.Run(DisposeCoreAsync);

            return new ValueTask(disposeTask);
        }
    }

    /// <summary>
    /// Периодически извлекает документы из очереди и отправляет их
    /// во внешнюю систему пакетами.
    /// </summary>
    /// <param name="token">
    /// Токен отмены фоновой обработки.
    /// </param>
    /// <returns>
    /// Задача фоновой обработки очереди.
    /// </returns>
    private async Task ProcessQueueAsync(CancellationToken token)
    {
        List<Document> batchDocuments = null!;

        try
        {
            while (await periodicTimer.WaitForNextTickAsync(token))
            {
                try
                {
                    token.ThrowIfCancellationRequested();

                    if (queue.Count is 0)
                        continue;

                    batchDocuments = new List<Document>();

                    if (!TryDequeueBatch(batchDocuments))
                        continue;

                    await externalSystemConnector.SendDocumentsAsync(batchDocuments, token);
                    sentDocumentCount += batchDocuments.Count;

                    ReportProgressSuccess(batchDocuments);

                }
                catch (OperationCanceledException) when (token.IsCancellationRequested)
                {
                    //graceful shutdown
                }
                catch (Exception e)
                {
                    // Не знаю, что сделать с батчем при эксепшене, оставил как есть
                    // Можно еще сделать retry
                    ReportProgressFailed(batchDocuments, e.Message);
                }
            }
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested)
        {
            //graceful shutdown
        }
    }

    #region private methods

    private void ValidateDocument(Document document)
    {
        if (document is null)
            throw new ArgumentNullException(nameof(document), "Document cannot be null.");

        if (document.PackageId is null or <= 0)
            throw new ArgumentException("Document must have a PackageId.", nameof(document));

        if (document.DocumentType is not ("INVOICE" or "ORDER"))
            throw new ArgumentException($"Unsupported document type: {document.DocumentType}", nameof(document));
    }

    private bool TryDequeueBatch(List<Document> batchDocuments)
    {
        while (batchDocuments.Count < BATCH_SIZE &&
                    queue.TryDequeue(out var document))
        {
            batchDocuments.Add(document);
        }

        return batchDocuments.Count is not 0;

    }

    private string FormatDocumentIds(List<Document> batchDocuments)
        => string.Join(',', batchDocuments.Select(d => d.Id));

    private void ReportProgressSuccess(List<Document> batchDocuments)
    {
        var displayedDocumentIds = FormatDocumentIds(batchDocuments);

        progress.Report(new DocumentQueueProgress(SentCount: sentDocumentCount,
                    PendingCount: queue.Count,
                    Message: $"Documents {displayedDocumentIds} sent documents to external system",
                    IsFailed: false,
                    ErrorMessage: null!));
    }

    private void ReportProgressFailed(List<Document> batchDocuments, string errorMessage)
    {
        var displayedDocumentIds = FormatDocumentIds(batchDocuments);

        progress.Report(new DocumentQueueProgress(SentCount: sentDocumentCount,
                    PendingCount: queue.Count,
                    Message: $"Documents {displayedDocumentIds} do not sent to external system.",
                    IsFailed: true,
                    ErrorMessage: errorMessage));
    }

    private async Task DisposeCoreAsync()
    {
        try
        {
            cst.Cancel();

            try
            {
                await processingTask;
            }
            catch (OperationCanceledException) when (cst.IsCancellationRequested)
            {
            }
        }
        finally
        {
            periodicTimer.Dispose();
            cst.Dispose();
        }
    }

    #endregion
}

