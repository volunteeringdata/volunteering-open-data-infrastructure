using System.Reflection;

namespace Query;

public class Endpoints
{
    internal static async Task<string?> Sparql(string name, CancellationToken ct)
    {
        var resourceName = $"Query.Endpoints.{name}.query.sparql";
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        if (stream is null)
        {
            return null;
        }

        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync(ct);
    }
}
