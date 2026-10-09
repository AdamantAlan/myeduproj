using System.Collections.Concurrent;
using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Domain;

namespace XDE.DocumentReportService.Infrastructure;

internal sealed class DocumentQueue : IDocumentsQueue, IAsyncDisposable
{
    private readonly int batchSize = 10;
    private readonly ConcurrentQueue<Document> documentsQueue = new();
    private readonly CancellationTokenSource cst = new();
    private readonly PeriodicTimer periodicTimer;
    private readonly Task worker;
    private readonly Lock sync = new();
    private int sentDocumentsCount;
    private bool disposed;
    private Task? disposeTask;

    private readonly ExternalSystemConnector externalSystemConnector;
    private readonly IProgress<DocumentQueueProgress> progress;

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

        worker = ProcessQueueAsync(cst.Token);
    }

    public void Enqueue(Document document)
    {
        ValidateDocument(document);

        lock (sync)
        {
            ObjectDisposedException.ThrowIf(disposed, this);
            documentsQueue.Enqueue(document);
        }
    }

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

    private async Task ProcessQueueAsync(CancellationToken token)
    {
        List<Document> batchDocuments = default!;

        try
        {
            while (await periodicTimer.WaitForNextTickAsync(token))
            {
                try
                {
                    if (token.IsCancellationRequested)
                        break;

                    if (documentsQueue.Count is 0)
                        continue;

                    batchDocuments = new List<Document>();

                    if (!TryEnrichBatchDocuments(batchDocuments))
                        continue;

                    await externalSystemConnector.SendDocumentsAsync(batchDocuments, token);
                    sentDocumentsCount += batchDocuments.Count;

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

    private bool TryEnrichBatchDocuments(List<Document> batchDocuments)
    {
        while (batchDocuments.Count < batchSize &&
                    documentsQueue.TryDequeue(out var document))
        {
            batchDocuments.Add(document);
        }

        return batchDocuments.Count is not 0;

    }

    private string GetDisplayedDocumentIds(List<Document> batchDocuments)
        => string.Join(',', batchDocuments.Select(d => d.Id));

    private void ReportProgressSuccess(List<Document> batchDocuments)
    {
        var displayedDocumentIds = GetDisplayedDocumentIds(batchDocuments);

        progress.Report(new DocumentQueueProgress(SentCount: sentDocumentsCount,
                    PendingCount: documentsQueue.Count,
                    Message: $"Documents {displayedDocumentIds} sent documents to external system",
                    IsFailed: false,
                    ErrorMessage: null!));
    }

    private void ReportProgressFailed(List<Document> batchDocuments, string errorMessage)
    {
        var displayedDocumentIds = GetDisplayedDocumentIds(batchDocuments);

        progress.Report(new DocumentQueueProgress(SentCount: sentDocumentsCount,
                    PendingCount: documentsQueue.Count,
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
                await worker;
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

