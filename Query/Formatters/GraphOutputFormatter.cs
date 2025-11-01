using Microsoft.AspNetCore.Mvc.Formatters;
using VDS.RDF;

namespace Query.Formatters;

internal class GraphOutputFormatter : IOutputFormatter
{
    public bool CanWriteResult(OutputFormatterCanWriteContext context) =>
        context.Object is ResponseContainer && context.ContentType != MimeTypesHelper.JsonLd[0];

    public async Task WriteAsync(OutputFormatterWriteContext context)
    {
        using var streamWriter = new StreamWriter(context.HttpContext.Response.Body);

        var graph = ((ResponseContainer)context.Object!).Graph;

        var datasetWriter = MimeTypesHelper
            .GetDefinitions(context.ContentType.ToString())
            .Where(static definition => definition.CanWriteRdfDatasets)
            .Select(static definition => definition.GetRdfDatasetWriter());

        if (datasetWriter.Any())
        {
            var ts = new TripleStore();
            ts.Add(graph);

            datasetWriter.First().Save(ts, streamWriter);
            return;
        }

        var rdfWriter = MimeTypesHelper
            .GetDefinitions([context.ContentType.ToString(), .. MimeTypesHelper.Turtle])
            .First(static definition => definition.CanWriteRdf)
            .GetRdfWriter();

        rdfWriter.Save(graph, streamWriter);
    }
}
