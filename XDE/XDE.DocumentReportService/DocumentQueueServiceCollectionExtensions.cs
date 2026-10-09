using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Infrastructure;

namespace XDE.DocumentReportService;

/// <summary>
/// Методы расширения для регистрации очереди отправки документов во внешнюю систему.
/// </summary>
public static class DocumentQueueServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует очередь документов и обработчик
    /// уведомлений о ходе отправки во внешнюю систему.
    /// </summary>
    public static IServiceCollection AddDocumentQueue(
    this IServiceCollection services)
    {
        services.AddSingleton<IProgress<DocumentQueueProgress>>(_ =>
            new Progress<DocumentQueueProgress>(progress =>
            {
                Console.WriteLine($"Sent: {progress.SentCount}, Pending: {progress.PendingCount}, Message: {progress.Message}");
            }));

        services.AddSingleton<IDocumentsQueue>(sp =>
                new DocumentQueue(sp.GetRequiredService<ExternalSystemConnector>(),
                sp.GetRequiredService<IProgress<DocumentQueueProgress>>(),
                TimeSpan.FromSeconds(5)));

        return services;
    }
}
