using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <remarks/>
	[System.Serializable]
	public class Photo 
	{
    
		private const string photoUrl = "http://static.flickr.com/{0}/{1}_{2}{3}.{4}";

		private string _photoId;
		private string _userId;
		private string _secret;
		private string _server;
		private string _title;
		private int _isPublic;
		private int _isFriend;
		private int _isFamily;
		private int _isPrimary;
		private string _license;
		private string _ownerName;
		private string _iconServer;
		private string _originalFormat;
		private string _cleanTags;
		private decimal _latitude;
		private decimal _longitude;
		private GeoAccuracy _accuracy;

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
		/// Undocumented tags atrribute. Renamed to CleanTags.
		/// </summary>
		[Obsolete("Renamed to CleanTags, as the tags are clean, not raw")]
		public string RawTags { get { return _cleanTags; } set { _cleanTags = value; } }

		/// <summary>
		/// Undocumented tags attribute
		/// </summary>
		[XmlAttribute("tags", Form=XmlSchemaForm.Unqualified)]
		public string CleanTags { get { return _cleanTags; } set { _cleanTags = value; } }

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
			get { return string.Format(photoUrl, Server, PhotoId, Secret, "_s", "jpg"); }
		}

		/// <summary>
		/// The URL for the thumbnail of a photo.
		/// </summary>
		[XmlIgnore()]
		public string ThumbnailUrl
		{
			get { return string.Format(photoUrl, Server, PhotoId, Secret, "_t", "jpg"); }
		}

		/// <summary>
		/// The URL for the small copy of a photo.
		/// </summary>
		[XmlIgnore()]
		public string SmallUrl
		{
			get { return string.Format(photoUrl, Server, PhotoId, Secret, "_m", "jpg"); }
		}

		/// <summary>
		/// The URL for the medium copy of a photo.
		/// </summary>
		/// <remarks>There is a chance that extremely small images will not have a medium copy.
		/// Use <see cref="Flickr.PhotosGetSizes"/> to get the available URLs for a photo.</remarks>
		[XmlIgnore()]
		public string MediumUrl
		{
			get { return string.Format(photoUrl, Server, PhotoId, Secret, "", "jpg"); }
		}

		/// <summary>
		/// The URL for the large copy of a photo.
		/// </summary>
		/// <remarks>There is a chance that small images will not have a large copy.
		/// Use <see cref="Flickr.PhotosGetSizes"/> to get the available URLs for a photo.</remarks>
		[XmlIgnore()]
		public string LargeUrl
		{
			get { return string.Format(photoUrl, Server, PhotoId, Secret, "_b", "jpg"); }
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

				return string.Format(photoUrl, Server, PhotoId, Secret, "_o", OriginalFormat);
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
		/// Longitude. Will be 0 if Geo extras not specified.
		/// </summary>
		[XmlAttribute("longitude", Form=XmlSchemaForm.Unqualified)]
		public decimal Longitude
		{
			get { return _longitude; }
			set { _longitude = value; }
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
	}

}
