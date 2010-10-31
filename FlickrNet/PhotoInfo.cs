using System;
using System.Collections.Generic;

namespace FlickrNet
{
    /// <summary>
    /// Detailed information returned by <see cref="Flickr.PhotosGetInfo(string)"/> or <see cref="Flickr.PhotosGetInfo(string, string)"/> methods.
    /// </summary>
    public sealed class PhotoInfo : IFlickrParsable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PhotoInfo()
        {
            Notes = new System.Collections.ObjectModel.Collection<PhotoInfoNote>();
            Tags = new System.Collections.ObjectModel.Collection<PhotoInfoTag>();
            Urls = new System.Collections.ObjectModel.Collection<PhotoInfoUrl>();
        }

        /// <summary>
        /// The id of the photo.
        /// </summary>
        public string PhotoId { get; set; }

        /// <summary>
        /// The secret of the photo. Used to calculate the URL (amongst other things).
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// The server on which the photo resides.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The server farm on which the photo resides.
        /// </summary>
        public string Farm { get; set; }

        /// <summary>
        /// The original format of the image (e.g. jpg, png etc).
        /// </summary>
        public string OriginalFormat { get; set; }

        /// <summary>
        /// Optional extra field containing the original 'secret' of the 
        /// photo used for forming the Url.
        /// </summary>
        public string OriginalSecret { get; set; }

        /// <summary>
        /// The date the photo was uploaded (or 'posted').
        /// </summary>
        public DateTime DateUploaded { get; set; }

        /// <summary>
        /// Is the photo a favorite of the current authorised user. 
        /// Will be 0 if the user is not authorised.
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// The license of the photo.
        /// </summary>
        public LicenseType License { get; set; }

        /// <summary>
        /// The number of views the photo has.
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// The rotational information for this photo - in degrees.
        /// </summary>
        public int Rotation { get; set; }

        /// <summary>
        /// The media type for this item.
        /// </summary>
        public MediaType Media { get; set; }

        /// <summary>
        /// The NSID of the owner of this item.
        /// </summary>
        public string OwnerUserId { get; set; }

        /// <summary>
        /// The username of the owner of this item.
        /// </summary>
        public string OwnerUserName { get; set; }

        /// <summary>
        /// The real name of the owner of this item.
        /// </summary>
        public string OwnerRealName { get; set; }

        /// <summary>
        /// The location of the owner of this photo.
        /// </summary>
        public string OwnerLocation { get; set; }

        /// <summary>
        /// The title of the photo.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of the photo.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Is the photo visible to the public.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Is the photo visible to contacts marked as friends.
        /// </summary>
        public bool IsFriend { get; set; }

        /// <summary>
        /// Is the photo visible to contacts marked as family.
        /// </summary>
        public bool IsFamily { get; set; }

        /// <summary>
        /// Can the authorized user add new comments.
        /// </summary>
        /// <remarks>
        /// "1" = true, "0" = false.
        /// </remarks>
        public bool CanComment { get; set; }

        /// <summary>
        /// Can the public add new comments.
        /// </summary>
        /// <remarks>
        /// "1" = true, "0" = false.
        /// </remarks>
        public bool CanPublicComment { get; set; }

        /// <summary>
        /// Can the authorized user add new meta data (tags and notes).
        /// </summary>
        /// <remarks>
        /// "1" = true, "0" = false.
        /// </remarks>
        public bool CanAddMeta { get; set; }

        /// <summary>
        /// Can the public add new meta data (tags and notes).
        /// </summary>
        /// <remarks>
        /// "1" = true, "0" = false.
        /// </remarks>
        public bool CanPublicAddMeta { get; set; }

        /// <summary>
        /// Specifies if the user allows blogging of this photos. 1 = Yes, 0 = No.
        /// </summary>
        public bool CanBlog { get; set; }

        /// <summary>
        /// Specifies if the user allows downloading of this photos. 1 = Yes, 0 = No.
        /// </summary>
        public bool CanDownload { get; set; }

        /// <summary>
        /// Specifies if the user allows printing of this photos. 1 = Yes, 0 = No.
        /// </summary>
        public bool CanPrint { get; set; }

