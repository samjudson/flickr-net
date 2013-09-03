using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A list of referring domains.
    /// </summary>
    public sealed class StatDomainCollection : Collection<StatDomain>, IFlickrParsable
    {
        /// <summary>
        /// The page of this set of results.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The number of pages of results that are available.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// The number of referrers per page.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// The total number of referrers that are available for this result set.
        /// </summary>
        public int Total { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "domains")
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
                    case "perpage":
                        PerPage = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "domain")
            {
                var domain = new StatDomain();
                ((IFlickrParsable) domain).Load(reader);
                Add(domain);
            }

            reader.Skip();
        }

        #endregion
    }
}