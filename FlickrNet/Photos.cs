using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace FlickrNet
{
	/// <remarks/>
	[Serializable]
	public class Photos : List<Photo>, IXmlSerializable, IFlickrParsable
	{
        /// <remarks/>
        public int Page { get; set; }

        /// <remarks/>
        public int Pages { get; set; }

        /// <remarks/>
        public int PerPage { get; set; }

		/// <remarks/>
        public int Total { get; set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "photos")
                throw new FlickrException("Unknown element found: " + reader.LocalName);


            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "total":
                        Total = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "perpage":
                        PerPage = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "page":
                        Page = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    default:
                        throw new Exception("Unknown element: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Read();

            while (reader.LocalName == "photo")
            {
                Photo p = new Photo();
                ((IFlickrParsable)p).Load(reader);
                if (!String.IsNullOrEmpty(p.PhotoId)) Add(p);
            }

            // Skip to next element (if any)
            reader.Skip();

        }

        #region IXmlSerializable Members

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            ((IFlickrParsable)this).Load(reader);
        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            
        }

        #endregion
    }
}