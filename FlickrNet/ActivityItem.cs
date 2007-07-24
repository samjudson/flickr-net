using System;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Activity class used for <see cref="Flickr.ActivityUserPhotos()"/>
	/// and <see cref="Flickr.ActivityUserComments"/>.
	/// </summary>
	public class ActivityItem
	{
		private ActivityItemType _activityType;
		private string _id;
		private string _secret;
		private string _server;
		private string _farm;
		private int _commentsNew;
		private int _commentsOld;
		private int _comments;
		private string _ownerId;
		private int _more;
		private int _views;

		// Photoset specific
		private int _photos = -1;
		private string _primaryId = null;

		// Photo specific
		private int _notesNew = -1;
		private int _notesOld = -1;
		private int _notes = -1;
		private int _favs = -1;

		private string _title;
		private ActivityEvent[] _events;

		/// <summary>
		/// The <see cref="ActivityItemType"/> of the item.
		/// </summary>
		public ActivityItemType ItemType
		{
			get { return _activityType; }
		}

		/// <summary>
		/// The ID of either the photoset or the photo.
		/// </summary>
		public string Id
		{
			get { return _id; }
		}

		/// <summary>
		/// The secret for either the photo, or the primary photo for the photoset.
		/// </summary>
		public string Secret
		{
			get { return _secret; }
		}

		/// <summary>
		/// The server for either the photo, or the primary photo for the photoset.
		/// </summary>
		public string Server
		{
			get { return _server; }
		}

		/// <summary>
		/// The server farm for either the photo, or the primary photo for the photoset.
		/// </summary>
		public string Farm
		{
			get { return _farm; }
		}

		/// <summary>
		/// The title of the photoset or photo.
		/// </summary>
		public string Title
		{
			get { return _title; }
		}

		/// <summary>
		/// The number of new comments within the given time frame. 
		/// </summary>
		/// <remarks>
		/// Only applicable for <see cref="Flickr.ActivityUserPhotos()"/>.
		/// </remarks>
		public int CommentsNew
		{
			get { return _commentsNew; }
		}

		/// <summary>
		/// The number of old comments within the given time frame. 
		/// </summary>
		/// <remarks>
		/// Only applicable for <see cref="Flickr.ActivityUserPhotos()"/>.
		/// </remarks>
		public int CommentsOld
		{
			get { return _commentsOld; }
		}

		/// <summary>
		/// The number of comments on the item. 
		/// </summary>
		/// <remarks>
		/// Only applicable for <see cref="Flickr.ActivityUserComments"/>.
		/// </remarks>
		public int Comments
		{
			get { return _comments; }
		}

		/// <summary>
		/// Gets the number of views for this photo or photoset.
		/// </summary>
		public int Views
		{
			get { return _views; }
		}

		/// <summary>
		/// You want more! You got it!
		/// </summary>
		/// <remarks>
		/// Actually, not sure what this it for!
		/// </remarks>
		public int More
		{
			get { return _more; }
		}

		/// <summary>
		/// The user id of the owner of this item.
		/// </summary>
		public string OwnerId
		{
			get { return _ownerId; }
		}

		/// <summary>
		/// If the type is a photoset then this contains the number of photos in the set. Otherwise returns -1.
		/// </summary>
		public int Photos
		{
			get { return _photos; }
		}

		/// <summary>
		/// If this is a photoset then returns the primary photo id, otherwise will be null (<code>Nothing</code> in VB.Net).
		/// </summary>
		public string PrimaryPhotoId
		{
			get { return _primaryId; }
		}

		/// <summary>
		/// The number of new notes within the given time frame. 
		/// </summary>
		/// <remarks>
		/// Only applicable for photos and when calling <see cref="Flickr.ActivityUserPhotos()"/>.
		/// </remarks>
		public int NotesNew
		{
			get { return _notesNew; }
		}

		/// <summary>
		/// The number of old notes within the given time frame. 
		/// </summary>
		/// <remarks>
		/// Only applicable for photos and when calling <see cref="Flickr.ActivityUserPhotos()"/>.
		/// </remarks>
		public int NotesOld
		{
			get { return _notesOld; }
		}

		/// <summary>
		/// The number of comments on the photo.
		/// </summary>
		/// <remarks>
		/// Only applicable for photos and when calling <see cref="Flickr.ActivityUserComments"/>.
		/// </remarks>
		public int Notes
		{
			get { return _notes; }
		}

		/// <summary>
		/// If the type is a photo then this contains the number of favourites in the set. Otherwise returns -1.
		/// </summary>
		public int Favourites
		{
			get { return _favs; }
		}

		/// <summary>
		/// The events that comprise this activity item.
		/// </summary>
		public ActivityEvent[] Events
		{
			get { return _events; }
		}

		internal ActivityItem(XmlNode itemNode)
		{
			XmlNode node;

			node = itemNode.Attributes.GetNamedItem("type");

			if( node == null )
				_activityType = ActivityItemType.Unknown;
			else if( node.Value == "photoset" )
				_activityType = ActivityItemType.Photoset;
			else if( node.Value == "photo" )
				_activityType = ActivityItemType.Photo;
			else
				_activityType = ActivityItemType.Unknown;

			node = itemNode.Attributes.GetNamedItem("owner");
			if( node != null ) _ownerId = node.Value;

			node = itemNode.Attributes.GetNamedItem("id");
			if( node != null ) _id = node.Value;

			node = itemNode.Attributes.GetNamedItem("secret");
			if( node != null ) _secret = node.Value;

			node = itemNode.Attributes.GetNamedItem("server");
			if( node != null ) _server = node.Value;

			node = itemNode.Attributes.GetNamedItem("farm");
			if( node != null ) _farm = node.Value;

			node = itemNode.Attributes.GetNamedItem("commentsnew");
			if( node != null ) _commentsNew = Convert.ToInt32(node.Value);

			node = itemNode.Attributes.GetNamedItem("commentsold");
			if( node != null ) _commentsOld = Convert.ToInt32(node.Value);

			node = itemNode.Attributes.GetNamedItem("comments");
			if( node != null ) _comments = Convert.ToInt32(node.Value);

			node = itemNode.Attributes.GetNamedItem("more");
			if( node != null ) _more = Convert.ToInt32(node.Value);

			node = itemNode.Attributes.GetNamedItem("views");
			if( node != null ) _views = Convert.ToInt32(node.Value);

			node = itemNode.SelectSingleNode("title");
			if( node != null ) _title = node.InnerText;

			XmlNodeList list = itemNode.SelectNodes("activity/event");
			
			_events = new ActivityEvent[list.Count];

			for(int i = 0; i < _events.Length; i++)
			{
				node = list[i];
				_events[i] = new ActivityEvent(node);
			}

			// Photoset specific
			// Photos, PrimaryPhotoId

			node = itemNode.Attributes.GetNamedItem("photos");
			if( node != null ) _photos = Convert.ToInt32(node.Value);

			node = itemNode.Attributes.GetNamedItem("primary");
			if( node != null ) _primaryId = node.Value;

			// Photo specific
			// NodesNew and NodesOld, Favourites

			node = itemNode.Attributes.GetNamedItem("notesnew");
			if( node != null ) _notesNew = Convert.ToInt32(node.Value);

			node = itemNode.Attributes.GetNamedItem("notesold");
			if( node != null ) _notesOld = Convert.ToInt32(node.Value);

			node = itemNode.Attributes.GetNamedItem("notes");
			if( node != null ) _notes = Convert.ToInt32(node.Value);

			node = itemNode.Attributes.GetNamedItem("faves");
			if( node != null ) _favs = Convert.ToInt32(node.Value);
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
