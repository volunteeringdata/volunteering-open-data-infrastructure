namespace DoIt.Models;

public class Activity
{
    [JsonIgnore]
    public string Id => IdObject.Value;

    [JsonIgnore]
    public DateTimeOffset CreatedAt => CreatedAtDate.Value;

    [JsonIgnore]
    public DateTimeOffset UpdatedAt => UpdatedAtDate.Value;

    [JsonPropertyName("_id")]
    public Id IdObject { get; set; }

    [JsonPropertyName("volunteerNumber")]
    public int? VolunteerNumber { get; set; }

    [JsonPropertyName("isVolunteerNumberLimited")]
    public bool? IsVolunteerNumberLimited { get; set; }

    [JsonPropertyName("isOnline")]
    public bool? IsOnline { get; set; }

    [JsonPropertyName("meetingLink")]
    public string? MeetingLink { get; set; }

    [JsonPropertyName("externalApplyLink")]
    public string ExternalApplyLink { get; set; }

    [JsonPropertyName("externalId")]
    public string ExternalId { get; set; }

    [JsonPropertyName("externalProvider")]
    public string ExternalProvider { get; set; }

    [JsonPropertyName("address")]
    public Address Address { get; set; }

    [JsonPropertyName("activityDefinitionSubDocument")]
    public ActivityDefinitionSubDocument ActivityDefinitionSubDocument { get; set; }

    [JsonPropertyName("bookingsNumber")]
    public int? BookingsNumber { get; set; }

    [JsonPropertyName("attendeesNumber")]
    public int? AttendeesNumber { get; set; }

    [JsonPropertyName("teamsNumber")]
    public int? TeamsNumber { get; set; }

    [JsonPropertyName("createdAt")]
    public Date CreatedAtDate { get; set; }

    [JsonPropertyName("updatedAt")]
    public Date UpdatedAtDate { get; set; }

    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }
}