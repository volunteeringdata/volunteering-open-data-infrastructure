namespace DoIt.Json;

public class Id
{
    [JsonPropertyName("$oid")] public required string Value { get; set; }
}