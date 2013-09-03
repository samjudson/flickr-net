using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A list of referrers.
    /// </summary>
    public sealed class StatReferrerCollection : Collection<StatReferrer>, IFlickrParsable
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

        /// <summary>
        /// The domain name for this set of referrers.
        /// </summary>
        public string DomainName { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "domain")
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
                    case "name":
                        DomainName = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "referrer")
            {
                var referrer = new StatReferrer();
                ((IFlickrParsable) referrer).Load(reader);
                Add(referrer);
            }

            reader.Skip();
        }

        #endregion
    }
}