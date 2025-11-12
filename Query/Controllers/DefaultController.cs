using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace Query.Controllers;

[Route("/{name}.{format?}")]
[ApiController]
[AllowSynchronousIO]
[FormatFilter]
public class DefaultController(HttpClient httpClient) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(string name, CancellationToken ct)
    {
        var resourceName = $"Query.Endpoints.{name}.query.sparql";
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        if (stream is null)
        {
            return this.NotFound();
        }

        var sparqlText = this.NewMethod(name, stream);

        var sparqlQuery = new SparqlQueryParser().ParseFromString(sparqlText);
        // TODO: Extract endpoint uri to config
        var sparqlClient = new SparqlQueryClient(httpClient, new Uri("https://openvolunteeringdata-edd0h6d2dwcaa8br.uksouth-01.azurewebsites.net/sparql"));

        if (sparqlQuery.QueryType == SparqlQueryType.Construct || sparqlQuery.QueryType == SparqlQueryType.Describe || sparqlQuery.QueryType == SparqlQueryType.DescribeAll)
        {
            var result = await sparqlClient.QueryWithResultGraphAsync(sparqlText, ct);

            var frameResourceName = $"Query.Endpoints.{name}.frame.jsonld";
            using var frameStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(frameResourceName);
            using var frameReader = frameStream is null ? null : new StreamReader(frameStream);
            var frameText = frameReader?.ReadToEnd();

            var frame = frameText is null ? null : JToken.Parse(frameText);

            return this.Ok(new ResponseContainer
            {
                Graph = result,
                Frame = frame,
            });
        }
        else
        {
            var resultSetResult = await sparqlClient.QueryWithResultSetAsync(sparqlText, ct);
            return this.Ok(resultSetResult);
        }
    }

    private string NewMethod(string name, Stream stream)
    {
        using var reader = new StreamReader(stream);
        var sparql = new SparqlParameterizedString(reader.ReadToEnd());

        if (Endpoints.TryGetValue(name, out var endpoint))
        {
            foreach (var parameter in endpoint.Parameters)
            {
                var value = Request.Query[parameter.Name].ToString();
                var valueNode = parameter.Type switch
                {
                    NodeType.Literal => new NodeFactory().CreateLiteralNode(value, new Uri(parameter.Datatype)),
                    _ => throw new InvalidOperationException("unknown node type"),
                };

                sparql.SetVariable(parameter.Name, valueNode);
            }
        }

        return sparql.ToString();
    }

    internal static IDictionary<string, Endpoint> Endpoints => new Dictionary<string, Endpoint>
    {
        ["organisation_by_name"] = new([
            new("name")
        ]),
        ["geo_example2"] = new([
            new("lat", datatype: XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("lon", datatype: XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("within", datatype: XmlSpecsHelper.XmlSchemaDataTypeInteger),
        ]),
    };
}
