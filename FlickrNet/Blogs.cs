using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// Contains a list of <see cref="Blog"/> items for the user.
	/// </summary>
	/// <remarks>
	/// <see cref="Blogs.BlogCollection"/> may be null if no blogs are specified.
	/// </remarks>
	[System.Serializable]
	public class Blogs
	{
		/// <summary>
		/// An array of <see cref="Blog"/> items for the user.
		/// </summary>
		[XmlElement("blog", Form=XmlSchemaForm.Unqualified)]
		public Blog[] BlogCollection;
	}

	/// <summary>
	/// Provides details of a specific blog, as configured by the user.
	/// </summary>
	[System.Serializable]
	public class Blog
	{
		/// <summary>
		/// The ID Flickr has assigned to the blog. Use this to post to the blog using 
        /// <see cref="Flickr.BlogPostPhoto(string, string, string, string)"/> or 
        /// <see cref="Flickr.BlogPostPhoto(string, string, string, string, string)"/>. 
		/// </summary>
		[XmlAttribute("id", Form=XmlSchemaForm.Unqualified)]
		public string BlogId;
    
		/// <summary>
		/// The name you have assigned to the blog in Flickr.
		/// </summary>
		[XmlAttribute("name", Form=XmlSchemaForm.Unqualified)]
		public string BlogName;
    
		/// <summary>
		/// The URL of the blog website.
		/// </summary>
		[XmlAttribute("url", Form=XmlSchemaForm.Unqualified)]
		public string BlogUrl;

		/// <summary>
		/// If Flickr stores the password for this then this will be 0, meaning you do not need to pass in the
		/// password when posting.
		/// </summary>
		[XmlAttribute("needspassword")]
		public int NeedsPassword;
	}
}