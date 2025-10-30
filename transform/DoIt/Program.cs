using DoIt.Rdf;
using System.Text.Json;
using Json = DoIt.Json;

var inputJsonFile = Path.Combine(Environment.CurrentDirectory, args[0]);
var outputRdfFile = Path.Combine(Environment.CurrentDirectory, args[1]);

var activities = await JsonSerializer.DeserializeAsync<IEnumerable<Json.Activity>>(File.OpenRead(inputJsonFile));

var targetGraph = new Graph();

foreach (var sourceActivity in activities)
{
    var targetActivity = Activity.Create(sourceActivity.Id, targetGraph);

    targetActivity.AttendeesNumber = sourceActivity.AttendeesNumber;
}

targetGraph.SaveToFile(outputRdfFile);
