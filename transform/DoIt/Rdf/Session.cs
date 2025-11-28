using VDS.RDF.Wrapping;

namespace DoIt.Rdf;

public class Session : GraphWrapperNode
{
    protected Session(INode node, IGraph graph) : base(node, graph) { }

    internal static Session Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Session Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal static Session Create(string uri, IGraph g) => Wrap(g.CreateUriNode(g.UriFactory.Create(Vocabulary.InstanceBaseUri, uri)), g);

    internal Activity Activity { set => this.Overwrite(Vocabulary.SessionActivity, value, Ecosystem.Wrap); }

    internal DateTimeOffset? Due { set => this.OverwriteNullable(Vocabulary.SessionDue, value); }

    internal DateTimeOffset? End { set => this.OverwriteNullable(Vocabulary.SessionEnd, value); }

    internal Uri? ExternalApplyLink { set => this.OverwriteNullable(Vocabulary.SessionApplyLink, value); }

    internal DateTimeOffset? Start { set => this.OverwriteNullable(Vocabulary.SessionStart, value); }

    internal ISet<Location> Locations { get => this.Objects(Vocabulary.SessionLocation, Location.Wrap, Location.Wrap); }

    internal string? Email { set => this.OverwriteNullable(Vocabulary.Email, value); }

    internal string? Phone { set => this.OverwriteNullable(Vocabulary.Phone, value); }
}
