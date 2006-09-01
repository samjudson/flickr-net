using System;
using System.Xml.Serialization;

namespace FlickrNet
{
	/// <summary>
	/// A list of service the Flickr.Net API Supports.
	/// </summary>
	/// <remarks>
	/// Not all methods are supported by all service. Behaviour of the library may be unpredictable if not using Flickr
	/// as your service.
	/// </remarks>
	public enum SupportedService
	{
		/// <summary>
		/// Flickr - http://www.flickr.com/services/api
		/// </summary>
		Flickr = 0,
		/// <summary>
		/// Zooomr - http://blog.zooomr.com/2006/03/27/attention-developers/
		/// </summary>
		Zooomr = 1,
		/// <summary>
		/// 23HQ = http://www.23hq.com/doc/api/
		/// </summary>
		TwentyThreeHQ = 2
	}
	
	/// <summary>
	/// Used to specify where all tags must be matched or any tag to be matched.
	/// </summary>
	public enum TagMode
	{
		/// <summary>
		/// No tag mode specified.
		/// </summary>
		None,
		/// <summary>
		/// Any tag must match, but not all.
		/// </summary>
		AnyTag,
		/// <summary>
		/// All tags must be found.
		/// </summary>
		AllTags,
		/// <summary>
		/// Uncodumented and unsupported tag mode where boolean operators are supported.
		/// </summary>
		Boolean
	}


	/// <summary>
	/// When searching for photos you can filter on the privacy of the photos.
	/// </summary>
	public enum PrivacyFilter
	{
		/// <summary>
		/// Do not filter.
		/// </summary>
		None = 0,
		/// <summary>
		/// Show only public photos.
		/// </summary>
		PublicPhotos = 1,
		/// <summary>
		/// Show photos which are marked as private but viewable by family contacts.
		/// </summary>
		PrivateVisibleToFamily = 2,
		/// <summary>
		/// Show photos which are marked as private but viewable by friends.
		/// </summary>
		PrivateVisibleToFriends = 3,
		/// <summary>
		/// Show photos which are marked as private but viewable by friends and family contacts.
		/// </summary>
		PrivateVisibleToFriendsFamily = 4,
		/// <summary>
		/// Show photos which are marked as completely private.
		/// </summary>
		CompletelyPrivate = 5
	}

	/// <summary>
	/// An enumeration defining who can add comments.
	/// </summary>
	public enum PermissionComment
	{
		/// <summary>
		/// Nobody.
		/// </summary>
		[XmlEnum("0")]
		Nobody = 0,
		/// <summary>
		/// Friends and family only.
		/// </summary>
		[XmlEnum("1")]
		FriendsAndFamily = 1,
		/// <summary>
		/// Contacts only.
		/// </summary>
		[XmlEnum("2")]
		ContactsOnly = 2,
		/// <summary>
		/// All Flickr users.
		/// </summary>
		[XmlEnum("3")]
		Everybody = 3
	}

	/// <summary>
	/// An enumeration defining who can add meta data (tags and notes).
	/// </summary>
	public enum PermissionAddMeta
	{
		/// <summary>
		/// The owner of the photo only.
		/// </summary>
		[XmlEnum("0")]
		Owner = 0,
		/// <summary>
		/// Friends and family only.
		/// </summary>
		[XmlEnum("1")]
		FriendsAndFamily = 1,
		/// <summary>
		/// All contacts.
		/// </summary>
		[XmlEnum("2")]
		Contacts = 2,
		/// <summary>
		/// All Flickr users.
		/// </summary>
		[XmlEnum("3")]
		Everybody = 3
	}


}
