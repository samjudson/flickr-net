using System;
using System.Xml;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// List containing <see cref="Tag"/> items.
    /// </summary>
    public sealed class TagCollection : System.Collections.ObjectModel.Collection<Tag>, IFlickrParsable
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
    public sealed class Tag : IFlickrParsable
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
    public sealed class RawTagCollection : System.Collections.ObjectModel.Collection<RawTag>, IFlickrParsable
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
    public sealed class RawTag : IFlickrParsable
	{
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RawTag()
        {
            RawTags = new Collection<string>();
        }

		/// <summary>
		/// An array of strings containing the raw tags returned by the method.
		/// </summary>
        public Collection<string> RawTags { get; private set; }

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
