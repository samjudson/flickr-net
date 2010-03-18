using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FlickrNet
{

    /// <summary>
    /// Details of the blog services supported by Flickr. e.g. Twitter, Blogger etc.
    /// </summary>
    public sealed class BlogService : IFlickrParsable
    {
        /// <summary>
        /// The unique ID for the blog service supported by Flickr.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The name of the blog service supported by Flickr.
        /// </summary>
        public string Name { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "service")
                throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        Id = reader.Value;
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();

            Name = reader.ReadContentAsString();

            reader.Skip();
        }
    }
}
