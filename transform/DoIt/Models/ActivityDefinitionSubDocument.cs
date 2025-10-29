namespace DoIt.Models;

public class ActivityDefinitionSubDocument
{
    [JsonIgnore]
    public string Id => IdObject.Value;

    [JsonIgnore]
    public DateTimeOffset CreatedAt => CreatedAtDate.Value;

    [JsonIgnore]
    public DateTimeOffset UpdatedAt => UpdatedAtDate.Value;

    [JsonPropertyName("_id")]
    public Id IdObject { get; set; }

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
    public Date CreatedAtDate { get; set; }

    [JsonPropertyName("updatedAt")]
    public Date UpdatedAtDate { get; set; }
}