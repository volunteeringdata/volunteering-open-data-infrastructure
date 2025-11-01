using Query.Formatters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;

    // So we don't get default JSON serializations of things
    options.OutputFormatters.Clear();

    options.OutputFormatters.Insert(0, new SparqlResultSetFormatter());
    options.OutputFormatters.Insert(0, new GraphOutputFormatter());

    // Added last so ends up in position 0 for highest priority
    options.OutputFormatters.Insert(0, new JsonLdGraphOutputFormatter());
});

builder.Services.AddHttpClient();

var app = builder.Build();
app.MapControllers();
app.Run();
