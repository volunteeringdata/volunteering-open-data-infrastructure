using VDS.RDF.Parsing;

namespace Query;

internal class Param(
    string name,
    string datatype = XmlSpecsHelper.XmlSchemaDataTypeString)
{
    internal string Name => name;

    internal string Datatype => datatype;
}