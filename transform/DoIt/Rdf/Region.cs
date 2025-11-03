using VDS.RDF.Wrapping;

namespace DoIt.Rdf;

public class Region : GraphWrapperNode
{
    protected Region(INode node, IGraph graph) : base(node, graph) { }

    internal static Region Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Region Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal static Region Create(string uri, IGraph g) => Wrap(g.CreateUriNode(g.UriFactory.Create(Vocabulary.InstanceBaseUri, uri)), g);

    internal string DisplayName { set => this.Overwrite(Vocabulary.RegionDisplayName, value); }

    internal string Type { set => this.Overwrite(Vocabulary.RegionType, value); }

    internal string? RelatedTo { set => this.OverwriteNullable(Vocabulary.RegionRelatedTo, value); }

    internal double? Latitude { set => this.OverwriteNullable(Vocabulary.Latitude, value); }

    internal double? Longitude { set => this.OverwriteNullable(Vocabulary.Longitude, value); }
}
