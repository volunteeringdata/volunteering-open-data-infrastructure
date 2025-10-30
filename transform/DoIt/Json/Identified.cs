namespace DoIt.Json;

public class Identified
{
    [JsonPropertyName("_id")] public required Id Id { get; set; }
}
