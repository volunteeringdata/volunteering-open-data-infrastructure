namespace DoIt.Json;

public class Address : Identified
{
    [JsonPropertyName("street")] public string Street { get; set; }

    [JsonPropertyName("location")] public Point Location { get; set; }

    [JsonPropertyName("createdAt")] public required Date CreatedAtObject { get; set; }
    [JsonIgnore] public DateTimeOffset CreatedAt => CreatedAtObject.Value;

    [JsonPropertyName("updatedAt")] public required Date UpdatedAtObject { get; set; }
    [JsonIgnore] public DateTimeOffset UpdatedAt => UpdatedAtObject.Value;
}