using Query.Formatters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;

    // So we don't get default JSON serializations of things
    options.OutputFormatters.Clear();

    options.OutputFormatters.Insert(0, new SparqlFormatter());
    options.OutputFormatters.Insert(0, new GraphFormatter());

    // Added last so ends up in position 0 for highest priority
    options.OutputFormatters.Insert(0, new JsonLdFormatter());
});

builder.Services.AddHttpClient();

var app = builder.Build();
app.MapControllers();
app.Run();
