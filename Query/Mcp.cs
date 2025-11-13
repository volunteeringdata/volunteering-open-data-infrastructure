using ModelContextProtocol.Server;
using Query.Services;
using System.ComponentModel;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Writing;
using StringWriter = VDS.RDF.Writing.StringWriter;

namespace Query;

[McpServerToolType]
public static class Mcp
{
    private static readonly IRdfWriter graphWriter = new CompressingTurtleWriter();
    private static readonly ISparqlResultsWriter sparqlWriter = new SparqlHtmlWriter();

    [McpServerTool]
    [Description("Searches for an organisation by name")]
    public async static Task<string> OrganisationByName(
        QueryService query,
        [Description("The name of the organisation to search for")]
        string organisationName,
        CancellationToken ct
    ) => Serialize(await query.ExecuteNamedQueryAsync(
            "organisation_by_name",
            new()
            {
                ["name"] = organisationName
            },
            ct));

    [McpServerTool]
    [Description("Retrieves the details of an organisation based on its identifier")]
    public async static Task<string> OrganisationById(
        QueryService query,
        [Description("The identifier of the organisation to retrieve details for. This identifier is the local part of the URI of the organisation, e.g. for an organisation with URI http://example.com/1234, the identifier would be 1234")]
        string organisationId,
        CancellationToken ct
    ) => Serialize(await query.ExecuteNamedQueryAsync(
            "organisation_by_id",
            new()
            {
                ["id"] = organisationId
            },
            ct));

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
