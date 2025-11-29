using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Wrapping;

namespace Query.Wrapping;

internal class EndpointGraph : WrapperGraph
{
    internal static EndpointGraph Instance { get; } = new EndpointGraph(new Graph());

    static EndpointGraph()
    {
        using var rdf = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Query.Endpoints.definition.ttl"));
        new TurtleParser().Load(Instance, rdf);
    }

    private EndpointGraph(IGraph g) : base(g) { }

    internal IEnumerable<Endpoint> Endpoints => GetTriplesWithPredicate(Vocabulary.Path)
        .Select(t => t.Subject)
        .Select(s => s.In(this))
        .Select(Endpoint.Wrap);

    internal Endpoint? this[string path] => Endpoints.SingleOrDefault(e => e.Path == path);
}
