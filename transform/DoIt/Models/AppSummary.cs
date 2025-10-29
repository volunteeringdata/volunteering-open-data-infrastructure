namespace DoIt.Models;

public class AppSummary : Identified
{
    // TODO: Color
    [JsonPropertyName("brandColor")] public required string BrandColor { get; set; }

    [JsonPropertyName("description")] public required string Description { get; set; }

    [JsonPropertyName("ecosystemSummary")] public required EcosystemSummary EcosystemSummary { get; set; }

    [JsonPropertyName("logo")] public required Uri Logo { get; set; }

    [JsonPropertyName("name")] public required string Name { get; set; }

    [JsonPropertyName("organizationSummary")] public required OrganizationSummary OrganizationSummary { get; set; }
}