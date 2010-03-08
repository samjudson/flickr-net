using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <remarks/>
	[System.Serializable]
    [XmlRoot("photo")]
    public class Photo : IXmlSerializable, IFlickrParsable
	{

		private string _photoId;
		private string _userId;
		private string _secret;
		private string _server;
		private string _farm;
		private string _title;
        private bool _isPublic;
        private bool _isFriend;
        private bool _isFamily;
        private bool _isPrimary;
        private LicenseType _license;
		private string _ownerName;
        private string _iconServer;
        private string _iconFarm;
        private string _originalFormat;
		private string _originalSecret;
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

        private List<string> _tags = new List<string>();

        /// <summary>
        /// The list of clean tags for the photograph.
        /// </summary>
        public List<string> Tags
        {
            get { return _tags; }
        }

		/// <remarks/>
		public string PhotoId { get { return _photoId; } set { _photoId = value; } }
    
		/// <remarks/>
		public string UserId { get { return _userId; } set { _userId = value; } }
    
		/// <remarks/>
		public string Secret { get { return _secret; } set { _secret = value; } }
    
		/// <remarks/>
		public string Server { get { return _server; } set { _server = value; } }
    
		/// <remarks/>
		public string Farm { get { return _farm; } set { _farm = value; } }
    
		/// <remarks/>
		public string Title { get { return _title; } set { _title = value; } }
    
		/// <remarks/>
		public bool IsPublic { get { return _isPublic; } set { _isPublic = value; } }
    
		/// <remarks/>
        public bool IsFriend { get { return _isFriend; } set { _isFriend = value; } }
    
		/// <remarks/>
        public bool IsFamily { get { return _isFamily; } set { _isFamily = value; } }

		/// <remarks/>
        public bool IsPrimary { get { return _isPrimary; } set { _isPrimary = value; } }

		/// <remarks/>
        public LicenseType License { get { return _license; } set { _license = value; } }

		/// <summary>
		/// The width of the original image. 
		/// Only returned if <see cref="PhotoSearchExtras.OriginalDimensions"/> is specified.
		/// </summary>
		public int OriginalWidth { get { return _originalWidth; } set { _originalWidth = value; } }

		/// <summary>
		/// The height of the original image. 
		/// Only returned if <see cref="PhotoSearchExtras.OriginalDimensions"/> is specified.
		/// </summary>
		public int OriginalHeight { get { return _originalHeight; } set { _originalHeight = value; } }

		/// <summary>
		/// Converts the raw dateupload field to a <see cref="DateTime"/>.
		/// </summary>
		public DateTime DateUploaded
		{
            get { return _dateAdded; }
            set { _dateAdded = value; }
		}

        private DateTime _dateLastUpdated;

		/// <summary>
		/// Converts the raw lastupdate field to a <see cref="DateTime"/>.
		/// Returns <see cref="DateTime.MinValue"/> if the raw value was not returned.
		/// </summary>
		public DateTime LastUpdated
		{
            get { return _dateLastUpdated; }
            set { _dateLastUpdated = value; }
		}

        private DateTime _dateAdded;
		/// <summary>
		/// Converts the raw DateAdded field to a <see cref="DateTime"/>. 
		/// Returns <see cref="DateTime.MinValue"/> if the raw value was not returned.
		/// </summary>
        
		[XmlIgnore]
		public DateTime DateAdded
		{
            get { return _dateAdded; } 
            set { _dateAdded = value; }
		}

        private DateTime _dateTaken;

		/// <summary>
		/// Converts the raw datetaken field to a <see cref="DateTime"/>.
		/// Returns <see cref="DateTime.MinValue"/> if the raw value was not returned.
		/// </summary>
        [XmlIgnore]
        public DateTime DateTaken
        {
            get { return _dateTaken; }
            set { _dateTaken = value; }
        }

		/// <remarks/>
		public string OwnerName { get { return _ownerName; } set { _ownerName = value; } }

        /// <remarks/>
        public string IconServer { get { return _iconServer; } set { _iconServer = value; } }

        /// <remarks/>
        public string IconFarm { get { return _iconFarm; } set { _iconFarm = value; } }

        /// <summary>
		/// Optional extra field containing the original format (jpg, png etc) of the 
		/// photo.
		/// </summary>
		public string OriginalFormat { get { return _originalFormat; } set { _originalFormat = value; } }

		/// <summary>
		/// Optional extra field containing the original 'secret' of the 
		/// photo used for forming the Url.
		/// </summary>
		public string OriginalSecret { get { return _originalSecret; } set { _originalSecret = value; } }

		/// <summary>
		/// Machine tags
		/// </summary>
		public string MachineTags { get { return _machineTags; } set { _machineTags = value; } }

		/// <summary>
		/// The url to the web page for this photo. Uses the users userId, not their web alias, but
		/// will still work.
		/// </summary>
		public string WebUrl
		{
			get { return string.Format("http://www.flickr.com/photos/{0}/{1}/", UserId, PhotoId); }
		}

		/// <summary>
		/// The URL for the square thumbnail of a photo.
		/// </summary>
		public string SquareThumbnailUrl
		{
			get { return Utils.UrlFormat(this, "_s", "jpg"); }
		}

		/// <summary>
		/// The URL for the thumbnail of a photo.
		/// </summary>
		public string ThumbnailUrl
		{
			get { return Utils.UrlFormat(this, "_t", "jpg"); }
		}

		/// <summary>
		/// The URL for the small copy of a photo.
		/// </summary>
		public string SmallUrl
		{
			get { return Utils.UrlFormat(this, "_m", "jpg"); }
		}

		/// <summary>
		/// The URL for the medium copy of a photo.
		/// </summary>
		/// <remarks>There is a chance that extremely small images will not have a medium copy.
		/// Use <see cref="Flickr.PhotosGetSizes"/> to get the available URLs for a photo.</remarks>
		public string MediumUrl
		{
			get { return Utils.UrlFormat(this, "", "jpg"); }
		}

		/// <summary>
		/// The URL for the large copy of a photo.
		/// </summary>
		/// <remarks>There is a chance that small images will not have a large copy.
		/// Use <see cref="Flickr.PhotosGetSizes"/> to get the available URLs for a photo.</remarks>
		public string LargeUrl
		{
			get { return Utils.UrlFormat(this, "_b", "jpg"); }
		}

		/// <summary>
		/// If <see cref="OriginalFormat"/> was returned then this will contain the url of the original file.
		/// </summary>
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

        /// <summary>
        /// Default constructor for the <see cref="Photo"/> class.
        /// </summary>
        public Photo()
        {
        }

        void IFlickrParsable.Load(XmlReader reader)
        {
            DoLoad(reader);

            reader.Skip();
        }

        /// <summary>
        /// Protected method that does the actual initialization of the Photo instance. Should be called by subclasses of the Photo class.
        /// </summary>
        /// <param name="reader">The reader containing the XML to be parsed.</param>
        protected void DoLoad(XmlReader reader)
        {
            if (reader.LocalName != "photo")
                throw new FlickrException("Unknown element found: " + reader.LocalName);


            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        PhotoId = reader.Value;
                        if (String.IsNullOrEmpty(reader.Value))
                        {
                            return;
                        }
                        break;
                    case "owner":
                        UserId = reader.Value;
                        break;
                    case "secret":
                        Secret = reader.Value;
                        break;
                    case "server":
                        Server = reader.Value;
                        break;
                    case "farm":
                        Farm = reader.Value;
                        break;
                    case "title":
                        Title = reader.Value;
                        break;
                    case "ispublic":
                        IsPublic = (reader.Value == "1");
                        break;
                    case "isfamily":
                        IsFamily = (reader.Value == "1");
                        break;
                    case "isfriend":
                        IsFriend = (reader.Value == "1");
                        break;
                    case "tags":
                        Tags.AddRange(reader.Value.Split(' '));
                        break;
                    case "datetaken":
                        //e.g. 2007-11-04 08:55:18
                        DateTaken = Utils.ParseDateWithGranularity(reader.Value);
                        break;
                    case "datetakengranularity":
                        break;
                    case "dateupload":
                        DateAdded = Utils.UnixTimestampToDate(reader.Value);
                        break;
                    case "license":
                        License = (LicenseType)int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "ownername":
                        OwnerName = reader.Value;
                        break;
                    case "lastupdate":
                        LastUpdated = Utils.UnixTimestampToDate(reader.Value);
                        break;
                    case "originalformat":
                        OriginalFormat = reader.Value;
                        break;
                    case "originalsecret":
                        OriginalSecret = reader.Value;
                        break;
                    case "place_id":
                        PlaceId = reader.Value;
                        break;
                    case "woeid":
                        WoeId = reader.Value;
                        break;
                    case "accuracy":
                        Accuracy = (GeoAccuracy)int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "latitude":
                        Latitude = decimal.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "longitude":
                        Longitude = decimal.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "machine_tags":
                        MachineTags = reader.Value;
                        break;
                    case "o_width":
                        OriginalWidth = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "o_height":
                        OriginalHeight = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "views":
                        Views = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "media":
                        Media = reader.Value;
                        break;
                    case "media_status":
                        MediaStatus = reader.Value;
                        break;
                    case "iconserver":
                        IconServer = reader.Value;
                        break;
                    case "iconfarm":
                        IconFarm = reader.Value;
                        break;
                    case "username":
                        OwnerName = reader.Value;
                        break;
                    case "isprimary":
                        break;
                    default:
                        //throw new Exception("Unknown attribute: " + reader.Name + "=" + reader.Value);
                        break;
                }
            }

        }

        #region IXmlSerializable Members

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            ((IFlickrParsable)this).Load(reader);
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            
        }

        #endregion

    }

}
