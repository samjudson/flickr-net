using System;
using System.Xml;
using System.Collections.Generic;

namespace FlickrNet
{
    /// <summary>
    /// List containing <see cref="Tag"/> items.
    /// </summary>
    public class TagCollection : List<Tag>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            reader.ReadToDescendant("tag");

            while (reader.LocalName == "tag")
            {
                Tag member = new Tag();
                ((IFlickrParsable)member).Load(reader);
                Add(member);
            }

            reader.Skip();
        }
    }

    /// <summary>
	/// A simple tag class, containing a tag name and optional count (for <see cref="Flickr.TagsGetListUserPopular()"/>)
	/// </summary>
	public class Tag : IFlickrParsable
	{
		/// <summary>
		/// The name of the tag.
		/// </summary>
		public string TagName { get; private set; }

		/// <summary>
        /// The poularity of the tag. Will be 0 if not returned via <see cref="Flickr.TagsGetListUserPopular()"/>
		/// </summary>
		public int Count { get; private set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "count":
                        Count = reader.ReadContentAsInt();
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();

            TagName = reader.ReadContentAsString();

            reader.Read();

        }
    }

    /// <summary>
    /// List containing <see cref="RawTag"/> items.
    /// </summary>
    public class RawTagCollection : List<RawTag>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            reader.ReadToDescendant("tag");

            while (reader.LocalName == "tag")
            {
                RawTag member = new RawTag();
                ((IFlickrParsable)member).Load(reader);
                Add(member);
            }

            reader.Skip();
        }
    }

	/// <summary>
	/// Raw tags, as returned by the <see cref="Flickr.TagsGetListUserRaw(string)"/> method.
	/// </summary>
	public class RawTag : IFlickrParsable
	{
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RawTag()
        {
            RawTags = new List<string>();
        }

		/// <summary>
		/// An array of strings containing the raw tags returned by the method.
		/// </summary>
		public List<string> RawTags { get; private set; }

		/// <summary>
		/// The clean tag.
		/// </summary>
		public string CleanTag { get; private set; }
		
        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "clean":
                        CleanTag = reader.ReadContentAsString();
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();

            while (reader.LocalName == "raw")
            {
                RawTags.Add(reader.ReadElementContentAsString());
            }

            reader.Read();
        }
    }
}
