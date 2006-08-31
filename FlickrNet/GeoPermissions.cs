using System;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Permissions for the selected photo.
	/// </summary>
	[System.Serializable]
	public class GeoPermissions
	{
		private string _photoId;
		private bool _isPublic;
		private bool _isContact;
		private bool _isFriend;
		private bool _isFamily;

		internal GeoPermissions(XmlElement element)
		{
			if( element.Attributes.GetNamedItem("id") != null )
				_photoId = element.Attributes.GetNamedItem("id").Value;
			if( element.Attributes.GetNamedItem("ispublic") != null )
				_isPublic = element.Attributes.GetNamedItem("ispublic").Value=="1";
			if( element.Attributes.GetNamedItem("iscontact") != null )
				_isContact = element.Attributes.GetNamedItem("iscontact").Value=="1";
			if( element.Attributes.GetNamedItem("isfamily") != null )
				_isFamily = element.Attributes.GetNamedItem("isfamily").Value=="1";
			if( element.Attributes.GetNamedItem("isfriend") != null )
				_isFriend = element.Attributes.GetNamedItem("isfriend").Value=="1";
		}

		/// <summary>
		/// The ID for the photo whose permissions these are.
		/// </summary>
		public string PhotoId
		{
			get { return _photoId; }
		}

		/// <summary>
		/// Are the general unwashed (public) allowed to see the Geo Location information for this photo.
		/// </summary>
		public bool IsPublic
		{
			get { return _isPublic; }
		}
    
		/// <summary>
		/// Are contacts allowed to see the Geo Location information for this photo.
		/// </summary>
		public bool IsContact
		{
			get { return _isContact; }
		}
    
		/// <summary>
		/// Are friends allowed to see the Geo Location information for this photo.
		/// </summary>
		public bool IsFriend
		{
			get { return _isFriend; }
		}
    
		/// <summary>
		/// Are family allowed to see the Geo Location information for this photo.
		/// </summary>
		public bool IsFamily
		{
			get { return _isFamily; }
		}

	}
}
