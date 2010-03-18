using System;
using System.Xml;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <summary>
	/// Activity class used for <see cref="Flickr.ActivityUserPhotos()"/>
	/// and <see cref="Flickr.ActivityUserComments"/>.
	/// </summary>
	public class ActivityItem : IFlickrParsable
	{
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ActivityItem()
        {
            Events = new List<ActivityEvent>();
        }

		/// <summary>
		/// The <see cref="ActivityItemType"/> of the item.
		/// </summary>
		public ActivityItemType ItemType { get; private set; }

		/// <summary>
		/// The ID of either the photoset or the photo.
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// The secret for either the photo, or the primary photo for the photoset.
		/// </summary>
		public string Secret { get; private set; }

		/// <summary>
		/// The server for either the photo, or the primary photo for the photoset.
		/// </summary>
		public string Server { get; private set; }

		/// <summary>
		/// The server farm for either the photo, or the primary photo for the photoset.
		/// </summary>
		public string Farm { get; private set; }

		/// <summary>
		/// The title of the photoset or photo.
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// The number of new comments within the given time frame. 
		/// </summary>
		/// <remarks>
		/// Only applicable for <see cref="Flickr.ActivityUserPhotos()"/>.
		/// </remarks>
		public int CommentsNew { get; private set; }

		/// <summary>
		/// The number of old comments within the given time frame. 
		/// </summary>
		/// <remarks>
		/// Only applicable for <see cref="Flickr.ActivityUserPhotos()"/>.
		/// </remarks>
		public int CommentsOld { get; private set; }

		/// <summary>
		/// The number of comments on the item. 
		/// </summary>
		/// <remarks>
		/// Only applicable for <see cref="Flickr.ActivityUserComments"/>.
		/// </remarks>
		public int Comments { get; private set; }

		/// <summary>
		/// Gets the number of views for this photo or photoset.
		/// </summary>
		public int Views { get; private set; }

		/// <summary>
		/// You want more! You got it!
		/// </summary>
		/// <remarks>
		/// Actually, not sure what this it for!
		/// </remarks>
		public bool More { get; private set; }

		/// <summary>
		/// The user id of the owner of this item.
		/// </summary>
		public string OwnerId { get; private set; }

        /// <summary>
        /// The username of the owner of this item.
        /// </summary>
        public string OwnerName { get; private set; }

		/// <summary>
		/// If the type is a photoset then this contains the number of photos in the set. Otherwise returns -1.
		/// </summary>
		public int? Photos { get; private set; }

		/// <summary>
		/// If this is a photoset then returns the primary photo id, otherwise will be null (<code>Nothing</code> in VB.Net).
		/// </summary>
		public string PrimaryPhotoId { get; private set; }

		/// <summary>
		/// The number of new notes within the given time frame. 
		/// </summary>
		/// <remarks>
		/// Only applicable for photos and when calling <see cref="Flickr.ActivityUserPhotos()"/>.
		/// </remarks>
		public int? NotesNew { get; private set; }

		/// <summary>
		/// The number of old notes within the given time frame. 
		/// </summary>
		/// <remarks>
		/// Only applicable for photos and when calling <see cref="Flickr.ActivityUserPhotos()"/>.
		/// </remarks>
		public int? NotesOld { get; private set; }

		/// <summary>
		/// The number of comments on the photo.
		/// </summary>
		/// <remarks>
		/// Only applicable for photos and when calling <see cref="Flickr.ActivityUserComments"/>.
		/// </remarks>
		public int? Notes { get; private set; }

		/// <summary>
		/// If the type is a photo then this contains the number of favourites in the set. Otherwise returns -1.
		/// </summary>
		public int? Favorites { get; private set; }

		/// <summary>
		/// The events that comprise this activity item.
		/// </summary>
		public List<ActivityEvent> Events { get; private set; }

		void IFlickrParsable.Load(XmlReader reader)
		{
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "type":
                        if (reader.Value == "photoset")
                            ItemType = ActivityItemType.Photoset;
                        else if (reader.Value == "photo")
                            ItemType = ActivityItemType.Photo;
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
                    case "commentsnew":
                        CommentsNew = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "commentsold":
                        CommentsOld = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
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
                        NotesNew = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "notesold":
                        NotesOld = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "notes":
                        Notes = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "faves":
                        Favorites = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();

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
                        throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");
                }
            }

            reader.Read();
		}

    }

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
		Photo
	}
}
