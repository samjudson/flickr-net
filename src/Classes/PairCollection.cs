using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="Pair"/> instances.
    /// </summary>
    public sealed class PairCollection : Collection<Pair>, IFlickrParsable
    {
        /// <summary>
        /// The name of the predicate searched for, if any.
        /// </summary>
        public string PredicateName { get; set; }

        /// <summary>
        /// The namespace that was searched for, if any.
        /// </summary>
        public string NamespaceName { get; set; }

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
            if (reader.LocalName != "pairs")
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
                    case "namespace":
                        NamespaceName = reader.Value;
                        break;
                    case "predicate":
                        PredicateName = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "pair")
            {
                var item = new Pair();
                ((IFlickrParsable) item).Load(reader);
                Add(item);
            }

            reader.Skip();
        }

        #endregion
    }
}