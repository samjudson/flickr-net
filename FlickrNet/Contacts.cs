using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// Contains a list of <see cref="Contact"/> items for a given user.
	/// </summary>
	[System.Serializable]
	public class Contacts
	{
		/// <summary>
		/// An array of <see cref="Contact"/> items for the user.
		/// </summary>
		[XmlElement("contact", Form=XmlSchemaForm.Unqualified)]
		public Contact[] ContactCollection = new Contact[0];
	}

	/// <summary>
	/// Contains details of a contact for a particular user.
	/// </summary>
	[System.Serializable]
	public class Contact
	{
		/// <summary>
		/// The user id of the contact.
		/// </summary>
		[XmlAttribute("nsid", Form=XmlSchemaForm.Unqualified)]
		public string UserId;
    
		/// <summary>
		/// The username (or screen name) of the contact.
		/// </summary>
		[XmlAttribute("username", Form=XmlSchemaForm.Unqualified)]
		public string UserName;
    
		/// <summary>
		/// Is this contact marked as a friend contact?
		/// </summary>
		[XmlAttribute("friend", Form=XmlSchemaForm.Unqualified)]
		public int IsFriend;
    
		/// <summary>
		/// Is this user marked a family contact?
		/// </summary>
		[XmlAttribute("family", Form=XmlSchemaForm.Unqualified)]
		public int IsFamily;
    
		/// <summary>
		/// Unsure how to even set this!
		/// </summary>
		[XmlAttribute("ignored", Form=XmlSchemaForm.Unqualified)]
		public int IsIgnored;

		/// <summary>
		/// Is the user online at the moment (FlickrLive)
		/// </summary>
		[XmlAttribute("online", Form=XmlSchemaForm.Unqualified)]
		public int IsOnline;

		/// <summary>
		/// If the user is online, but marked as away, then this will contains their away message.
		/// </summary>
		[XmlText()]
		public string AwayDescription;
	}
}