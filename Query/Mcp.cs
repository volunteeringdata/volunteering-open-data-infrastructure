using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using Query.Controllers;
using Query.Services;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;
using StringWriter = VDS.RDF.Writing.StringWriter;

namespace Query;

public static partial class Mcp
{
    private static readonly IRdfWriter graphWriter = new CompressingTurtleWriter();
    private static readonly ISparqlResultsWriter sparqlWriter = new SparqlHtmlWriter();

    [GeneratedRegex(@"(?<=Query\.Endpoints\.).+(?=\.query\.sparql)")]
    private static partial Regex ResourceNameExtractor { get; }

    internal static async ValueTask<ListToolsResult> ListTools(RequestContext<ListToolsRequestParams> _, CancellationToken ct) =>
        new()
        {
            Tools = (await Task.WhenAll(Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .Select(static name => ResourceNameExtractor.Match(name))
                .Where(static match => match.Success)
                .Select(static match => match.Value)
                .Select(static name => new
                {
                    Name = name,
                    Parameters = DefaultController.Endpoints.TryGetValue(name, out var endpoint) ?
                        endpoint.Parameters :
                        []
                })
                .Select(async endpoint =>
                {
                    var sparqlText = await Endpoints.Sparql(endpoint.Name, ct);

                    return new Tool
                    {
                        Name = endpoint.Name,
                        Description = $"""
                            Underlying SPARQL query:
                            ```sparql
                            {sparqlText}
                            ```
                            """,
                        InputSchema = System.Text.Json.JsonSerializer.SerializeToElement(new
                        {
                            type = "object",
                            properties = new JsonObject(endpoint.Parameters.ToDictionary(
                                p => p.Name,
                                p => new JsonObject
                                {
                                    ["type"] = AsJsonSchemaType(p.Datatype)
                                } as JsonNode)!),
                        })
                    };
                })))
            .ToList()
        };

    internal static async ValueTask<CallToolResult> CallTool(RequestContext<CallToolRequestParams> context, CancellationToken ct) =>
        new()
        {
            Content = [
                new TextContentBlock {
                    Text = Serialize(
                        await context.Services!.GetRequiredService<QueryService>().ExecuteNamedQueryAsync(
                            context.Params!.Name,
                            context.Params.Arguments!.ToDictionary(
                                x => x.Key,
                                x => x.Value.GetString()!
                            ),
                            ct
                        )
                    )
                }
            ]
        };

    private static string AsJsonSchemaType(string datatype) => datatype switch
    {
        XmlSpecsHelper.XmlSchemaDataTypeInteger or
        XmlSpecsHelper.XmlSchemaDataTypeDouble => "number",

        XmlSpecsHelper.XmlSchemaDataTypeString => "string",

        _ => throw new Exception($"unknown literal parameter datatype {datatype}")
    };

    private static string Serialize(object? result)
    {
        return result switch
        {
            null => throw new Exception("Result was nothing"),
            ResponseContainer container => StringWriter.Write(container.Graph, graphWriter),
            SparqlResultSet sparql => StringWriter.Write(sparql, sparqlWriter),
            _ => throw new Exception($"Unknown result type {result.GetType()}")
        };
    }
}
