using VDS.RDF.Wrapping;

namespace DoIt.Rdf;

public class Activity : GraphWrapperNode
{
    protected Activity(INode node, IGraph graph) : base(node, graph) { }

    internal static Activity Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Activity Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal static Activity Create(string uri, IGraph g) => Wrap(g.CreateUriNode(g.UriFactory.Create(Vocabulary.InstanceBaseUri, uri)), g);

    internal int Attendees { set => this.Overwrite(Vocabulary.ActivityAttendees, value); }

    internal int Bookings { set => this.Overwrite(Vocabulary.ActivityBookings, value); }

    internal bool Deleted { set => this.Overwrite(Vocabulary.ActivityDeleted, value); }

    internal DateTimeOffset? Due { set => this.OverwriteNullable(Vocabulary.ActivityDue, value); }

    internal Ecosystem Ecosystem { set => this.Overwrite(Vocabulary.ActivityEcosystem, value, Ecosystem.Wrap); }

    internal DateTimeOffset? End { set => this.OverwriteNullable(Vocabulary.ActivityEnd, value); }

    internal Uri? ExternalApplyLink { set => this.OverwriteNullable(Vocabulary.ActivityExternalApplyLink, value); }

    internal bool? IsOnline { set => this.OverwriteNullable(Vocabulary.ActivityIsOnline, value); }

    internal bool? IsVolunteerNumberLimited { set => this.OverwriteNullable(Vocabulary.ActivityIsVolunteerNumberLimited, value); }

    internal Uri? Meeting { set => this.OverwriteNullable(Vocabulary.ActivityMeeting, value); }

    internal Organization Organization { 
        get => this.Singular(Vocabulary.ActivityOrganization, Organization.Wrap); 
        set => this.Overwrite(Vocabulary.ActivityOrganization, value, Organization.Wrap); 
    }

    internal ISet<Uri> PublishedApps { get => this.Objects(Vocabulary.ActivityPublishedApps, NodeMappings.From, ValueMappings.As<Uri>); }

    internal DateTimeOffset? Start { set => this.OverwriteNullable(Vocabulary.ActivityStart, value); }

    internal int? Volunteers { set => this.OverwriteNullable(Vocabulary.ActivityVolunteers, value); }

    internal string Title { set => this.Overwrite(Vocabulary.ActivityTitle, value); }

    internal string Description { set => this.Overwrite(Vocabulary.ActivityDescription, value); }

    internal string Type { set => this.Overwrite(Vocabulary.ActivityType, value); }

    internal string? EventType { set => this.OverwriteNullable(Vocabulary.ActivityEventType, value); }

    internal string LocationOption { set => this.Overwrite(Vocabulary.ActivityLocationOption, value); }

    internal App App
    {
        get => this.Singular(Vocabulary.ActivityApp, App.Wrap);
        set => this.Overwrite(Vocabulary.ActivityApp, value, App.Wrap);
    }

    internal MeasurementUnit MeasurementUnit
    {
        get => this.Singular(Vocabulary.ActivityMeasurementUnit, MeasurementUnit.Wrap);
        set => this.Overwrite(Vocabulary.ActivityMeasurementUnit, value, MeasurementUnit.Wrap);
    }

    internal ISet<Option> Cause { get => this.Objects(Vocabulary.ActivityCause, Option.Wrap, Option.Wrap); }

    internal ISet<Option> Requirement { get => this.Objects(Vocabulary.ActivityRequirement, Option.Wrap, Option.Wrap); }

    internal string? Address { set => this.OverwriteNullable(Vocabulary.Address, value); }

    internal double? Latitude { set => this.OverwriteNullable(Vocabulary.Latitude, value); }

    internal double? Longitude { set => this.OverwriteNullable(Vocabulary.Longitude, value); }


    internal ISet<Region> Regions { get => this.Objects(Vocabulary.ActivityRegion, Region.Wrap, Region.Wrap); }
}
