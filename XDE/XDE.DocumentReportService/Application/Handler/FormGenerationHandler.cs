using XDE.DocumentReportService.Application.Dto;
using XDE.DocumentReportService.Application.Interface;
using XDE.DocumentReportService.Application.Service;
using XDE.DocumentReportService.Domain;

namespace XDE.DocumentReportService.Application.Handler;

public class FormGenerationHandler(PrintFormGenerator printFormGenerator) : IFormGenerationHandler
{
    public IReadOnlyCollection<GenerateFormsResponse> HandleAsync(
        IReadOnlyCollection<GenerateFormsCommand> generateFormsCommand)
    {
        var documents = generateFormsCommand
            .Select(f => new Document() 
            {
                Id = f.Id,
                PackageId = f.PackageId,
                DocumentType = f.DocumentType,
                Title = f.Title
            });

        var documentsByPackage = documents
            .Where(d => d.PackageId.HasValue)
            .Where(d => d.DocumentType is "ORDER" or "INVOICE")
            .GroupBy(d => d.PackageId!.Value)
            .ToDictionary(
                g => g.Key,
                g => g.Select(v => new DocumentInPackage
                {
                    DocumentId = v.Id,
                    DocumentType = v.DocumentType,
                    Title = v.Title,
                    PrintForm = printFormGenerator.GeneratePrintForm(v)
                }).ToList()
            );

        //что-то делаем с пакетами
        //и возвращаем ответ

        return documentsByPackage.Select(kvp => new GenerateFormsResponse
        {
            PackageId = kvp.Key,
            DocumentIds = kvp.Value.Select(d => d.DocumentId)
        }).ToArray();
    }
}

