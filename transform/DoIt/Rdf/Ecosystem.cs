using VDS.RDF.Wrapping;

namespace DoIt.Rdf;

public class Ecosystem : GraphWrapperNode
{
    protected Ecosystem(INode node, IGraph graph) : base(node, graph) { }

    internal static Ecosystem Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Ecosystem Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal static Ecosystem Create(string uri, IGraph g) => Wrap(g.CreateUriNode(g.UriFactory.Create(Vocabulary.InstanceBaseUri, uri)), g);
}
