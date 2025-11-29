using VDS.RDF.Nodes;
using VDS.RDF.Parsing;

namespace VDS.RDF.Wrapping;

public static class Extensions
{
    public static GraphWrapperNode In(this INode node, IGraph graph) => node switch
    {
        GraphWrapperNode { Graph: var otherGraph } nodeWithGraph when ReferenceEquals(otherGraph, graph) => nodeWithGraph,
        _ => new(node, graph),
    };

    public static IEnumerable<GraphWrapperNode> In(this IEnumerable<INode> nodes, IGraph graph) => nodes.Select(node => node.In(graph));

    public static object? AsObject(this INode node) =>
        node.AsValuedNode() switch
        {
            null => null,
            IUriNode uriNode => uriNode.Uri,
            DoubleNode doubleNode => doubleNode.AsDouble(),
            FloatNode floatNode => floatNode.AsFloat(),
            DecimalNode decimalNode => decimalNode.AsDecimal(),
            BooleanNode booleanNode => booleanNode.AsBoolean(),
            DateTimeNode dateTimeNode => dateTimeNode.AsDateTimeOffset(),
            TimeSpanNode timeSpanNode => timeSpanNode.AsTimeSpan(),
            NumericNode numericNode => numericNode.AsInteger(),
            StringNode stringNode when stringNode.DataType.AbsoluteUri.Equals(XmlSpecsHelper.XmlSchemaDataTypeString) => stringNode.AsString(),
            _ => node,
        };
}

