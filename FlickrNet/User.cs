using System;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Contains details of a user
	/// </summary>
	[System.Serializable]
	public class FoundUser
	{
		private string _userId;
		private string _username;

		/// <summary>
		/// The ID of the found user.
		/// </summary>
		public string UserId
		{
			get { return _userId; }
		}

		/// <summary>
		/// The username of the found user.
		/// </summary>
		public string Username 
		{
			get { return _username; }
		}

		internal FoundUser(string userId, string username)
		{
			_userId = userId;
			_username = username;
		}

		internal FoundUser(XmlNode node)
		{
			if( node.Attributes["nsid"] != null )
				_userId = node.Attributes["nsid"].Value;
			if( node.Attributes["id"] != null )
				_userId = node.Attributes["id"].Value;
			if( node.Attributes["username"] != null )
				_username = node.Attributes["username"].Value;
			if( node.SelectSingleNode("username") != null )
				_username = node.SelectSingleNode("username").InnerText;
		}
	}

	/// <summary>
	/// The upload status of the user, as returned by <see cref="Flickr.PeopleGetUploadStatus"/>.
	/// </summary>
	[System.Serializable]
	public class UserStatus
	{
		private bool _isPro;
		private string _userId;
		private string _username;
		private long _bandwidthMax;
		private long _bandwidthUsed;
		private long _filesizeMax;

		internal UserStatus(XmlNode node)
		{
			if( node == null ) 
				throw new ArgumentNullException("node");

			if( node.Attributes["id"] != null )
				_userId = node.Attributes["id"].Value;
			if( node.Attributes["nsid"] != null )
				_userId = node.Attributes["nsid"].Value;
			if( node.Attributes["ispro"] != null )
				_isPro = node.Attributes["ispro"].Value=="1";
			if( node.SelectSingleNode("username") != null )
				_username = node.SelectSingleNode("username").InnerText;
			XmlNode bandwidth = node.SelectSingleNode("bandwidth");
			if( bandwidth != null )
			{
				_bandwidthMax = Convert.ToInt64(bandwidth.Attributes["max"].Value);
				_bandwidthUsed = Convert.ToInt64(bandwidth.Attributes["used"].Value);
			}
			XmlNode filesize = node.SelectSingleNode("filesize");
			if( filesize != null )
			{
				_filesizeMax = Convert.ToInt64(filesize.Attributes["max"].Value);
			}
		}
		/// <summary>
		/// The id of the user object.
		/// </summary>
		public string UserId
		{
			get { return _userId; }
		}

		/// <summary>
		/// The Username of the selected user.
		/// </summary>
		public string UserName
		{
			get { return _username; }
		}

		/// <summary>
		/// Is the current user a Pro account.
		/// </summary>
		public bool IsPro
		{
			get { return _isPro; }
		}

		/// <summary>
		/// The maximum bandwidth (in bytes) that the user can use each month.
		/// </summary>
		public long BandwidthMax
		{
			get { return _bandwidthMax; }
		}

		/// <summary>
		/// The number of bytes of the current months bandwidth that the user has used.
		/// </summary>
		public long BandwidthUsed
		{
			get { return _bandwidthUsed; }
		}

		/// <summary>
		/// The maximum filesize (in bytes) that the user is allowed to upload.
		/// </summary>
		public long FilesizeMax
		{
			get { return _filesizeMax; }
		}

		/// <summary>
		/// <see cref="Double"/> representing the percentage bandwidth used so far. Will range from 0 to 1.
		/// </summary>
		public Double PercentageUsed
		{
			get { return BandwidthUsed * 1.0 / BandwidthMax; }
		}
	}
}