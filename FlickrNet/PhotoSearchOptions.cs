using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for PhotoSearchOptions.
	/// </summary>
	[Serializable]
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
		public PhotoSearchOptions(string userId, string tags) : this( userId, tags, TagMode.AllTags, null)
		{
		}

		/// <summary>
		/// Create an instance of the <see cref="PhotoSearchOptions"/> for a given user ID and tag list,
		/// with the selected tag mode.
		/// </summary>
		/// <param name="userId">The ID of the User to search for.</param>
		/// <param name="tags">The tags (comma delimited) to search for.</param>
		/// <param name="tagMode">The <see cref="TagMode"/> to use to search.</param>
		public PhotoSearchOptions(string userId, string tags, TagMode tagMode) : this( userId, tags, tagMode, null)
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

		internal string TagModeString
		{
			get
			{
				switch(TagMode)
				{
					case TagMode.None:
						return "";
					case TagMode.AllTags:
						return "all";
					case TagMode.AnyTag:
						return "any";
					case TagMode.Boolean:
						return "bool";
					default:
						return "";
				}
			}
		}

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

		internal string MachineTagModeString
		{
			get
			{
                switch (MachineTagMode)
				{
					case MachineTagMode.None:
						return "";
					case MachineTagMode.AllTags:
						return "all";
					case MachineTagMode.AnyTag:
						return "any";
					default:
						return "";
				}
			}
		}

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

        private Collection<LicenseType> _licenses = new Collection<LicenseType>();

        /// <summary>
        /// The licenses you wish to search for.
        /// </summary>
        public Collection<LicenseType> Licenses
        {
            get { return _licenses; }
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
		public SafetyLevel SafeSearch  { get; set; }

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

		internal string ExtrasString
		{
			get { return UtilityMethods.ExtrasToString(Extras); }
		}

		internal string SortOrderString
		{
			get	{ return UtilityMethods.SortOrderToString(SortOrder); }
		}
	}

}
