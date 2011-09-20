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
        public string GalleryId { get; set; }

        /// <summary>
        /// The URL to the gallery on the web.
        /// </summary>
        public string GalleryUrl { get; set; }

        /// <summary>
        /// The user ID of the gallery owner.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// Server for the gallery ownsers buddy icon. See <see cref="OwnerBuddyIcon"/> for full url.
        /// </summary>
        public string OwnerServer { get; set; }

        /// <summary>
        /// Farm for the gallery ownsers buddy icon. See <see cref="OwnerBuddyIcon"/> for full url.
        /// </summary>
        public string OwnerFarm { get; set; }

        /// <summary>
        /// Gallery owner's buddy icon url.
        /// </summary>
        public string OwnerBuddyIcon
        {
            get
            {
                return UtilityMethods.BuddyIcon(OwnerServer, OwnerFarm, OwnerId);
            }
        }

        /// <summary>
        /// The username (screen name) of the gallery owner.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The date the gallery was first created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The date the gallery was last updated.
        /// </summary>
        public DateTime DateLastUpdated { get; set; }

        /// <summary>
        /// The photo id of the primary photo for the gallery.
        /// </summary>
        public string PrimaryPhotoId { get; set; }

        /// <summary>
        /// The server for the primary photo for the gallery.
        /// </summary>
        public string PrimaryPhotoServer { get; set; }

        /// <summary>
        /// The web farm for the primary photo for the gallery.
        /// </summary>
        public string PrimaryPhotoFarm { get; set; }

        /// <summary>
        /// The saecret for the primary photo for the gallery.
        /// </summary>
        public string PrimaryPhotoSecret { get; set; }

        /// <summary>
        /// The number of photos in this gallery.
        /// </summary>
        public int PhotosCount { get; set; }

        /// <summary>
        /// The number of videos in this gallery.
        /// </summary>
        public int VideosCount { get; set; }

        /// <summary>
        /// The number of views for this gallery.
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// The number of comments on the gallery.
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// The title of this gallery.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of this gallery.
        /// </summary>
        public string Description { get; set; }

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
                    case "username":
                        Username = reader.Value;
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
                    case "iconserver":
                        OwnerServer = reader.Value;
                        break;
                    case "iconfarm":
                        OwnerFarm = reader.Value;
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
                    case "count_views":
                        ViewCount = reader.ReadContentAsInt();
                        break;
                    case "count_comments":
                        CommentCount = reader.ReadContentAsInt();
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
                        reader.Skip();
                        break;
                }
            }

            reader.Read();
        }
    }
}
