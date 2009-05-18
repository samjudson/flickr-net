using System;
using System.Text;
using System.Xml.Serialization;

namespace FlickrNet
{
	/// <summary>
	/// Details for a Flickr member, as returned by the <see cref="Flickr.GroupsMemberGetList"/> method.
	/// </summary>
    public class Member : IXmlSerializable
    {

        private string _memberId;

		/// <summary>
		/// The user id for the member.
		/// </summary>
        public string MemberId
        {
            get { return _memberId; }
            set { _memberId = value; }
        }

        private string _username;

		/// <summary>
		/// The members name.
		/// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _iconServer;

		/// <summary>
		/// The icon server for the users buddy icon. See <see cref="IconUrl"/> for the complete URL.
		/// </summary>
        public string IconServer
        {
            get { return _iconServer; }
            set { _iconServer = value; }
        }

        private string _iconFarm;

		/// <summary>
		/// The icon farm for the users buddy icon. See <see cref="IconUrl"/> for the complete URL.
		/// </summary>
		public string IconFarm
        {
            get { return _iconFarm; }
            set { _iconFarm = value; }
        }

        private MemberType _memberType;

		/// <summary>
		/// The type of the member (basic, moderator or administrator).
		/// </summary>
		public MemberType MemberType
        {
            get { return _memberType; }
            set { _memberType = value; }
        }

		/// <summary>
		/// The icon URL for the users buddy icon. Calculated from the <see cref="IconFarm"/> and <see cref="IconServer"/>.
		/// </summary>
		public string IconUrl
        {
            get {
				if (IconServer != null && IconServer.Length > 0 && IconServer != "0")
				{
					return String.Format("http://farm{0}.static.flickr.com/{1}/buddyicons/{2}.jpg", IconFarm, IconServer, MemberId);
				}
				else
				{
					return "http://www.flickr.com/images/buddyicon.jpg";
				}
            }
        }


        #region IXmlSerializable Members

        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            //<member nsid="123456@N01" username="foo" iconserver="1" iconfarm="1" membertype="2"/>
            MemberId = reader.GetAttribute("nsid");
            Username = reader.GetAttribute("username");
            IconServer = reader.GetAttribute("iconserver");
            IconFarm = reader.GetAttribute("iconfarm");
            MemberType = Utils.ParseIdToMemberType(reader.GetAttribute("membertype"));
        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            
        }

        #endregion

    }
}
