using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Provides details of a particular group.
	/// </summary>
	/// <remarks>Used by <see cref="Flickr.GroupsBrowse()"/> and
	/// <see cref="Flickr.GroupsBrowse(string)"/>.</remarks>
	[System.Serializable]
	public class Group
	{
		/// <summary>
		/// The id of the group.
		/// </summary>
		[XmlAttribute("nsid", Form=XmlSchemaForm.Unqualified)]
		public string GroupId;
    
		/// <summary>
		/// The name of the group
		/// </summary>
		[XmlAttribute("name", Form=XmlSchemaForm.Unqualified)]
		public string GroupName;

		/// <summary>
		/// The number of memebers of the group.
		/// </summary>
		[XmlAttribute("members", Form=XmlSchemaForm.Unqualified)]
		public long Members;
	}

	/// <summary>
	/// Provides details of a particular group.
	/// </summary>
	/// <remarks>
	/// Used by the Url methods and <see cref="Flickr.GroupsGetInfo"/> method.
	/// The reason for a <see cref="Group"/> and <see cref="GroupFullInfo"/> are due to xml serialization
	/// incompatabilities.
	/// </remarks>
	[System.Serializable]
	public class GroupFullInfo
	{
		internal GroupFullInfo()
		{
		}

		internal GroupFullInfo(XmlNode node)
		{
			if( node.Attributes.GetNamedItem("id") != null )
				_groupId = node.Attributes.GetNamedItem("id").Value;
			if( node.SelectSingleNode("name") != null )
				_groupName = node.SelectSingleNode("name").InnerText;
			if( node.SelectSingleNode("description") != null )
				_description = node.SelectSingleNode("description").InnerXml;
            if (node.SelectSingleNode("iconserver") != null)
                _iconServer = node.SelectSingleNode("iconserver").InnerText;
            if (node.SelectSingleNode("members") != null)
				_members = int.Parse(node.SelectSingleNode("members").InnerText);
			if( node.SelectSingleNode("privacy") != null )
				_privacy = (PoolPrivacy)int.Parse(node.SelectSingleNode("privacy").InnerText);

			if( node.SelectSingleNode("throttle") != null )
			{
				XmlNode throttle = node.SelectSingleNode("throttle");
				ThrottleInfo = new GroupThrottleInfo();
				if( throttle.Attributes.GetNamedItem("count") != null )
					ThrottleInfo.Count = int.Parse(throttle.Attributes.GetNamedItem("count").Value);
				if( throttle.Attributes.GetNamedItem("mode") != null )
					ThrottleInfo.setMode(throttle.Attributes.GetNamedItem("mode").Value);
				if( throttle.Attributes.GetNamedItem("remaining") != null )
					ThrottleInfo.Remaining = int.Parse(throttle.Attributes.GetNamedItem("remaining").Value);
			}
		}

        private string _groupId;
        private string _groupName;
        private string _description;
        private long _members;
        private string _iconServer;
        private PoolPrivacy _privacy;

		/// <remarks/>
        public string GroupId
        {
            get { return _groupId; }
        }
    
		/// <remarks/>
        public string GroupName
        {
            get { return _groupName; }
        }

		/// <remarks/>
        public string Description
        {
            get { return _description; }
        }

		/// <remarks/>
        public long Members
        {
            get { return _members; }
        }

		/// <summary>
		/// The server number used for the groups icon.
		/// </summary>
        public string IconServer
        {
            get { return _iconServer; }
        }

		/// <remarks/>
        public PoolPrivacy Privacy
        {
            get { return _privacy; }
        }
	
		/// <remarks/>
		public GroupThrottleInfo ThrottleInfo;

		/// <summary>
		/// Methods for automatically converting a <see cref="GroupFullInfo"/> object into
		/// and instance of a <see cref="Group"/> object.
		/// </summary>
		/// <param name="groupInfo">The incoming object.</param>
		/// <returns>The <see cref="Group"/> instance.</returns>
		public static implicit operator Group( GroupFullInfo groupInfo )	
		{
			Group g = new Group();
			g.GroupId = groupInfo.GroupId;
			g.GroupName = groupInfo.GroupName;
			g.Members = groupInfo.Members;

			return g;
		}

		/// <summary>
		/// Converts the current <see cref="GroupFullInfo"/> into an instance of the
		/// <see cref="Group"/> class.
		/// </summary>
		/// <returns>A <see cref="Group"/> instance.</returns>
		public Group ToGroup()
		{
			return (Group)this;
		}

	}

	/// <summary>
	/// Throttle information about a group (i.e. posting limit)
	/// </summary>
	public class GroupThrottleInfo
	{
		/// <summary>
		/// The number of posts in each period allowed to this group.
		/// </summary>
		public int Count;

		/// <summary>
		/// The posting limit mode for a group.
		/// </summary>
		public GroupThrottleMode Mode;

		internal void setMode(string mode)
		{
			switch(mode)
			{
				case "day":
					Mode = GroupThrottleMode.PerDay;
					break;
				case "week":
					Mode = GroupThrottleMode.PerWeek;
					break;
				case "month":
					Mode = GroupThrottleMode.PerMonth;
					break;
				case "ever":
					Mode = GroupThrottleMode.Ever;
					break;
				case "none":
					Mode = GroupThrottleMode.NoLimit;
					break;
				case "disabled":
					Mode = GroupThrottleMode.Disabled;
					break;
				default:
					throw new ArgumentException(string.Format("Unknown mode found {0}", mode), "mode");
			}
		}

		/// <summary>
		/// The number of remainging posts allowed by this user. If unauthenticated then this will be zero.
		/// </summary>
		public int Remaining;
	}

	/// <summary>
	/// The posting limit most for a group.
	/// </summary>
	public enum GroupThrottleMode
	{
		/// <summary>
		/// Per day posting limit.
		/// </summary>
		PerDay,
		/// <summary>
		/// Per week posting limit.
		/// </summary>
		PerWeek,
		/// <summary>
		/// Per month posting limit.
		/// </summary>
		PerMonth,
		/// <summary>
		/// No posting limit.
		/// </summary>
		NoLimit,
		/// <summary>
		/// Posting limit is total number of photos in the group.
		/// </summary>
		Ever,
		/// <summary>
		/// Posting is disabled to this group.
		/// </summary>
		Disabled

	}

	/// <summary>
	/// Information about a group the authenticated user is a member of.
	/// </summary>
	public class MemberGroupInfo
	{
		internal static MemberGroupInfo[] GetMemberGroupInfo(XmlNode node)
		{
			XmlNodeList list = node.SelectNodes("//group");
			MemberGroupInfo[] infos = new MemberGroupInfo[list.Count];
			for(int i = 0; i < infos.Length; i++)
			{
				infos[i] = new MemberGroupInfo(list[i]);
			}
			return infos;
		}

		internal MemberGroupInfo(XmlNode node)
		{
			if( node.Attributes["nsid"] != null )
				_groupId = node.Attributes["nsid"].Value;
			if( node.Attributes["name"] != null )
				_groupName = node.Attributes["name"].Value;
			if( node.Attributes["admin"] != null )
				_isAdmin = node.Attributes["admin"].Value=="1";
			if( node.Attributes["privacy"] != null )
				_privacy = (PoolPrivacy)Enum.Parse(typeof(PoolPrivacy),node.Attributes["privacy"].Value, true);
			if( node.Attributes["photos"] != null )
				_numberOfPhotos = Int32.Parse(node.Attributes["photos"].Value);
			if( node.Attributes["iconserver"] != null )
				_iconServer = node.Attributes["iconserver"].Value;
		}

		private string _groupId;

		/// <summary>
		/// Property which returns the group id for the group.
		/// </summary>
		public string GroupId
		{
			get { return _groupId; }
		}

		private string _groupName;

		/// <summary>The group name.</summary>
		public string GroupName
		{
			get { return _groupName; }
		}

		private bool _isAdmin;

		/// <summary>
		/// True if the user is the admin for the group, false if they are not.
		/// </summary>
		public bool IsAdmin
		{
			get { return _isAdmin; }
		}
	
		private long _numberOfPhotos;

		/// <summary>
		/// The number of photos currently in the group pool.
		/// </summary>
		public long NumberOfPhotos
		{ 
			get { return _numberOfPhotos; }
		}

		private PoolPrivacy _privacy;

		/// <summary>
		/// The privacy of the pool (see <see cref="PoolPrivacy"/>).
		/// </summary>
		public PoolPrivacy Privacy
		{
			get { return _privacy; }
		}

		private string _iconServer;

		/// <summary>
		/// The server number for the group icon.
		/// </summary>
		public string IconServer
		{
			get { return _iconServer; }
		}

		/// <summary>
		/// The URL for the group icon.
		/// </summary>
		public Uri GroupIconUrl
		{
			get { return new Uri(String.Format("http://static.flickr.com/{0}/buddyicons/{1}.jpg", IconServer, GroupId)); }
		}

		/// <summary>
		/// The URL for the group web page.
		/// </summary>
		public Uri GroupUrl
		{
			get { return new Uri(String.Format("http://www.flickr.com/groups/{0}/", GroupId)); }
		}

	}

	/// <summary>
	/// Information about public groups for a user.
	/// </summary>
	[System.Serializable]
	public class PublicGroupInfo
	{
		internal static PublicGroupInfo[] GetPublicGroupInfo(XmlNode node)
		{
			XmlNodeList list = node.SelectNodes("//group");
			PublicGroupInfo[] infos = new PublicGroupInfo[list.Count];
			for(int i = 0; i < infos.Length; i++)
			{
				infos[i] = new PublicGroupInfo(list[i]);
			}
			return infos;
		}

		internal PublicGroupInfo(XmlNode node)
		{
			if( node.Attributes["nsid"] != null )
				_groupId = node.Attributes["nsid"].Value;
			if( node.Attributes["name"] != null )
				_groupName = node.Attributes["name"].Value;
			if( node.Attributes["admin"] != null )
				_isAdmin = node.Attributes["admin"].Value=="1";
			if( node.Attributes["eighteenplus"] != null )
				_isEighteenPlus = node.Attributes["eighteenplus"].Value=="1";
		}

		private string _groupId;
    
		/// <summary>
		/// Property which returns the group id for the group.
		/// </summary>
		public string GroupId
		{
			get { return _groupId; }
		}

		private string _groupName;

		/// <summary>The group name.</summary>
		public string GroupName
		{
			get { return _groupName; }
		}

		private bool _isAdmin;

		/// <summary>
		/// True if the user is the admin for the group, false if they are not.
		/// </summary>
		public bool IsAdmin
		{
			get { return _isAdmin; }
		}

		private bool _isEighteenPlus;
	
		/// <summary>
		/// Will contain 1 if the group is restricted to people who are 18 years old or over, 0 if it is not.
		/// </summary>
		public bool EighteenPlus
		{
			get { return _isEighteenPlus; }
		}

		/// <summary>
		/// The URL for the group web page.
		/// </summary>
		public Uri GroupUrl
		{
			get { return new Uri(String.Format("http://www.flickr.com/groups/{0}/", GroupId)); }
		}
	}

	/// <summary>
	/// The various pricay settings for a group.
	/// </summary>
	[System.Serializable]
	public enum PoolPrivacy
	{
		/// <summary>
		/// No privacy setting specified.
		/// </summary>
		[XmlEnum("0")]
		None = 0,

		/// <summary>
		/// The group is a private group. You cannot view pictures or posts until you are a 
		/// member. The group is also invite only.
		/// </summary>
		[XmlEnum("1")]
		Private = 1,
		/// <summary>
		/// A public group where you can see posts and photos in the group. The group is however invite only.
		/// </summary>
		[XmlEnum("2")]
		InviteOnlyPublic = 2,
		/// <summary>
		/// A public group.
		/// </summary>
		[XmlEnum("3")]
		OpenPublic = 3
	}

}
