namespace DoIt.Models;

public class Id
{
    [JsonPropertyName("$oid")] public required string Value { get; set; }
}