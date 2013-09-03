using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A list of <see cref="PhotoComment"/> items.
    /// </summary>
    public sealed class PhotosetCommentCollection : Collection<PhotoComment>, IFlickrParsable
    {
        /// <summary>
        /// The ID of the photoset for this comment.
        /// </summary>
        public string PhotosetId { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "comments")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "photoset_id":
                        PhotosetId = reader.Value;
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