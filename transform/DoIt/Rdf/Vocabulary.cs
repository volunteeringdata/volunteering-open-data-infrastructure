namespace DoIt.Rdf;

internal class Vocabulary
{
    internal const string VocabularyBaseUri = "https://id.volunteeringdata.io/schema/";
    internal static Uri InstanceBaseUri => new("https://id.volunteeringdata.io/");

    private static readonly NodeFactory Factory = new();

    // Activity
    internal static IUriNode ActivityApp { get; } = Node("activityApp");
    internal static IUriNode ActivityAttendees { get; } = Node("activityAttendees");
    internal static IUriNode ActivityBookings { get; } = Node("activityBookings");
    internal static IUriNode ActivityCause { get; } = Node("activityCause");
    internal static IUriNode ActivityDeleted { get; } = Node("activityDeleted");
    internal static IUriNode ActivityDue { get; } = Node("activityDue");
    internal static IUriNode ActivityEcosystem { get; } = Node("activityEcosystem");
    internal static IUriNode ActivityEnd { get; } = Node("activityEnd");
    internal static IUriNode ActivityExternalApplyLink { get; } = Node("activityExternalApplyLink");
    internal static IUriNode ActivityIsOnline { get; } = Node("allowsRemoteParticipation");
    internal static IUriNode ActivityIsVolunteerNumberLimited { get; } = Node("activityIsVolunteerNumberLimited");
    internal static IUriNode ActivityMeeting { get; } = Node("activityMeeting");
    internal static IUriNode ActivityOrganisation { get; } = Node("activityOrganisation");
    internal static IUriNode ActivityPublishedApps { get; } = Node("activityPublishedApps");
    internal static IUriNode ActivityRequirement { get; } = Node("activityRequirement");
    internal static IUriNode ActivityStart { get; } = Node("activityStart");
    internal static IUriNode ActivityVolunteers { get; } = Node("activityVolunteers");
    internal static IUriNode ActivityTitle { get; } = Node("activityLabel");
    internal static IUriNode ActivityDescription { get; } = Node("activityDescription");
    internal static IUriNode ActivityType { get; } = Node("activityType");
    internal static IUriNode ActivityEventType { get; } = Node("activityEventType");
    internal static IUriNode ActivityLocation { get; } = Node("activityLocation");
    internal static IUriNode ActivityLocationOption { get; } = Node("activityLocationOption");
    internal static IUriNode ActivityMeasurementUnit { get; } = Node("activityMeasurementUnit");
    internal static IUriNode ActivityRegion { get; } = Node("activityRegion");

    // Application
    internal static IUriNode AppBrandColor { get; } = Node("appBrandColor");
    internal static IUriNode AppEcosystem { get; } = Node("appEcosystem");
    internal static IUriNode AppDescription { get; } = Node("appDescription");
    internal static IUriNode AppLogo { get; } = Node("appLogo");
    internal static IUriNode AppName { get; } = Node("appName");
    internal static IUriNode AppOrganization { get; } = Node("appOrganisation");

    // Location
    internal static IUriNode LocationLabel { get; } = Node("locationLabel");
    internal static IUriNode LocationType { get; } = Node("locationType");
    internal static IUriNode Address { get; } = Node("address");
    internal static IUriNode Latitude { get; } = Node("latitude");
    internal static IUriNode Longitude { get; } = Node("longitude");

    internal static IUriNode MeasurementUnitCategory { get; } = Node("measurementUnitCategory");
    internal static IUriNode MeasurementUnitPluralLabel { get; } = Node("measurementUnitPluralLabel");
    internal static IUriNode MeasurementUnitSingularLabel { get; } = Node("measurementUnitSingularLabel");

    internal static IUriNode OptionDisplayName { get; } = Node("optionDisplayName");
    internal static IUriNode OptionIcon { get; } = Node("optionIcon");
    internal static IUriNode OptionApp { get; } = Node("optionApp");

    // Organisation
    internal static IUriNode OrganizationLocation { get; } = Node("organisationLocation");
    internal static IUriNode OrganizationLogo { get; } = Node("organisationLogo");
    internal static IUriNode OrganizationEmail { get; } = Node("organisationEmail");
    internal static IUriNode OrganizationPhone { get; } = Node("organisationPhone");
    internal static IUriNode OrganizationDeleted { get; } = Node("organisationDeleted");
    internal static IUriNode OrganizationDescription { get; } = Node("organisationDescription");
    internal static IUriNode OrganizationName { get; } = Node("organisationLabel");
    internal static IUriNode OrganizationPurpose { get; } = Node("organisationPurpose");
    internal static IUriNode OrganizationTos { get; } = Node("organisationTos");
    internal static IUriNode OrganizationType { get; } = Node("organisationType");
    internal static IUriNode OrganizationWebsite { get; } = Node("organisationUrl");
    internal static IUriNode OrganizationCause { get; } = Node("organisationCause");

    private static IUriNode Node(string name) => AnyNode($"{VocabularyBaseUri}{name}");

    private static IUriNode AnyNode(string uri) => Factory.CreateUriNode(Factory.UriFactory.Create(uri));
}
