using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Details of the favourites for a photo.
    /// </summary>
    public class PhotoFavourite: IXmlSerializable
    {
        /// <summary>
        /// The Flickr User ID of the user who favourited the photo.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The user name of the user who favourited the photo.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The date the hoto was favourited.
        /// </summary>
        public DateTime FavoriteDate { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PhotoFavourite()
        {
        }

        /// <summary>
        /// The serializing constructor, used by the internal classes to construct a new class.
        /// </summary>
        /// <param name="reader"></param>
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
