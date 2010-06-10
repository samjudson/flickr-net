using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Contains the details of a comment made on a photo.
    /// returned by the <see cref="Flickr.PhotosCommentsGetList"/> method.
    /// </summary>
    public sealed class PhotoComment : IFlickrParsable
    {
        /// <summary>
        /// The comment id of this comment.
        /// </summary>
        public string CommentId { get; private set; }

        /// <summary>
        /// The user id of the author of the comment.
        /// </summary>
        public string AuthorUserId { get; private set; }

        /// <summary>
        /// The username (screen name) of the author of the comment.
        /// </summary>
        public string AuthorUserName { get; private set; }

        /// <summary>
        /// The permalink to the comment on the photos web page.
        /// </summary>
        public string Permalink { get; private set; }

        /// <summary>
        /// The date and time that the comment was created.
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// The comment text (can contain HTML).
        /// </summary>
        public string CommentHtml { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "comment")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        CommentId = reader.Value;
                        break;
                    case "author":
                        AuthorUserId = reader.Value;
                        break;
                    case "authorname":
                        AuthorUserName = reader.Value;
                        break;
                    case "permalink":
                        Permalink = reader.Value;
                        break;
                    case "datecreate":
                        DateCreated = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            CommentHtml = reader.ReadContentAsString();

            reader.Skip();
        }
    }
}
