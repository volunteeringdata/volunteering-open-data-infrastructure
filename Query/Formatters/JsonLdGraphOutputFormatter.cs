using Microsoft.AspNetCore.Mvc.Formatters;
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
        var framingResult = JsonLdProcessor
                   .Frame(jsonLdNode, container.Frame, new JsonLdProcessorOptions())
                   .ToString();


        var bytes = System.Text.Encoding.UTF8.GetBytes(framingResult);

        await context.HttpContext.Response.BodyWriter.WriteAsync(bytes);
    }
}
