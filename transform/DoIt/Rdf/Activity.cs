using VDS.RDF.Wrapping;

namespace DoIt.Rdf;

public class Activity : GraphWrapperNode
{
    protected Activity(INode node, IGraph graph) : base(node, graph) { }

    internal static Activity Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Activity Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal static Activity Create(string uri, IGraph g) => Wrap(g.CreateUriNode(g.UriFactory.Create(Vocabulary.InstanceBaseUri, uri)), g);

    internal int AttendeesNumber { set => this.Overwrite(Vocabulary.AttendeesNumber, value); }

    internal int BookingsNumber { set => this.Overwrite(Vocabulary.BookingsNumber, value); }

    internal DateTimeOffset CreatedAt { set => this.Overwrite(Vocabulary.CreatedAt, value); }

    internal bool Deleted { set => this.Overwrite(Vocabulary.Deleted, value); }

    internal DateTimeOffset? DueDate { set => this.OverwriteNullable(Vocabulary.DueDate, value); }

    internal Ecosystem Ecosystem { set => this.Overwrite(Vocabulary.Ecosystem, value, Ecosystem.Wrap); }

    internal DateTimeOffset? EndDate { set => this.OverwriteNullable(Vocabulary.EndDate, value); }

    internal Uri? ExternalApplyLink { set => this.OverwriteNullable(Vocabulary.ExternalApplyLink, value); }

    internal bool? IsOnline { set => this.OverwriteNullable(Vocabulary.IsOnline, value); }

    internal bool? IsVolunteerNumberLimited { set => this.OverwriteNullable(Vocabulary.IsVolunteerNumberLimited, value); }

    internal Uri? MeetingLink { set => this.OverwriteNullable(Vocabulary.MeetingLink, value); }

    internal Organization Organization { set => this.Overwrite(Vocabulary.Organization, value, Organization.Wrap); }

    internal ISet<Uri> PublishedApps { get => this.Objects(Vocabulary.PublishedApps, NodeMappings.From, ValueMappings.As<Uri>); }

    internal DateTimeOffset? StartDate { set => this.OverwriteNullable(Vocabulary.StartDate, value); }

    internal DateTimeOffset UpdatedAt { set => this.Overwrite(Vocabulary.UpdatedAt, value); }

    internal int? VolunteerNumber { set => this.OverwriteNullable(Vocabulary.VolunteerNumber, value); }
}
