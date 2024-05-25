using System.ComponentModel.DataAnnotations;

namespace OpenSearchLearn.Domain;

public class Document
{
    public int Id { get; set; }
    public string Department { get; set; }
    public string Description { get; set; }
}