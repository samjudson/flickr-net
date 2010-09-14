using System;
using System.Text;
using System.Xml.Serialization;

namespace FlickrNet
{
    /// <summary>
    /// Details for a Flickr member, as returned by the <see cref="Flickr.GroupsMembersGetList(string)"/> method.
    /// </summary>
    public sealed class Member : IFlickrParsable
    {

        /// <summary>
        /// The user id for the member.
        /// </summary>
        public string MemberId { get; set; }

        /// <summary>
        /// The members name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The icon server for the users buddy icon. See <see cref="IconUrl"/> for the complete URL.
        /// </summary>
        public string IconServer { get; set; }

        /// <summary>
        /// The icon farm for the users buddy icon. See <see cref="IconUrl"/> for the complete URL.
        /// </summary>
        public string IconFarm { get; set; }

        /// <summary>
        /// The type of the member (basic, moderator or administrator).
        /// </summary>
        public MemberTypes MemberType { get; set; }

        /// <summary>
        /// The icon URL for the users buddy icon. Calculated from the <see cref="IconFarm"/> and <see cref="IconServer"/>.
        /// </summary>
        public string IconUrl
        {
            get {
                if (IconServer != null && IconServer.Length > 0 && IconServer != "0")
                {
                    return String.Format(System.Globalization.CultureInfo.InvariantCulture, "http://farm{0}.static.flickr.com/{1}/buddyicons/{2}.jpg", IconFarm, IconServer, MemberId);
                }
                else
                {
                    return "http://www.flickr.com/images/buddyicon.jpg";
                }
            }
        }


        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            // To parse: <member nsid="123456@N01" username="foo" iconserver="1" iconfarm="1" membertype="2"/>
            MemberId = reader.GetAttribute("nsid");
            UserName = reader.GetAttribute("username");
            IconServer = reader.GetAttribute("iconserver");
            IconFarm = reader.GetAttribute("iconfarm");
            MemberType = UtilityMethods.ParseIdToMemberType(reader.GetAttribute("membertype"));
        }
    }
}
