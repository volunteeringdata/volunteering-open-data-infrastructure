using VDS.RDF;

namespace Query;

internal class Vocabulary
{
    internal const string VocabularyBaseUri = "urn:named-query:";

    private static readonly NodeFactory Factory = new();

    internal static IUriNode Path { get; } = Node("path");
    internal static IUriNode Parameter { get; } = Node("parameter");
    internal static IUriNode Name { get; } = Node("name");
    internal static IUriNode Example { get; } = Node("example");
    internal static IUriNode Datatype { get; } = Node("datatype");

    private static IUriNode Node(string name) => AnyNode($"{VocabularyBaseUri}{name}");

    private static IUriNode AnyNode(string uri) => Factory.CreateUriNode(Factory.UriFactory.Create(uri));
}
