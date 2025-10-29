namespace DoIt.Models;

public class Address
{
    [JsonIgnore]
    public string Id => IdObject.Value;

    [JsonIgnore]
    public DateTimeOffset CreatedAt => CreatedAtObject.Value;

    [JsonIgnore]
    public DateTimeOffset UpdatedAt => UpdatedAtObject.Value;

    [JsonPropertyName("street")]
    public string Street { get; set; }

    [JsonPropertyName("location")]
    public Point Location { get; set; }

    [JsonPropertyName("_id")]
    public required Id IdObject { get; set; }

    [JsonPropertyName("createdAt")]
    public required Date CreatedAtObject { get; set; }

    [JsonPropertyName("updatedAt")]
    public required Date UpdatedAtObject { get; set; }
}