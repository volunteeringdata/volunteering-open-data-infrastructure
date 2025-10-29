namespace DoIt.Models;

public class Location
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("coordinates")]
    public double[] Coordinates { get; set; }
}