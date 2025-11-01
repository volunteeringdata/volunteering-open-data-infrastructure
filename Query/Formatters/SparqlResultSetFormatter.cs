using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using VDS.RDF;
using VDS.RDF.Query;

namespace Query.Formatters;

internal class SparqlResultSetFormatter : TextOutputFormatter
{
    public SparqlResultSetFormatter()
    {
        var writers = MimeTypesHelper.Definitions
           .Where(static definition => definition.CanWriteSparqlResults)
           .Select(static definition => definition.CanonicalMimeType)
           .Distinct();

        foreach (var mime in writers)
        {
            SupportedMediaTypes.Add(mime);
        }

        SupportedEncodings.Add(Encoding.UTF8);
    }

    protected override bool CanWriteType(Type? type) => type!.IsAssignableFrom(typeof(SparqlResultSet));

    public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var writer = MimeTypesHelper
            .GetDefinitions([context.ContentType.ToString(), .. MimeTypesHelper.SparqlResults])
            .Where(static definition => definition.CanWriteSparqlResults)
            .Select(static definition => definition.GetSparqlResultsWriter())
            .First();

        var results = (SparqlResultSet)context.Object!;
        using var streamWriter = new StreamWriter(context.HttpContext.Response.Body);
        writer.Save(results, streamWriter);
        return Task.CompletedTask;
    }
}
