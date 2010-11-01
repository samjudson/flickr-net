using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A set of properties for the photoset.
    /// </summary>
    public sealed class Photoset : IFlickrParsable
    {
        private string url;

        /// <summary>
        /// The ID of the photoset.
        /// </summary>
        public string PhotosetId { get; set; }

        /// <summary>
        /// The URL of the photoset.
        /// </summary>
        public string Url
        {
            get
            {
                if (url == null) url = String.Format(System.Globalization.CultureInfo.InvariantCulture, "http://www.flickr.com/photos/{0}/sets/{1}/", OwnerId, PhotosetId);
                return url;
            }
            set { url = value; }
        }

        /// <summary>
        /// The ID of the owner of the photoset.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// The photo ID of the primary photo of the photoset.
        /// </summary>
        public string PrimaryPhotoId { get; set; }

        /// <summary>
        /// The secret for the primary photo for the photoset.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// The server for the primary photo for the photoset.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The server farm for the primary photo for the photoset.
        /// </summary>
        public string Farm { get; set; }

        /// <summary>
        /// The number of photos in this photoset.
        /// </summary>
        public int NumberOfPhotos { get; set; }

        /// <summary>
        /// The number of videos in this photoset.
        /// </summary>
        public int NumberOfVideos { get; set; }

        /// <summary>
        /// The title of the photoset.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of the photoset.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The URL for the thumbnail of a photo.
        /// </summary>
        public string PhotosetThumbnailUrl
        {
            get { return UtilityMethods.UrlFormat(this, "_t", "jpg"); }
        }

        /// <summary>
        /// The URL for the square thumbnail of a photo.
        /// </summary>
        public string PhotosetSquareThumbnailUrl
        {
            get { return UtilityMethods.UrlFormat(this, "_s", "jpg"); }
        }

        /// <summary>
        /// The URL for the small copy of a photo.
        /// </summary>
        public string PhotosetSmallUrl
        {
            get { return UtilityMethods.UrlFormat(this, "_m", "jpg"); }
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photoset")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        PhotosetId = reader.Value;
                        break;
                    case "url":
                        Url = reader.Value;
                        break;
                    case "owner_id":
                    case "owner":
                        OwnerId = reader.Value;
                        break;
                    case "primary":
                        PrimaryPhotoId = reader.Value;
                        break;
                    case "secret":
                        Secret = reader.Value;
                        break;
                    case "farm":
                        Farm = reader.Value;
                        break;
                    case "server":
                        Server = reader.Value;
                        break;
                    case "photos":
                    case "total":
                        NumberOfPhotos = reader.ReadContentAsInt();
                        break;
                    case "videos":
                        NumberOfVideos = reader.ReadContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName != "photoset" && reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                switch (reader.LocalName)
                {
                    case "title":
                        Title = reader.ReadElementContentAsString();
                        break;
                    case "description":
                        Description = reader.ReadElementContentAsString();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        reader.Skip();
                        break;
                }
            }

            reader.Read();
        }
    }
}
