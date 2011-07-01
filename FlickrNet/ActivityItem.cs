
namespace FlickrNet
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    
    /// <summary>
    /// The type of the <see cref="ActivityItem"/>.
    /// </summary>
    public enum ActivityItemType
    {
        /// <summary>
        /// The type is unknown, either not set of a new unsupported type.
        /// </summary>
        Unknown,
        /// <summary>
        /// The activity item is on a photoset.
        /// </summary>
        Photoset,
        /// <summary>
        /// The activitiy item is on a photo.
        /// </summary>
        Photo,
        /// <summary>
        /// The activity item is on a gallery.
        /// </summary>
        Gallery
    }

    /// <summary>
    /// Activity class used for <see cref="Flickr.ActivityUserPhotos()"/>
    /// and <see cref="Flickr.ActivityUserComments"/>.
    /// </summary>
    public sealed class ActivityItem : IFlickrParsable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ActivityItem()
        {
            Events = new System.Collections.ObjectModel.Collection<ActivityEvent>();
        }

        /// <summary>
        /// The <see cref="ActivityItemType"/> of the item.
        /// </summary>
        public ActivityItemType ItemType { get; set; }

        /// <summary>
        /// The ID of either the photoset or the photo.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The secret for either the photo, or the primary photo for the photoset.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// The server for either the photo, or the primary photo for the photoset.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The server farm for either the photo, or the primary photo for the photoset.
        /// </summary>
        public string Farm { get; set; }

        /// <summary>
        /// The title of the photoset or photo.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The number of new comments within the given time frame. 
        /// </summary>
        /// <remarks>
        /// Only applicable for <see cref="Flickr.ActivityUserPhotos()"/>.
        /// </remarks>
        public int NewComments { get; set; }

        /// <summary>
        /// The number of old comments within the given time frame. 
        /// </summary>
        /// <remarks>
        /// Only applicable for <see cref="Flickr.ActivityUserPhotos()"/>.
        /// </remarks>
        public int OldComments { get; set; }

        /// <summary>
        /// The number of comments on the item. 
        /// </summary>
        /// <remarks>
        /// Only applicable for <see cref="Flickr.ActivityUserComments"/>.
        /// </remarks>
        public int Comments { get; set; }

        /// <summary>
        /// Gets the number of views for this photo or photoset.
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// You want more! You got it!
        /// </summary>
        /// <remarks>
        /// Actually, not sure what this it for!
        /// </remarks>
        public bool More { get; set; }

        /// <summary>
        /// The user id of the owner of this item.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// The username of the owner of this item.
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// The web server number for the activity item owners buddy icon.
        /// </summary>
        public string OwnerServer { get; set; }

        /// <summary>
        /// The server farm number for the activity item owners buddy icon.
        /// </summary>
        public string OwnerFarm { get; set; }

        /// <summary>
        /// The activity item owners buddy icon.
        /// </summary>
        public string OwnerBuddyIcon
        {
            get
            {
                return UtilityMethods.BuddyIcon(OwnerServer, OwnerFarm, OwnerId);
            }
        }

        /// <summary>
        /// If the type is a photoset then this contains the number of photos in the set. Otherwise returns -1.
        /// </summary>
        public int? Photos { get; set; }

        /// <summary>
        /// If this is a photoset then returns the primary photo id, otherwise will be null (<code>Nothing</code> in VB.Net).
        /// </summary>
        public string PrimaryPhotoId { get; set; }

        /// <summary>
        /// The number of new notes within the given time frame. 
        /// </summary>
        /// <remarks>
        /// Only applicable for photos and when calling <see cref="Flickr.ActivityUserPhotos()"/>.
        /// </remarks>
        public int? NewNotes { get; set; }

        /// <summary>
        /// The number of old notes within the given time frame. 
        /// </summary>
        /// <remarks>
        /// Only applicable for photos and when calling <see cref="Flickr.ActivityUserPhotos()"/>.
        /// </remarks>
        public int? OldNotes { get; set; }

        /// <summary>
        /// The number of comments on the photo.
        /// </summary>
        /// <remarks>
        /// Only applicable for photos and when calling <see cref="Flickr.ActivityUserComments"/>.
        /// </remarks>
        public int? Notes { get; set; }

        /// <summary>
        /// If the type is a photo then this contains the number of favourites in the set. Otherwise returns -1.
        /// </summary>
        public int? Favorites { get; set; }

        public MediaType Media { get; set; }

        /// <summary>
        /// The events that comprise this activity item.
        /// </summary>
        public System.Collections.ObjectModel.Collection<ActivityEvent> Events { get; set; }

        /// <summary>
        /// The URL for the square thumbnail of a photo or the primary photo for a photoset or gallery.
        /// </summary>
        public string SquareThumbnailUrl
        {
            get
            {
                if (ItemType == ActivityItemType.Photo)
                    return UtilityMethods.UrlFormat(Farm, Server, Id, Secret, "_s", "jpg");
                if (ItemType == ActivityItemType.Photoset || ItemType == ActivityItemType.Gallery)
                    return UtilityMethods.UrlFormat(Farm, Server, PrimaryPhotoId, Secret, "_s", "jpg");
                return null;
            }
        }

        /// <summary>
        /// The URL for the small thumbnail of a photo or the primary photo for a photoset or gallery.
        /// </summary>
        public string SmallUrl
        {
            get
            {
                if (ItemType == ActivityItemType.Photo)
                    return UtilityMethods.UrlFormat(Farm, Server, Id, Secret, "_m", "jpg");
                if (ItemType == ActivityItemType.Photoset || ItemType == ActivityItemType.Gallery)
                    return UtilityMethods.UrlFormat(Farm, Server, PrimaryPhotoId, Secret, "_m", "jpg");
                return null;
            }
        }

        void IFlickrParsable.Load(XmlReader reader)
        {
            LoadAttributes(reader);

            LoadElements(reader);
        }

        private void LoadElements(XmlReader reader)
        {
            while (reader.LocalName != "item")
            {
                switch (reader.LocalName)
                {
                    case "title":
                        Title = reader.ReadElementContentAsString();
                        break;
                    case "activity":
                        reader.ReadToDescendant("event");
                        while (reader.LocalName == "event")
                        {
                            ActivityEvent e = new ActivityEvent();
                            ((IFlickrParsable)e).Load(reader);
                            Events.Add(e);
                        }
                        reader.Read();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        reader.Skip();
                        break;
                }
            }

            reader.Read();
        }

        private void LoadAttributes(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "type":
                        switch (reader.Value)
                        {
                            case "photoset":
                                ItemType = ActivityItemType.Photoset;
                                break;
                            case "photo":
                                ItemType = ActivityItemType.Photo;
                                break;
                            case "gallery":
                                ItemType = ActivityItemType.Gallery;
                                break;
                        }
                        break;
                    case "media":
                        switch (reader.Value)
                        {
                            case "photo":
                                Media = MediaType.Photos;
                                break;
                            case "video":
                                Media = MediaType.Videos;
                                break;
                        }
                        break;
                    case "owner":
                        OwnerId = reader.Value;
                        break;
                    case "ownername":
                        OwnerName = reader.Value;
                        break;
                    case "id":
                        Id = reader.Value;
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
                    case "iconserver":
                        OwnerServer = reader.Value;
                        break;
                    case "iconfarm":
                        OwnerFarm = reader.Value;
                        break;
                    case "commentsnew":
                        NewComments = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "commentsold":
                        OldComments = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "comments":
                        Comments = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "views":
                        Views = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "photos":
                        Photos = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "more":
                        More = reader.Value == "0";
                        break;
                    case "primary":
                        PrimaryPhotoId = reader.Value;
                        break;
                    case "notesnew":
                        NewNotes = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "notesold":
                        OldNotes = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "notes":
                        Notes = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "faves":
                        Favorites = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
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
