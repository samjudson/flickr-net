using System.Xml.Serialization;
using System.Xml.Schema;
using System.IO;
using System;
using System.Collections.Generic;

namespace FlickrNet
{
    /// <summary>
    /// Collection containing a users photosets.
    /// </summary>
    public sealed class PhotosetCollection : System.Collections.ObjectModel.Collection<Photoset>, IFlickrParsable
    {
        /// <summary>
        /// Can the user create more photosets.
        /// </summary>
        /// <remarks>
        /// 1 meants yes, 0 means no.
        /// </remarks>
        public bool CanCreate { get; set; }

        /// <summary>
        /// The current page of the results.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The total number of pages of results available.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// The maximum number of photosets returned per page.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// The total number of photosets available.
        /// </summary>
        public int Total { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photosets")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "cancreate":
                        CanCreate = reader.Value == "1";
                        break;
                    case "page":
                        Page = reader.ReadContentAsInt();
                        break;
                    case "perpage":
                        PerPage = reader.ReadContentAsInt();
                        break;
                    case "pages":
                        Pages = reader.ReadContentAsInt();
                        break;
                    case "total":
                        Total = reader.ReadContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }
            reader.Read();

            while (reader.LocalName == "photoset")
            {
                Photoset photoset = new Photoset();
                ((IFlickrParsable)photoset).Load(reader);
                Add(photoset);
            }

            reader.Skip();
        }
    }

}