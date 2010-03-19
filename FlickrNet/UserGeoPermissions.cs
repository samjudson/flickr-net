using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The default privacy level for geographic information attached to the user's photos and whether or not the user has chosen to use geo-related EXIF information to automatically geotag their photos.
    /// </summary>
    public sealed class UserGeoPermissions : IFlickrParsable
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// The default privacy level for geographic information attached to the user's photos.
        /// </summary>
        public GeoPermissionType GeoPermissions { get; private set; }

        /// <summary>
        /// Whether or not the user has chosen to use geo-related EXIF information to automatically geotag their photos.
        /// </summary>
        public bool ImportGeoExif { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "nsid":
                        UserId = reader.Value;
                        break;
                    case "geoperms":
                        GeoPermissions = (GeoPermissionType)reader.ReadContentAsInt();
                        break;
                    case "importgeoexif":
                        ImportGeoExif = reader.Value == "1";
                        break;
                }
            }
        }
    }
}
