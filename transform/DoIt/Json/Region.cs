namespace DoIt.Json;

public class Region : Identified
{
    [JsonPropertyName("displayName")] public required string DisplayName { get; set; }

    [JsonPropertyName("relatedTo")] public string? RelatedTo { get; set; }

    // TODO: Enum?
    [JsonPropertyName("type")] public required string Type { get; set; }

    [JsonPropertyName("geocenterLocation")] public required LonLat GeocenterLocation { get; set; }

    [JsonPropertyName("createdAt")] public required Date CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")] public required Date UpdatedAt { get; set; }
}
