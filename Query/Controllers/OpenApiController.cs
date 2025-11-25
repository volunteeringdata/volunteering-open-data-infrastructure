using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using System.Reflection;
using System.Text.RegularExpressions;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace Query.Controllers;

[Route("/openapi.json")]
[ApiController]
[AllowSynchronousIO]
public partial class OpenApiController() : ControllerBase
{
    private const string graphResponse = "graphResponse";
    private const string nonGraphResponse = "nonGraphResponse";

    [GeneratedRegex(@"(?<=Query\.Endpoints\.).+(?=\.query\.sparql)")]
    private static partial Regex ResourceNameExtractor { get; }

    [HttpGet]
    public OpenApiDocument Get() => new()
    {
        Info = new()
        {
            Title = "OpenVolunteering Hackathon API", // TODO: Don't hardcode
            Version = "1" // TODO: Don't hardcode
        },
        Paths = Paths,
        Components = new OpenApiComponents
        {
            Responses = new Dictionary<string, IOpenApiResponse>
            {
                [graphResponse] = new OpenApiResponse
                {
                    Description = "OK",
                    Content = MimeTypesHelper.Definitions
                        .Where(d => d.CanWriteRdfDatasets || d.CanWriteRdf)
                        .Select(d => d.CanonicalMimeType)
                        .Distinct()
                        .ToDictionary(d => d, d => new OpenApiMediaType()),
                },
                [nonGraphResponse] = new OpenApiResponse
                {
                    Description = "OK",
                    Content = MimeTypesHelper.Definitions
                        .Where(d => d.CanWriteSparqlResults)
                        .Select(d => d.CanonicalMimeType)
                        .Distinct()
                        .ToDictionary(d => d, d => new OpenApiMediaType { }),
                },
            },
        },
    };


    private static OpenApiPaths Paths
    {
        get
        {
            var paths = new OpenApiPaths();

            var endpointNames = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .Select(name => ResourceNameExtractor.Match(name))
                .Where(match => match.Success)
                .Select(match => match.Value)
                .Distinct();

            foreach (var endpointName in endpointNames)
            {
                DefaultController.Endpoints.TryGetValue(endpointName, out var endpoint);

                var resourceName = $"Query.Endpoints.{endpointName}.query.sparql";
                using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
                using var reader = new StreamReader(stream!);
                var sparqlText = reader.ReadToEnd();
                var sparqlQuery = new SparqlQueryParser().ParseFromString(sparqlText);
                var responses = new OpenApiResponses
                {
                    ["200"] = sparqlQuery.QueryType switch
                    {
                        SparqlQueryType.Construct or
                        SparqlQueryType.Describe or
                        SparqlQueryType.DescribeAll => new OpenApiResponseReference(graphResponse),

                        _ => new OpenApiResponseReference(nonGraphResponse),
                    },
                };

                var pathItem = new OpenApiPathItem
                {
                    Operations = new()
                    {
                        [HttpMethod.Get] = new OpenApiOperation
                        {
                            Description = $"""
                            Underlying SPARQL query:
                            ```sparql
                            {sparqlText}
                            ```
                            """,
                            Parameters = endpoint is null ? [] : [.. endpoint.Parameters.Select(p => new OpenApiParameter {
                                Name = p.Name,
                                In = ParameterLocation.Query,
                                Schema = new OpenApiSchema {
                                    Type = AsJsonSchemaType(p),
                                },
                                Required = true,
                            })],
                            Responses = responses
                        }
                    }
                };

                paths.Add($"/{endpointName}", pathItem);
            }

            return paths;
        }
    }

    private static JsonSchemaType AsJsonSchemaType(Param param) => param.Datatype switch
    {
        XmlSpecsHelper.XmlSchemaDataTypeInteger or
        XmlSpecsHelper.XmlSchemaDataTypeDouble => JsonSchemaType.Number,

        XmlSpecsHelper.XmlSchemaDataTypeString => JsonSchemaType.String,

        _ => throw new Exception($"unknown literal parameter datatype {param.Datatype}")
    };
}
