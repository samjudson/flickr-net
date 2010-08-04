using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// Summary description for PhotoSearchOptions.
    /// </summary>
    public class PhotoSearchOptions
    {
        /// <summary>
        /// Creates a new instance of the search options.
        /// </summary>
        public PhotoSearchOptions()
        {
        }

        /// <summary>
        /// Creates a new instance of the search options, setting the UserId property to the parameter 
        /// passed in.
        /// </summary>
        /// <param name="userId">The ID of the User to search for.</param>
        public PhotoSearchOptions(string userId) : this(userId, null, TagMode.AllTags, null)
        {
        }

        /// <summary>
        /// Create an instance of the <see cref="PhotoSearchOptions"/> for a given user ID and tag list.
        /// </summary>
        /// <param name="userId">The ID of the User to search for.</param>
        /// <param name="tags">The tags (comma delimited) to search for. Will match all tags.</param>
        public PhotoSearchOptions(string userId, string tags)
            : this(userId, tags, TagMode.AllTags, null)
        {
        }

        /// <summary>
        /// Create an instance of the <see cref="PhotoSearchOptions"/> for a given user ID and tag list,
        /// with the selected tag mode.
        /// </summary>
        /// <param name="userId">The ID of the User to search for.</param>
        /// <param name="tags">The tags (comma delimited) to search for.</param>
        /// <param name="tagMode">The <see cref="TagMode"/> to use to search.</param>
        public PhotoSearchOptions(string userId, string tags, TagMode tagMode)
            : this(userId, tags, tagMode, null)
        {
        }

        /// <summary>
        /// Create an instance of the <see cref="PhotoSearchOptions"/> for a given user ID and tag list,
        /// with the selected tag mode, and containing the selected text.
        /// </summary>
        /// <param name="userId">The ID of the User to search for.</param>
        /// <param name="tags">The tags (comma delimited) to search for.</param>
        /// <param name="tagMode">The <see cref="TagMode"/> to use to search.</param>
        /// <param name="text">The text to search for in photo title and descriptions.</param>
        public PhotoSearchOptions(string userId, string tags, TagMode tagMode, string text)
        {
            this.UserId = userId;
            this.Tags = tags;
            this.TagMode = tagMode;
            this.Text = text;
        }

        /// <summary>
        /// The user Id of the user to search on. Defaults to null for no specific user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The geocontext for the resulting photos.
        /// </summary>
        public GeoContext GeoContext { get; set; }

        /// <summary>
        /// The group id of the group to search within.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// A comma delimited list of tags
        /// </summary>
        public string Tags { get; set; }
    
        /// <summary>
        /// Tag mode can either be 'all', or 'any'. Defaults to <see cref="FlickrNet.TagMode.AllTags"/>
        /// </summary>
        public TagMode TagMode { get; set; }

        /// <summary>
        /// Search for the given machine tags.
        /// </summary>
        /// <remarks>
        /// See http://www.flickr.com/services/api/flickr.photos.search.html for details 
        /// on how to search for machine tags.
        /// </remarks>
        public string MachineTags { get; set; }

        /// <summary>
        /// The machine tag mode. 
        /// </summary>
        /// <remarks>
        /// Allowed values are any and all. It defaults to any if none specified.
        /// </remarks>
        public MachineTagMode MachineTagMode { get; set; }

        /// <summary>
        /// Search for the given text in photo titles and descriptions.
        /// </summary>
        public string Text { get; set; }
    
        /// <summary>
        /// Minimum date uploaded. Defaults to <see cref="DateTime.MinValue"/> which
        /// signifies that the value is not to be used.
        /// </summary>
        public DateTime MinUploadDate { get; set; }
    
        /// <summary>
        /// Maximum date uploaded. Defaults to <see cref="DateTime.MinValue"/> which
        /// signifies that the value is not to be used.
        /// </summary>
        public DateTime MaxUploadDate { get; set; }
    
        /// <summary>
        /// Minimum date taken. Defaults to <see cref="DateTime.MinValue"/> which
        /// signifies that the value is not to be used.
        /// </summary>
        public DateTime MinTakenDate { get; set; }
    
        /// <summary>
        /// Maximum date taken. Defaults to <see cref="DateTime.MinValue"/> which
        /// signifies that the value is not to be used.
        /// </summary>
        public DateTime MaxTakenDate { get; set; }

        /// <summary>
        /// Filter by media type.
        /// </summary>
        public MediaType MediaType { get; set; }

        private Collection<LicenseType> licenses = new Collection<LicenseType>();

        /// <summary>
        /// The licenses you wish to search for.
        /// </summary>
        public Collection<LicenseType> Licenses
        {
            get { return licenses; }
        }

        /// <summary>
        /// Optional extras to return, defaults to all. See <see cref="PhotoSearchExtras"/> for more details.
        /// </summary>
        public PhotoSearchExtras Extras { get; set; }

        /// <summary>
        /// Number of photos to return per page. Defaults to 100.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// The page to return. Defaults to page 1.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The sort order of the returned list. Default is <see cref="PhotoSearchSortOrder.None"/>.
        /// </summary>
        public PhotoSearchSortOrder SortOrder { get; set; }

        /// <summary>
        /// The privacy fitler to filter the search on.
        /// </summary>
        public PrivacyFilter PrivacyFilter { get; set; }

        /// <summary>
        /// The boundary box for which to search for geo location photos.
        /// </summary>
        public BoundaryBox BoundaryBox { get; set; }

        /// <summary>
        /// The accuracy of the search for geo location photos.
        /// </summary>
        /// <remarks>
        /// Can also be set as a property of the <see cref="BoundaryBox"/> property.
        /// </remarks>
        public GeoAccuracy Accuracy
        {
            get { return BoundaryBox == null ? GeoAccuracy.None : BoundaryBox.Accuracy; }
            set 
            {
                if (BoundaryBox == null) { BoundaryBox = new BoundaryBox(); }
                BoundaryBox.Accuracy = value;
            }
        
        }

        /// <summary>
        /// Which type of safe search to perform.
        /// </summary>
        /// <remarks>
        /// An unauthenticated search will only ever return safe photos.
        /// </remarks>
        public SafetyLevel SafeSearch { get; set; }

        /// <summary>
        /// Filter your search on a particular type of content (photo, screenshot or other).
        /// </summary>
        public ContentTypeSearch ContentType { get; set; }

        /// <summary>
        /// Specify the units to use for a Geo location based search. Default is Kilometers.
        /// </summary>
        public RadiusUnit RadiusUnits { get; set; }

        /// <summary>
        /// Specify the radius of a particular geo-location search.
        /// Maximum of 20 miles, 32 kilometers.
        /// </summary>
        public float? Radius { get; set; }

        /// <summary>
        /// Specify the longitude center of a geo-location search.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Specify the latitude center of a geo-location search.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Filter the search results on those that have Geolocation information.
        /// </summary>
        public bool? HasGeo { get; set; }

        /// <summary>
        /// Fitler the search results on a particular users contacts. You must set UserId for this option to be honoured.
        /// </summary>
        public ContactSearch Contacts { get; set; }

        /// <summary>
        /// The WOE id to return photos for. This is a spatial reference.
        /// </summary>
        public string WoeId { get; set; }

        /// <summary>
        /// The Flickr Place to return photos for.
        /// </summary>
        public string PlaceId { get; set; }

        /// <summary>
        /// True if the photo is taken from the Flickr Commons project.
        /// </summary>
        public bool IsCommons { get; set; }

        /// <summary>
        /// Is the image in a gallery.
        /// </summary>
        public bool InGallery { get; set; }

        /// <summary>
        /// Is the photo a part of the getty images collection on Flickr.
        /// </summary>
        public bool IsGetty { get; set; }

        internal string ExtrasString
        {
            get { return UtilityMethods.ExtrasToString(Extras); }
        }

        internal string SortOrderString
        {
            get { return UtilityMethods.SortOrderToString(SortOrder); }
        }

        /// <summary>
        /// Calculates the Uri for a Flash slideshow for the given search options.
        /// </summary>
        /// <returns></returns>
        public string CalculateSlideshowUrl()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("http://www.flickr.com/show.gne");
            sb.Append("?api_method=flickr.photos.search&method_params=");

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            AddToDictionary(parameters);

            List<string> parts = new List<string>();
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                parts.Add(Uri.EscapeDataString(pair.Key) + "|" + Uri.EscapeDataString(pair.Value));
            }

            sb.Append(String.Join(";", parts.ToArray()));

            return sb.ToString();
        }

        /// <summary>
        /// Takes the various properties of this instance and adds them to a <see cref="Dictionary{K,V}"/> instanced passed in, ready for sending to Flickr.
        /// </summary>
        /// <param name="parameters">The <see cref="Dictionary{K,V}"/> to add the options to.</param>
        public void AddToDictionary(Dictionary<string, string> parameters)
        {
            if (UserId != null && UserId.Length > 0) parameters.Add("user_id", UserId);
            if (GroupId != null && GroupId.Length > 0) parameters.Add("group_id", GroupId);
            if (Text != null && Text.Length > 0) parameters.Add("text", Text);
            if (Tags != null && Tags.Length > 0) parameters.Add("tags", Tags);
            if (TagMode != TagMode.None) parameters.Add("tag_mode", UtilityMethods.TagModeToString(TagMode));
            if (MachineTags != null && MachineTags.Length > 0) parameters.Add("machine_tags", MachineTags);
            if (MachineTagMode != MachineTagMode.None) parameters.Add("machine_tag_mode", UtilityMethods.MachineTagModeToString(MachineTagMode));
            if (MinUploadDate != DateTime.MinValue) parameters.Add("min_upload_date", UtilityMethods.DateToUnixTimestamp(MinUploadDate).ToString());
            if (MaxUploadDate != DateTime.MinValue) parameters.Add("max_upload_date", UtilityMethods.DateToUnixTimestamp(MaxUploadDate).ToString());
            if (MinTakenDate != DateTime.MinValue) parameters.Add("min_taken_date", UtilityMethods.DateToMySql(MinTakenDate));
            if (MaxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", UtilityMethods.DateToMySql(MaxTakenDate));
            if (Licenses.Count != 0)
            {
                List<string> licenseArray = new List<string>();
                foreach (LicenseType license in Licenses)
                {
                    licenseArray.Add(license.ToString("d"));
                }
                parameters.Add("license", String.Join(",", licenseArray.ToArray()));
            }
            if (PerPage != 0) parameters.Add("per_page", PerPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (Page != 0) parameters.Add("page", Page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (Extras != PhotoSearchExtras.None) parameters.Add("extras", ExtrasString);
            if (SortOrder != PhotoSearchSortOrder.None) parameters.Add("sort", SortOrderString);
            if (PrivacyFilter != PrivacyFilter.None) parameters.Add("privacy_filter", PrivacyFilter.ToString("d"));
            if (BoundaryBox != null && BoundaryBox.IsSet) parameters.Add("bbox", BoundaryBox.ToString());
            if (Accuracy != GeoAccuracy.None) parameters.Add("accuracy", Accuracy.ToString("d"));
            if (SafeSearch != SafetyLevel.None) parameters.Add("safe_search", SafeSearch.ToString("d"));
            if (ContentType != ContentTypeSearch.None) parameters.Add("content_type", ContentType.ToString("d"));
            if (HasGeo != null) parameters.Add("has_geo", HasGeo.Value ? "1" : "0");
            if (Latitude != null) parameters.Add("lat", Latitude.Value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (Longitude != null) parameters.Add("lon", Longitude.Value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (Radius != null) parameters.Add("radius", Radius.Value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (RadiusUnits != RadiusUnit.None) parameters.Add("radius_units", (RadiusUnits == RadiusUnit.Miles ? "mi" : "km"));
            if (Contacts != ContactSearch.None) parameters.Add("contacts", (Contacts == ContactSearch.AllContacts ? "all" : "ff"));
            if (WoeId != null) parameters.Add("woe_id", WoeId);
            if (PlaceId != null) parameters.Add("place_id", PlaceId);
            if (IsCommons) parameters.Add("is_commons", "1");
            if (InGallery) parameters.Add("in_gallery", "1");
            if (IsGetty) parameters.Add("is_getty", "1");
            if (MediaType != MediaType.None) parameters.Add("media", UtilityMethods.MediaTypeToString(MediaType));
            if (GeoContext != GeoContext.NotDefined) parameters.Add("geo_context", GeoContext.ToString("d"));
        }
    }

}
