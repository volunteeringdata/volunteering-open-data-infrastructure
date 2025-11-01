using Newtonsoft.Json.Linq;
using VDS.RDF;

namespace Query;

internal class ResponseContainer
{
    internal required JToken? Frame { get; set; }

    internal required IGraph Graph { get; set; }
}