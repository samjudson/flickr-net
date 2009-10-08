using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace FlickrNet
{
    public class PhotoFavourites : List<PhotoFavourite>, IXmlSerializable
    {
        public string PhotoId { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int Pages { get; set; }

        public PhotoFavourites()
        {
        }

        public PhotoFavourites(XmlReader reader)
        {
            Loader(reader);
        }

        private void Loader(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "id":
                        PhotoId = reader.Value;
                        break;
                    case "page":
                        Page = int.Parse(reader.Value);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value);
                        break;
                    case "perpage":
                        PerPage = int.Parse(reader.Value);
                        break;
                    case "total":
                        Total = int.Parse(reader.Value);
                        break;
                    default:
                        break;
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
            Loader(reader);
        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            
        }

        #endregion
    }
}
