namespace DoIt.Models;

public class Identified
{
    [JsonPropertyName("_id")] public required Id IdObject { get; set; }
    [JsonIgnore] public string Id => IdObject.Value;
}
