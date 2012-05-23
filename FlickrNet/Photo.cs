using System;
using System.Xml;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <remarks/>
    public class Photo : IFlickrParsable
    {
        private string urlSquare;
        private string urlLargeSquare;
        private string urlThumbnail;
        private string urlMedium;
        private string urlMedium640;
        private string urlSmall;
        private string urlSmall320;
        private string urlLarge;
        private string urlOriginal;

        /// <summary>
        /// Initializes a new instance of the <see cref="Photo"/> class.
        /// </summary>
        public Photo()
        {
            Tags = new Collection<string>();
        }

        /// <summary>
        /// The list of clean tags for the photograph.
        /// </summary>
        public Collection<string> Tags { get; set; }

        /// <remarks/>
        public string PhotoId { get; set; }
    
        /// <remarks/>
        public string UserId { get; set; }
    
        /// <remarks/>
        public string Secret { get; set; }
    
        /// <remarks/>
        public string Server { get; set; }
    
        /// <remarks/>
        public string Farm { get; set; }
    
        /// <remarks/>
        public string Title { get; set; }
    
        /// <remarks/>
        public bool IsPublic { get; set; }
    
        /// <remarks/>
        public bool IsFriend { get; set; }
    
        /// <remarks/>
        public bool IsFamily { get; set; }

        /// <remarks/>
        public LicenseType License { get; set; }

        /// <summary>
        /// The width of the original image. 
        /// Only returned if <see cref="PhotoSearchExtras.OriginalDimensions"/> is specified.
        /// </summary>
        public int OriginalWidth { get; set; }

        /// <summary>
        /// The height of the original image. 
        /// Only returned if <see cref="PhotoSearchExtras.OriginalDimensions"/> is specified.
        /// </summary>
        public int OriginalHeight { get; set; }

        /// <summary>
        /// Converts the raw dateupload field to a <see cref="DateTime"/>.
        /// </summary>
        public DateTime DateUploaded { get; set; }

        /// <summary>
        /// Converts the raw lastupdate field to a <see cref="DateTime"/>.
        /// Returns <see cref="DateTime.MinValue"/> if the raw value was not returned.
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Converts the raw datetaken field to a <see cref="DateTime"/>.
        /// Returns <see cref="DateTime.MinValue"/> if the raw value was not returned.
        /// </summary>
        public DateTime DateTaken { get; set; }

        /// <summary>
        /// The date the photo was added to the group. Only returned by <see cref="Flickr.GroupsPoolsGetPhotos(string)"/>.
        /// </summary>
        public DateTime? DateAddedToGroup { get; set; }

        /// <summary>
        /// The date the photo was favourited. Only returned by <see cref="Flickr.FavoritesGetPublicList(string)"/>.
        /// </summary>
        public DateTime? DateFavorited { get; set; }

        /// <remarks/>
        public string OwnerName { get; set; }

        /// <remarks/>
        public string IconServer { get; set; }

        /// <remarks/>
        public string IconFarm { get; set; }

        /// <summary>
        /// Optional extra field containing the original format (jpg, png etc) of the 
        /// photo.
        /// </summary>
        public string OriginalFormat { get; set; }

        /// <summary>
        /// Optional extra field containing the original 'secret' of the 
        /// photo used for forming the Url.
        /// </summary>
        public string OriginalSecret { get; set; }

        /// <summary>
        /// Machine tags
        /// </summary>
        public string MachineTags { get; set; }

        /// <summary>
        /// The url to the web page for this photo. Uses the users userId, not their web alias, but
        /// will still work.
        /// </summary>
        public string WebUrl
        {
            get
            {
                return String.Format(System.Globalization.CultureInfo.InvariantCulture, "http://www.flickr.com/photos/{0}/{1}/", String.IsNullOrEmpty(PathAlias) ? UserId : PathAlias, PhotoId);
            }
        }

        /// <summary>
        /// The URL for the square thumbnail of a photo.
        /// </summary>
        public string SquareThumbnailUrl
        {
            get
            {
                if (urlSquare != null)
                    return urlSquare;
                else
                    return UtilityMethods.UrlFormat(this, "_s", "jpg");
            }
        }

        /// <summary>
        /// The width of the square thumbnail image. Only returned if <see cref="PhotoSearchExtras.SquareUrl"/> is specified.
        /// </summary>
        public int? SquareThumbnailWidth { get; set; }
        /// <summary>
        /// The height of the square thumbnail image. Only returned if <see cref="PhotoSearchExtras.SquareUrl"/> is specified.
        /// </summary>
        public int? SquareThumbnailHeight { get; set; }

        /// <summary>
        /// The URL for the large (150x150) square thumbnail of a photo.
        /// </summary>
        public string LargeSquareThumbnailUrl
        {
            get
            {
                if (urlLargeSquare != null)
                    return urlLargeSquare;
                else
                    return UtilityMethods.UrlFormat(this, "_q", "jpg");
            }
        }

        /// <summary>
        /// The width of the square thumbnail image. Only returned if <see cref="PhotoSearchExtras.SquareUrl"/> is specified.
        /// </summary>
        public int? LargeSquareThumbnailWidth { get; set; }
        /// <summary>
        /// The height of the square thumbnail image. Only returned if <see cref="PhotoSearchExtras.SquareUrl"/> is specified.
        /// </summary>
        public int? LargeSquareThumbnailHeight { get; set; }
        /// <summary>
        /// The URL for the thumbnail of a photo.
        /// </summary>
        public string ThumbnailUrl
        {
            get
            {
                if (urlThumbnail != null)
                    return urlThumbnail;
                else
                    return UtilityMethods.UrlFormat(this, "_t", "jpg");
            }
        }

        /// <summary>
        /// The width of the thumbnail image. Only returned if <see cref="PhotoSearchExtras.ThumbnailUrl"/> is specified.
        /// </summary>
        public int? ThumbnailWidth { get; set; }
        /// <summary>
        /// The height of the thumbnail image. Only returned if <see cref="PhotoSearchExtras.ThumbnailUrl"/> is specified.
        /// </summary>
        public int? ThumbnailHeight { get; set; }

        /// <summary>
        /// The URL for the small copy of a photo.
        /// </summary>
        public string SmallUrl
        {
            get
            {
                if (urlSmall != null)
                    return urlSmall;
                else
                    return UtilityMethods.UrlFormat(this, "_m", "jpg");
            }
        }

        /// <summary>
        /// The width of the small image. Only returned if <see cref="PhotoSearchExtras.SmallUrl"/> is specified.
        /// </summary>
        public int? SmallWidth { get; set; }
        /// <summary>
        /// The height of the small image. Only returned if <see cref="PhotoSearchExtras.SmallUrl"/> is specified.
        /// </summary>
        public int? SmallHeight { get; set; }

        /// <summary>
        /// The URL for the small (320 on longest side) copy of a photo.
        /// </summary>
        public string Small320Url
        {
            get
            {
                if (urlSmall320 != null)
                    return urlSmall320;
                else
                    return UtilityMethods.UrlFormat(this, "_n", "jpg");
            }
        }

        /// <summary>
        /// The width of the small 320 image. Only returned if <see cref="PhotoSearchExtras.Small320Url"/> is specified.
        /// </summary>
        public int? Small320Width { get; set; }
        /// <summary>
        /// The height of the small 320 image. Only returned if <see cref="PhotoSearchExtras.Small320Url"/> is specified.
        /// </summary>
        public int? Small320Height { get; set; }

        /// <summary>
        /// The URL for the medium 640 copy of a photo.
        /// </summary>
        /// <remarks>There is a chance that extremely small images will not have a medium 640 copy.
        /// Use <see cref="Flickr.PhotosGetSizes"/> to get the available URLs for a photo.</remarks>
        public string Medium640Url
        {
            get
            {
                if (urlMedium640 != null)
                    return urlMedium640;
                else
                    return UtilityMethods.UrlFormat(this, "_z", "jpg");
            }
        }

        /// <summary>
        /// The width of the medium image. Only returned if <see cref="PhotoSearchExtras.Medium640Url"/> is specified.
        /// </summary>
        public int? Medium640Width { get; set; }
        /// <summary>
        /// The height of the medium image. Only returned if <see cref="PhotoSearchExtras.Medium640Url"/> is specified.
        /// </summary>
        public int? Medium640Height { get; set; }

        /// <summary>
        /// The URL for the medium copy of a photo.
        /// </summary>
        /// <remarks>There is a chance that extremely small images will not have a medium copy.
        /// Use <see cref="Flickr.PhotosGetSizes"/> to get the available URLs for a photo.</remarks>
        public string MediumUrl
        {
            get
            {
                if (urlMedium != null)
                    return urlMedium;
                else
                    return UtilityMethods.UrlFormat(this, String.Empty, "jpg");
            }
        }

        /// <summary>
        /// The width of the medium image. Only returned if <see cref="PhotoSearchExtras.MediumUrl"/> is specified.
        /// </summary>
        public int? MediumWidth { get; set; }
        /// <summary>
        /// The height of the medium image. Only returned if <see cref="PhotoSearchExtras.MediumUrl"/> is specified.
        /// </summary>
        public int? MediumHeight { get; set; }

        /// <summary>
        /// The URL for the large copy of a photo.
        /// </summary>
        /// <remarks>There is a chance that small images will not have a large copy.
        /// Use <see cref="Flickr.PhotosGetSizes"/> to get the available URLs for a photo.</remarks>
        public string LargeUrl
        {
            get
            {
                if (urlLarge != null)
                    return urlLarge;
                else
                    return UtilityMethods.UrlFormat(this, "_b", "jpg");
            }
        }

        /// <summary>
        /// The width of the large image, if one exists. Only returned if <see cref="PhotoSearchExtras.LargeUrl"/> is specified and a large image exists.
        /// </summary>
        public int? LargeWidth { get; set; }
        /// <summary>
        /// The height of the large image, if one exists. Only returned if <see cref="PhotoSearchExtras.LargeUrl"/> is specified and a large image exists.
        /// </summary>
        public int? LargeHeight { get; set; }

        /// <summary>
        /// If <see cref="OriginalFormat"/> was returned then this will contain the url of the original file.
        /// </summary>
        public string OriginalUrl
        {
            get 
            {
                if (urlOriginal != null)
                    return urlOriginal;

                if (OriginalFormat == null || OriginalFormat.Length == 0)
                    return null;

                return UtilityMethods.UrlFormat(this, "_o", OriginalFormat);
            }
        }

        /// <summary>
        /// Latitude. Will be 0 if Geo extras not specified.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude. Will be 0 if <see cref="PhotoSearchExtras.Geo"/> not specified.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The Place ID. Will be null if <see cref="PhotoSearchExtras.Geo"/> is not specified in the search.
        /// </summary>
        public string PlaceId { get; set; }

        /// <summary>
        /// The WOE (Where On Earth) ID. Will be null if <see cref="PhotoSearchExtras.Geo"/> is not specified in the search.
        /// </summary>
        public string WoeId { get; set; }

        /// <summary>
        /// Geo-location accuracy. A value of None means that the information was not returned.
        /// </summary>
        public GeoAccuracy Accuracy { get; set; }

        /// <summary>
        /// The GeoContext of the photo, if it has location information.
        /// </summary>
        public GeoContext? GeoContext { get; set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) comment on this photo.
        /// </summary>
        /// <remarks>Will always be false for unauthenticated calls.</remarks>
        public bool? CanComment { get; set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) print this photo.
        /// </summary>
        /// <remarks>Will always be false for unauthenticated calls.</remarks>
        public bool? CanPrint { get; set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) download this photo.
        /// </summary>
        public bool? CanDownload { get; set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) add 'meta' to this photo (notes, tags etc).
        /// </summary>
        /// <remarks>Will always be false for unauthenticated calls.</remarks>
        public bool? CanAddMeta { get; set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) blog this photo.
        /// </summary>
        /// <remarks>Will always be false for unauthenticated calls.</remarks>
        public bool? CanBlog { get; set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) share on this photo.
        /// </summary>
        /// <remarks>Will always be false for unauthenticated calls.</remarks>
        public bool? CanShare { get; set; }

        /// <summary>
        /// The number of views for this photo. Only returned if PhotoSearchExtras.Views is set.
        /// </summary>
        public int? Views { get; set; }

        /// <summary>
        /// The media format for this photo. Only returned if PhotoSearchExtras.Media is set.
        /// </summary>
        public string Media { get; set; }

        /// <summary>
        /// The url alias the user has picked, it applicable.
        /// </summary>
        public string PathAlias { get; set; }

        /// <summary>
        /// The status of the media for this photo. Only returned if PhotoSearchExtras.Media is set.
        /// </summary>
        public string MediaStatus { get; set; }

        /// <summary>
        /// The description for the photo. Only returned if <see cref="PhotoSearchExtras.Description"/> is set.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// If Geolocation information is returned for this photo then this will contain the permissions for who can see those permissions.
        /// </summary>
        public GeoPermissions GeoPermissions { get; set; }

        /// <summary>
        /// If requested will contain the number of degrees the photo has been rotated since upload.
        /// </summary>
        /// <remarks>
        /// This might be due to the photo containing rotation information so done automatically, or by manually rotating the photo in Flickr.
        /// </remarks>
        public int? Rotation { get; set; }

        /// <summary>
        /// A helper method which tries to guess if a large image will be available for this photograph
        /// based on the original dimensions returned with the photo.
        /// </summary>
        public bool DoesLargeExist
        {
            get
            {
                if (urlLarge != null) return true;

                if (OriginalHeight > 1280 || OriginalWidth > 1280)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// A helper method which tries to guess if a medium image will be available for this photograph
        /// based on the original dimensions returned with the photo.
        /// </summary>
        public bool DoesMediumExist
        {
            get
            {
                if (urlMedium != null) return true;

                if (OriginalHeight > 500 || OriginalWidth > 500)
                    return true;
                else
                    return false;
            }
        }

        void IFlickrParsable.Load(XmlReader reader)
        {
            Load(reader, false);

            if (reader.LocalName == "photo" && reader.NodeType == XmlNodeType.EndElement) reader.Read();
        }

        /// <summary>
        /// Protected method that does the actual initialization of the Photo instance. Should be called by subclasses of the Photo class.
        /// </summary>
        /// <param name="reader">The reader containing the XML to be parsed.</param>
        /// <param name="allowExtraAtrributes">Wheither to allow unknown extra attributes. In debug builds will throw an exception if this parameter is false and an unknown attribute is found.</param>
        protected void Load(XmlReader reader, bool allowExtraAtrributes)
        {
            if (reader.LocalName != "photo")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        PhotoId = reader.Value;
                        if (String.IsNullOrEmpty(reader.Value))
                        {
                            reader.Skip();
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
                        IsPublic = reader.Value == "1";
                        break;
                    case "isfamily":
                        IsFamily = reader.Value == "1";
                        break;
                    case "isfriend":
                        IsFriend = reader.Value == "1";
                        break;
                    case "tags":
                        foreach (string tag in reader.Value.Split(' '))
                        {
                            Tags.Add(tag);
                        }
                        break;
                    case "datetaken":
                        // For example : 2007-11-04 08:55:18
                        DateTaken = UtilityMethods.ParseDateWithGranularity(reader.Value);
                        break;
                    case "datetakengranularity":
                        break;
                    case "dateupload":
                        DateUploaded = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "license":
                        License = (LicenseType)int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "ownername":
                        OwnerName = reader.Value;
                        break;
                    case "lastupdate":
                        LastUpdated = UtilityMethods.UnixTimestampToDate(reader.Value);
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
                        Accuracy = (GeoAccuracy)reader.ReadContentAsInt();
                        break;
                    case "latitude":
                        Latitude = reader.ReadContentAsDouble();
                        break;
                    case "longitude":
                        Longitude = reader.ReadContentAsDouble();
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
                    case "is_primary":
                        break;
                    case "pathalias":
                        PathAlias = reader.Value;
                        break;
                    case "url_sq":
                        urlSquare = reader.Value;
                        break;
                    case "width_sq":
                        SquareThumbnailWidth = reader.ReadContentAsInt();
                        break;
                    case "height_sq":
                        SquareThumbnailHeight = reader.ReadContentAsInt();
                        break;
                    case "url_t":
                        urlThumbnail = reader.Value;
                        break;
                    case "width_t":
                        ThumbnailWidth = reader.ReadContentAsInt();
                        break;
                    case "height_t":
                        ThumbnailHeight = reader.ReadContentAsInt();
                        break;
                    case "url_q":
                        urlLargeSquare = reader.Value;
                        break;
                    case "width_q":
                        LargeSquareThumbnailWidth = reader.ReadContentAsInt();
                        break;
                    case "height_q":
                        LargeSquareThumbnailHeight = reader.ReadContentAsInt();
                        break;
                    case "url_n":
                        urlSmall320 = reader.Value;
                        break;
                    case "width_n":
                        Small320Width = reader.ReadContentAsInt();
                        break;
                    case "height_n":
                        Small320Height = reader.ReadContentAsInt();
                        break;
                    case "url_s":
                        urlSmall = reader.Value;
                        break;
                    case "width_s":
                        SmallWidth = reader.ReadContentAsInt();
                        break;
                    case "height_s":
                        SmallHeight = reader.ReadContentAsInt();
                        break;
                    case "url_m":
                        urlMedium = reader.Value;
                        break;
                    case "width_m":
                        MediumWidth = reader.ReadContentAsInt();
                        break;
                    case "height_m":
                        MediumHeight = reader.ReadContentAsInt();
                        break;
                    case "url_l":
                        urlLarge = reader.Value;
                        break;
                    case "width_l":
                        LargeWidth = reader.ReadContentAsInt();
                        break;
                    case "height_l":
                        LargeHeight = reader.ReadContentAsInt();
                        break;
                    case "url_z":
                        urlMedium640 = reader.Value;
                        break;
                    case "width_z":
                        Medium640Width = reader.ReadContentAsInt();
                        break;
                    case "height_z":
                        Medium640Height = reader.ReadContentAsInt();
                        break;
                    case "url_o":
                        urlOriginal = reader.Value;
                        break;
                    case "width_o":
                        OriginalWidth = reader.ReadContentAsInt();
                        break;
                    case "height_o":
                        OriginalHeight = reader.ReadContentAsInt();
                        break;
                    case "dateadded":
                        DateAddedToGroup = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "date_faved":
                        DateFavorited = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "has_comment": // Gallery photos return this, but we ignore it and set GalleryPhoto.Comment instead.
                        break;
                    case "can_comment":
                        CanComment = reader.Value == "1";
                        break;
                    case "can_addmeta":
                        CanAddMeta = reader.Value == "1";
                        break;
                    case "can_blog":
                        CanBlog = reader.Value == "1";
                        break;
                    case "can_print":
                        CanPrint = reader.Value == "1";
                        break;
                    case "can_download":
                        CanDownload = reader.Value == "1";
                        break;
                    case "can_share":
                        CanShare = reader.Value == "1";
                        break;
                    case "geo_is_family":
                        if (GeoPermissions == null)
                        {
                            GeoPermissions = new GeoPermissions(); 
                            GeoPermissions.PhotoId = PhotoId;
                        }
                        GeoPermissions.IsFamily = reader.Value == "1";
                        break;
                    case "geo_is_friend":
                        if (GeoPermissions == null)
                        {
                            GeoPermissions = new GeoPermissions(); 
                            GeoPermissions.PhotoId = PhotoId;
                        }
                        GeoPermissions.IsFriend = reader.Value == "1";
                        break;
                    case "geo_is_public":
                        if (GeoPermissions == null)
                        {
                            GeoPermissions = new GeoPermissions(); 
                            GeoPermissions.PhotoId = PhotoId;
                        }
                        GeoPermissions.IsPublic = reader.Value == "1";
                        break;
                    case "geo_is_contact":
                        if (GeoPermissions == null)
                        {
                            GeoPermissions = new GeoPermissions(); 
                            GeoPermissions.PhotoId = PhotoId;
                        }
                        GeoPermissions.IsContact = reader.Value == "1";
                        break;
                    case "context":
                        GeoContext = (GeoContext)reader.ReadContentAsInt();
                        break;
                    case "rotation":
                        Rotation = reader.ReadContentAsInt();
                        break;
                    default:
                        if (!allowExtraAtrributes) UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            if (reader.LocalName == "description")
                Description = reader.ReadElementContentAsString();

        }
    }
}
