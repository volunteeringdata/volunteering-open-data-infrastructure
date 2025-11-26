using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

var baseUri302 = builder.Configuration.GetValue<string>("BaseUri302");
var baseUri303 = builder.Configuration.GetValue<string>("BaseUri303");
if (string.IsNullOrEmpty(baseUri302) || string.IsNullOrEmpty(baseUri303))
{
    throw new InvalidOperationException("BaseUri302 and BaseUri303 must be set in configuration.");
}

var app = builder.Build();

var redirectOptions = new RewriteOptions();
redirectOptions.AddRedirect(@"(.+)\.(.+)", baseUri302, 302);
redirectOptions.AddRedirect(".+", baseUri303, 303);

app.UseRewriter(redirectOptions);

app.MapGet("/", () => "Volunteering Data Dereference service");

app.Run();
