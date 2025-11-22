using Microsoft.AspNetCore.Mvc;
using Query.Services;
using VDS.RDF.Parsing;

namespace Query.Controllers;

[Route("/{name}.{format?}")]
[ApiController]
[AllowSynchronousIO]
[FormatFilter]
public class DefaultController(QueryService someService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(string name, CancellationToken ct)
    {
        if (Endpoints.TryGetValue(name, out var endpoint))
        {
            foreach (var item in endpoint.Parameters.Select(p => p.Name).Where(n => !Request.Query.TryGetValue(n, out _)))
            {
                ModelState.AddModelError(item, "This query string parameter is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        }

        var query = Request.Query.ToDictionary(i => i.Key, i => i.Value.ToString());
        var result = await someService.ExecuteNamedQueryAsync(name, query, ct);
        if (result is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(result);
        }
    }

    internal static IDictionary<string, Endpoint> Endpoints => new Dictionary<string, Endpoint>
    {
        ["activity_by_id"] = new([
            new("id")
        ]),
        ["organisation_by_distance"] = new([
            new("lat", XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("lon", XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("within", XmlSpecsHelper.XmlSchemaDataTypeInteger),
        ]),
        ["organisation_by_distance_json"] = new([
            new("lat", datatype: XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("lon", datatype: XmlSpecsHelper.XmlSchemaDataTypeDouble),
            new("within", datatype: XmlSpecsHelper.XmlSchemaDataTypeInteger),
        ]),
        ["organisation_by_id"] = new([
            new("id")
        ]),
        ["organisation_by_name"] = new([
            new("name")
        ]),
        ["schema_by_class"] = new([
            new("class")
        ]),
        ["organisation_search"] = new([
            new("query")
        ]),
    };
}
