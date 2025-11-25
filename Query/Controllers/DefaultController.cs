using Microsoft.AspNetCore.Mvc;
using Query.Services;

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
        if (Endpoints.ParameterMapping.TryGetValue(name, out var endpoint))
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
}
