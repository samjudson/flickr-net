using System;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for PhotoSearchOptions.
	/// </summary>
	[Serializable]
	public class PhotoSearchOptions
	{
		private string _userId;
		private string _tags;
		private TagMode _tagMode = TagMode.None;
		private string _machineTags;
		private MachineTagMode _machineTagMode = MachineTagMode.None;
		private string _text;
		private DateTime _minUploadDate = DateTime.MinValue;
		private DateTime _maxUploadDate = DateTime.MinValue;
		private DateTime _minTakenDate = DateTime.MinValue;
		private DateTime _maxTakenDate = DateTime.MinValue;
		private System.Collections.ArrayList _licenses = new System.Collections.ArrayList();
		private PhotoSearchExtras _extras = PhotoSearchExtras.None;
		private int _perPage = 0;
		private int _page = 0;
		private PhotoSearchSortOrder _sort = PhotoSearchSortOrder.None;
		private PrivacyFilter _privacyFilter = PrivacyFilter.None;
		private BoundaryBox _boundaryBox = new BoundaryBox();
		private string _groupId;
		private SafetyLevel _safeSearch = SafetyLevel.None;
		private ContentTypeSearch _contentType = ContentTypeSearch.None;

		private float _longitude = float.NaN;
		private float _latitude = float.NaN;
		private bool _hasGeo;
		private float _radius = float.NaN;
		private RadiusUnits _radiusUnits = RadiusUnits.None;
		private ContactSearch _contacts = ContactSearch.None;
		private string _woeId;
		private string _placeId;

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
		public string UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		/// <summary>
		/// The group id of the group to search within.
		/// </summary>
		public string GroupId
		{
			get { return _groupId; }
			set { _groupId = value; }
		}

		/// <summary>
		/// A comma delimited list of tags
		/// </summary>
		public string Tags
		{
			get { return _tags; }
			set { _tags = value; }
		}
	
		/// <summary>
		/// Tag mode can either be 'all', or 'any'. Defaults to <see cref="FlickrNet.TagMode.AllTags"/>
		/// </summary>
		public TagMode TagMode
		{
			get { return _tagMode; }
			set { _tagMode = value; }
		}

		internal string TagModeString
		{
			get
			{
				switch(_tagMode)
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
		public string MachineTags
		{
			get { return _machineTags; } set { _machineTags = value; }
		}

		/// <summary>
		/// The machine tag mode. 
		/// </summary>
		/// <remarks>
		/// Allowed values are any and all. It defaults to any if none specified.
		/// </remarks>
		public MachineTagMode MachineTagMode
		{
			get { return _machineTagMode; } set { _machineTagMode = value; }
		}

		internal string MachineTagModeString
		{
			get
			{
				switch(_machineTagMode)
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
		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}
	
		/// <summary>
		/// Minimum date uploaded. Defaults to <see cref="DateTime.MinValue"/> which
		/// signifies that the value is not to be used.
		/// </summary>
		public DateTime MinUploadDate
		{
			get { return _minUploadDate; }
			set { _minUploadDate = value; }
		}
	
		/// <summary>
		/// Maximum date uploaded. Defaults to <see cref="DateTime.MinValue"/> which
		/// signifies that the value is not to be used.
		/// </summary>
		public DateTime MaxUploadDate
		{
			get { return _maxUploadDate; }
			set { _maxUploadDate = value; }
		}
	
		/// <summary>
		/// Minimum date taken. Defaults to <see cref="DateTime.MinValue"/> which
		/// signifies that the value is not to be used.
		/// </summary>
		public DateTime MinTakenDate
		{
			get { return _minTakenDate; }
			set { _minTakenDate = value; }
		}
	
		/// <summary>
		/// Maximum date taken. Defaults to <see cref="DateTime.MinValue"/> which
		/// signifies that the value is not to be used.
		/// </summary>
		public DateTime MaxTakenDate
		{
			get { return _maxTakenDate; }
			set { _maxTakenDate = value; }
		}

		/// <summary>
		/// Only return licenses with the selected license number.
		/// See http://www.flickr.com/services/api/flickr.photos.licenses.getInfo.html
		/// for more details on the numbers to use.
		/// </summary>
		[Obsolete("Use AddLicense/RemoveLicense to add/remove licenses")]
		public int License
		{
			get 
			{
				if( _licenses.Count == 0 )
					return 0;
				else
					return (int)_licenses[0];
			}
			set 
			{
				if( _licenses.Count == 0 )
					_licenses.Add(value);
				else
					_licenses[0] = value;
			}
		}

		/// <summary>
		/// Returns a copy of the licenses to be searched for.
		/// </summary>
		public int[] Licenses
		{
			get 
			{
				return (int[])_licenses.ToArray(typeof(int));
			}
		}

		/// <summary>
		/// Adds a new license to the list of licenses to be searched for.
		/// </summary>
		/// <param name="license">The number of the license to search for.</param>
		public void AddLicense(int license)
		{
			if( !_licenses.Contains(license) ) _licenses.Add(license);
		}

		/// <summary>
		/// Removes a license from the list of licenses to be searched for.
		/// </summary>
		/// <param name="license">The number of the license to remove.</param>
		public void RemoveLicense(int license)
		{
			if( _licenses.Contains(license) ) _licenses.Remove(license);
		}

		/// <summary>
		/// Optional extras to return, defaults to all. See <see cref="PhotoSearchExtras"/> for more details.
		/// </summary>
		public PhotoSearchExtras Extras
		{
			get { return _extras; }
			set { _extras = value; }
		}

		/// <summary>
		/// Number of photos to return per page. Defaults to 100.
		/// </summary>
		public int PerPage
		{
			get { return _perPage; }
			set { _perPage = value; }
		}

		/// <summary>
		/// The page to return. Defaults to page 1.
		/// </summary>
		public int Page
		{
			get { return _page; }
			set 
			{
				if( value < 0 ) throw new ArgumentOutOfRangeException("Page", "Must be greater than 0");
				_page = value; 
			}
		}

		/// <summary>
		/// The sort order of the returned list. Default is <see cref="PhotoSearchSortOrder.None"/>.
		/// </summary>
		public PhotoSearchSortOrder SortOrder
		{
			get { return _sort; }
			set { _sort = value; }
		}

		/// <summary>
		/// The privacy fitler to filter the search on.
		/// </summary>
		public PrivacyFilter PrivacyFilter
		{
			get { return _privacyFilter; }
			set { _privacyFilter = value; }
		}

		/// <summary>
		/// The boundary box for which to search for geo location photos.
		/// </summary>
		public BoundaryBox BoundaryBox
		{
			get { return _boundaryBox; }
			set 
			{
				if( value == null )
					  _boundaryBox = new BoundaryBox();
				  else 
					  _boundaryBox = value; 
			}
		}

		/// <summary>
		/// The accuracy of the search for geo location photos.
		/// </summary>
		/// <remarks>
		/// Can also be set as a property of the <see cref="BoundaryBox"/> property.
		/// </remarks>
		public GeoAccuracy Accuracy
		{
			get { return _boundaryBox==null?GeoAccuracy.None:_boundaryBox.Accuracy; }
			set 
			{ 
				if (_boundaryBox==null) { _boundaryBox = new BoundaryBox(); }
				_boundaryBox.Accuracy = value;
			}
		
		}

		/// <summary>
		/// Which type of safe search to perform.
		/// </summary>
		/// <remarks>
		/// An unauthenticated search will only ever return safe photos.
		/// </remarks>
		public SafetyLevel SafeSearch 
		{
			get 
			{
				return _safeSearch; 
			}
			set 
			 {
				 _safeSearch = value; 
			 }
		}

		/// <summary>
		/// Filter your search on a particular type of content (photo, screenshot or other).
		/// </summary>
		public ContentTypeSearch ContentType
		{
			get { return _contentType; }
			set { _contentType = value; }
		}

		/// <summary>
		/// Specify the units to use for a Geo location based search. Default is Kilometers.
		/// </summary>
		public RadiusUnits RadiusUnits
		{
			get { return _radiusUnits; }
			set { _radiusUnits = value; }
		}

		/// <summary>
		/// Specify the radius of a particular geo-location search.
		/// Maximum of 20 miles, 32 kilometers.
		/// </summary>
		public float Radius
		{
			get { return _radius; }
			set { _radius = value; }
		}

		/// <summary>
		/// Specify the longitude center of a geo-location search.
		/// </summary>
		public float Longitude
		{
			get { return _longitude; }
			set { _longitude = value; }
		}

		/// <summary>
		/// Specify the latitude center of a geo-location search.
		/// </summary>
		public float Latitude
		{
			get { return _latitude; }
			set { _latitude = value; }
		}

		/// <summary>
		/// Filter the search results on those that have Geolocation information.
		/// </summary>
		public bool HasGeo
		{
			get { return _hasGeo; }
			set { _hasGeo = value; }
		}

		/// <summary>
		/// Fitler the search results on a particular users contacts. You must set UserId for this option to be honoured.
		/// </summary>
		public ContactSearch Contacts
		{
			get { return _contacts; }
			set { _contacts = value; }
		}

		/// <summary>
		/// The WOE id to return photos for. This is a spatial reference.
		/// </summary>
		public string WoeId
		{
			get { return _woeId; }
			set { _woeId = value; }
		}

		/// <summary>
		/// The Flickr Place to return photos for.
		/// </summary>
		public string PlaceId
		{
			get { return _placeId; }
			set { _placeId = value; }
		}

		internal string ExtrasString
		{
			get { return Utils.ExtrasToString(Extras); }
		}

		internal string SortOrderString
		{
			get	{ return Utils.SortOrderToString(_sort); }
		}
	}

}
