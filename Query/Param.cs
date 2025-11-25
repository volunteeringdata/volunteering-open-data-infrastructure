using VDS.RDF.Parsing;

namespace Query;

internal class Param(
    string name,
    string example,
    string datatype = XmlSpecsHelper.XmlSchemaDataTypeString)
{
    internal string Name => name;

    internal string Datatype => datatype;

    internal string Example => example;
}