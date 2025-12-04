using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using Query.Services;
using Query.Wrapping;
using System.ComponentModel;

namespace Query;

[McpServerToolType]
public sealed class EchoTool
{
    [McpServerTool, Description("Echoes the input back to the client.")]
    public static async ValueTask<string> Echo(McpServer server, QueryService queryService, string message, CancellationToken ct = default)
    {
        var request = new ElicitRequestParams
        {
            Message = "ELICIT",
            RequestedSchema = new ElicitRequestParams.RequestSchema
            {
                Properties = {
                    ["p1"] = new ElicitRequestParams.StringSchema
                    {
                        Description = "p1desc"
                    }
                },
                Required = ["p1"],
            }
        };

        var response = await server.ElicitAsync(request, ct);

        var x = await queryService.ExecuteNamedQueryAsync(EndpointGraph.Instance["schema"], [], ct);

        return "hello " + message + " " + response;
    }
}