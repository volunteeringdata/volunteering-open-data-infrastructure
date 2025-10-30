namespace DoIt.Json;

public class ActivityDefinitionSubDocument : Identified
{
    // activitiesSummary (all empty arrays)

    // app (redundant with appSummary)

    [JsonPropertyName("appSummary")] public required App AppSummary { get; set; }

    [JsonPropertyName("measurementUnitSummary")] public required MeasurementUnit MeasurementUnit { get; set; }

    [JsonPropertyName("title")] public required string Title { get; set; }

    [JsonPropertyName("description")] public required string Description { get; set; }

    [JsonPropertyName("type")] public required string Type { get; set; }

    [JsonPropertyName("eventType")] public string? EventType { get; set; }

    [JsonPropertyName("causeOptions")] public required List<OptionItem> Causes { get; set; }

    [JsonPropertyName("requirementOptions")] public required List<OptionItem> Requirements { get; set; }

    [JsonPropertyName("locationOption")] public required string LocationOption { get; set; }

    [JsonPropertyName("organizationSubDocument")] public required OrganizationSubDocument Organization { get; set; }

    [JsonPropertyName("createdAt")] public required Date CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")] public required Date UpdatedAt { get; set; }
}