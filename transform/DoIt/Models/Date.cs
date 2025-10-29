namespace DoIt.Models;

public class Date
{
    [JsonPropertyName("$date")]
    public DateTimeOffset Value { get; set; }
}