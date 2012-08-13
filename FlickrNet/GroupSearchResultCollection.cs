using System;
using System.Xml;
using System.Collections.Generic;

namespace FlickrNet
{
    /// <summary>
    /// Returned by <see cref="Flickr.GroupsSearch(string)"/> methods.
    /// </summary>
    public sealed class GroupSearchResultCollection : System.Collections.ObjectModel.Collection<GroupSearchResult>, IFlickrParsable
    {
        /// <summary>
        /// The current page that the group search results represents.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The total number of pages this search would return.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// The number of groups returned per photo.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// The total number of groups that where returned for the search.
        /// </summary>
        public int Total { get; set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "groups")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "page":
                        Page = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "perpage":
                    case "per_page":
                        PerPage = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "total":
                        Total = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;

                }
            }

            reader.Read();

            while (reader.LocalName == "group")
            {
                GroupSearchResult r = new GroupSearchResult();
                ((IFlickrParsable)r).Load(reader);
                Add(r);
            }

            // Skip to next element (if any)
            reader.Skip();

        }
    }

    /// <summary>
    /// A class which encapsulates a single group search result.
    /// </summary>
    public sealed class GroupSearchResult : IFlickrParsable
    {
        /// <summary>
        /// The group id for the result.
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// The group name for the result.
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// True if the group is an over eighteen (adult) group only.
        /// </summary>
        public bool EighteenPlus { get; set; }

        /// <summary>
        /// Group icon server.
        /// </summary>
        public string IconServer { get; set; }

        /// <summary>
        /// Group icon farm.
        /// </summary>
        public string IconFarm { get; set; }

        /// <summary>
        /// Number of members in the group.
        /// </summary>
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
        /// The url for the group's icon. 
        /// </summary>
        public string GroupIconUrl
        {
            get
            {
                return UtilityMethods.BuddyIcon(IconServer, IconFarm, GroupId);
            }
        }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "group")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "nsid":
                        GroupId = reader.Value;
                        break;
                    case "name":
                        GroupName = reader.Value;
                        break;
                    case "eighteenplus":
                        EighteenPlus = reader.Value == "1";
                        break;
                    case "iconserver":
                        IconServer = reader.Value;
                        break;
                    case "iconfarm":
                        IconFarm = reader.Value;
                        break;
                    case "members":
                        Members = reader.ReadContentAsInt();
                        break;
                    case "pool_count":
                        PoolCount = reader.ReadContentAsInt();
                        break;
                    case "topic_count":
                        TopicCount = reader.ReadContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;

                }
            }

            reader.Skip();
        }
    }
}
