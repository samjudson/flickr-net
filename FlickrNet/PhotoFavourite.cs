using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace FlickrNet
{
    public class PhotoFavourite: IXmlSerializable
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime FavoriteDate { get; set; }

        public PhotoFavourite()
        {
        }

        public PhotoFavourite(XmlReader reader)
        {
            LoadXml(reader);
        }

        private void LoadXml(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "nsid":
                        UserId = reader.Value;
                        break;
                    case "username":
                        UserName = reader.Value;
                        break;
                    case "favedate":
                        FavoriteDate = Utils.UnixTimestampToDate(reader.Value);
                        break;
                    default:
                        break;
                }
            }

            reader.Skip();
        }


        #region IXmlSerializable Members

        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            LoadXml(reader);
        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            
        }

        #endregion
    }
}
