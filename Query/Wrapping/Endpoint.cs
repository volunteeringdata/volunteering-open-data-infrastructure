using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Wrapping;

namespace Query.Wrapping;

internal class Endpoint : GraphWrapperNode
{
    protected Endpoint(INode node, IGraph graph) : base(node, graph) { }

    internal static Endpoint Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Endpoint Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal string Path { get => this.Singular(Vocabulary.Path, ValueMappings.As<string>); }

    internal ISet<Parameter> Parameters { get => this.Objects(Vocabulary.Parameter, Parameter.Wrap, Parameter.Wrap); }

    internal string Sparql
    {
        get
        {
            var resourceName = $"Query.Endpoints.{Path}.query.sparql";
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName) ??
                throw new Exception($"SPARQL query embedded resource not found for {Path}");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }

    internal string Link => QueryHelpers.AddQueryString(
        $"{Path}.html",
        Parameters.ToDictionary(
            parameter => parameter.Name,
            parameter => parameter.Example));

    public SparqlQueryType QueryType => new SparqlQueryParser().ParseFromString(Sparql).QueryType;

    public JToken? Frame
    {
        get
        {
            var frameResourceName = $"Query.Endpoints.{Path}.frame.jsonld";
            using var frameStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(frameResourceName);
            using var frameReader = frameStream is null ? null : new StreamReader(frameStream);
            var frameText = frameReader?.ReadToEnd();

            return frameText is null ? null : JToken.Parse(frameText);
        }
    }
}
