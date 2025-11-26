using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using VDS.RDF;

namespace Query.Controllers;

[Route("/context/v1")]
[ApiController]
public class ContextController : ControllerBase
{
    [HttpGet]
    [Produces("application/ld+json")]
    public IResult Get() =>
        Results.Stream(
            Assembly.GetExecutingAssembly().GetManifestResourceStream("Query.Context.v1.jsonld")!,
            MimeTypesHelper.JsonLd[0]);
}
