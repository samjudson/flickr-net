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
	public class ActivityEvent : IFlickrParsable
	{
		/// <summary>
		/// The <see cref="ActivityEventType"/> of the event, either Comment or Note.
		/// </summary>
		public ActivityEventType EventType { get; private set; }

		/// <summary>
		/// The user id of the user who made the comment or note.
		/// </summary>
		public string UserId { get; private set; }

		/// <summary>
		/// The screen name of the user who made the comment or note.
		/// </summary>
		public string UserName { get; private set; }

		/// <summary>
		/// The date the note or comment was added.
		/// </summary>
		public DateTime DateAdded { get; private set; }

		/// <summary>
		/// The text of the note or comment.
		/// </summary>
		public string Value { get; private set; }

        /// <summary>
        /// If this event is a comment then this is the ID of the comment.
        /// </summary>
        public string CommentId { get; private set; }

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
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);

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
		Favorite
	}
}
