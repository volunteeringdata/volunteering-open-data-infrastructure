using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using VDS.RDF;

namespace Query.Formatters;

internal class GraphOutputFormatter : TextOutputFormatter
{
    public GraphOutputFormatter()
    {
        var writers = MimeTypesHelper.Definitions
           .Where(static definition => definition.CanWriteRdfDatasets || definition.CanWriteRdf)
           .Select(static definition => definition.CanonicalMimeType)
           .Distinct();


        foreach (var mime in writers)
        {
            SupportedMediaTypes.Add(mime);
        }

        SupportedEncodings.Add(Encoding.UTF8);
    }

    protected override bool CanWriteType(Type? type) => type!.IsAssignableFrom(typeof(ResponseContainer));

    public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        using var streamWriter = new StreamWriter(context.HttpContext.Response.Body);

        var graph = ((ResponseContainer)context.Object!).Graph;

        var datasetWriter = MimeTypesHelper
            .GetDefinitions(context.ContentType.ToString())
            .Where(static definition => definition.CanWriteRdfDatasets)
            .Select(static definition => definition.GetRdfDatasetWriter())
            .FirstOrDefault();

        if (datasetWriter is not null)
        {
            var ts = new TripleStore();
            ts.Add(graph);

            datasetWriter.Save(ts, streamWriter);
        }
        else
        {
            var rdfWriter = MimeTypesHelper
                .GetDefinitions([context.ContentType.ToString(), .. MimeTypesHelper.Turtle])
                .First(static definition => definition.CanWriteRdf)
                .GetRdfWriter();

            rdfWriter.Save(graph, streamWriter);
        }

        return Task.CompletedTask;
    }
}
