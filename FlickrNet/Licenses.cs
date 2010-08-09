using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;

namespace FlickrNet
{

    /// <summary>
    /// Details of a particular license available from Flickr.
    /// </summary>
    public sealed class License : IFlickrParsable
    {
        /// <summary>
        ///     The ID of the license. Used by <see cref="Flickr.PhotosGetInfo(string)"/> and 
        ///     <see cref="Flickr.PhotosGetInfo(string, string)"/>.
        /// </summary>
        public LicenseType LicenseId { get; private set; }

        /// <summary>The name of the license.</summary>
        public string LicenseName { get; private set; }

        /// <summary>The URL for the license text.</summary>
        public string LicenseUrl { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        LicenseId = (LicenseType)reader.ReadContentAsInt();
                        break;
                    case "name":
                        LicenseName = reader.Value;
                        break;
                    case "url":
                        if (!String.IsNullOrEmpty(reader.Value))
                        {
                            LicenseUrl = reader.Value;
                        }
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }
    }
}
