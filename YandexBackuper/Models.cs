using System.Text.Json.Serialization;

namespace YandexBackuper;

public sealed record DiskResource(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("path")] string Path,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("size")] long? Size,
    [property: JsonPropertyName("md5")] string? Md5,
    [property: JsonPropertyName("_embedded")] ResourceList? Embedded);

public sealed record ResourceList(
    [property: JsonPropertyName("items")] List<DiskResource> Items);

public sealed record DownloadLink(
    [property: JsonPropertyName("href")] string Href);

