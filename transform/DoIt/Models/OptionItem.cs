namespace DoIt.Models;

public class OptionItem
{
    [JsonIgnore]
    public string Id => IdObject.Value;

    [JsonIgnore]
    public DateTimeOffset CreatedAt => CreatedAtDate.Value;

    [JsonIgnore]
    public DateTimeOffset UpdatedAt => UpdatedAtDate.Value;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }

    [JsonPropertyName("app")]
    public Id App { get; set; }

    [JsonPropertyName("_id")]
    public Id IdObject { get; set; }

    [JsonPropertyName("createdAt")]
    public Date CreatedAtDate { get; set; }

    [JsonPropertyName("updatedAt")]
    public Date UpdatedAtDate { get; set; }
}