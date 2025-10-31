using Microsoft.AspNetCore.Mvc.Formatters;
using VDS.RDF;

namespace Query.Formatters;

internal class GraphOutputFormatter : IOutputFormatter
{
    public bool CanWriteResult(OutputFormatterCanWriteContext context) =>
        context.Object is ResponseContainer && context.ContentType != MimeTypesHelper.JsonLd[0];

    public async Task WriteAsync(OutputFormatterWriteContext context)
    {
        var graph = ((ResponseContainer)context.Object!).Graph;

        var datasetWriter = MimeTypesHelper
            .GetDefinitions(context.ContentType.ToString())
            .Where(static definition => definition.CanWriteRdfDatasets)
            .Select(static definition => definition.GetRdfDatasetWriter());

        if (datasetWriter.Any())
        {
            var ts = new TripleStore();
            ts.Add(graph);

            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            datasetWriter.First().Save(ts, writer);
            await context.HttpContext.Response.BodyWriter.WriteAsync(stream.ToArray());
            return;
        }

        var rdfWriter = MimeTypesHelper
            .GetDefinitions([context.ContentType.ToString(), .. MimeTypesHelper.Turtle])
            .First(static definition => definition.CanWriteRdf)
            .GetRdfWriter();

        using var stream1 = new MemoryStream();
        using var writer1 = new StreamWriter(stream1);

        rdfWriter.Save(graph, writer1);

        await context.HttpContext.Response.BodyWriter.WriteAsync(stream1.ToArray());
    }
}
