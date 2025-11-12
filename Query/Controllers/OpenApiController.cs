using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace Query.Controllers;

[Route("/openapi.json")]
[ApiController]
[AllowSynchronousIO]
public class OpenApiController() : ControllerBase
{
    [HttpGet]
    public OpenApiDocument Get() => new()
    {
        Info = new()
        {
            Title = "OpenVolunteering Hackathon API", // TODO: Don't hardcode
            Version = "1" // TODO: Don't hardcode
        },
        Paths = Paths,
    };


    private static OpenApiPaths Paths
    {
        get
        {
            var paths = new OpenApiPaths();

            foreach (var endpoint in DefaultController.Endpoints)
            {
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
                            Responses = {
                                ["200"] = new OpenApiResponse {
                                    Description = "" // TODO: Take from endpoint definition
                                },
                            }
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
