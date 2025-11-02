using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

namespace Query.Controllers;

[Route("/openapi.json")]
[ApiController]
[AllowSynchronousIO]
public class OpenApiController() : ControllerBase
{
    [HttpGet]
    public OpenApiDocument Get() => new()
    {
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
                                Name = p.Name
                            } as IOpenApiParameter)],
                        }
                    }
                };

                paths.Add($"/{endpoint.Key}", pathItem);
            }

            return paths;
        }
    }
}
