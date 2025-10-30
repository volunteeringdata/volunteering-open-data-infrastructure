namespace DoIt.Json;

public class Organization : Identified
{
    [JsonPropertyName("logo")] public required Uri Logo { get; set; }

    [JsonPropertyName("name")] public required string Name { get; set; }
}