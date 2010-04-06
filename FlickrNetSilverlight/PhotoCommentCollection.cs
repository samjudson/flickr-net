using System;
using System.Xml;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <summary>
	/// A list of <see cref="PhotoComment"/> items.
	/// </summary>
    public sealed class PhotoCommentCollection : System.Collections.ObjectModel.Collection<PhotoComment>, IFlickrParsable
	{
        /// <summary>
        /// The ID of photo for these comments.
        /// </summary>
        public string PhotoId { get; private set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "comments")
                throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "photo_id":
                        PhotoId = reader.Value;
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
