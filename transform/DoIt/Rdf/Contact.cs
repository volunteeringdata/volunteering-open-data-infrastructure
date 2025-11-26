using VDS.RDF.Wrapping;

namespace DoIt.Rdf;

public class Contact : GraphWrapperNode
{
    protected Contact(INode node, IGraph graph) : base(node, graph) { }

    internal static Contact Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Contact Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal static Contact Create(string uri, IGraph g) => Wrap(g.CreateUriNode(g.UriFactory.Create(Vocabulary.InstanceBaseUri, uri)), g);

    internal string? Email { set => this.OverwriteNullable(Vocabulary.Email, value); }

    internal string? Phone { set => this.OverwriteNullable(Vocabulary.Phone, value); }
}