        /// <summary>
        /// Does the user allow sharing of this photo.
        /// </summary>
        public bool CanShare { get; set; }

        /// <summary>
        /// The number of comments the photo has.
        /// </summary>
        public int CommentsCount { get; set; }

        /// <summary>
        /// The notes for the photo.
        /// </summary>
        public System.Collections.ObjectModel.Collection<PhotoInfoNote> Notes { get; set; }

        /// <summary>
        /// The tags for the photo.
        /// </summary>
        public System.Collections.ObjectModel.Collection<PhotoInfoTag> Tags { get; set; }

        /// <summary>
        /// The urls for this photo.
        /// </summary>
        public System.Collections.ObjectModel.Collection<PhotoInfoUrl> Urls { get; set; }

        /// <summary>
        /// The date the photo was posted/uploaded.
        /// </summary>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// The date the photo was taken.
        /// </summary>
        public DateTime DateTaken { get; set; }

        /// <summary>
        /// The date the photo was last updated.
        /// </summary>
        public DateTime DateLastUpdated { get; set; }

        /// <summary>
        /// The granularity of the date taken data.
        /// </summary>
        public DateGranularity DateTakenGranularity { get; set; }

        /// <summary>
        /// Who has permissions to add comments to this photo.
        /// </summary>
        public PermissionComment? PermissionComment { get; set; }

        /// <summary>
        /// Who has permissions to add meta data (tags and notes) to this photo.
        /// </summary>
        public PermissionAddMeta? PermissionAddMeta { get; set; }

        /// <summary>
        /// The location information of this photo, if available.
        /// </summary>
        /// <remarks>
        /// Will be null if the photo has no location information stored on Flickr.
        /// </remarks>
        public PlaceInfo Location { get; set; }

        /// <summary>
        /// Who has permissions to see the geo-location data for this photo.
        /// </summary>
        public GeoPermissions GeoPermissions { get; set; }

        /// <summary>
        /// If this item is a video this contains information such as if it is ready, its duration etc.
        /// </summary>
        public VideoInfo VideoInfo { get; set; }

        /// <summary>
        /// The Web url for flickr web page for this photo.
        /// </summary>
        public string WebUrl
        {
            get
            {
                return String.Format(System.Globalization.CultureInfo.InvariantCulture, "http://www.flickr.com/photos/{0}/{1}/", OwnerUserId, PhotoId);
            }
        }

        /// <summary>
        /// The URL for the square thumbnail for the photo.
        /// </summary>
        public string SquareThumbnailUrl
        {
            get { return UtilityMethods.UrlFormat(this, "_s", "jpg"); }
        }

        /// <summary>
        /// The URL for the thumbnail for the photo.
        /// </summary>
        public string ThumbnailUrl
        {
            get { return UtilityMethods.UrlFormat(this, "_t", "jpg"); }
        }

        /// <summary>
        /// The URL for the small version of this photo.
        /// </summary>
        public string SmallUrl
        {
            get { return UtilityMethods.UrlFormat(this, "_m", "jpg"); }
        }

        /// <summary>
        /// The URL for the medium version of this photo.
        /// </summary>
        /// <remarks>
        /// There is no guarentee that this size of the image actually exists.
        /// Use <see cref="Flickr.PhotosGetSizes"/> to get a list of existing photo URLs.
        /// </remarks>
        public string MediumUrl
        {
            get { return UtilityMethods.UrlFormat(this, String.Empty, "jpg"); }
        }

        /// <summary>
        /// The URL for the large version of this photo.
        /// </summary>
        /// <remarks>
        /// There is no guarentee that this size of the image actually exists.
        /// Use <see cref="Flickr.PhotosGetSizes"/> to get a list of existing photo URLs.
        /// </remarks>
        public string LargeUrl
        {
            get { return UtilityMethods.UrlFormat(this, "_b", "jpg"); }
        }

