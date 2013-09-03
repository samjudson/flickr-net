using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="Namespace"/> instances.
    /// </summary>
    public sealed class NamespaceCollection : Collection<Namespace>, IFlickrParsable
    {
        /// <summary>
        /// The total number of namespaces that match the calling query.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// The page of the result set that has been returned.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The number of namespaces returned per page.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// The number of pages of namespaces that are available.
        /// </summary>
        public int Pages { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "namespaces")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "page":
                        Page = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "total":
                        Total = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "per_page":
                    case "perpage":
                        PerPage = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "namespace")
            {
                var ns = new Namespace();
                ((IFlickrParsable) ns).Load(reader);
                Add(ns);
            }

            reader.Skip();
        }

        #endregion
    }
}