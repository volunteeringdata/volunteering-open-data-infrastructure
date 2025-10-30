namespace DoIt.Json;

public class Address : Identified
{
    [JsonPropertyName("street")] public required string Street { get; set; }

    [JsonPropertyName("location")] public required Point Location { get; set; }
}