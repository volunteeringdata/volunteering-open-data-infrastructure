namespace DoIt.Models;

public class Address
{
    [JsonIgnore]
    public string Id => IdObject.Value;

    [JsonIgnore]
    public DateTimeOffset CreatedAt => CreatedAtDate.Value;

    [JsonIgnore]
    public DateTimeOffset UpdatedAt => UpdatedAtDate.Value;

    [JsonPropertyName("street")]
    public string Street { get; set; }

    [JsonPropertyName("location")]
    public Location Location { get; set; }

    [JsonPropertyName("_id")]
    public Id IdObject { get; set; }

    [JsonPropertyName("createdAt")]
    public Date CreatedAtDate { get; set; }

    [JsonPropertyName("updatedAt")]
    public Date UpdatedAtDate { get; set; }
}