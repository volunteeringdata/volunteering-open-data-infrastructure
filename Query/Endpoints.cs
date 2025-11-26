using System.Reflection;
using System.Text.RegularExpressions;
using VDS.RDF.Parsing;

namespace Query;

public partial class Endpoints
{
    [GeneratedRegex(@"(?<=Query\.Endpoints\.).+(?=\.query\.sparql)")]
    private static partial Regex ResourceNameExtractor { get; }

    internal static ISet<string> Names =>
        Assembly.GetExecutingAssembly().GetManifestResourceNames()
            .Select(name => ResourceNameExtractor.Match(name))
            .Where(match => match.Success)
            .Select(match => match.Value)
            .ToHashSet();

    internal static IDictionary<string, Endpoint> ParameterMapping => new Dictionary<string, Endpoint>
    {
        ["activity_by_id"] = new([
            new("id", "68f792b0bf48f6d5969d0ead")
        ]),
        ["activity_by_location"] = new([
            new("lat", "51.509", XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("lon", "-0.118", XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("within", "10", XmlSpecsHelper.XmlSchemaDataTypeInteger),
        ]),
        ["activity_by_name"] = new([
            new("name", "Childbirth")
        ]),
        ["activity_search"] = new([
            new("query", "biodiversity")
        ]),
        ["describe_entity_by_id"] = new([
            new("id", "68f5406090268edd4a86435a")
        ]),
        ["organisation_by_id"] = new([
            new("id", "68f53f8346b712131f0148e8")
        ]),
        ["organisation_by_location"] = new([
            new("lat", "51.509", XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("lon", "-0.118", XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("within", "10", XmlSpecsHelper.XmlSchemaDataTypeInteger),
        ]),
        ["organisation_by_name"] = new([
            new("name", "green~")
        ]),
        ["organisation_search"] = new([
            new("query", "network")
        ]),
        ["schema_by_class"] = new([
            new("class", "Activity")
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
