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
	public class ActivityEvent
	{
		private ActivityEventType _type;
		private string _userId;
		private string _userName;
		private DateTime _dateAdded;
		private string _content;

		/// <summary>
		/// The <see cref="ActivityEventType"/> of the event, either Comment or Note.
		/// </summary>
		public ActivityEventType EventType
		{
			get { return _type; }
		}

		/// <summary>
		/// The user id of the user who made the comment or note.
		/// </summary>
		public string UserId
		{
			get { return _userId; }
		}

		/// <summary>
		/// The screen name of the user who made the comment or note.
		/// </summary>
		public string UserName
		{
			get { return _userName; }
		}

		/// <summary>
		/// The date the note or comment was added.
		/// </summary>
		public DateTime DateAdded
		{
			get { return _dateAdded; }
		}

		/// <summary>
		/// The text of the note or comment.
		/// </summary>
		public string Value
		{
			get { return _content; }
		}

		internal ActivityEvent(XmlNode eventNode)
		{
			XmlNode node;

			node = eventNode.Attributes.GetNamedItem("type");
			if( node == null ) 
				_type = ActivityEventType.Unknown;
			else if( node.Value == "comment" )
				_type = ActivityEventType.Comment;
			else if( node.Value == "note" )
				_type = ActivityEventType.Note;
			else if( node.Value == "fave" )
				_type = ActivityEventType.Favourite;
			else
				_type = ActivityEventType.Unknown;

			node = eventNode.Attributes.GetNamedItem("user");
			if( node != null ) _userId = node.Value;

			node = eventNode.Attributes.GetNamedItem("username");
			if( node != null ) _userName = node.Value;

			node = eventNode.Attributes.GetNamedItem("dateadded");
			if( node != null ) _dateAdded = Utils.UnixTimestampToDate(node.Value);

			node = eventNode.FirstChild;
			if( node != null && node.NodeType == XmlNodeType.Text ) _content = node.Value;

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
		Favourite
	}
}
