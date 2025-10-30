namespace DoIt.Rdf;

internal class Vocabulary
{
    internal const string VocabularyBaseUri = "https://id.example.org/schema/";
    internal static Uri InstanceBaseUri => new("https://id.example.org/");

    private static readonly NodeFactory Factory = new();

    internal static IUriNode AttendeesNumber { get; } = Node("attendeesNumber");
    internal static IUriNode BookingsNumber { get; } = Node("bookingsNumber");
    internal static IUriNode CreatedAt { get; } = Node("createdAt");
    internal static IUriNode Deleted { get; } = Node("deleted");
    internal static IUriNode DueDate { get; } = Node("dueDate");
    internal static IUriNode Ecosystem { get; } = Node("ecosystem");
    internal static IUriNode EndDate { get; } = Node("endDate");
    internal static IUriNode ExternalApplyLink { get; } = Node("externalApplyLink");
    internal static IUriNode IsOnline { get; } = Node("isOnline");
    internal static IUriNode IsVolunteerNumberLimited { get; } = Node("isVolunteerNumberLimited");
    internal static IUriNode MeetingLink { get; } = Node("meetingLink");
    internal static IUriNode Organization { get; } = Node("organization");

    private static IUriNode Node(string name) => AnyNode($"{VocabularyBaseUri}{name}");

    private static IUriNode AnyNode(string uri) => Factory.CreateUriNode(Factory.UriFactory.Create(uri));

}
