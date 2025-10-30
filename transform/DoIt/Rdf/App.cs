using VDS.RDF.Wrapping;

namespace DoIt.Rdf;

public class App : GraphWrapperNode
{
    protected App(INode node, IGraph graph) : base(node, graph) { }

    internal static App Wrap(INode node, IGraph graph) => new(node, graph);

    internal static App Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal static App Create(string uri, IGraph g) => Wrap(g.CreateUriNode(g.UriFactory.Create(Vocabulary.InstanceBaseUri, uri)), g);

    internal string? BrandColor { set => this.OverwriteNullable(Vocabulary.AppBrandColor, value); }

    internal string Description { set => this.Overwrite(Vocabulary.AppDescription, value); }

    internal Ecosystem Ecosystem { set => this.Overwrite(Vocabulary.AppEcosystem, value, Ecosystem.Wrap); }

    internal Uri Logo { set => this.Overwrite(Vocabulary.AppLogo, value); }
   
    internal string Name{ set => this.Overwrite(Vocabulary.AppName, value); }

    internal Organization Organization { set => this.Overwrite(Vocabulary.AppOrganization, value, Organization.Wrap); }
}
