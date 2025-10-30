namespace DoIt.Json;

public class Date
{
    [JsonPropertyName("$date")] public required DateTimeOffset Value { get; set; }
}