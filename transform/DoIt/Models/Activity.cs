namespace DoIt.Models;

public class Activity
{
    [JsonIgnore]
    public string Id => IdObject.Value;

    [JsonIgnore]
    public DateTimeOffset CreatedAt => CreatedAtObject.Value;

    [JsonIgnore]
    public DateTimeOffset UpdatedAt => UpdatedAtObject.Value;

    [JsonIgnore]
    public string Ecosystem => EcosystemObject.Value;

    [JsonIgnore]
    public string Organization => OrganizationObject.Value;

    [JsonIgnore]
    public string App => AppObject.Value;

    [JsonIgnore]
    public IEnumerable<string> PublishedApps => PublishedAppObjects.Select(a => a.Value);

    [JsonIgnore]
    public string ActivityDefinition => ActivityDefinitionObject.Value;

    [JsonIgnore]
    public Uri? MeetingLink => string.IsNullOrWhiteSpace(MeetingLinkString) ? null : new Uri(MeetingLinkString);

    [JsonIgnore]
    public IEnumerable<Region> Regions => RegionsNullable ?? [];

    [JsonPropertyName("_id")]
    public required Id IdObject { get; set; }

    [JsonPropertyName("volunteerNumber")]
    public int? VolunteerNumber { get; set; }

    [JsonPropertyName("isVolunteerNumberLimited")]
    public bool? IsVolunteerNumberLimited { get; set; }

    [JsonPropertyName("isOnline")]
    public bool? IsOnline { get; set; }

    [JsonPropertyName("meetingLink")]
    public string? MeetingLinkString { get; set; }

    [JsonPropertyName("externalApplyLink")]
    public Uri? ExternalApplyLink { get; set; }

    [JsonPropertyName("address")]
    public Address? Address { get; set; }

    [JsonPropertyName("activityDefinitionSubDocument")]
    public required ActivityDefinitionSubDocument ActivityDefinitionSubDocument { get; set; }

    [JsonPropertyName("bookingsNumber")]
    public required int BookingsNumber { get; set; }

    // All zeros except 1*1+1*2
    [JsonPropertyName("attendeesNumber")]
    public required int AttendeesNumber { get; set; }

    // All zeros except a few nulls
    [JsonPropertyName("teamsNumber")]
    public int? TeamsNumber { get; set; }

    // All zeros except a few nulls
    [JsonPropertyName("teamsMinSize")]
    public int? TeamsMinSize { get; set; }

    // All zeros except a few nulls
    [JsonPropertyName("teamsMaxSize")]
    public int? TeamsMaxSize { get; set; }

    [JsonPropertyName("ecosystem")]
    public required Id EcosystemObject { get; set; }

    [JsonPropertyName("organization")]
    public required Id OrganizationObject { get; set; }

    [JsonPropertyName("app")]
    public required Id AppObject { get; set; }

    [JsonPropertyName("publishedApps")]
    public required IEnumerable<Id> PublishedAppObjects { get; set; }

    [JsonPropertyName("regions")]
    public IEnumerable<Region>? RegionsNullable { get; set; }

    [JsonPropertyName("activityDefinition")]
    public required Id ActivityDefinitionObject { get; set; }

    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    [JsonPropertyName("createdAt")]
    public required Date CreatedAtObject { get; set; }

    [JsonPropertyName("updatedAt")]
    public required Date UpdatedAtObject { get; set; }
}
