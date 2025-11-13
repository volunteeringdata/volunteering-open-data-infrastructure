using Newtonsoft.Json.Linq;
using Query.Controllers;
using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace Query.Services;

public class QueryService(HttpClient httpClient)
{
    public async Task<object?> ExecuteNamedQueryAsync(string name, Dictionary<string, string> parameters, CancellationToken ct)
    {
        var resourceName = $"Query.Endpoints.{name}.query.sparql";
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        if (stream is null)
        {
            return null;
        }

        var sparqlText = Parametrize(name, parameters, stream);

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

    private static string Parametrize(string name, Dictionary<string, string> parameters, Stream stream)
    {
        using var reader = new StreamReader(stream);
        var sparql = new SparqlParameterizedString(reader.ReadToEnd());

        if (DefaultController.Endpoints.TryGetValue(name, out var endpoint))
        {
            foreach (var parameter in endpoint.Parameters)
            {
                var value = parameters[parameter.Name];
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
}
