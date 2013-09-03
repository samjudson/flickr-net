using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A list of <see cref="PhotoComment"/> items.
    /// </summary>
    public sealed class PhotoCommentCollection : Collection<PhotoComment>, IFlickrParsable
    {
        /// <summary>
        /// The ID of photo for these comments.
        /// </summary>
        public string PhotoId { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "comments")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "photo_id":
                        PhotoId = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "comment")
            {
                var comment = new PhotoComment();
                ((IFlickrParsable) comment).Load(reader);
                Add(comment);
            }
            reader.Skip();
        }

        #endregion
    }
}