using DoIt.Rdf;
using System.Text.Json;
using Json = DoIt.Json;

var activities = await JsonSerializer.DeserializeAsync<IEnumerable<Json.Activity>>(File.OpenRead("../../../../../data/doit/activities.json"));

var targetGraph = new Graph();

foreach (var sourceActivity in activities)
{
    var targetActivity = Activity.Create(sourceActivity.Id, targetGraph);

    targetActivity.AttendeesNumber = sourceActivity.AttendeesNumber;
}

targetGraph.SaveToFile("../../../../../fuseki/data.ttl");