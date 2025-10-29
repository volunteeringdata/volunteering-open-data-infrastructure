namespace DoIt.Models;

public class Activity : Identified
{
    // __v (seems irrelevant, probably version tracking, some null, some 0, some 1)

    // activityDefinition (redundant with subdocument)

    [JsonPropertyName("activityDefinitionSubDocument")] public required ActivityDefinitionSubDocument ActivityDefinitionSubDocument { get; set; }

    [JsonPropertyName("address")] public Address? Address { get; set; }

    // app (redundant with appSummary.id)

    // All zeros except 1*1+1*2
    [JsonPropertyName("attendeesNumber")] public required int AttendeesNumber { get; set; }

    [JsonPropertyName("bookingsNumber")] public required int BookingsNumber { get; set; }

    [JsonPropertyName("createdAt")] public required Date CreatedAtObject { get; set; }
    [JsonIgnore] public DateTimeOffset CreatedAt => CreatedAtObject.Value;

    [JsonPropertyName("deleted")] public bool? Deleted { get; set; }

    [JsonPropertyName("dueDate")] public Date? DueDateObject { get; set; }
    [JsonIgnore] public DateTimeOffset? DueDate => DueDateObject?.Value;

    [JsonPropertyName("ecosystem")] public required Id EcosystemObject { get; set; }
    [JsonIgnore] public string Ecosystem => EcosystemObject.Value;

    [JsonPropertyName("endDate")] public Date? EndDateObject { get; set; }
    [JsonIgnore] public DateTimeOffset? EndDate => EndDateObject?.Value;

    [JsonPropertyName("externalApplyLink")] public Uri? ExternalApplyLink { get; set; }

    // externalId (all null) TODO: Check

    // externalProvider (all null) TODO: Check

    [JsonPropertyName("isOnline")] public bool? IsOnline { get; set; }

    [JsonPropertyName("isVolunteerNumberLimited")] public bool? IsVolunteerNumberLimited { get; set; }

    [JsonPropertyName("meetingLink")] public string? MeetingLinkString { get; set; }
    [JsonIgnore] public Uri? MeetingLink => string.IsNullOrWhiteSpace(MeetingLinkString) ? null : new Uri(MeetingLinkString);

    [JsonPropertyName("organization")] public required Id OrganizationObject { get; set; }
    [JsonIgnore] public string Organization => OrganizationObject.Value;

    [JsonPropertyName("publishedApps")] public required IEnumerable<Id> PublishedAppObjects { get; set; }
    [JsonIgnore] public IEnumerable<string> PublishedApps => PublishedAppObjects.Select(a => a.Value);

    [JsonIgnore] public IEnumerable<Region> Regions => RegionsNullable ?? [];
    [JsonPropertyName("regions")] public IEnumerable<Region>? RegionsNullable { get; set; }

    [JsonPropertyName("startDate")] public Date? StartDateObject { get; set; }
    [JsonIgnore] public DateTimeOffset? StartDate => StartDateObject?.Value;

    // All zeros except a few nulls
    [JsonPropertyName("teamsMaxSize")] public int? TeamsMaxSize { get; set; }

    // All zeros except a few nulls
    [JsonPropertyName("teamsMinSize")] public int? TeamsMinSize { get; set; }

    // All zeros except a few nulls
    [JsonPropertyName("teamsNumber")] public int? TeamsNumber { get; set; }

    [JsonPropertyName("updatedAt")] public required Date UpdatedAtObject { get; set; }
    [JsonIgnore] public DateTimeOffset UpdatedAt => UpdatedAtObject.Value;

    [JsonPropertyName("volunteerNumber")] public int? VolunteerNumber { get; set; }
}
