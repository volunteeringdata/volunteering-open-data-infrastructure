namespace DoIt.Models;

public class Activity
{
    #region Calculated convenience properties

    [JsonIgnore]
    public string Id => IdObject.Value;

    [JsonIgnore]
    public DateTimeOffset CreatedAt => CreatedAtObject.Value;

    [JsonIgnore]
    public DateTimeOffset? DueDate => DueDateObject?.Value;

    [JsonIgnore]
    public string Ecosystem => EcosystemObject.Value;

    [JsonIgnore]
    public DateTimeOffset? EndDate => EndDateObject?.Value;

    [JsonIgnore]
    public Uri? MeetingLink => string.IsNullOrWhiteSpace(MeetingLinkString) ? null : new Uri(MeetingLinkString);

    [JsonIgnore]
    public string Organization => OrganizationObject.Value;

    [JsonIgnore]
    public IEnumerable<string> PublishedApps => PublishedAppObjects.Select(a => a.Value);

    [JsonIgnore]
    public IEnumerable<Region> Regions => RegionsNullable ?? [];

    [JsonIgnore]
    public DateTimeOffset? StartDate => StartDateObject?.Value;

    [JsonIgnore]
    public DateTimeOffset UpdatedAt => UpdatedAtObject.Value;

    #endregion

    // __v (seems irrelevant, probably version tracking, some null, some 0, some 1)

    [JsonPropertyName("_id")]
    public required Id IdObject { get; set; }

    // activityDefinition (redundant with subdocument)

    [JsonPropertyName("activityDefinitionSubDocument")]
    public required ActivityDefinitionSubDocument ActivityDefinitionSubDocument { get; set; }

    [JsonPropertyName("address")]
    public Address? Address { get; set; }

    // app (redundant with appSummary.id)

    // All zeros except 1*1+1*2
    [JsonPropertyName("attendeesNumber")]
    public required int AttendeesNumber { get; set; }

    [JsonPropertyName("bookingsNumber")]
    public required int BookingsNumber { get; set; }

    [JsonPropertyName("createdAt")]
    public required Date CreatedAtObject { get; set; }

    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    [JsonPropertyName("dueDate")]
    public Date? DueDateObject { get; set; }

    [JsonPropertyName("ecosystem")]
    public required Id EcosystemObject { get; set; }

    [JsonPropertyName("endDate")]
    public Date? EndDateObject { get; set; }

    [JsonPropertyName("externalApplyLink")]
    public Uri? ExternalApplyLink { get; set; }

    // externalId (all null) TODO: Check

    // externalProvider (all null) TODO: Check

    [JsonPropertyName("isOnline")]
    public bool? IsOnline { get; set; }

    [JsonPropertyName("isVolunteerNumberLimited")]
    public bool? IsVolunteerNumberLimited { get; set; }

    [JsonPropertyName("meetingLink")]
    public string? MeetingLinkString { get; set; }

    [JsonPropertyName("organization")]
    public required Id OrganizationObject { get; set; }

    [JsonPropertyName("publishedApps")]
    public required IEnumerable<Id> PublishedAppObjects { get; set; }

    [JsonPropertyName("regions")]
    public IEnumerable<Region>? RegionsNullable { get; set; }

    [JsonPropertyName("startDate")]
    public Date? StartDateObject { get; set; }

    // All zeros except a few nulls
    [JsonPropertyName("teamsMaxSize")]
    public int? TeamsMaxSize { get; set; }

    // All zeros except a few nulls
    [JsonPropertyName("teamsMinSize")]
    public int? TeamsMinSize { get; set; }

    // All zeros except a few nulls
    [JsonPropertyName("teamsNumber")]
    public int? TeamsNumber { get; set; }

    [JsonPropertyName("updatedAt")]
    public required Date UpdatedAtObject { get; set; }

    [JsonPropertyName("volunteerNumber")]
    public int? VolunteerNumber { get; set; }
}
