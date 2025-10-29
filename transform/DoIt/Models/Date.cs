namespace DoIt.Models;

public class Date
{
    [JsonPropertyName("$date")]
    public required DateTimeOffset Value { get; set; }
}