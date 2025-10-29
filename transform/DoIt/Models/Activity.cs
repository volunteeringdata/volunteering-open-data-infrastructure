namespace DoIt.Models;

public class Activity
{
    [JsonIgnore]
    public string Id => IdObject.Value;

    [JsonIgnore]
    public DateTimeOffset CreatedAt => CreatedAtObject.Value;

    [JsonIgnore]
    public DateTimeOffset UpdatedAt => UpdatedAtObject.Value;

    [JsonPropertyName("_id")]
    public required Id IdObject { get; set; }

    [JsonPropertyName("volunteerNumber")]
    public int? VolunteerNumber { get; set; }

    [JsonPropertyName("isVolunteerNumberLimited")]
    public bool? IsVolunteerNumberLimited { get; set; }

    [JsonPropertyName("isOnline")]
    public bool? IsOnline { get; set; }

    [JsonPropertyName("meetingLink")]
    public string? MeetingLink { get; set; }

    [JsonPropertyName("externalApplyLink")]
    public required Uri ExternalApplyLink { get; set; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("externalProvider")]
    public string? ExternalProvider { get; set; }

    [JsonPropertyName("address")]
    public required Address Address { get; set; }

    [JsonPropertyName("activityDefinitionSubDocument")]
    public required ActivityDefinitionSubDocument ActivityDefinitionSubDocument { get; set; }

    [JsonPropertyName("bookingsNumber")]
    public int? BookingsNumber { get; set; }

    [JsonPropertyName("attendeesNumber")]
    public int? AttendeesNumber { get; set; }

    [JsonPropertyName("teamsNumber")]
    public int? TeamsNumber { get; set; }

    [JsonPropertyName("createdAt")]
    public required Date CreatedAtObject { get; set; }

    [JsonPropertyName("updatedAt")]
    public required Date UpdatedAtObject { get; set; }

    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }
}