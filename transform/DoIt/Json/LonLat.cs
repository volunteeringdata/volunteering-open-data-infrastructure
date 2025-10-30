namespace DoIt.Json;

public class LonLat
{
    [JsonPropertyName("lon")] public required double Lon { get; set; }

    [JsonPropertyName("lat")] public required double Lat { get; set; }
}