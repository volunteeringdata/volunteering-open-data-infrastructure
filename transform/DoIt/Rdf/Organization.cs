using VDS.RDF.Wrapping;

namespace DoIt.Rdf;

public class Organization : GraphWrapperNode
{
    protected Organization(INode node, IGraph graph) : base(node, graph) { }

    internal static Organization Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Organization Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal static Organization Create(string uri, IGraph g) => Wrap(g.CreateUriNode(g.UriFactory.Create(Vocabulary.InstanceBaseUri, uri)), g);

    internal Uri? Logo { set => this.OverwriteNullable(Vocabulary.OrganizationLogo, value); }

    internal bool? Deleted { set => this.OverwriteNullable(Vocabulary.OrganizationDeleted, value); }

    internal string? Description { set => this.OverwriteNullable(Vocabulary.OrganizationDescription, value); }

    internal string Name { set => this.Overwrite(Vocabulary.OrganizationName, value); }

    internal string Purpose { set => this.Overwrite(Vocabulary.OrganizationPurpose, value); }

    internal Uri? Tos { set => this.OverwriteNullable(Vocabulary.OrganizationTos, value); }

    internal string? Type { set => this.OverwriteNullable(Vocabulary.OrganizationType, value); }

    internal Uri? Website { set => this.OverwriteNullable(Vocabulary.OrganizationWebsite, value); }

    internal ISet<Option> Cause { get => this.Objects(Vocabulary.OrganizationCause, Option.Wrap, Option.Wrap); }

    internal ISet<Contact> OrganisationContact { get => this.Objects(Vocabulary.OrganizationContact, Contact.Wrap, Contact.Wrap); }

    internal ISet<Location> Locations { get => this.Objects(Vocabulary.OrganizationLocation, Location.Wrap, Location.Wrap); }
}
