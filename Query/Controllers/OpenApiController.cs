using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace Query.Controllers;

[Route("/openapi.json")]
[ApiController]
[AllowSynchronousIO]
public class OpenApiController() : ControllerBase
{
    private const string graphResponse = "graphResponse";
    private const string nonGraphResponse = "nonGraphResponse";

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

            foreach (var endpoint in DefaultController.Endpoints) // TODO: Iterate resources instead or ensure Endpoints has all of them
            {
                var name = endpoint.Key;
                var resourceName = $"Query.Endpoints.{name}.query.sparql";
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
                            Parameters = [.. endpoint.Value.Parameters.Select(p => new OpenApiParameter {
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

                paths.Add($"/{endpoint.Key}", pathItem);
            }

            return paths;
        }
    }

    private static JsonSchemaType AsJsonSchemaType(Param param) => param.Type switch
    {
        NodeType.Literal => param.Datatype switch
        {
            XmlSpecsHelper.XmlSchemaDataTypeInteger or
            XmlSpecsHelper.XmlSchemaDataTypeDouble => JsonSchemaType.Number,

            XmlSpecsHelper.XmlSchemaDataTypeString => JsonSchemaType.String,

            _ => throw new Exception($"unknown literal parameter datatype {param.Datatype}")
        },

        _ => throw new Exception($"unknown parameter type {param.Type}")
    };
}
