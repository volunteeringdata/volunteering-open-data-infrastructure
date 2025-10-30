namespace DoIt.Json;

public class OptionItem : Identified
{
    [JsonPropertyName("displayName")] public required string DisplayName { get; set; }

    [JsonPropertyName("icon")] public string? Icon { get; set; }

    [JsonPropertyName("app")] public Id? App { get; set; }

    [JsonPropertyName("createdAt")] public required Date CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")] public required Date UpdatedAt { get; set; }
}