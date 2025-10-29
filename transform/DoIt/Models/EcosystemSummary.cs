namespace DoIt.Models;

public class EcosystemSummary
{
    #region Calculated convenience properties

    [JsonIgnore]
    public string Id => IdObject.Value;

    #endregion

    [JsonPropertyName("_id")]
    public required Id IdObject { get; set; }

    [JsonPropertyName("logo")]
    public required Uri Logo { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}