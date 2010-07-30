using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Information about the types of 'places' available from the Flickr API.
    /// </summary>
    /// <remarks>
    /// Use the <see cref="PlaceInfo"/> enumeration were possible.
    /// </remarks>
    public sealed class PlaceTypeInfo : IFlickrParsable
    {
        /// <summary>
        /// The unique ID for the blog service supported by Flickr.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the blog service supported by Flickr.
        /// </summary>
        public string Name { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "place_type")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "place_type_id":
                    case "id":
                        Id = reader.ReadContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            Name = reader.ReadContentAsString();

            reader.Skip();
        }
    }
}
