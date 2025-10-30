using DoIt.Rdf;
using System.Text.Json;
using VDS.RDF.Writing;
using Json = DoIt.Json;

var inputJsonFile = Path.Combine(Environment.CurrentDirectory, args[0]);
var outputRdfFile = Path.Combine(Environment.CurrentDirectory, args[1]);

var activities = await JsonSerializer.DeserializeAsync<IEnumerable<Json.Activity>>(File.OpenRead(inputJsonFile));

var targetGraph = new Graph();

foreach (var sourceActivity in activities!)
{
    var targetActivity = Activity.Create(sourceActivity.Id.Value, targetGraph);

    // ActivityDefinitionSubDocument.AppSummary
    // ActivityDefinitionSubDocument.MeasurementUnit
    targetActivity.Title = sourceActivity.ActivityDefinitionSubDocument.Title;
    targetActivity.Description = sourceActivity.ActivityDefinitionSubDocument.Description;
    targetActivity.Type = sourceActivity.ActivityDefinitionSubDocument.Type;
    targetActivity.EventType = sourceActivity.ActivityDefinitionSubDocument.EventType;
    // ActivityDefinitionSubDocument.Causes
    // ActivityDefinitionSubDocument.Requirements
    targetActivity.LocationOption = sourceActivity.ActivityDefinitionSubDocument.LocationOption;
    // ActivityDefinitionSubDocument.Organization
    // Address
    targetActivity.AttendeesNumber = sourceActivity.AttendeesNumber;
    targetActivity.BookingsNumber = sourceActivity.BookingsNumber;
    targetActivity.Deleted = sourceActivity.Deleted;
    targetActivity.DueDate = sourceActivity.DueDate?.Value;
    targetActivity.Ecosystem = Ecosystem.Create(sourceActivity.Ecosystem.Value, targetGraph);
    targetActivity.EndDate = sourceActivity.DueDate?.Value;
    targetActivity.ExternalApplyLink = sourceActivity.ExternalApplyLink;
    targetActivity.IsOnline = sourceActivity.IsOnline;
    targetActivity.IsVolunteerNumberLimited = sourceActivity.IsVolunteerNumberLimited;
    targetActivity.MeetingLink = sourceActivity.MeetingLink;
    targetActivity.Organization = Organization.Create(sourceActivity.Organization.Value, targetGraph);
    targetActivity.PublishedApps.UnionWith(sourceActivity.PublishedApp.Select(a => new Uri(Vocabulary.InstanceBaseUri, a.Value)));
    // Regions
    targetActivity.StartDate = sourceActivity.StartDate?.Value;
    targetActivity.VolunteerNumber = sourceActivity.VolunteerNumber;

}

var turtleWriter = new CompressingTurtleWriter();
turtleWriter.DefaultNamespaces.AddNamespace("id", Vocabulary.InstanceBaseUri);
turtleWriter.DefaultNamespaces.AddNamespace("", new Uri(Vocabulary.VocabularyBaseUri));
turtleWriter.Save(targetGraph, outputRdfFile);
