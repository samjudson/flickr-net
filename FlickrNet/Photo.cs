using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <remarks/>
	[System.Serializable]
	public class Photo 
	{
    
		/// <remarks/>
		[XmlAttribute("id", Form=XmlSchemaForm.Unqualified)]
		public string PhotoId;
    
		/// <remarks/>
		[XmlAttribute("owner", Form=XmlSchemaForm.Unqualified)]
		public string UserId;
    
		/// <remarks/>
		[XmlAttribute("secret", Form=XmlSchemaForm.Unqualified)]
		public string Secret;
    
		/// <remarks/>
		[XmlAttribute("server", Form=XmlSchemaForm.Unqualified)]
		public string Server;
    
		/// <remarks/>
		[XmlAttribute("title", Form=XmlSchemaForm.Unqualified)]
		public string Title;
    
		/// <remarks/>
		[XmlAttribute("ispublic", Form=XmlSchemaForm.Unqualified)]
		public int IsPublic;
    
		/// <remarks/>
		[XmlAttribute("isfriend", Form=XmlSchemaForm.Unqualified)]
		public int IsFriend;
    
		/// <remarks/>
		[XmlAttribute("isfamily", Form=XmlSchemaForm.Unqualified)]
		public int IsFamily;

		/// <remarks/>
		[XmlAttribute("isprimary", Form=XmlSchemaForm.Unqualified)]
		public int IsPrimary;

		/// <remarks/>
		[XmlAttribute("license", Form=XmlSchemaForm.Unqualified)]
		public string License;

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
		[XmlAttribute("datetaken", Form=XmlSchemaForm.Unqualified)]
		public string datetaken_raw;

		/// <summary>
		/// Converts the raw datetaken field to a <see cref="DateTime"/>.
		/// </summary>
		[XmlIgnore]
		public DateTime DateTaken
		{
			get { return System.DateTime.Parse(datetaken_raw); }
		}

		/// <remarks/>
		[XmlAttribute("ownername", Form=XmlSchemaForm.Unqualified)]
		public string OwnerName;

		/// <remarks/>
		[XmlAttribute("iconserver", Form=XmlSchemaForm.Unqualified)]
		public string IconServer;

		/// <summary>
		/// Optional extra field containing the original format (jpg, png etc) of the 
		/// photo.
		/// </summary>
		[XmlAttribute("originalformat", Form=XmlSchemaForm.Unqualified)]
		public string OriginalFormat;

		/// <summary>
		/// Undocumented tags attribute
		/// </summary>
		[XmlAttribute("tags", Form=XmlSchemaForm.Unqualified)]
		public string RawTags;

		private const string photoUrl = "http://static.flickr.com/{0}/{1}_{2}{3}.{4}";

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

		private decimal _latitude;
		private decimal _longitude;

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

		private GeoAccuracy _accuracy;

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
