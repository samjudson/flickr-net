using System;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A reply to a topic.
    /// </summary>
    public class TopicReply : IFlickrParsable
    {
        /// <summary>
        /// The id of the reply.
        /// </summary>
        public string ReplyId { get; set; }

        /// <summary>
        /// The id of the author of the reply.
        /// </summary>
        public string AuthorUserId { get; set; }

        /// <summary>
        /// The username of the author of the reply.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// The path alias for the author of this topic.
        /// </summary>
        public string AuthorPathAlias { get; set; }

        /// <summary>
        /// Is the author of this topic from a deleted account.
        /// </summary>
        public bool AuthorIsDeleted { get; set; }

        /// <summary>
        /// True if the author of the reply has a pro account.
        /// </summary>
        public bool AuthorIsPro { get; set; }

        /// <summary>
        /// The role within the group of the reply author.
        /// </summary>
        public MemberTypes AuthorRole { get; set; }

        /// <summary>
        /// The server for the reply author's buddy icon.
        /// </summary>
        public string AuthorIconServer { get; set; }

        /// <summary>
        /// The farm for the reply author's buddy icon.
        /// </summary>
        public string AuthorIconFarm { get; set; }

        /// <summary>
        /// Can the calling user edit the reply.
        /// </summary>
        public bool CanEdit { get; set; }

        /// <summary>
        /// Can the calling user delete the reply.
        /// </summary>
        public bool CanDelete { get; set; }

        /// <summary>
        /// The date the reply was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The date the reply was last edited.
        /// </summary>
        public DateTime DateLastEdited { get; set; }

        /// <summary>
        /// The message contents of the reply.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The buddy icon for the author of the reply.
        /// </summary>
        public string AuthorBuddyIcon
        {
            get { return UtilityMethods.BuddyIcon(AuthorIconServer, AuthorIconFarm, AuthorPathAlias ?? AuthorUserId); }
        }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            if (reader.LocalName != "reply")
            {
                UtilityMethods.CheckParsingException(reader);
                return;
            }

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        ReplyId = reader.Value;
                        break;
                    case "author":
                        AuthorUserId = reader.Value;
                        break;
                    case "authorname":
                        AuthorName = reader.Value;
                        break;
                    case "author_path_alias":
                        AuthorPathAlias = reader.Value == "" ? null : reader.Value;
                        break;
                    case "author_is_deleted":
                        AuthorIsDeleted = reader.Value == "1";
                        break;
                    case "is_pro":
                        AuthorIsPro = reader.Value == "1";
                        break;
                    case "role":
                        AuthorRole = UtilityMethods.ParseRoleToMemberType(reader.Value);
                        break;
                    case "iconserver":
                        AuthorIconServer = reader.Value;
                        break;
                    case "iconfarm":
                        AuthorIconFarm = reader.Value;
                        break;
                    case "can_edit":
                        CanEdit = reader.Value == "1";
                        break;
                    case "can_delete":
                        CanDelete = reader.Value == "1";
                        break;
                    case "datecreate":
                        DateCreated = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "lastedit":
                        DateLastEdited = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            if (reader.LocalName == "message")
                Message = reader.ReadElementContentAsString();

            reader.Skip();
        }

        #endregion
    }
}