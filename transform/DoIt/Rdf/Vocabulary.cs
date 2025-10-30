namespace DoIt.Rdf;

internal class Vocabulary
{
    internal const string VocabularyBaseUri = "https://id.example.org/schema/";
    internal static Uri InstanceBaseUri => new("https://id.example.org/");

    private static readonly NodeFactory Factory = new();

    internal static IUriNode attendeesNumber { get; } = Node("attendeesNumber");

    private static IUriNode Node(string name) => AnyNode($"{VocabularyBaseUri}{name}");

    private static IUriNode AnyNode(string uri) => Factory.CreateUriNode(Factory.UriFactory.Create(uri));

}
