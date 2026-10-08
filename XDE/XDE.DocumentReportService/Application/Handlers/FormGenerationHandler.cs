using XDE.DocumentReportService.Application.Dtos;
using XDE.DocumentReportService.Application.Abstractions;
using XDE.DocumentReportService.Application.Services;
using XDE.DocumentReportService.Domain;

namespace XDE.DocumentReportService.Application.Handlers;

internal class FormGenerationHandler(PrintFormGeneratorContext formGeneratorContext) : IFormGenerationHandler
{
    private readonly string[] allowedDocumentTypes = ["ORDER", "INVOICE"];
    public IReadOnlyCollection<GenerateFormsResponse> HandleAsync(
        IReadOnlyCollection<GenerateFormsCommand> generateFormsCommand)
    {
        var documents = MapToDocuments(generateFormsCommand);

        var documentsByPackage = GroupDocumentsByPackage(documents);

        //что-то делаем с пакетами
        //и возвращаем ответ

        return MapToResponse(documentsByPackage).ToArray();
    }

    private IEnumerable<Document> MapToDocuments(IEnumerable<GenerateFormsCommand> generateFormsCommand)
    {
        return generateFormsCommand
                     .Select(f => new Document()
                     {
                         Id = f.Id,
                         PackageId = f.PackageId,
                         DocumentType = f.DocumentType,
                         Title = f.Title
                     });
    }

    private Dictionary<int, DocumentPackage> GroupDocumentsByPackage(IEnumerable<Document> documents)
    {
        return documents
            .Where(d => d.PackageId.HasValue)
            .Where(d => allowedDocumentTypes.Contains(d.DocumentType))
            .GroupBy(d => d.PackageId!.Value)
            .ToDictionary(
                g => g.Key,
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
                    }).ToArray()
                }
            );
    }

    private IEnumerable<GenerateFormsResponse> MapToResponse(Dictionary<int, DocumentPackage> documentsByPackage)
    {
        return documentsByPackage.Select(kvp => new GenerateFormsResponse
        {
            PackageId = kvp.Key,
            DocumentIds = kvp.Value.Documents.Select(d => d.DocumentId)
        }).ToArray();
    }
}

