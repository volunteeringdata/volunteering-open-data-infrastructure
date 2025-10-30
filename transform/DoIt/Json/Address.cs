namespace DoIt.Json;

public class Address : Identified
{
    [JsonPropertyName("street")] public required string Street { get; set; }

    [JsonPropertyName("location")] public required Point Location { get; set; }

    [JsonPropertyName("createdAt")] public required Date CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")] public required Date UpdatedAt { get; set; }
}