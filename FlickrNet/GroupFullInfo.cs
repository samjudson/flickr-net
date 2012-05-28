using System;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Provides details of a particular group.
    /// </summary>
    /// <remarks>
    /// Used by the Url methods and <see cref="Flickr.GroupsGetInfo"/> method.
    /// The reason for a <see cref="Group"/> and <see cref="GroupFullInfo"/> are due to xml serialization
    /// incompatabilities.
    /// </remarks>
    public sealed class GroupFullInfo : IFlickrParsable
    {
        /// <remarks/>
        public string GroupId { get; set; }

        /// <remarks/>
        public string GroupName { get; set; }

        /// <remarks/>
        public string Description { get; set; }

        /// <remarks/>
        public int Members { get; set; }

        /// <summary>
        /// The number of photos in this groups pool.
        /// </summary>
        public int PoolCount { get; set; }

        /// <summary>
        /// The number of topics in this groups discussion list.
        /// </summary>
        public int TopicCount { get; set; }

        /// <summary>
        /// The server number used for the groups icon.
        /// </summary>
        public string IconServer { get; set; }

        /// <summary>
        /// The server farm for the group icon. If zero then the group uses the default icon.
        /// </summary>
        public string IconFarm { get; set; }

        /// <summary>
        /// The language that the group information has been returned in.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Is this group's pool moderated.
        /// </summary>
        public bool IsPoolModerated { get; set; }

        /// <summary>
        /// The HTML for the group's 'Blast' (the banner seen on the group home page).
        /// </summary>
        public string BlastHtml { get; set; }

        /// <summary>
        /// The User ID for the user who last set the group's 'Blast' (the banner seen on the group home page).
        /// </summary>
        public string BlastUserId { get; set; }

        /// <summary>
        /// The date the group's 'Blast' (the banner seen on the group home page) was last updated.
        /// </summary>
        public DateTime? BlastDateAdded { get; set; }

        /// <summary>
        /// The role name assigned to members of this group.
        /// </summary>
        public string MemberRoleName { get; set; }

        /// <summary>
        /// The role name assigned to moderators of this group.
        /// </summary>
        public string ModeratorRoleName { get; set; }

        /// <summary>
        /// The role name assigned to admins of this group.
        /// </summary>
        public string AdminRoleName { get; set; }

        /// <summary>
        /// The url for the group's icon. 
        /// </summary>
        public string GroupIconUrl
        {
            get
            {
                return UtilityMethods.BuddyIcon(IconServer, IconFarm, GroupId);
            }
        }

        /// <remarks/>
        public PoolPrivacy Privacy { get; set; }

        /// <remarks/>
        public GroupThrottleInfo ThrottleInfo { get; set; }

        /// <summary>
        /// The restrictions that apply to new items added to this group's pool.
        /// </summary>
        public GroupInfoRestrictions Restrictions { get; set; }

        /// <summary>
        /// Any rules that the group has for new members.
        /// </summary>
        public string Rules { get; set; }

        /// <summary>
        /// Methods for automatically converting a <see cref="GroupFullInfo"/> object into
        /// and instance of a <see cref="Group"/> object.
        /// </summary>
        /// <param name="groupInfo">The incoming object.</param>
        /// <returns>The <see cref="Group"/> instance.</returns>
        public static implicit operator Group(GroupFullInfo groupInfo)
        {
            Group g = new Group();
            g.GroupId = groupInfo.GroupId;
            g.GroupName = groupInfo.GroupName;
            g.Members = groupInfo.Members;

            return g;
        }

        /// <summary>
        /// Converts the current <see cref="GroupFullInfo"/> into an instance of the
        /// <see cref="Group"/> class.
        /// </summary>
        /// <returns>A <see cref="Group"/> instance.</returns>
        public Group ToGroup()
        {
            return (Group)this;
        }

        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "nsid":
                    case "id":
                        GroupId = reader.Value;
                        break;
                    case "iconserver":
                        IconServer = reader.Value;
                        break;
                    case "iconfarm":
                        IconFarm = reader.Value;
                        break;
                    case "lang":
                        Language = reader.Value;
                        break;
                    case "ispoolmoderated":
                        IsPoolModerated = reader.Value == "1";
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName != "group")
            {
                switch (reader.LocalName)
                {
                    case "name":
                        GroupName = reader.ReadElementContentAsString();
                        break;
                    case "description":
                        Description = reader.ReadElementContentAsString();
                        break;
                    case "members":
                        Members = reader.ReadElementContentAsInt();
                        break;
                    case "privacy":
                        Privacy = (PoolPrivacy)reader.ReadElementContentAsInt();
                        break;
                    case "blast":
                        BlastDateAdded = UtilityMethods.UnixTimestampToDate(reader.GetAttribute("date_blast_added"));
                        BlastUserId = reader.GetAttribute("user_id");
                        BlastHtml = reader.ReadElementContentAsString();
                        break;
                    case "throttle":
                        ThrottleInfo = new GroupThrottleInfo();
                        ((IFlickrParsable)ThrottleInfo).Load(reader);
                        break;
                    case "restrictions":
                        Restrictions = new GroupInfoRestrictions();
                        ((IFlickrParsable)Restrictions).Load(reader);
                        break;
                    case "roles":
                        MemberRoleName = reader.GetAttribute("member");
                        ModeratorRoleName = reader.GetAttribute("moderator");
                        AdminRoleName = reader.GetAttribute("admin");
                        reader.Read();
                        break;
                    case "rules":
                        Rules = reader.ReadElementContentAsString();
                        break;
                    case "pool_count":
                        PoolCount = reader.ReadElementContentAsInt();
                        break;
                    case "topic_count":
                        TopicCount = reader.ReadElementContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        reader.Skip();
                        break;

                }
            }

            reader.Read();
        }
    }

}
