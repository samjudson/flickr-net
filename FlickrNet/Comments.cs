using System;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Parent object containing details of a photos comments.
	/// </summary>
	internal class PhotoComments
	{
		static private string _photoId;

		static private Comment[] _comments;

		static internal Comment[] GetComments(XmlNode node)
		{
			if( node.Attributes["photo_id"] != null )
				_photoId = node.Attributes["photo_id"].Value;
			XmlNodeList nodes = node.SelectNodes("comment");
			_comments = new Comment[nodes.Count];

			for(int i = 0; i < _comments.Length; i++)
			{
				_comments[i] = new Comment(_photoId, nodes[i]);
			}

			return _comments;
		}
	}

	/// <summary>
	/// Contains the details of a comment made on a photo.
	/// returned by the <see cref="Flickr.PhotosCommentsGetList"/> method.
	/// </summary>
	public class Comment
	{
		private string _photoId;
		private string _authorUserId;
		private string _authorUserName;
		private string _commentId;
		private Uri _permaLink;
		private DateTime _dateCreated;
		private string _comment;

		/// <summary>
		/// The photo id associated with this comment.
		/// </summary>
		public string PhotoId
		{
			get { return _photoId; }
		}

		/// <summary>
		/// The comment id of this comment.
		/// </summary>
		public string CommentId
		{
			get { return _commentId; }
		}

		/// <summary>
		/// The user id of the author of the comment.
		/// </summary>
		public string AuthorUserId
		{
			get { return _authorUserId; }
		}

		/// <summary>
		/// The username (screen name) of the author of the comment.
		/// </summary>
		public string AuthorUserName
		{
			get { return _authorUserName; }
		}

		/// <summary>
		/// The permalink to the comment on the photos web page.
		/// </summary>
		public Uri Permalink
		{
			get { return _permaLink; }
		}

		/// <summary>
		/// The date and time that the comment was created.
		/// </summary>
		public DateTime DateCreated
		{
			get { return _dateCreated; }
		}

		/// <summary>
		/// The comment text (can contain HTML).
		/// </summary>
		public string CommentHtml
		{
			get { return _comment; }
		}

		internal Comment(string photoId, XmlNode node)
		{
			_photoId = photoId;

			if( node.Attributes["id"] != null )
				_commentId = node.Attributes["id"].Value;
			if( node.Attributes["author"] != null )
				_authorUserId = node.Attributes["author"].Value;
			if( node.Attributes["authorname"] != null )
				_authorUserName = node.Attributes["authorname"].Value;
			if( node.Attributes["permalink"] != null )
				_permaLink = new Uri(node.Attributes["permalink"].Value);
			if( node.Attributes["datecreate"] != null )
				_dateCreated = Utils.UnixTimestampToDate(node.Attributes["datecreate"].Value);
			if( node.InnerXml.Length > 0 )
				_comment = node.InnerText;
		}
	}
}
