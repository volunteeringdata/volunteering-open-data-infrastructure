using System.Reflection;
using VDS.RDF.Parsing;

namespace Query;

public class Endpoints
{
    internal static IDictionary<string, Endpoint> ParameterMapping => new Dictionary<string, Endpoint>
    {
        ["activity_by_id"] = new([
            new("id")
        ]),
        ["activity_by_location"] = new([
            new("lat", XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("lon", XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("within", XmlSpecsHelper.XmlSchemaDataTypeInteger),
        ]),
        ["activity_by_name"] = new([
            new("name")
        ]),
        ["activity_search"] = new([
            new("query")
        ]),
        ["organisation_by_id"] = new([
            new("id")
        ]),
        ["organisation_by_location"] = new([
            new("lat", XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("lon", XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("within", XmlSpecsHelper.XmlSchemaDataTypeInteger),
        ]),
        ["organisation_by_name"] = new([
            new("name")
        ]),
        ["organisation_search"] = new([
            new("query")
        ]),
        ["schema_by_class"] = new([
            new("class")
        ]),
    };

    internal static async Task<string?> Sparql(string name, CancellationToken ct)
    {
        var resourceName = $"Query.Endpoints.{name}.query.sparql";
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        if (stream is null)
        {
            return null;
        }

        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync(ct);
    }
}
