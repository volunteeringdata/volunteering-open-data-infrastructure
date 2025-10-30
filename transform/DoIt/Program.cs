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

    targetActivity.App = App.Create(sourceActivity.ActivityDefinitionSubDocument.App.Id.Value, targetGraph);
    targetActivity.App.BrandColor = sourceActivity.ActivityDefinitionSubDocument.App.BrandColor;
    targetActivity.App.Description = sourceActivity.ActivityDefinitionSubDocument.App.Description;
    targetActivity.App.Ecosystem = Ecosystem.Create(sourceActivity.ActivityDefinitionSubDocument.App.Ecosystem.Id.Value, targetGraph);
    targetActivity.App.Logo = sourceActivity.ActivityDefinitionSubDocument.App.Logo;
    targetActivity.App.Name = sourceActivity.ActivityDefinitionSubDocument.App.Name;
    targetActivity.App.Organization = Organization.Create(sourceActivity.ActivityDefinitionSubDocument.App.Organization.Id.Value, targetGraph);
    targetActivity.MeasurementUnit = MeasurementUnit.Create(sourceActivity.ActivityDefinitionSubDocument.MeasurementUnit.Id.Value, targetGraph);
    targetActivity.MeasurementUnit.Category = sourceActivity.ActivityDefinitionSubDocument.MeasurementUnit.Category;
    targetActivity.MeasurementUnit.PluralLabel = sourceActivity.ActivityDefinitionSubDocument.MeasurementUnit.PluralLabel;
    targetActivity.MeasurementUnit.SingularLabel = sourceActivity.ActivityDefinitionSubDocument.MeasurementUnit.SingularLabel;
    targetActivity.Title = sourceActivity.ActivityDefinitionSubDocument.Title;
    targetActivity.Description = sourceActivity.ActivityDefinitionSubDocument.Description;
    targetActivity.Type = sourceActivity.ActivityDefinitionSubDocument.Type;
    targetActivity.EventType = sourceActivity.ActivityDefinitionSubDocument.EventType;
    targetActivity.Causes.UnionWith(sourceActivity.ActivityDefinitionSubDocument.Causes.Select(c =>
    {
        var option = Option.Create(c.Id.Value, targetGraph);
        option.DisplayName = c.DisplayName;
        option.Icon = c.Icon;
        option.App = App.Create(sourceActivity.ActivityDefinitionSubDocument.App.Id.Value, targetGraph);
        return option;
    }));
    // ActivityDefinitionSubDocument.Causes
    // ActivityDefinitionSubDocument.Requirements
    targetActivity.LocationOption = sourceActivity.ActivityDefinitionSubDocument.LocationOption;
    // ActivityDefinitionSubDocument.Organization
    // Address
    targetActivity.Attendees = sourceActivity.AttendeesNumber;
    targetActivity.Bookings = sourceActivity.BookingsNumber;
    targetActivity.Deleted = sourceActivity.Deleted;
    targetActivity.Due = sourceActivity.DueDate?.Value;
    targetActivity.Ecosystem = Ecosystem.Create(sourceActivity.Ecosystem.Value, targetGraph);
    targetActivity.End = sourceActivity.DueDate?.Value;
    targetActivity.ExternalApplyLink = sourceActivity.ExternalApplyLink;
    targetActivity.IsOnline = sourceActivity.IsOnline;
    targetActivity.IsVolunteerNumberLimited = sourceActivity.IsVolunteerNumberLimited;
    targetActivity.MeetingLink = sourceActivity.MeetingLink;
    targetActivity.Organization = Organization.Create(sourceActivity.Organization.Value, targetGraph);
    targetActivity.PublishedApps.UnionWith(sourceActivity.PublishedApp.Select(a => new Uri(Vocabulary.InstanceBaseUri, a.Value)));
    // Regions
    targetActivity.Start = sourceActivity.StartDate?.Value;
    targetActivity.Volunteers = sourceActivity.VolunteerNumber;

}

var turtleWriter = new CompressingTurtleWriter();
turtleWriter.DefaultNamespaces.AddNamespace("id", Vocabulary.InstanceBaseUri);
turtleWriter.DefaultNamespaces.AddNamespace("", new Uri(Vocabulary.VocabularyBaseUri));
turtleWriter.Save(targetGraph, outputRdfFile);
