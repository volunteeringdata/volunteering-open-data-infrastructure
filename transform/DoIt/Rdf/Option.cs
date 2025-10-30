using VDS.RDF.Wrapping;

namespace DoIt.Rdf;

public class Option : GraphWrapperNode
{
    protected Option(INode node, IGraph graph) : base(node, graph) { }

    internal static Option Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Option Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal static Option Create(string uri, IGraph g) => Wrap(g.CreateUriNode(g.UriFactory.Create(Vocabulary.InstanceBaseUri, uri)), g);

    internal string DisplayName { set => this.Overwrite(Vocabulary.OptionDisplayName, value); }

    internal string? Icon { set => this.OverwriteNullable(Vocabulary.OptionIcon, value); }

    internal App App { set => this.Overwrite(Vocabulary.OptionApp, value,App.Wrap); }
}
