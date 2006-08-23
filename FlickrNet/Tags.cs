using System;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// A simple tag class, containing a tag name and optional count (for <see cref="Flickr.TagsGetListUserPopular"/>)
	/// </summary>
	public class Tag
	{
		private string _tagName;
		private int _count;

		/// <summary>
		/// The name of the tag.
		/// </summary>
		public string TagName
		{
			get { return _tagName; }
		}

		/// <summary>
		/// The poularity of the tag. Will be 0 where the popularity is not retreaved.
		/// </summary>
		public int Count
		{
			get { return _count; }
		}

		internal Tag(XmlNode node)
		{
			if( node.Attributes["count"] != null ) _count = Convert.ToInt32(node.Attributes["count"].Value);
			_tagName = node.InnerText;
		}

		internal Tag(string tagName, int count)
		{
			_tagName = tagName;
			_count = count;
		}
	}
}
