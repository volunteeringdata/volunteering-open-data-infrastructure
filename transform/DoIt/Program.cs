using DoIt.Rdf;
using System.Text.Json;
using Json = DoIt.Json;

var inputJsonFile = Path.Combine(Environment.CurrentDirectory, args[0]);
var outputRdfFile = Path.Combine(Environment.CurrentDirectory, args[1]);

var activities = await JsonSerializer.DeserializeAsync<IEnumerable<Json.Activity>>(File.OpenRead(inputJsonFile));

var targetGraph = new Graph();

foreach (var sourceActivity in activities!)
{
    var targetActivity = Activity.Create(sourceActivity.Id.Value, targetGraph);

    targetActivity.AttendeesNumber = sourceActivity.AttendeesNumber;
    targetActivity.BookingsNumber = sourceActivity.BookingsNumber;
    targetActivity.CreatedAt = sourceActivity.CreatedAt.Value;
    targetActivity.Deleted = sourceActivity.Deleted;
    targetActivity.DueDate = sourceActivity.DueDate?.Value;
    targetActivity.Ecosystem = Ecosystem.Create(sourceActivity.Ecosystem.Value, targetGraph);
    targetActivity.EndDate = sourceActivity.DueDate?.Value;
    targetActivity.ExternalApplyLink = sourceActivity.ExternalApplyLink;
    targetActivity.IsOnline = sourceActivity.IsOnline;
    targetActivity.IsVolunteerNumberLimited = sourceActivity.IsVolunteerNumberLimited;
    targetActivity.MeetingLink = sourceActivity.MeetingLink;
    targetActivity.Organization = Organization.Create(sourceActivity.Organization.Value, targetGraph);
}

targetGraph.SaveToFile(outputRdfFile);
