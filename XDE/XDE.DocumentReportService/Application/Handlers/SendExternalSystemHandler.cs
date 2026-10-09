using XDE.DocumentReportService.Application.Dtos;
using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Application.Services;
using XDE.DocumentReportService.Domain;
using System.Collections.ObjectModel;

namespace XDE.DocumentReportService.Application.Handlers;

/// <inheritdoc cref="ISendExternalSystemHandler" />
internal class SendExternalSystemHandler(PrintFormGeneratorContext formGeneratorContext,
    IDocumentsQueue documentsQueue,
    ILogger<SendExternalSystemHandler> logger) : ISendExternalSystemHandler
{
    private static readonly string[] allowedDocumentTypes = ["ORDER", "INVOICE"];

    /// <inheritdoc />
    public IReadOnlyCollection<SendExternalSystemResponse> Handle(
        IReadOnlyCollection<SendExternalSystemCommand> generateFormsCommand)
    {
        ArgumentNullException.ThrowIfNull(generateFormsCommand);

        var documents = MapToDocuments(generateFormsCommand)
            .Where(d => d.PackageId.HasValue && d.PackageId > 0)
            .Where(d => allowedDocumentTypes.Contains(d.DocumentType))
            .ToArray();

        //Задание 1+2.
        var documentsByPackage = GroupDocumentsByPackage(documents);

        //к примеру тут логика обработки пакетов...

        //Задание 3.
        EnqueueDocumentsForExternalSystem(documents);

        //response для нас, посмотреть, что группировка верная
        return MapToResponse(documentsByPackage).ToArray();
    }

    /// <summary>
    /// Ставит документы в очередь для последующей отправки
    /// во внешнюю систему.
    /// </summary>
    private void EnqueueDocumentsForExternalSystem(IEnumerable<Document> documents)
    {
        try
        {
            foreach (var document in documents)
            {
                documentsQueue.Enqueue(document);
            }
        }
        catch (ObjectDisposedException)
        {
            logger.LogInformation("Documents queue is closed.");
            throw;
        }
    }

    private IEnumerable<Document> MapToDocuments(IEnumerable<SendExternalSystemCommand> generateFormsCommand)
    {
        return generateFormsCommand.Select(f => new Document()
        {
            Id = f.Id,
            PackageId = f.PackageId,
            DocumentType = f.DocumentType,
            Title = f.Title
        });
    }

    /// <summary>
    /// Группирует документы по идентификатору пакета
    /// и генерирует печатную форму для каждого документа.
    /// </summary>
    /// <param name="documents">
    /// Коллекция документов с заполненным идентификатором пакета.
    /// </param>
    /// <returns>
    /// Словарь пакетов документов, где ключом является идентификатор пакета,
    /// а значением — пакет со сформированными печатными формами.
    /// </returns>
    private ReadOnlyCollection<DocumentPackage> GroupDocumentsByPackage(IEnumerable<Document> documents)
    {
        var packages = documents
            .GroupBy(d => d.PackageId!.Value)
            .Select(
                g => new DocumentPackage()
                {
                    PackageId = g.Key,
                    Documents = g.Select(d =>
                    {
                        var form = formGeneratorContext.GetOrDefault(d.DocumentType);
                        return new DocumentInPackage
                        {
                            DocumentId = d.Id,
                            DocumentType = d.DocumentType,
                            Title = d.Title,
                            PrintForm = form?.GeneratePrintForm(d)!
                        };
                    })
                }
            );

        return new ReadOnlyCollection<DocumentPackage>(packages.ToArray());
    }

    private IEnumerable<SendExternalSystemResponse> MapToResponse(IEnumerable<DocumentPackage> documentsByPackage)
    {
        return documentsByPackage.Select(p => new SendExternalSystemResponse
        {
            PackageId = p.PackageId!.Value,
            DocumentIds = p.Documents.Select(d => d.DocumentId)
        });
    }
}

