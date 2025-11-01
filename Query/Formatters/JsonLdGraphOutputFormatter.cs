using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Text;
using VDS.RDF;
using VDS.RDF.JsonLd;
using VDS.RDF.Writing;

namespace Query.Formatters;

internal class JsonLdGraphOutputFormatter : TextOutputFormatter
{
    public JsonLdGraphOutputFormatter()
    {
        foreach (var mime in MimeTypesHelper.JsonLd)
        {
            SupportedMediaTypes.Add(mime);
        }

        SupportedEncodings.Add(Encoding.UTF8);
    }

    protected override bool CanWriteType(Type? type) => type!.IsAssignableFrom(typeof(ResponseContainer));

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var container = (ResponseContainer)context.Object!;

        var datasetWriter = new JsonLdWriter();

        var ts = new TripleStore();
        ts.Add(container.Graph);

        var jsonLdNode = new JsonLdWriter().SerializeStore(ts);
        using var textWriter = new StreamWriter(context.HttpContext.Response.Body, selectedEncoding);
        using var writer = new JsonTextWriter(textWriter);
        await JsonLdProcessor
                   .Frame(jsonLdNode, container.Frame, new JsonLdProcessorOptions())
                   .WriteToAsync(writer);
    }
}
