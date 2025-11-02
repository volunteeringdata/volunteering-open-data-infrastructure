using VDS.RDF;
using VDS.RDF.Parsing;

namespace Query;

internal class Param(
    string name,
    NodeType type = NodeType.Literal,
    string datatype = XmlSpecsHelper.XmlSchemaDataTypeString)
{
    internal string Name => name;

    internal NodeType Type => type;

    internal string Datatype => datatype;
}