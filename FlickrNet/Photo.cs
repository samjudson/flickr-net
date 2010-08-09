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
        private string urlThumbnail;
        private string urlMedium;
        private string urlSmall;
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
        public Collection<string> Tags { get; private set; }

        /// <remarks/>
        public string PhotoId { get; private set; }
    
        /// <remarks/>
        public string UserId { get; internal set; }
    
        /// <remarks/>
        public string Secret { get; private set; }
    
        /// <remarks/>
        public string Server { get; private set; }
    
        /// <remarks/>
        public string Farm { get; private set; }
    
        /// <remarks/>
        public string Title { get; private set; }
    
        /// <remarks/>
        public bool IsPublic { get; private set; }
    
        /// <remarks/>
        public bool IsFriend { get; private set; }
    
        /// <remarks/>
        public bool IsFamily { get; private set; }

        /// <remarks/>
        public LicenseType License { get; private set; }

        /// <summary>
        /// The width of the original image. 
        /// Only returned if <see cref="PhotoSearchExtras.OriginalDimensions"/> is specified.
        /// </summary>
        public int OriginalWidth { get; private set; }

        /// <summary>
        /// The height of the original image. 
        /// Only returned if <see cref="PhotoSearchExtras.OriginalDimensions"/> is specified.
        /// </summary>
        public int OriginalHeight { get; private set; }

        /// <summary>
        /// Converts the raw dateupload field to a <see cref="DateTime"/>.
        /// </summary>
        public DateTime DateUploaded { get; private set; }

        /// <summary>
        /// Converts the raw lastupdate field to a <see cref="DateTime"/>.
        /// Returns <see cref="DateTime.MinValue"/> if the raw value was not returned.
        /// </summary>
        public DateTime LastUpdated { get; private set; }

        /// <summary>
        /// Converts the raw datetaken field to a <see cref="DateTime"/>.
        /// Returns <see cref="DateTime.MinValue"/> if the raw value was not returned.
        /// </summary>
        public DateTime DateTaken { get; private set; }

        /// <summary>
        /// The date the photo was added to the group. Only returned by <see cref="Flickr.GroupsPoolsGetPhotos(string)"/>.
        /// </summary>
        public DateTime? DateAddedToGroup { get; private set; }

        /// <summary>
        /// The date the photo was favourited. Only returned by <see cref="Flickr.FavoritesGetPublicList(string)"/>.
        /// </summary>
        public DateTime? DateFavorited { get; private set; }

        /// <remarks/>
        public string OwnerName { get; private set; }

        /// <remarks/>
        public string IconServer { get; private set; }

        /// <remarks/>
        public string IconFarm { get; private set; }

        /// <summary>
        /// Optional extra field containing the original format (jpg, png etc) of the 
        /// photo.
        /// </summary>
        public string OriginalFormat { get; private set; }

        /// <summary>
        /// Optional extra field containing the original 'secret' of the 
        /// photo used for forming the Url.
        /// </summary>
        public string OriginalSecret { get; private set; }

        /// <summary>
        /// Machine tags
        /// </summary>
        public string MachineTags { get; private set; }

        /// <summary>
        /// The url to the web page for this photo. Uses the users userId, not their web alias, but
        /// will still work.
        /// </summary>
        public string WebUrl
        {
            get
            {
                return String.Format(System.Globalization.CultureInfo.InvariantCulture, "http://www.flickr.com/photos/{0}/{1}/", PathAlias ?? UserId, PhotoId);
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
        public int? SquareThumbnailWidth { get; private set; }
        /// <summary>
        /// The height of the square thumbnail image. Only returned if <see cref="PhotoSearchExtras.SquareUrl"/> is specified.
        /// </summary>
        public int? SquareThumbnailHeight { get; private set; }

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
        public int? ThumbnailWidth { get; private set; }
        /// <summary>
        /// The height of the thumbnail image. Only returned if <see cref="PhotoSearchExtras.ThumbnailUrl"/> is specified.
        /// </summary>
        public int? ThumbnailHeight { get; private set; }

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
        public int? SmallWidth { get; private set; }
        /// <summary>
        /// The height of the small image. Only returned if <see cref="PhotoSearchExtras.SmallUrl"/> is specified.
        /// </summary>
        public int? SmallHeight { get; private set; }

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
        public int? MediumWidth { get; private set; }
        /// <summary>
        /// The height of the medium image. Only returned if <see cref="PhotoSearchExtras.MediumUrl"/> is specified.
        /// </summary>
        public int? MediumHeight { get; private set; }

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
        public int? LargeWidth { get; private set; }
        /// <summary>
        /// The height of the large image, if one exists. Only returned if <see cref="PhotoSearchExtras.LargeUrl"/> is specified and a large image exists.
        /// </summary>
        public int? LargeHeight { get; private set; }

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
        public double Latitude { get; private set; }

        /// <summary>
        /// Longitude. Will be 0 if <see cref="PhotoSearchExtras.Geo"/> not specified.
        /// </summary>
        public double Longitude { get; private set; }

        /// <summary>
        /// The Place ID. Will be null if <see cref="PhotoSearchExtras.Geo"/> is not specified in the search.
        /// </summary>
        public string PlaceId { get; private set; }

        /// <summary>
        /// The WOE (Where On Earth) ID. Will be null if <see cref="PhotoSearchExtras.Geo"/> is not specified in the search.
        /// </summary>
        public string WoeId { get; private set; }

        /// <summary>
        /// Geo-location accuracy. A value of None means that the information was not returned.
        /// </summary>
        public GeoAccuracy Accuracy { get; private set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) comment on this photo.
        /// </summary>
        /// <remarks>Will always be false for unauthenticated calls.</remarks>
        public bool? CanComment { get; private set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) print this photo.
        /// </summary>
        /// <remarks>Will always be false for unauthenticated calls.</remarks>
        public bool? CanPrint { get; private set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) download this photo.
        /// </summary>
        public bool? CanDownload { get; private set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) add 'meta' to this photo (notes, tags etc).
        /// </summary>
        /// <remarks>Will always be false for unauthenticated calls.</remarks>
        public bool? CanAddMeta { get; private set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) blog this photo.
        /// </summary>
        /// <remarks>Will always be false for unauthenticated calls.</remarks>
        public bool? CanBlog { get; private set; }

        /// <summary>
        /// Can the current user (or unauthenticated user if no authentication token provided) share on this photo.
        /// </summary>
        /// <remarks>Will always be false for unauthenticated calls.</remarks>
        public bool? CanShare { get; private set; }

        /// <summary>
        /// The number of views for this photo. Only returned if PhotoSearchExtras.Views is set.
        /// </summary>
        public int? Views { get; private set; }

        /// <summary>
        /// The media format for this photo. Only returned if PhotoSearchExtras.Media is set.
        /// </summary>
        public string Media { get; private set; }

        /// <summary>
        /// The url alias the user has picked, it applicable.
        /// </summary>
        public string PathAlias { get; private set; }

        /// <summary>
        /// The status of the media for this photo. Only returned if PhotoSearchExtras.Media is set.
        /// </summary>
        public string MediaStatus { get; private set; }

        /// <summary>
        /// The description for the photo. Only returned if <see cref="PhotoSearchExtras.Description"/> is set.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// If Geolocation information is returned for this photo then this will contain the permissions for who can see those permissions.
        /// </summary>
        public GeoPermissions GeoPermissions { get; private set; }

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
            Load(reader);

            if (reader.LocalName == "photo" && reader.NodeType == XmlNodeType.EndElement) reader.Read();
        }

        /// <summary>
        /// Protected method that does the actual initialization of the Photo instance. Should be called by subclasses of the Photo class.
        /// </summary>
        /// <param name="reader">The reader containing the XML to be parsed.</param>
        protected void Load(XmlReader reader)
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
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            if (reader.LocalName == "description")
                Description = reader.ReadElementContentAsString();

        }
    }
}
