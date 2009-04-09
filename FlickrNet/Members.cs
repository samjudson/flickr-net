using System;
using System.Xml.Serialization;
using System.Collections;

namespace FlickrNet
{
    public class Members : IXmlSerializable
    {
        private int _page;

        public int Page
        {
            get { return _page; }
            set { _page = value; }
        }

        private int _pages;

        public int Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        private int _numMembers;

        public int TotalMembers
        {
            get { return _numMembers; }
            set { _numMembers = value; }
        }

        private int _membersPerPage;

        public int MembersPerPage
        {
            get { return _membersPerPage; }
            set { _membersPerPage = value; }
        }

        private Member[] _members;


        public Member[] MembersCollection
        {
            get { return _members; }
        }

        #region IXmlSerializable Members

        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            //<members page="1" pages="1" perpage="100" total="33">

            if (reader.GetAttribute("page") != null) _page = int.Parse(reader.GetAttribute("page"));
            if (reader.GetAttribute("pages") != null) _pages = int.Parse(reader.GetAttribute("pages"));
            if (reader.GetAttribute("perpage") != null) _membersPerPage = int.Parse(reader.GetAttribute("perpage"));
            if (reader.GetAttribute("total") != null) _numMembers = int.Parse(reader.GetAttribute("total"));

            ArrayList list = new ArrayList();

            while (reader.Read())
            {
                if (reader.Name == "member")
                {
                    IXmlSerializable m = (IXmlSerializable)new Member();
                    m.ReadXml(reader);
                    list.Add(m);
                }
            }

            _members = new Member[list.Count];
            list.CopyTo(_members);

        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            
        }

        #endregion
    }
}
