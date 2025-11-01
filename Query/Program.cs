using Query.Formatters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Insert(0, new JsonLdGraphOutputFormatter());
    options.OutputFormatters.Insert(0, new GraphOutputFormatter());
    options.OutputFormatters.Insert(0, new SparqlResultSetFormatter());
});

builder.Services.AddHttpClient();

var app = builder.Build();
app.MapControllers();
app.Run();
