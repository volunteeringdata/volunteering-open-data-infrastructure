using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using VDS.RDF;
using VDS.RDF.JsonLd;
using VDS.RDF.Writing;

namespace Query.Formatters;

internal class JsonLdGraphOutputFormatter : IOutputFormatter
{
    public bool CanWriteResult(OutputFormatterCanWriteContext context) =>
        context.Object is ResponseContainer && context.ContentType == MimeTypesHelper.JsonLd[0];

    public async Task WriteAsync(OutputFormatterWriteContext context)
    {
        var container = (ResponseContainer)context.Object!;

        var datasetWriter = new JsonLdWriter();

        var ts = new TripleStore();
        ts.Add(container.Graph);

        var jsonLdNode = new JsonLdWriter().SerializeStore(ts);
        using var textWriter = new StreamWriter(context.HttpContext.Response.Body);
        using var writer = new JsonTextWriter(textWriter);
        await JsonLdProcessor
                   .Frame(jsonLdNode, container.Frame, new JsonLdProcessorOptions())
                   .WriteToAsync(writer);
    }
}
