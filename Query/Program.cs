using Query.Formatters;
using Query.Services;
using VDS.RDF;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.FormatterMappings.SetMediaTypeMappingForFormat("csv", MimeTypesHelper.Csv[0]);
    options.FormatterMappings.SetMediaTypeMappingForFormat("jsonld", MimeTypesHelper.JsonLd[0]);
    options.FormatterMappings.SetMediaTypeMappingForFormat("ttl", MimeTypesHelper.Turtle[0]);
    options.FormatterMappings.SetMediaTypeMappingForFormat("rdf", MimeTypesHelper.RdfXml[0]);
    options.FormatterMappings.SetMediaTypeMappingForFormat("nt", MimeTypesHelper.NTriples[0]);
    options.FormatterMappings.SetMediaTypeMappingForFormat("html", MimeTypesHelper.Html[0]);

    options.ReturnHttpNotAcceptable = true;

    // So we don't get default JSON serializations of things
    //options.OutputFormatters.Clear();

    options.OutputFormatters.Insert(0, new OpenApiFormatter());

    options.OutputFormatters.Insert(0, new SparqlFormatter());
    options.OutputFormatters.Insert(0, new GraphFormatter());

    // Added last so ends up in position 0 for highest priority
    options.OutputFormatters.Insert(0, new JsonLdFormatter());
});

builder.Services.AddHttpClient();

builder.Services.AddSingleton<QueryService>();

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

var app = builder.Build();

app.MapControllers();
app.UseSwaggerUI(static options => options.SwaggerEndpoint("/openapi.json", "live"));
app.MapMcp("mcp");

app.Run();
