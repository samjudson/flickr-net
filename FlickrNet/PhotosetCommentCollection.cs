using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A list of <see cref="PhotoComment"/> items.
    /// </summary>
    public class PhotosetCommentCollection : List<PhotoComment>, IFlickrParsable
    {
        /// <summary>
        /// The ID of the photoset for this comment.
        /// </summary>
        public string PhotosetId { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "comments")
                throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "photoset_id":
                        PhotosetId = reader.Value;
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();

            while (reader.LocalName == "comment")
            {
                PhotoComment comment = new PhotoComment();
                ((IFlickrParsable)comment).Load(reader);
                Add(comment);
            }
            reader.Skip();
        }
    }

}
