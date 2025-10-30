namespace DoIt.Json;

public class MeasurementUnitSummary : Identified
{
    // TODO: Enum?
    [JsonPropertyName("category")] public required string Category { get; set; }

    [JsonPropertyName("pluralLabel")] public required string PluralLabel { get; set; }

    [JsonPropertyName("singularLabel")] public required string SingularLabel { get; set; }
}