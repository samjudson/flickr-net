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
	[Serializable]
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
	/// What type of content is the upload representing.
	/// </summary>
	[Serializable]
	public enum ContentType
	{
		/// <summary>
		/// No content type specified.
		/// </summary>
		None = 0,
		/// <summary>
		/// For normal photographs.
		/// </summary>
		Photo = 1,
		/// <summary>
		/// For screenshots.
		/// </summary>
		Screenshot = 2,
		/// <summary>
		/// For other uploads, such as artwork.
		/// </summary>
		Other = 3
	}

	/// <summary>
	/// The values for a content type search.
	/// </summary>
	[Serializable]
	public enum ContentTypeSearch
	{
		/// <summary>
		/// No content type specified.
		/// </summary>
		None = 0,
		/// <summary>
		/// For normal photographs.
		/// </summary>
		PhotosOnly = 1,
		/// <summary>
		/// For screenshots.
		/// </summary>
		ScreenshotsOnly = 2,
		/// <summary>
		/// For other uploads, such as artwork.
		/// </summary>
		OtherOnly = 3,
		/// <summary>
		/// Search for photos and screenshots
		/// </summary>
		PhotosAndScreenshots = 4,
		/// <summary>
		/// Search for screenshots and others
		/// </summary>
		ScreenshotsAndOthers = 5,
		/// <summary>
		/// Search for photos and other things
		/// </summary>
		PhotosAndOthers = 6,
		/// <summary>
		/// Search for anything (default)
		/// </summary>
		All = 7
	}

	/// <summary>
	/// The units of a radius search
	/// </summary>
	[Serializable]
	public enum RadiusUnits
	{
		/// <summary>
		/// The radius units are unspecified.
		/// </summary>
		None = 0,
		/// <summary>
		/// The radius is in Kilometers.
		/// </summary>
		Kilometers = 1,
		/// <summary>
		/// The radius is in Miles.
		/// </summary>
		Miles = 0
	}

	/// <summary>
	/// Allows you to perform a search on a users contacts.
	/// </summary>
	[Serializable]
	public enum ContactSearch
	{
		/// <summary>
		/// Do not perform a contact search.
		/// </summary>
		None = 0,
		/// <summary>
		/// Perform a search on all a users contacts.
		/// </summary>
		AllContacts = 1,
		/// <summary>
		/// Perform a search on only a users friends and family contacts.
		/// </summary>
		FriendsAndFamilyOnly = 2
	}

	/// <summary>
	/// Safety level of the photographic image.
	/// </summary>
	[Serializable]
	public enum SafetyLevel
	{
		/// <summary>
		/// No safety level specified.
		/// </summary>
		None = 0,
		/// <summary>
		/// Very safe (suitable for a global family audience).
		/// </summary>
		Safe = 1,
		/// <summary>
		/// Moderate (the odd articstic nude is ok, but thats the limit).
		/// </summary>
		Moderate = 2,
		/// <summary>
		/// Restricted (suitable for over 18s only).
		/// </summary>
		Restricted = 3
	}

	/// <summary>
	/// Determines weither the photo is visible in public searches. The default is 1, Visible.
	/// </summary>
	[Serializable]
	public enum HiddenFromSearch
	{
		/// <summary>
		/// No preference specified, defaults to your preferences on Flickr.
		/// </summary>
		None = 0,
		/// <summary>
		/// Photo is visible to public searches.
		/// </summary>
		Visible = 1,
		/// <summary>
		/// photo is hidden from public searches.
		/// </summary>
		Hidden = 2
	}


	/// <summary>
	/// Used to specify where all tags must be matched or any tag to be matched.
	/// </summary>
	[Serializable]
	public enum MachineTagMode
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
		AllTags
	}

	/// <summary>
	/// When searching for photos you can filter on the privacy of the photos.
	/// </summary>
	[Serializable]
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
		/// Show photos which are marked as private but viewable by friends.
		/// </summary>
		PrivateVisibleToFriends = 2,
		/// <summary>
		/// Show photos which are marked as private but viewable by family contacts.
		/// </summary>
		PrivateVisibleToFamily = 3,
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
	[Serializable]
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
