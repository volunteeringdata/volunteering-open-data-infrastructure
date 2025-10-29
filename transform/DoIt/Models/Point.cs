namespace DoIt.Models;

public class Point
{
    // TOTO: Harmonize with Loction
    [JsonPropertyName("coordinates")] public required double[] Coordinates { get; set; }
}