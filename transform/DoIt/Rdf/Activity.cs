using VDS.RDF.Wrapping;

namespace DoIt.Rdf;

public class Activity : GraphWrapperNode
{
    protected Activity(INode node, IGraph graph) : base(node, graph) { }

    internal static Activity Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Activity Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal static Activity Create(string uri, IGraph g) => Wrap(g.CreateUriNode(g.UriFactory.Create(Vocabulary.InstanceBaseUri, uri)), g);

    internal int AttendeesNumber
    {
        set => this.Overwrite(Vocabulary.attendeesNumber, value);
    }
}
