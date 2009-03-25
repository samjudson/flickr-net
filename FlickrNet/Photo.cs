using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <remarks/>
	[System.Serializable]
    [XmlRoot("photo", Namespace = "", IsNullable = false)]
    public class Photo 
	{
    
		private string _photoId;
		private string _userId;
		private string _secret;
		private string _server;
		private string _farm;
		private string _title;
		private int _isPublic;
		private int _isFriend;
		private int _isFamily;
		private int _isPrimary;
		private string _license;
		private string _ownerName;
		private string _iconServer;
		private string _originalFormat;
		private string _originalSecret;
		private string _cleanTags;
		private string _machineTags;
		private decimal _latitude;
		private decimal _longitude;
		private GeoAccuracy _accuracy;
		private int _originalWidth = -1;
		private int _originalHeight = -1;
		private int _views = -1;
		private string _media;
		private string _mediaStatus;
		private string _placeId;
		private string _woeId;

		/// <remarks/>
		[XmlAttribute("id", Form=XmlSchemaForm.Unqualified)]
		public string PhotoId { get { return _photoId; } set { _photoId = value; } }
    
		/// <remarks/>
		[XmlAttribute("owner", Form=XmlSchemaForm.Unqualified)]
		public string UserId { get { return _userId; } set { _userId = value; } }
    
		/// <remarks/>
		[XmlAttribute("secret", Form=XmlSchemaForm.Unqualified)]
		public string Secret { get { return _secret; } set { _secret = value; } }
    
		/// <remarks/>
		[XmlAttribute("server", Form=XmlSchemaForm.Unqualified)]
		public string Server { get { return _server; } set { _server = value; } }
    
		/// <remarks/>
		[XmlAttribute("farm", Form=XmlSchemaForm.Unqualified)]
		public string Farm { get { return _farm; } set { _farm = value; } }
    
		/// <remarks/>
		[XmlAttribute("title", Form=XmlSchemaForm.Unqualified)]
		public string Title { get { return _title; } set { _title = value; } }
    
		/// <remarks/>
		[XmlAttribute("ispublic", Form=XmlSchemaForm.Unqualified)]
		public int IsPublic { get { return _isPublic; } set { _isPublic = value; } }
    
		/// <remarks/>
		[XmlAttribute("isfriend", Form=XmlSchemaForm.Unqualified)]
		public int IsFriend { get { return _isFriend; } set { _isFriend = value; } }
    
		/// <remarks/>
		[XmlAttribute("isfamily", Form=XmlSchemaForm.Unqualified)]
		public int IsFamily { get { return _isFamily; } set { _isFamily = value; } }

		/// <remarks/>
		[XmlAttribute("isprimary", Form=XmlSchemaForm.Unqualified)]
		public int IsPrimary { get { return _isPrimary; } set { _isPrimary = value; } }

		/// <remarks/>
		[XmlAttribute("license", Form=XmlSchemaForm.Unqualified)]
		public string License { get { return _license; } set { _license = value; } }

		/// <summary>
		/// The width of the original image. 
		/// Only returned if <see cref="PhotoSearchExtras.OriginalDimensions"/> is specified.
		/// </summary>
		[XmlAttribute("o_width")]
		public int OriginalWidth { get { return _originalWidth; } set { _originalWidth = value; } }

		/// <summary>
		/// The height of the original image. 
		/// Only returned if <see cref="PhotoSearchExtras.OriginalDimensions"/> is specified.
		/// </summary>
		[XmlAttribute("o_height")]
		public int OriginalHeight { get { return _originalHeight; } set { _originalHeight = value; } }

		/// <remarks/>
		[XmlAttribute("dateupload", Form=XmlSchemaForm.Unqualified)]
		public string dateupload_raw;

		/// <summary>
		/// Converts the raw dateupload field to a <see cref="DateTime"/>.
		/// </summary>
		[XmlIgnore]
		public DateTime DateUploaded
		{
			get { return Utils.UnixTimestampToDate(dateupload_raw); }
		}

		/// <summary>
		/// Converts the raw lastupdate field to a <see cref="DateTime"/>.
		/// Returns <see cref="DateTime.MinValue"/> if the raw value was not returned.
		/// </summary>
		[XmlIgnore]
		public DateTime LastUpdated
		{
			get { return Utils.UnixTimestampToDate(lastupdate_raw); }
		}

		/// <remarks/>
		[XmlAttribute("lastupdate", Form=XmlSchemaForm.Unqualified)]
		public string lastupdate_raw;

		/// <remarks/>
		[XmlAttribute("dateadded", Form = XmlSchemaForm.Unqualified)]
		public string dateadded_raw;

		/// <summary>
		/// Converts the raw DateAdded field to a <see cref="DateTime"/>. 
		/// Returns <see cref="DateTime.MinValue"/> if the raw value was not returned.
		/// </summary>
		[XmlIgnore]
		public DateTime DateAdded
		{
			get { return Utils.UnixTimestampToDate(dateadded_raw); }
		}

		/// <remarks/>
		[XmlAttribute("datetaken", Form=XmlSchemaForm.Unqualified)]
		public string datetaken_raw;

		/// <summary>
		/// Converts the raw datetaken field to a <see cref="DateTime"/>.
		/// Returns <see cref="DateTime.MinValue"/> if the raw value was not returned.
		/// </summary>
		[XmlIgnore]
		public DateTime DateTaken
		{
			get 
			{
				if( datetaken_raw == null || datetaken_raw.Length == 0 ) return DateTime.MinValue;
				return System.DateTime.Parse(datetaken_raw);
			}
		}

		/// <remarks/>
		[XmlAttribute("ownername", Form=XmlSchemaForm.Unqualified)]
		public string OwnerName { get { return _ownerName; } set { _ownerName = value; } }

		/// <remarks/>
		[XmlAttribute("iconserver", Form=XmlSchemaForm.Unqualified)]
		public string IconServer { get { return _iconServer; } set { _iconServer = value; } }

		/// <summary>
		/// Optional extra field containing the original format (jpg, png etc) of the 
		/// photo.
		/// </summary>
		[XmlAttribute("originalformat", Form=XmlSchemaForm.Unqualified)]
		public string OriginalFormat { get { return _originalFormat; } set { _originalFormat = value; } }

		/// <summary>
		/// Optional extra field containing the original 'secret' of the 
		/// photo used for forming the Url.
		/// </summary>
		[XmlAttribute("originalsecret", Form=XmlSchemaForm.Unqualified)]
		public string OriginalSecret { get { return _originalSecret; } set { _originalSecret = value; } }

		/// <summary>
		/// Undocumented tags atrribute. Renamed to CleanTags.
		/// </summary>
		[Obsolete("Renamed to CleanTags, as the tags are clean, not raw")]
		public string RawTags { get { return _cleanTags; } set { _cleanTags = value; } }

		/// <summary>
		/// Tags, in their clean format (exception is machine tags which retain their machine encoding).
		/// </summary>
		[XmlAttribute("tags", Form=XmlSchemaForm.Unqualified)]
		public string CleanTags { get { return _cleanTags; } set { _cleanTags = value; } }

		/// <summary>
		/// Machine tags
		/// </summary>
		[XmlAttribute("machine_tags", Form=XmlSchemaForm.Unqualified)]
		public string MachineTags { get { return _machineTags; } set { _machineTags = value; } }

		/// <summary>
		/// The url to the web page for this photo. Uses the users userId, not their web alias, but
		/// will still work.
		/// </summary>
		[XmlIgnore()]
		public string WebUrl
		{
			get { return string.Format("http://www.flickr.com/photos/{0}/{1}/", UserId, PhotoId); }
		}

		/// <summary>
		/// The URL for the square thumbnail of a photo.
		/// </summary>
		[XmlIgnore()]
		public string SquareThumbnailUrl
		{
			get { return Utils.UrlFormat(this, "_s", "jpg"); }
		}

		/// <summary>
		/// The URL for the thumbnail of a photo.
		/// </summary>
		[XmlIgnore()]
		public string ThumbnailUrl
		{
			get { return Utils.UrlFormat(this, "_t", "jpg"); }
		}

		/// <summary>
		/// The URL for the small copy of a photo.
		/// </summary>
		[XmlIgnore()]
		public string SmallUrl
		{
			get { return Utils.UrlFormat(this, "_m", "jpg"); }
		}

		/// <summary>
		/// The URL for the medium copy of a photo.
		/// </summary>
		/// <remarks>There is a chance that extremely small images will not have a medium copy.
		/// Use <see cref="Flickr.PhotosGetSizes"/> to get the available URLs for a photo.</remarks>
		[XmlIgnore()]
		public string MediumUrl
		{
			get { return Utils.UrlFormat(this, "", "jpg"); }
		}

		/// <summary>
		/// The URL for the large copy of a photo.
		/// </summary>
		/// <remarks>There is a chance that small images will not have a large copy.
		/// Use <see cref="Flickr.PhotosGetSizes"/> to get the available URLs for a photo.</remarks>
		[XmlIgnore()]
		public string LargeUrl
		{
			get { return Utils.UrlFormat(this, "_b", "jpg"); }
		}

		/// <summary>
		/// If <see cref="OriginalFormat"/> was returned then this will contain the url of the original file.
		/// </summary>
		[XmlIgnore()]
		public string OriginalUrl
		{
			get 
			{ 
				if( OriginalFormat == null || OriginalFormat.Length == 0 )
					throw new InvalidOperationException("No original format information available.");

				return Utils.UrlFormat(this, "_o", OriginalFormat);
			}
		}

		/// <summary>
		/// Latitude. Will be 0 if Geo extras not specified.
		/// </summary>
		[XmlAttribute("latitude", Form=XmlSchemaForm.Unqualified)]
		public decimal Latitude
		{
			get { return _latitude; }
			set { _latitude = value; }
		}

		/// <summary>
		/// Longitude. Will be 0 if <see cref="PhotoSearchExtras.Geo"/> not specified.
		/// </summary>
		[XmlAttribute("longitude", Form=XmlSchemaForm.Unqualified)]
		public decimal Longitude
		{
			get { return _longitude; }
			set { _longitude = value; }
		}

		/// <summary>
		/// The Place ID. Will be null if <see cref="PhotoSearchExtras.Geo"/> is not specified in the search.
		/// </summary>
		[XmlAttribute("place_id", Form=XmlSchemaForm.Unqualified)]
		public string PlaceId
		{
			get { return _placeId; }
			set { _placeId = value; }
		}

		/// <summary>
		/// The WOE (Where On Earth) ID. Will be null if <see cref="PhotoSearchExtras.Geo"/> is not specified in the search.
		/// </summary>
		[XmlAttribute("woeid", Form=XmlSchemaForm.Unqualified)]
		public string WoeId
		{
			get { return _woeId; }
			set { _woeId = value; }
		}

		/// <summary>
		/// Geo-location accuracy. A value of None means that the information was not returned.
		/// </summary>
		[XmlAttribute("accuracy", Form=XmlSchemaForm.Unqualified)]
		public GeoAccuracy Accuracy
		{
			get { return _accuracy; }
			set { _accuracy = value; }
		}

		/// <summary>
		/// The number of views for this photo. Only returned if PhotoSearchExtras.Views is set.
		/// </summary>
		[XmlAttribute("views", Form=XmlSchemaForm.Unqualified)]
		public int Views
		{
			get { return _views; }
			set { _views = value; }
		}

		/// <summary>
		/// The media format for this photo. Only returned if PhotoSearchExtras.Media is set.
		/// </summary>
		[XmlAttribute("media", Form=XmlSchemaForm.Unqualified)]
		public string Media
		{
			get { return _media; }
			set { _media = value; }
		}

		/// <summary>
		/// The status of the media for this photo. Only returned if PhotoSearchExtras.Media is set.
		/// </summary>
		[XmlAttribute("media_status", Form=XmlSchemaForm.Unqualified)]
		public string MediaStatus
		{
			get { return _mediaStatus; }
			set { _mediaStatus = value; }
		}

		/// <summary>
		/// A helper method which tries to guess if a large image will be available for this photograph
		/// based on the original dimensions returned with the photo.
		/// </summary>
		[XmlIgnore()]
		public bool DoesLargeExist
		{
			get
			{
				if( _originalHeight < 0 ) throw new InvalidOperationException("Original Dimensions are not available");

				if( _originalHeight > 1280 || _originalWidth > 1280 ) 
					return true;
				else 
					return false;
			}
		}

		/// <summary>
		/// A helper method which tries to guess if a medium image will be available for this photograph
		/// based on the original dimensions returned with the photo.
		/// </summary>
		[XmlIgnore()]
		public bool DoesMediumExist
		{
			get
			{
				if( _originalHeight < 0 ) throw new InvalidOperationException("Original Dimensions are not available");

				if( _originalHeight > 500 || _originalWidth > 500 ) 
					return true;
				else 
					return false;
			}
		}

	}

}
