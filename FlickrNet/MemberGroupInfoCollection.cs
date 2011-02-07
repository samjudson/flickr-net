using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A list of <see cref="MemberGroupInfo"/> items.
    /// </summary>
    public sealed class MemberGroupInfoCollection : System.Collections.ObjectModel.Collection<MemberGroupInfo>, IFlickrParsable
    {
        /// <summary>
        /// The total number of pages of results that are available.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// The total number of groups that are available.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// The page of the results that has been returned.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The number of results that are returned per page.
        /// </summary>
        /// <remarks>
        /// This may be more than the total number of groups returned if this is the last page.
        /// </remarks>
        public int PerPage { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "groups")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "per_page":
                        PerPage = reader.ReadContentAsInt();
                        break;
                    case "page":
                        Page = reader.ReadContentAsInt();
                        break;
                    case "pages":
                        Pages = reader.ReadContentAsInt();
                        break;
                    case "total":
                        Total = reader.ReadContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "group")
            {
                MemberGroupInfo member = new MemberGroupInfo();
                ((IFlickrParsable)member).Load(reader);
                Add(member);
            }

            reader.Skip();
        }
    }
}
