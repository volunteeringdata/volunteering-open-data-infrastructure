namespace DoIt.Models;

public class Id
{
    [JsonPropertyName("$oid")]
    public string Value { get; set; }
}