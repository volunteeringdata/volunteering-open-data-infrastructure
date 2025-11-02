using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi;
using System.Text;

namespace Query.Formatters;

internal class OpenApiFormatter : TextOutputFormatter
{
    public OpenApiFormatter()
    {
        SupportedMediaTypes.Add("application/json");
        SupportedEncodings.Add(Encoding.UTF8);
    }

    protected override bool CanWriteType(Type? type) => type!.IsAssignableFrom(typeof(OpenApiDocument));

    public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding) =>
        ((OpenApiDocument)context.Object!).SerializeAsJsonAsync(context.HttpContext.Response.Body, OpenApiSpecVersion.OpenApi3_1);
}
