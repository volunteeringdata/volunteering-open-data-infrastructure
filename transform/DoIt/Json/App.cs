namespace DoIt.Json;

public class App : Identified
{
    // TODO: Color
    [JsonPropertyName("brandColor")] public string? BrandColor { get; set; }

    [JsonPropertyName("description")] public required string Description { get; set; }

    [JsonPropertyName("ecosystemSummary")] public required Ecosystem Ecosystem { get; set; }

    [JsonPropertyName("logo")] public required Uri Logo { get; set; }

    [JsonPropertyName("name")] public required string Name { get; set; }

    [JsonPropertyName("organizationSummary")] public required Organization Organization { get; set; }
}