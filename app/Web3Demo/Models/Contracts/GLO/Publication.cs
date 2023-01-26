using Newtonsoft.Json;

namespace Web3Demo.Models.Contracts.GLO
{


    public partial class UpdatedPublication
    {
        public UpdatedPublication(Publication publication, UploadedFileInfo uploadedFile)
        {
            //title, description, types, document, practices, authors, locations, languages, creation_date
            Title = publication.TechDocument.Title;
            Description = publication.TechDocument.Description;
            Types = publication.TechDocument.Types.Select(s => s.Id).ToArray();
            Document = new Document
            {
                Url = publication.TechDocument.Url,
                Name = uploadedFile.Name,
            };
            Authors = publication.TechDocument.Authors;
            Locations = publication.TechDocument.Locations.Select(s => new Location
            {
                Id = s.Id,
                City = s.City,
                Text = s.FullName
            }).ToArray();

            Languages = new long[] { publication.TechDocument.Language.Id };
            CreationDate = publication.TechDocument.CreationDate;
            Tags = publication.TechDocument.Tags;
            Practices = publication.TechDocument.Practices.Select(s => s.Id).ToArray(); 
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("types")]
        public long[] Types { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("document")]
        public Document Document { get; set; }

        //[JsonProperty("initiatives")]
        //public object[] Initiatives { get; set; }

        [JsonProperty("practices")]
        public long[] Practices { get; set; }

        //[JsonProperty("secondary_practices")]
        //public object[] SecondaryPractices { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("authors")]
        public Author[] Authors { get; set; }

        [JsonProperty("locations")]
        public Location[] Locations { get; set; }

        [JsonProperty("languages")]
        public long[] Languages { get; set; }

        [JsonProperty("creation_date")]
        public string CreationDate { get; set; }

        //[JsonProperty("accept")]
        //public bool Accept { get; set; }
    }

    //public partial class Author
    //{
    //    [JsonProperty("id")]
    //    public long Id { get; set; }

    //    [JsonProperty("login")]
    //    public string Login { get; set; }

    //    [JsonProperty("text")]
    //    public string Text { get; set; }

    //    [JsonProperty("email")]
    //    public string Email { get; set; }

    //    [JsonProperty("photo_url")]
    //    public Uri PhotoUrl { get; set; }

    //    [JsonProperty("title")]
    //    public string Title { get; set; }
    //}

    public partial class Document
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }

    //public partial class Tag
    //{
    //    [JsonProperty("id")]
    //    public string Id { get; set; }

    //    [JsonProperty("text")]
    //    public string Text { get; set; }
    //}












    public partial class UploadedFileInfo
    {
        [JsonProperty("authenticated_url")]
        public Uri AuthenticatedUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }


    }
    public class SignedUrlInfo
    {
        [JsonProperty("signed_url")]
        public string SignedUrl { get; set; }
    }

    public partial class Publication
    {
        [JsonProperty("tech_document")]
        public TechDocument TechDocument { get; set; }
    }

    public partial class TechDocument
    {
        [JsonProperty("types")]
        public TypeElement[] Types { get; set; }

        [JsonProperty("boosted_tags")]
        public object[] BoostedTags { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("practices")]
        public Practice[] Practices { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("is_training")]
        public bool IsTraining { get; set; }

        [JsonProperty("page_views")]
        public long PageViews { get; set; }

        [JsonProperty("preview_url")]
        public Uri PreviewUrl { get; set; }

        [JsonProperty("unique_page_views")]
        public long UniquePageViews { get; set; }

        [JsonProperty("locations")]
        public LocationElement[] Locations { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("authors")]
        public Author[] Authors { get; set; }

        [JsonProperty("creation_date")]
        public string CreationDate { get; set; }

        [JsonProperty("external_publication_url")]
        public object ExternalPublicationUrl { get; set; }

        [JsonProperty("initiatives")]
        public object[] Initiatives { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }

        [JsonProperty("reviewer")]
        public object Reviewer { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Author
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("practice_co_heads")]
        public object[] PracticeCoHeads { get; set; }

        [JsonProperty("boosted_tags")]
        public object[] BoostedTags { get; set; }

        [JsonProperty("practice_heads")]
        public object[] PracticeHeads { get; set; }

        [JsonProperty("roles")]
        public object[] Roles { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("practices")]
        public Practice[] Practices { get; set; }

        [JsonProperty("tags")]
        public object[] Tags { get; set; }

        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("employee_number")]
        public string EmployeeNumber { get; set; }

        [JsonProperty("photo_url")]
        public Uri PhotoUrl { get; set; }

        [JsonProperty("designation")]
        public string Designation { get; set; }

        [JsonProperty("person_id")]
        public long PersonId { get; set; }
    }

    public partial class Contact
    {
        [JsonProperty("email_id")]
        public string EmailId { get; set; }

        [JsonProperty("location")]
        public ContactLocation Location { get; set; }

        [JsonProperty("glo_url")]
        public Uri GloUrl { get; set; }
    }

    public partial class ContactLocation
    {
        [JsonProperty("city")]
        public string City { get; set; }
    }

    public partial class Name
    {
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
    }

    public partial class Practice
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("c_name")]
        public string CName { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("memberships", NullValueHandling = NullValueHandling.Ignore)]
        public Membership[] Memberships { get; set; }

        [JsonProperty("is_primary", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsPrimary { get; set; }
    }

    public partial class Membership
    {
        [JsonProperty("member_type_id")]
        public long MemberTypeId { get; set; }

        [JsonProperty("status_id")]
        public long StatusId { get; set; }

        [JsonProperty("center")]
        public object Center { get; set; }

        [JsonProperty("c_name")]
        public string CName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Language
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("language")]
        public string LanguageLanguage { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public partial class LocationElement
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public partial class Tag
    {
        [JsonProperty("is_globallogic_education")]
        public bool IsGloballogicEducation { get; set; }

        [JsonProperty("synonyms")]
        public string Synonyms { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public partial class TypeElement
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

}
