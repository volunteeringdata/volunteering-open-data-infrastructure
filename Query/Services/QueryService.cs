using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace Query.Services;

public class QueryService(HttpClient httpClient, IOptions<QueryServiceOptions> options)
{
    private static readonly NodeFactory factory = new();

    private readonly QueryServiceOptions options = options.Value;

    public async Task<object?> ExecuteNamedQueryAsync(string name, Dictionary<string, string> parameters, CancellationToken ct)
    {
        var sparql = await Endpoints.Sparql(name, ct);
        if (sparql is null)
        {
            return null;
        }

        var sparqlText = Parametrize(name, parameters, sparql);
        var sparqlQuery = new SparqlQueryParser().ParseFromString(sparqlText);
        var sparqlClient = new SparqlQueryClient(httpClient, options.SparqlEndpointUri);

        if (sparqlQuery.QueryType == SparqlQueryType.Construct || sparqlQuery.QueryType == SparqlQueryType.Describe || sparqlQuery.QueryType == SparqlQueryType.DescribeAll)
        {
            var result = await sparqlClient.QueryWithResultGraphAsync(sparqlText, ct);

            var frameResourceName = $"Query.Endpoints.{name}.frame.jsonld";
            using var frameStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(frameResourceName);
            using var frameReader = frameStream is null ? null : new StreamReader(frameStream);
            var frameText = frameReader?.ReadToEnd();

            var frame = frameText is null ? null : JToken.Parse(frameText);

            return new ResponseContainer
            {
                Graph = result,
                Frame = frame,
            };
        }
        else
        {
            var resultSetResult = await sparqlClient.QueryWithResultSetAsync(sparqlText, ct);
            return resultSetResult;
        }
    }

    private static string Parametrize(string name, Dictionary<string, string> parameters, string sparqlText)
    {
        var sparql = new SparqlParameterizedString(sparqlText);

        if (Endpoints.ParameterMapping.TryGetValue(name, out var endpoint))
        {
            foreach (var parameter in endpoint.Parameters)
            {
                var value = parameters[parameter.Name];
                var valueNode = factory.CreateLiteralNode(value, new Uri(parameter.Datatype));

                sparql.SetVariable(parameter.Name, valueNode);
            }
        }

        return sparql.ToString();
    }
}
