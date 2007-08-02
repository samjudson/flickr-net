using System;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// A simple tag class, containing a tag name and optional count (for <see cref="Flickr.TagsGetListUserPopular()"/>)
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

	/// <summary>
	/// Raw tags, as returned by the <see cref="Flickr.TagsGetListUserRaw(string)"/> method.
	/// </summary>
	public class RawTag
	{
		private string _cleanTag;
		private string[] _rawTags = new string[0];

		/// <summary>
		/// An array of strings containing the raw tags returned by the method.
		/// </summary>
		public string[] RawTags
		{
			get { return _rawTags; }
		}

		/// <summary>
		/// The clean tag.
		/// </summary>
		public string CleanTag
		{
			get { return _cleanTag; }
		}
		
		internal RawTag(XmlNode node)
		{
			if( node.Attributes.GetNamedItem("clean") == null ) throw new ResponseXmlException("clean attribute not found");
			_cleanTag = node.Attributes.GetNamedItem("clean").Value;

			XmlNodeList list = node.SelectNodes("raw");
			_rawTags = new string[list.Count];

			for(int i = 0; i < list.Count; i++)
			{
				XmlNode rawNode = list[i];
				_rawTags[i] = rawNode.InnerText;
			}
		}
	}
}
