using Microsoft.AspNetCore.Mvc.Formatters;
using VDS.RDF;
using VDS.RDF.Query;

namespace Query.Formatters;

internal class SparqlResultSetFormatter : IOutputFormatter
{
    public bool CanWriteResult(OutputFormatterCanWriteContext context) =>
        context.Object is SparqlResultSet;

    public async Task WriteAsync(OutputFormatterWriteContext context)
    {
        var writer = MimeTypesHelper
            .GetDefinitions([context.ContentType.ToString(), .. MimeTypesHelper.SparqlResults])
            .Where(static definition => definition.CanWriteSparqlResults)
            .Select(static definition => definition.GetSparqlResultsWriter())
            .First();

        var results = (SparqlResultSet)context.Object!;
        using var streamWriter = new StreamWriter(context.HttpContext.Response.Body);
        writer.Save(results, streamWriter);

    }
}
