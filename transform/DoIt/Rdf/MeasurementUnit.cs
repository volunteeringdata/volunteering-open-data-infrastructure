using VDS.RDF.Wrapping;

namespace DoIt.Rdf;

public class MeasurementUnit : GraphWrapperNode
{
    protected MeasurementUnit(INode node, IGraph graph) : base(node, graph) { }

    internal static MeasurementUnit Wrap(INode node, IGraph graph) => new(node, graph);

    internal static MeasurementUnit Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal static MeasurementUnit Create(string uri, IGraph g) => Wrap(g.CreateUriNode(g.UriFactory.Create(Vocabulary.InstanceBaseUri, uri)), g);

    internal string Category { set => this.Overwrite(Vocabulary.MeasurementUnitCategory, value); }

    internal string PluralLabel { set => this.Overwrite(Vocabulary.MeasurementUnitPluralLabel, value); }

    internal string SingularLabel { set => this.Overwrite(Vocabulary.MeasurementUnitSingularLabel, value); }
}
