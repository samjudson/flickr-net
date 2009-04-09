using System;
using System.Text;
using System.Xml.Serialization;

namespace FlickrNet
{
    public class Member : IXmlSerializable
    {

        private string _memberId;

        public string MemberId
        {
            get { return _memberId; }
            set { _memberId = value; }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _iconServer;

        public string IconServer
        {
            get { return _iconServer; }
            set { _iconServer = value; }
        }

        private string _iconFarm;

        public string IconFarm
        {
            get { return _iconFarm; }
            set { _iconFarm = value; }
        }

        private MemberType _memberType;

        public MemberType MemberType
        {
            get { return _memberType; }
            set { _memberType = value; }
        }

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
            throw new NotImplementedException();
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
