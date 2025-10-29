namespace DoIt.Models;

public class ActivityDefinitionSubDocument
{
    [JsonIgnore]
    public string Id => IdObject.Value;

    [JsonIgnore]
    public DateTimeOffset CreatedAt => CreatedAtObject.Value;

    [JsonIgnore]
    public DateTimeOffset UpdatedAt => UpdatedAtObject.Value;

    [JsonPropertyName("_id")]
    public required Id IdObject { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("eventType")]
    public string EventType { get; set; }

    [JsonPropertyName("causeOptions")]
    public List<OptionItem> CauseOptions { get; set; }

    [JsonPropertyName("requirementOptions")]
    public List<OptionItem> RequirementOptions { get; set; }

    [JsonPropertyName("locationOption")]
    public string LocationOption { get; set; }

    [JsonPropertyName("organizationSubDocument")]
    public OrganizationSummary OrganizationSubDocument { get; set; }

    [JsonPropertyName("appSummary")]
    public AppSummary AppSummary { get; set; }

    [JsonPropertyName("createdAt")]
    public Date CreatedAtObject { get; set; }

    [JsonPropertyName("updatedAt")]
    public Date UpdatedAtObject { get; set; }
}