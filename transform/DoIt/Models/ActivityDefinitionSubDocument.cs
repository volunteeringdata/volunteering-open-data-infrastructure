namespace DoIt.Models;

public class ActivityDefinitionSubDocument : Identified
{
    // activitiesSummary (all empty arrays)

    // app (redundant with appSummary)

    [JsonPropertyName("appSummary")] public required AppSummary AppSummary { get; set; }

    [JsonPropertyName("measurementUnitSummary")] public required MeasurementUnitSummary MeasurementUnitSummary { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("eventType")] public string EventType { get; set; }

    [JsonPropertyName("causeOptions")] public List<OptionItem> CauseOptions { get; set; }

    [JsonPropertyName("requirementOptions")] public List<OptionItem> RequirementOptions { get; set; }

    [JsonPropertyName("locationOption")] public string LocationOption { get; set; }

    [JsonPropertyName("organizationSubDocument")] public required OrganizationSubDocument OrganizationSubDocument { get; set; }

    [JsonPropertyName("createdAt")] public Date CreatedAtObject { get; set; }
    [JsonIgnore] public DateTimeOffset CreatedAt => CreatedAtObject.Value;

    [JsonPropertyName("updatedAt")] public Date UpdatedAtObject { get; set; }
    [JsonIgnore] public DateTimeOffset UpdatedAt => UpdatedAtObject.Value;
}