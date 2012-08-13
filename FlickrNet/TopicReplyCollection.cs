using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The list of replies for a particular topic. 
    /// </summary>
    /// <remarks>
    /// Includes most of the properties of the topic as well, such as <see cref="TopicReplyCollection.TopicId"/>
    /// and <see cref="TopicReplyCollection.Subject"/>
    /// </remarks>
    public class TopicReplyCollection : System.Collections.ObjectModel.Collection<TopicReply>, IFlickrParsable
    {
        /// <summary>
        /// The id of the topic for which these replies are too.
        /// </summary>
        public string TopicId { get; set; }
        /// <summary>
        /// The subject of the topic.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The group id for the topic.
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// The name of the group for this topic.
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// The server for the group icon.
        /// </summary>
        public string GroupIconServer { get; set; }
        /// <summary>
        /// The farm for the group icon.
        /// </summary>
        public string GroupIconFarm { get; set; }
        /// <summary>
        /// The topic authos user id.
        /// </summary>
        public string AuthorUserId { get; set; }
        /// <summary>
        /// The topic author name.
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// True if the topic authos has a pro account.
        /// </summary>
        public bool AuthorIsPro { get; set; }
        /// <summary>
        /// The role within the group of the topic author.
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
        /// Can the current user edit this topic.
        /// </summary>
        public bool CanEdit { get; set; }
        /// <summary>
        /// Can the current user delete this topic.
        /// </summary>
        public bool CanDelete { get; set; }
        /// <summary>
        /// Can the current user reply to this topic.
        /// </summary>
        public bool CanReply { get; set; }
        /// <summary>
        /// Is this topic sticky.
        /// </summary>
        public bool IsSticky { get; set; }
        /// <summary>
        /// Is this topic locked.
        /// </summary>
        public bool IsLocked { get; set; }
        /// <summary>
        /// Date the topic was created.
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Date of the last post to this topic.
        /// </summary>
        public DateTime DateLastPost { get; set; }
        /// <summary>
        /// The message body for this topic.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The total number of replies to this topic.
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// The number of replies per page.
        /// </summary>
        public int PerPage { get; set; }
        /// <summary>
        /// The current page of replies included.
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// The total number of pages of replies available.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// The buddy icon of the topic author.
        /// </summary>
        public string AuthorBuddyIcon
        {
            get
            {
                return UtilityMethods.BuddyIcon(AuthorIconServer, AuthorIconFarm, AuthorUserId);
            }
        }

        /// <summary>
        /// The icon of the group for this topic.
        /// </summary>
        public string GroupIconUrl
        {
            get
            {
                return UtilityMethods.BuddyIcon(GroupIconServer, GroupIconFarm, GroupId);
            }
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            if (reader.LocalName != "replies") { UtilityMethods.CheckParsingException(reader); return; }

            reader.Read();
            if (reader.LocalName != "topic") { UtilityMethods.CheckParsingException(reader); return; }

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "topic_id":
                        TopicId = reader.Value;
                        break;
                    case "subject":
                        Subject = reader.Value;
                        break;
                    case "group_id":
                        GroupId = reader.Value;
                        break;
                    case "name":
                        GroupName = reader.Value;
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
                        GroupIconServer = reader.Value;
                        break;
                    case "iconfarm":
                        GroupIconFarm = reader.Value;
                        break;
                    case "author_iconfarm":
                        AuthorIconFarm = reader.Value;
                        break;
                    case "author_iconserver":
                        AuthorIconServer = reader.Value;
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
                    case "total":
                        Total = reader.ReadContentAsInt();
                        break;
                    case "pages":
                        Pages = reader.ReadContentAsInt();
                        break;
                    case "page":
                        Page = reader.ReadContentAsInt();
                        break;
                    case "per_page":
                        PerPage = reader.ReadContentAsInt();
                        break;

                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            if (reader.LocalName == "message")
            {
                Message = reader.ReadElementContentAsString();
                reader.Read();
            }

            while (reader.LocalName == "reply" && reader.NodeType == System.Xml.XmlNodeType.Element)
            {
                TopicReply reply = new TopicReply();
                ((IFlickrParsable)reply).Load(reader);
                Add(reply);
            }

        }
    }
}
