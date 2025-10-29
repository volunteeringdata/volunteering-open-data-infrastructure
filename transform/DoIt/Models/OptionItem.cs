namespace DoIt.Models;

public class OptionItem
{
    [JsonIgnore]
    public string Id => IdObject.Value;

    [JsonIgnore]
    public DateTimeOffset CreatedAt => CreatedAtObject.Value;

    [JsonIgnore]
    public DateTimeOffset UpdatedAt => UpdatedAtObject.Value;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }

    [JsonPropertyName("app")]
    public Id App { get; set; }

    [JsonPropertyName("_id")]
    public required Id IdObject { get; set; }

    [JsonPropertyName("createdAt")]
    public required Date CreatedAtObject { get; set; }

    [JsonPropertyName("updatedAt")]
    public required Date UpdatedAtObject { get; set; }
}