        /// <summary>
        /// If <see cref="OriginalFormat"/> was returned then this will contain the url of the original file.
        /// </summary>
        public string OriginalUrl
        {
            get 
            {
                if (OriginalFormat == null || OriginalFormat.Length == 0)
                    return null;

                return UtilityMethods.UrlFormat(this, "_o", OriginalFormat);
            }
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photo")
                UtilityMethods.CheckParsingException(reader);

            LoadAttributes(reader);

            LoadElements(reader);
        }

        private void LoadElements(System.Xml.XmlReader reader)
        {
            while (reader.LocalName != "photo")
            {
                switch (reader.LocalName)
                {
                    case "owner":
                        ParseOwner(reader);
                        break;
                    case "title":
                        Title = reader.ReadElementContentAsString();
                        break;
                    case "description":
                        Description = reader.ReadElementContentAsString();
                        break;
                    case "visibility":
                        ParseVisibility(reader);
                        break;
                    case "permissions":
                        ParsePermissions(reader);
                        break;
                    case "editability":
                        ParseEditability(reader);
                        break;
                    case "publiceditability":
                        ParsePublicEditability(reader);
                        break;
                    case "dates":
                        ParseDates(reader);
                        break;
                    case "usage":
                        ParseUsage(reader);
                        break;
                    case "comments":
                        CommentsCount = reader.ReadElementContentAsInt();
                        break;
                    case "notes":
                        ParseNotes(reader);
                        break;
                    case "tags":
                        ParseTags(reader);
                        break;
                    case "urls":
                        ParseUrls(reader);
                        break;
                    case "location":
                        Location = new PlaceInfo();
                        ((IFlickrParsable)Location).Load(reader);
                        break;
                    case "geoperms":
                        GeoPermissions = new GeoPermissions();
                        ((IFlickrParsable)GeoPermissions).Load(reader);
                        break;
                    case "video":
                        VideoInfo = new VideoInfo();
                        ((IFlickrParsable)VideoInfo).Load(reader);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        reader.Skip();
                        break;

                }
            }

            reader.Skip();
        }

        private void LoadAttributes(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        PhotoId = reader.Value;
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
                    case "originalformat":
                        OriginalFormat = reader.Value;
                        break;
                    case "originalsecret":
                        OriginalSecret = reader.Value;
                        break;
                    case "dateuploaded":
                        DateUploaded = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "isfavorite":
                        IsFavorite = reader.Value == "1";
                        break;
                    case "license":
                        License = (LicenseType)int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "views":
                        ViewCount = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "rotation":
                        Rotation = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "media":
                        Media = reader.Value == "photo" ? MediaType.Photos : MediaType.Videos;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }

