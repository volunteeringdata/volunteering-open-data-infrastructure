namespace DoIt.Json;

public class OrganizationSubDocument : Identified
{
    [JsonPropertyName("logo")] public string? LogoNullable { get; set; }
    [JsonIgnore] public Uri? Logo => string.IsNullOrWhiteSpace(LogoNullable) ? null : new Uri(LogoNullable);

    [JsonPropertyName("causeOptions")] public required IEnumerable<OptionItem> Causes { get; set; }

    [JsonPropertyName("contactEmail")] public string? ContactEmail { get; set; }

    [JsonPropertyName("contactPhoneNumber")] public string? ContactPhoneNumber { get; set; }

    [JsonPropertyName("deleted")] public bool? Deleted { get; set; }

    [JsonPropertyName("description")] public string? Description { get; set; }

    // domainSlug (don't see why we'd need)

    [JsonPropertyName("fullAddress")] public Address? FullAddress { get; set; }

    [JsonPropertyName("name")] public required string Name { get; set; }

    [JsonPropertyName("purpose")] public required string Purpose { get; set; }

    // All null but 1
    [JsonPropertyName("termsOfServicesLink")] public string? TermsOfServicesLinkNullable { get; set; }
    [JsonIgnore] public Uri? TermsOfServicesLink => string.IsNullOrWhiteSpace(TermsOfServicesLinkNullable) ? null : new Uri(TermsOfServicesLinkNullable);

    // TODO: Enum?
    [JsonPropertyName("type")] public string? Type { get; set; }

    [JsonPropertyName("websiteLink")] public string? WebsiteLinkNullable { get; set; }
    [JsonIgnore]
    public Uri? WebsiteLink
    {
        get
        {
            if (string.IsNullOrWhiteSpace(WebsiteLinkNullable))
            {
                return null;
            }

            try
            {
                return new Uri(WebsiteLinkNullable!);
            }
            catch (UriFormatException)
            {
                return new Uri($"https://{WebsiteLinkNullable}");
            }
        }
    }
}