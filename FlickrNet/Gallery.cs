using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A user created gallery of other peoples photos.
    /// </summary>
    public sealed class Gallery : IFlickrParsable
    {
        /// <summary>
        /// The ID for the gallery.
        /// </summary>
        public string GalleryId { get; private set; }

        /// <summary>
        /// The URL to the gallery on the web.
        /// </summary>
        public string GalleryUrl { get; private set; }

        /// <summary>
        /// The user ID of the gallery owner.
        /// </summary>
        public string OwnerId { get; private set; }

        /// <summary>
        /// The date the gallery was first created.
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// The date the gallery was last updated.
        /// </summary>
        public DateTime DateLastUpdated { get; private set; }

        /// <summary>
        /// The photo id of the primary photo for the gallery.
        /// </summary>
        public string PrimaryPhotoId { get; private set; }

        /// <summary>
        /// The server for the primary photo for the gallery.
        /// </summary>
        public string PrimaryPhotoServer { get; private set; }

        /// <summary>
        /// The web farm for the primary photo for the gallery.
        /// </summary>
        public string PrimaryPhotoFarm { get; private set; }

        /// <summary>
        /// The saecret for the primary photo for the gallery.
        /// </summary>
        public string PrimaryPhotoSecret { get; private set; }

        /// <summary>
        /// The number of photos in this gallery.
        /// </summary>
        public int PhotosCount { get; private set; }

        /// <summary>
        /// The number of videos in this gallery.
        /// </summary>
        public int VideosCount { get; private set; }

        /// <summary>
        /// The title of this gallery.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The description of this gallery.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The URL of the thumbnail for the primary image for this gallery.
        /// </summary>
        public string PrimaryPhotoThumbnailUrl
        {
            get
            {
                return UtilityMethods.UrlFormat(PrimaryPhotoFarm, PrimaryPhotoServer, PrimaryPhotoId, PrimaryPhotoSecret, "thumbnail", "jpg");
            }
        }

        /// <summary>
        /// The URL of the small image for the primary image for this gallery.
        /// </summary>
        public string PrimaryPhotoSmallUrl
        {
            get
            {
                return UtilityMethods.UrlFormat(PrimaryPhotoFarm, PrimaryPhotoServer, PrimaryPhotoId, PrimaryPhotoSecret, "small", "jpg");
            }
        }

        /// <summary>
        /// The URL of the squrea thumbnail for the primary image for this gallery.
        /// </summary>
        public string PrimaryPhotoSquareThumbnailUrl
        {
            get
            {
                return UtilityMethods.UrlFormat(PrimaryPhotoFarm, PrimaryPhotoServer, PrimaryPhotoId, PrimaryPhotoSecret, "square", "jpg");
            }
        }

        /// <summary>
        /// The URL of the medium image for the primary image for this gallery. For large sizes call <see cref="Flickr.PhotosGetSizes(string)"/>
        /// </summary>
        public string PrimaryPhotoMediumUrl
        {
            get
            {
                return UtilityMethods.UrlFormat(PrimaryPhotoFarm, PrimaryPhotoServer, PrimaryPhotoId, PrimaryPhotoSecret, "medium", "jpg");
            }
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        GalleryId = reader.Value;
                        break;
                    case "url":
                        if (reader.Value.IndexOf("www.flickr.com") > 0)
                            GalleryUrl = reader.Value;
                        else
                            GalleryUrl = "http://www.flickr.com" + reader.Value;
                        break;
                    case "owner":
                        OwnerId = reader.Value;
                        break;
                    case "date_create":
                        DateCreated = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "date_update":
                        DateLastUpdated = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;

                    case "primary_photo_id":
                        PrimaryPhotoId = reader.Value;
                        break;
                    case "primary_photo_server":
                    case "server":
                        PrimaryPhotoServer = reader.Value;
                        break;
                    case "primary_photo_farm":
                    case "farm":
                        PrimaryPhotoFarm = reader.Value;
                        break;
                    case "primary_photo_secret":
                    case "secret":
                        PrimaryPhotoSecret = reader.Value;
                        break;

                    case "count_photos":
                        PhotosCount = reader.ReadContentAsInt();
                        break;
                    case "count_videos":
                        VideosCount = reader.ReadContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
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
                        break;
                }
            }

            reader.Read();
        }
    }
}
