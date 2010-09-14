using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="Value"/> instances.
    /// </summary>
    public sealed class ValueCollection : Collection<Value>, IFlickrParsable
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
        /// The date the values where added since, if specified in the query.
        /// </summary>
        public DateTime? DateAddedSince { get; set; }

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

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "values")
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
                    case "namespace":
                        NamespaceName = reader.Value;
                        break;
                    case "predicate":
                        PredicateName = reader.Value;
                        break;
                    case "added_since":
                        DateAddedSince = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "value":
                        // This sometimes gets returned - it always appears to be blank.
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "value")
            {
                Value item = new Value();
                ((IFlickrParsable)item).Load(reader);
                if (!String.IsNullOrEmpty(NamespaceName) && String.IsNullOrEmpty(item.NamespaceName)) item.NamespaceName = NamespaceName;
                if (!String.IsNullOrEmpty(PredicateName) && String.IsNullOrEmpty(item.PredicateName)) item.PredicateName = PredicateName;
                Add(item);
            }

            reader.Skip();
        }
    }
}
