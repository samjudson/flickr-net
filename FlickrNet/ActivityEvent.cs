using System;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A user event on a photo, e.g. Comment or Favourite etc.
    /// </summary>
    /// <remarks>
    /// Includes the user's Flickr ID, name, the date of the activity and its content (if a comment).
    /// </remarks>
    public sealed class ActivityEvent : IFlickrParsable
    {
        /// <summary>
        /// The <see cref="ActivityEventType"/> of the event, either Comment or Note.
        /// </summary>
        public ActivityEventType EventType { get; set; }

        /// <summary>
        /// The user id of the user who made the comment or note.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The screen name of the user who made the comment or note.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Server for the buddy icon for the event user.
        /// </summary>
        public string IconServer { get; set; }

        /// <summary>
        /// Farm for the buddy icon for the event user.
        /// </summary>
        public string IconFarm { get; set; }

        /// <summary>
        /// The date the note or comment was added.
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// The text of the note or comment.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// If this event is a comment then this is the ID of the comment.
        /// </summary>
        public string CommentId { get; set; }

        /// <summary>
        /// If this is a note activity then this is the ID of the note.
        /// </summary>
        public string NoteId { get; set; }

        /// <summary>
        /// If this is a gallery activityits then this will contain the ID of the gallery.
        /// </summary>
        public string GalleryId { get; set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "type":
                        switch (reader.Value)
                        {
                            case "fave":
                                EventType = ActivityEventType.Favorite;
                                break;
                            case "note":
                                EventType = ActivityEventType.Note;
                                break;
                            case "comment":
                                EventType = ActivityEventType.Comment;
                                break;
                            case "added_to_gallery":
                                EventType = ActivityEventType.Gallery;
                                break;
                            case "tag":
                                EventType = ActivityEventType.Tag;
                                break;
                            case "group_invite":
                                EventType = ActivityEventType.GroupInvite;
                                break;
                            default:
                                UtilityMethods.CheckParsingException(reader);
                                break;
                        }
                        break;
                    case "user":
                        UserId = reader.Value;
                        break;
                    case "username":
                        UserName = reader.Value;
                        break;
                    case "dateadded":
                        DateAdded = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "commentid":
                        CommentId = reader.Value;
                        break;
                    case "noteid":
                        NoteId = reader.Value;
                        break;
                    case "galleryid":
                        GalleryId = reader.Value;
                        break;
                    case "iconserver":
                        IconServer = reader.Value;
                        break;
                    case "iconfarm":
                        IconFarm = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }
            
            reader.Read();

            if (reader.NodeType == XmlNodeType.Text)
            {
                Value = reader.ReadContentAsString();
                reader.Read();
            }
        }

    }

    /// <summary>
    /// The type of the <see cref="ActivityEvent"/>.
    /// </summary>
    public enum ActivityEventType
    {
        /// <summary>
        /// The event type was not specified, or was a new unsupported type.
        /// </summary>
        Unknown,

        /// <summary>
        /// The event was a comment.
        /// </summary>
        Comment,

        /// <summary>
        /// The event was a note.
        /// </summary>
        Note,

        /// <summary>
        /// The event is a favourite.
        /// </summary>
        Favorite,

        /// <summary>
        /// The event is for a gallery.
        /// </summary>
        Gallery,

        /// <summary>
        /// The event is a tag being added to a item.
        /// </summary>
        Tag,

        /// <summary>
        /// The event is a group invite.
        /// </summary>
        GroupInvite
    }
}
