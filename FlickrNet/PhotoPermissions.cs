using System;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Permissions for the selected photo.
	/// </summary>
	[System.Serializable]
	public class PhotoPermissions
	{
		private string _photoId;
		private bool _isPublic;
		private bool _isFriend;
		private bool _isFamily;
		private PermissionAddMeta _permAddMeta;
		private PermissionComment _permComment;

		internal PhotoPermissions(XmlElement element)
		{
			if( element.Attributes.GetNamedItem("id") != null )
				_photoId = element.Attributes.GetNamedItem("id").Value;
			if( element.Attributes.GetNamedItem("ispublic") != null )
				_isPublic = element.Attributes.GetNamedItem("ispublic").Value=="1";
			if( element.Attributes.GetNamedItem("isfamily") != null )
				_isFamily = element.Attributes.GetNamedItem("isfamily").Value=="1";
			if( element.Attributes.GetNamedItem("isfriend") != null )
				_isFriend = element.Attributes.GetNamedItem("isfriend").Value=="1";
			if( element.Attributes.GetNamedItem("permcomment") != null )
				_permComment = (PermissionComment)Enum.Parse(typeof(PermissionComment), element.Attributes.GetNamedItem("permcomment").Value, true);
			if( element.Attributes.GetNamedItem("permaddmeta") != null )
				_permAddMeta = (PermissionAddMeta)Enum.Parse(typeof(PermissionAddMeta), element.Attributes.GetNamedItem("permaddmeta").Value, true);
		}

		/// <remarks/>
		public string PhotoId
		{
			get { return _photoId; }
		}

		/// <remarks/>
		public bool IsPublic
		{
			get { return _isPublic; }
		}
    
		/// <remarks/>
		public bool IsFriend
		{
			get { return _isFriend; }
		}
    
		/// <remarks/>
		public bool IsFamily
		{
			get { return _isFamily; }
		}

		/// <remarks/>
		public PermissionComment PermissionComment
		{
			get { return _permComment; }
		}

		/// <remarks/>
		public PermissionAddMeta PermissionAddMeta
		{
			get { return _permAddMeta; }
		}
	}

}