        private void ParseUrls(System.Xml.XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "url")
            {
                PhotoInfoUrl url = new PhotoInfoUrl();
                ((IFlickrParsable)url).Load(reader);
                Urls.Add(url);
            }
        }

        private void ParseTags(System.Xml.XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "tag")
            {
                PhotoInfoTag tag = new PhotoInfoTag();
                ((IFlickrParsable)tag).Load(reader);
                Tags.Add(tag);
            }
        }

        private void ParseNotes(System.Xml.XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "note")
            {
                PhotoInfoNote note = new PhotoInfoNote();
                ((IFlickrParsable)note).Load(reader);
                Notes.Add(note);
            }
        }

        private void ParseUsage(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "canblog":
                        CanBlog = reader.Value == "1";
                        break;
                    case "candownload":
                        CanDownload = reader.Value == "1";
                        break;
                    case "canprint":
                        CanPrint = reader.Value == "1";
                        break;
                    case "canshare":
                        CanShare = reader.Value == "1";
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }

        private void ParseVisibility(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "isfamily":
                        IsFamily = reader.Value == "1";
                        break;
                    case "ispublic":
                        IsPublic = reader.Value == "1";
                        break;
                    case "isfriend":
                        IsFriend = reader.Value == "1";
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }

        private void ParseEditability(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "cancomment":
                        CanComment = reader.Value == "1";
                        break;
                    case "canaddmeta":
                        CanAddMeta = reader.Value == "1";
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }

        private void ParsePublicEditability(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "cancomment":
                        CanPublicComment = reader.Value == "1";
                        break;
                    case "canaddmeta":
                        CanPublicAddMeta = reader.Value == "1";
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }

        private void ParsePermissions(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "permcomment":
                        PermissionComment = (PermissionComment)int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "permaddmeta":
                        PermissionAddMeta = (PermissionAddMeta)int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }

        private void ParseDates(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "posted":
                        DatePosted = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "taken":
                        DateTaken = UtilityMethods.ParseDateWithGranularity(reader.Value);
                        break;
                    case "takengranularity":
                        DateTakenGranularity = (DateGranularity)int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "lastupdate":
                        DateLastUpdated = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }

        private void ParseOwner(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "nsid":
                        OwnerUserId = reader.Value;
                        break;
                    case "username":
                        OwnerUserName = reader.Value;
                        break;
                    case "realname":
                        OwnerRealName = reader.Value;
                        break;
                    case "location":
                        OwnerLocation = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }

    }

    /// <summary>
    /// A class containing information about a note on a photo.
    /// </summary>
    public sealed class PhotoInfoNote : IFlickrParsable
    {
        /// <summary>
        /// The notes unique ID.
        /// </summary>
        public string NoteId { get; set; }

        /// <summary>
        /// The User ID of the user who wrote the note.
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// The name of the user who wrote the note.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// The x (left) position of the top left corner of the note.
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// The y (top) position of the top left corner of the note.
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// The width of the note.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the note.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The text of the note.
        /// </summary>
        public string NoteText { get; set; }

#if SILVERLIGHT
        public System.Windows.Size Size
        {
            get { return new System.Windows.Size(Width, Height); }
        }

        public System.Windows.Point Location
        {
            get { return new System.Windows.Point(XPosition, YPosition); }
        }
#else
        /// <summary>
        /// The <see cref="System.Drawing.Size"/> of this note. Derived from <see cref="Width"/> and <see cref="Height"/>.
        /// </summary>
        public System.Drawing.Size Size
        {
            get
            {
                return new System.Drawing.Size(Width, Height);
            }
        }

        /// <summary>
        /// The location of this note on the medium sized thumbnail of this photo. Derived from <see cref="XPosition"/> and <see cref="YPosition"/>.
        /// </summary>
        public System.Drawing.Point Location
        {
            get
            {
                return new System.Drawing.Point(XPosition, YPosition);
            }
        }
#endif
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "note")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        NoteId = reader.Value;
                        break;
                    case "author":
                        AuthorId = reader.Value;
                        break;
                    case "authorname":
                        AuthorName = reader.Value;
                        break;
                    case "x":
                        XPosition = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "y":
                        YPosition = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "w":
                        Width = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "h":
                        Height = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            NoteText = reader.ReadContentAsString();

            reader.Skip();
        }
    }

    /// <summary>
    /// The details of a tag of a photo.
    /// </summary>
    public sealed class PhotoInfoTag : IFlickrParsable
    {
        /// <summary>
        /// The id of the tag.
        /// </summary>
        public string TagId { get; set; }

        /// <summary>
        /// The author id of the tag.
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// The real name of the author of the tag.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Raw copy of the tag, as the user entered it.
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        /// Is the tag a machine tag.
        /// </summary>
        public bool IsMachineTag { get; set; }

        /// <summary>
        /// The actually tag.
        /// </summary>
        public string TagText { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "tag")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        TagId = reader.Value;
                        break;
                    case "author":
                        AuthorId = reader.Value;
                        break;
                    case "authorname":
                        AuthorName = reader.Value;
                        break;
                    case "raw":
                        Raw = reader.Value;
                        break;
                    case "machine_tag":
                        IsMachineTag = reader.Value == "1";
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            TagText = reader.ReadContentAsString();

            reader.Skip();
        }
    }

    /// <summary>
    /// The details of a tag of a photo.
    /// </summary>
    public sealed class PhotoInfoUrl : IFlickrParsable
    {
        /// <summary>
        /// The url for the photoset.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The type of the url.
        /// </summary>
        public string UrlType { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "url")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "type":
                        UrlType = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            Url = reader.ReadContentAsString();

            reader.Skip();
        }
    }

}
