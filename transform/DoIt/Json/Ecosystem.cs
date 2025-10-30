namespace DoIt.Json;

public class Ecosystem : Identified
{
    [JsonPropertyName("logo")] public required Uri Logo { get; set; }

    [JsonPropertyName("name")] public required string Name { get; set; }
}