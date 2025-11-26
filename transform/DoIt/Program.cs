using DoIt.Rdf;
using System.Text.Json;
using VDS.RDF.Writing;
using Json = DoIt.Json;

var inputJsonFile = Path.Combine(Environment.CurrentDirectory, args[0]);
var outputRdfFile = Path.Combine(Environment.CurrentDirectory, args[1]);

var activities = await JsonSerializer.DeserializeAsync<IEnumerable<Json.Activity>>(File.OpenRead(inputJsonFile));

var targetGraph = new Graph();

foreach (var source in activities!)
{
    var target = Activity.Create(source.Id.Value, targetGraph);

    target.App = App.Create(source.Details.App.Id.Value, targetGraph);
    target.App.BrandColor = source.Details.App.BrandColor;
    target.App.Description = source.Details.App.Description;
    target.App.Ecosystem = Ecosystem.Create(source.Details.App.Ecosystem.Id.Value, targetGraph);
    target.App.Logo = source.Details.App.Logo;
    target.App.Name = source.Details.App.Name;
    target.App.Organization = Organization.Create(source.Details.App.Organization.Id.Value, targetGraph);
    target.App.Organization.Logo = source.Details.App.Organization.Logo;
    target.App.Organization.Name = source.Details.App.Organization.Name;
    target.MeasurementUnit = MeasurementUnit.Create(source.Details.MeasurementUnit.Id.Value, targetGraph);
    target.MeasurementUnit.Category = source.Details.MeasurementUnit.Category;
    target.MeasurementUnit.PluralLabel = source.Details.MeasurementUnit.PluralLabel;
    target.MeasurementUnit.SingularLabel = source.Details.MeasurementUnit.SingularLabel;
    target.Title = source.Details.Title;
    target.Description = source.Details.Description;
    target.Type = source.Details.Type;
    target.EventType = source.Details.EventType;
    target.Cause.UnionWith(source.Details.Causes.Select(c =>
    {
        var option = Option.Create(c.Id.Value, targetGraph);
        option.DisplayName = c.DisplayName;
        option.Icon = c.Icon;
        option.App = App.Create(source.Details.App.Id.Value, targetGraph);
        return option;
    }));
    target.Requirement.UnionWith(source.Details.Requirements.Select(c =>
    {
        var option = Option.Create(c.Id.Value, targetGraph);
        option.DisplayName = c.DisplayName;
        option.Icon = c.Icon;
        option.App = App.Create(source.Details.App.Id.Value, targetGraph);
        return option;
    }));
    //target.LocationOption = source.Details.LocationOption;
    target.Organization = Organization.Create(source.Details.Organization.Id.Value, targetGraph);
    target.Organization.Logo = source.Details.Organization.Logo;
    target.Organization.Cause.UnionWith(source.Details.Organization.Causes.Select(c =>
    {
        var option = Option.Create(c.Id.Value, targetGraph);
        option.DisplayName = c.DisplayName;
        option.Icon = c.Icon;
        option.App = App.Create(source.Details.App.Id.Value, targetGraph);
        return option;
    }));
    target.Organization.Deleted = source.Details.Organization.Deleted;
    target.Organization.Description = source.Details.Organization.Description;
    if (source.Details.Organization.FullAddress is Json.Address fullAddress)
    {
        var location = Location.Create(fullAddress.Id.Value, targetGraph);
        location.Type = "Address";
        location.Address = fullAddress.Street;
        location.Longitude = fullAddress.Location.Coordinates[0];
        location.Latitude = fullAddress.Location.Coordinates[1];
        target.Organization.Locations.Add(location);
    }
    if (source.Details.Organization.ContactEmail is not null || source.Details.Organization.ContactPhoneNumber is not null)
    {
        // TODO: can we make the GUID deterministic based on values of email and phone?
        var contact = Contact.Create(Guid.NewGuid().ToString(), targetGraph);
        contact.Email = source.Details.Organization.ContactEmail;
        contact.Phone = source.Details.Organization.ContactPhoneNumber;
        target.Organization.OrganisationContact.Add(contact);
    }
    target.Organization.Name = source.Details.Organization.Name;
    target.Organization.Purpose = source.Details.Organization.Purpose;
    target.Organization.Tos = source.Details.Organization.TermsOfServicesLink;
    target.Organization.Type = source.Details.Organization.Type;
    target.Organization.Website = source.Details.Organization.WebsiteLink;
    target.Attendees = source.AttendeesNumber;
    target.Bookings = source.BookingsNumber;
    target.Deleted = source.Deleted;
    target.Due = source.DueDate?.Value;
    target.Ecosystem = Ecosystem.Create(source.Ecosystem.Value, targetGraph);
    target.End = source.DueDate?.Value;
    target.ExternalApplyLink = source.ExternalApplyLink;
    target.IsOnline = source.IsOnline;
    target.IsVolunteerNumberLimited = source.IsVolunteerNumberLimited;
    target.Meeting = source.MeetingLink;
    target.PublishedApps.UnionWith(source.PublishedApp.Select(a => new Uri(Vocabulary.InstanceBaseUri, a.Value)));
    target.Locations.UnionWith(source.Regions.Select(r =>
    {
        var location = Location.Create(r.Id.Value, targetGraph);
        location.Label = r.DisplayName;
        //location.RelatedTo = r.RelatedTo;
        location.Type = r.Type;
        location.Longitude = r.GeocenterLocation?.Lon;
        location.Latitude = r.GeocenterLocation?.Lat;
        return location;
    }));
    if (source.Address is Json.Address address)
    {
        var location = Location.Create(address.Id.Value, targetGraph);
        location.Type = "Address";
        location.Address = address.Street;
        location.Longitude = address.Location.Coordinates[0];
        location.Latitude = address.Location.Coordinates[1];
        target.Locations.Add(location);

    }
    target.Start = source.StartDate?.Value;
    target.Volunteers = source.VolunteerNumber;
}

var turtleWriter = new CompressingTurtleWriter();
turtleWriter.DefaultNamespaces.AddNamespace("id", Vocabulary.InstanceBaseUri);
turtleWriter.DefaultNamespaces.AddNamespace("", new Uri(Vocabulary.VocabularyBaseUri));
turtleWriter.Save(targetGraph, outputRdfFile);
