using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Reflection;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace Query.Controllers;

[Route("/{name}")]
[ApiController]
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

        using var reader = new StreamReader(stream);
        var sparqlText = reader.ReadToEnd();

        var sparqlQuery = new SparqlQueryParser().ParseFromString(sparqlText);
        var sparqlClient = new SparqlQueryClient(httpClient, new Uri("https://202510300952apptriplestoretest.azurewebsites.net/sparql"));

        if (sparqlQuery.QueryType == SparqlQueryType.Construct || sparqlQuery.QueryType == SparqlQueryType.Describe || sparqlQuery.QueryType == SparqlQueryType.DescribeAll)
        {
            var result = await sparqlClient.QueryWithResultGraphAsync(sparqlText, ct);

            var frameResourceName = $"Query.Endpoints.{name}.frame.jsonld";
            using var frameStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(frameResourceName);
            if (frameStream is null)
            {
                throw new Exception("Frame resource not found");
            }

            using var frameReader = new StreamReader(frameStream);
            var frameText = frameReader.ReadToEnd();

            var frame = JToken.Parse(frameText);

            return this.Ok(new ResponseContainer
            {
                Graph = result,
                Frame = frame,
            });
        }
        else
        {
            var resultSetResult = await sparqlClient.QueryWithResultSetAsync(sparqlText, ct);
            var resultText = VDS.RDF.Writing.StringWriter.Write(resultSetResult, new SparqlJsonWriter());
            return this.Ok(resultText);
        }
    }
}
