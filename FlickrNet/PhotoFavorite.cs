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
    public class PhotoFavorite: IFlickrParsable
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
        public PhotoFavorite()
        {
        }

        void IFlickrParsable.Load(XmlReader reader)
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
                        FavoriteDate = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    default:
                        break;
                }
            }

            reader.Skip();
        }
    }
}
