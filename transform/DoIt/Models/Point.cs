namespace DoIt.Models;

public class Point
{
    [JsonPropertyName("coordinates")]
    public required double[] Coordinates { get; set; }
}