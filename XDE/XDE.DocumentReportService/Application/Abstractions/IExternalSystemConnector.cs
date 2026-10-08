using XDE.DocumentReportService.Domain;

namespace XDE.DocumentReportService.Application.Abstractions
{
    public interface IExternalSystemConnector 
    {
        Task SendDocumentsAsync(IReadOnlyCollection<Document> documents, CancellationToken cancellationToken);
    }
}
