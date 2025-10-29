namespace DoIt.Models;

public class OptionItem : Identified
{
    [JsonPropertyName("displayName")] public string DisplayName { get; set; }

    [JsonPropertyName("icon")] public string Icon { get; set; }

    [JsonPropertyName("app")] public Id App { get; set; }

    [JsonPropertyName("createdAt")] public required Date CreatedAtObject { get; set; }
    [JsonIgnore] public DateTimeOffset CreatedAt => CreatedAtObject.Value;

    [JsonPropertyName("updatedAt")] public required Date UpdatedAtObject { get; set; }
    [JsonIgnore] public DateTimeOffset UpdatedAt => UpdatedAtObject.Value;
}