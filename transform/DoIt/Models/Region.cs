namespace DoIt.Models;

public class Region
{
    [JsonIgnore]
    public string Id => IdObject.Value;

    [JsonIgnore]
    public DateTimeOffset CreatedAt => CreatedAtObject.Value;

    [JsonIgnore]
    public DateTimeOffset UpdatedAt => UpdatedAtObject.Value;

    [JsonPropertyName("displayName")]
    public required string DisplayName { get; set; }

    [JsonPropertyName("relatedTo")]
    public string? RelatedTo { get; set; }

    // TODO: Enum?
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("geocenterLocation")]
    public required LonLat GeocenterLocation { get; set; }

    [JsonPropertyName("_id")]
    public required Id IdObject { get; set; }

    [JsonPropertyName("createdAt")]
    public required Date CreatedAtObject { get; set; }

    [JsonPropertyName("updatedAt")]
    public required Date UpdatedAtObject { get; set; }
}
