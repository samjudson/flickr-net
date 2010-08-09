using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;
using System;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Contains a list of <see cref="Contact"/> items for a given user.
    /// </summary>
    public sealed class ContactCollection : System.Collections.ObjectModel.Collection<Contact>, IFlickrParsable
    {
        /// <summary>
        /// The total number of contacts that match the calling query.
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// The page of the result set that has been returned.
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// The number of contacts returned per page.
        /// </summary>
        public int PerPage { get; set; }
        /// <summary>
        /// The number of pages of contacts that are available.
        /// </summary>
        public int Pages { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "contacts")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "page":
                        Page = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "total":
                        Total = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "per_page":
                    case "perpage":
                        PerPage = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "contact")
            {
                Contact contact = new Contact();
                ((IFlickrParsable)contact).Load(reader);
                Add(contact);
            }

            reader.Skip();
        }
    }

}