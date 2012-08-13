using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Details of a topic
    /// </summary>
    public class Topic : IFlickrParsable
    {
        /// <summary>
        /// The ID for the topic
        /// </summary>
        public string TopicId { get; set; }
        /// <summary>
        /// The subject line of the topic.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The user id of the author of the topic.
        /// </summary>
        public string AuthorUserId { get; set; }
        /// <summary>
        /// The user name of the author of the topic.
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// True if the author of the topic has a pro account.
        /// </summary>
        public bool AuthorIsPro { get; set; }
        /// <summary>
        /// The role within the group of the author of the topic.
        /// </summary>
        public MemberTypes AuthorRole { get; set; }
        /// <summary>
        /// The server for the author's buddy icon.
        /// </summary>
        public string AuthorIconServer { get; set; }
        /// <summary>
        /// The farm for the author's buddy icon.
        /// </summary>
        public string AuthorIconFarm { get; set; }
        /// <summary>
        /// The number of replies for this topic.
        /// </summary>
        public int RepliesCount { get; set; }
        /// <summary>
        /// Can the calling user edit this topic.
        /// </summary>
        public bool CanEdit { get; set; }
        /// <summary>
        /// Can the calling user delete this topic.
        /// </summary>
        public bool CanDelete { get; set; }
        /// <summary>
        /// Can the calling user reply to this topic. Flickr currently returns False in all instances, so do not rely on this issue.
        /// </summary>
        public bool CanReply { get; set; }
        /// <summary>
        /// Is the topic marked as sticky.
        /// </summary>
        public bool IsSticky { get; set; }
        /// <summary>
        /// Is the topic marked as locked.
        /// </summary>
        public bool IsLocked { get; set; }
        /// <summary>
        /// Date the topic was created.
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Date the last reply was made to this property.
        /// </summary>
        public DateTime DateLastPost { get; set; }
        /// <summary>
        /// The id of the last reply to this topic.
        /// </summary>
        public string LastReplyId { get; set; }
        /// <summary>
        /// The message content of this topic.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The id of the group this topic belongs to.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// The url for the buddy icon for the author of this topic.
        /// </summary>
        public string AuthorBuddyIcon
        {
            get
            {
                return UtilityMethods.BuddyIcon(AuthorIconServer, AuthorIconFarm, AuthorUserId);
            }
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            if (reader.LocalName != "topic") { UtilityMethods.CheckParsingException(reader); return; }

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        TopicId = reader.Value;
                        break;
                    case "subject":
                        Subject = reader.Value;
                        break;
                    case "author":
                        AuthorUserId = reader.Value;
                        break;
                    case "authorname":
                        AuthorName = reader.Value;
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
                    case "count_replies":
                        RepliesCount = reader.ReadContentAsInt();
                        break;
                    case "can_edit":
                        CanEdit = reader.Value == "1";
                        break;
                    case "can_delete":
                        CanDelete = reader.Value == "1";
                        break;
                    case "can_reply":
                        CanReply = reader.Value == "1";
                        break;
                    case "is_sticky":
                        IsSticky = reader.Value == "1";
                        break;
                    case "is_locked":
                        IsLocked = reader.Value == "1";
                        break;
                    case "datecreate":
                        DateCreated = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "datelastpost":
                        DateLastPost = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "last_reply":
                        LastReplyId = reader.Value;
                        break;
                    case "group_id":
                        GroupId = reader.Value;
                        break;
                    case "name":
                        // Group Name
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
    }
}
