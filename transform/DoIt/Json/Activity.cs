namespace DoIt.Json;

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

    [JsonPropertyName("createdAt")] public required Date CreatedAt { get; set; }

    [JsonPropertyName("deleted")] public required bool Deleted { get; set; }

    [JsonPropertyName("dueDate")] public Date? DueDate { get; set; }

    [JsonPropertyName("ecosystem")] public required Id Ecosystem { get; set; }

    [JsonPropertyName("endDate")] public Date? EndDate { get; set; }

    [JsonPropertyName("externalApplyLink")] public Uri? ExternalApplyLink { get; set; }

    // externalId (all null) TODO: Check

    // externalProvider (all null) TODO: Check

    [JsonPropertyName("isOnline")] public bool? IsOnline { get; set; }

    [JsonPropertyName("isVolunteerNumberLimited")] public bool? IsVolunteerNumberLimited { get; set; }

    [JsonPropertyName("meetingLink")] public string? MeetingLinkString { get; set; }
    [JsonIgnore] public Uri? MeetingLink => string.IsNullOrWhiteSpace(MeetingLinkString) ? null : new Uri(MeetingLinkString);

    [JsonPropertyName("organization")] public required Id Organization { get; set; }

    [JsonPropertyName("publishedApps")] public required IEnumerable<Id> PublishedAppObjects { get; set; }
    [JsonIgnore] public IEnumerable<string> PublishedApps => PublishedAppObjects.Select(a => a.Value);

    [JsonIgnore] public IEnumerable<Region> Regions => RegionsNullable ?? [];
    [JsonPropertyName("regions")] public IEnumerable<Region>? RegionsNullable { get; set; }

    [JsonPropertyName("startDate")] public Date? StartDate { get; set; }

    // All zeros except a few nulls
    [JsonPropertyName("teamsMaxSize")] public int? TeamsMaxSize { get; set; }

    // All zeros except a few nulls
    [JsonPropertyName("teamsMinSize")] public int? TeamsMinSize { get; set; }

    // All zeros except a few nulls
    [JsonPropertyName("teamsNumber")] public int? TeamsNumber { get; set; }

    [JsonPropertyName("updatedAt")] public required Date UpdatedAt { get; set; }

    [JsonPropertyName("volunteerNumber")] public int? VolunteerNumber { get; set; }
}
