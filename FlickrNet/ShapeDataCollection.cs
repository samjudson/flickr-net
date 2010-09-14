using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="ShapeData"/> instances.
    /// </summary>
    public sealed class ShapeDataCollection : Collection<ShapeData>, IFlickrParsable
    {
        /// <summary>
        /// The WOE (Where On Earth) ID for these shapes.
        /// </summary>
        public string WoeId { get; set; }

        /// <summary>
        /// The Flickr place ID for these shapes.
        /// </summary>
        public string PlaceId { get; set; }

        /// <summary>
        /// The type for this place.
        /// </summary>
        public PlaceType PlaceType { get; set; }

        /// <summary>
        /// The total number of shapes that match the calling query.
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// The page of the result set that has been returned.
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// The number of shapes returned per page.
        /// </summary>
        public int PerPage { get; set; }
        /// <summary>
        /// The number of pages of shapes that are available.
        /// </summary>
        public int Pages { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "shapes")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "page":
                        Page = reader.ReadContentAsInt();
                        break;
                    case "total":
                        Total = reader.ReadContentAsInt();
                        break;
                    case "pages":
                        Pages = reader.ReadContentAsInt();
                        break;
                    case "per_page":
                    case "perpage":
                        PerPage = reader.ReadContentAsInt();
                        break;
                    case "woe_id":
                        WoeId = reader.Value;
                        break;
                    case "place_id":
                        PlaceId = reader.Value;
                        break;
                    case "place_type_id":
                        PlaceType = (PlaceType)reader.ReadContentAsInt();
                        break;
                    case "place_type":
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "shape")
            {
                ShapeData item = new ShapeData();
                ((IFlickrParsable)item).Load(reader);
                Add(item);
            }

            reader.Skip();
        }
    }
}
