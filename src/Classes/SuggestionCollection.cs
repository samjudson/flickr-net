using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// The collection of location suggestions returned by <see cref="Flickr.PhotosSuggestionsGetList"/>.
    /// </summary>
    public sealed class SuggestionCollection : Collection<Suggestion>, IFlickrParsable
    {
        /// <summary>
        /// The total number of suggestions available.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// The number of suggestions per page.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// The current page of suggestions returned.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The total number of pages of suggestions available.
        /// </summary>
        public int Pages
        {
            get { return (int) Math.Ceiling(1.0*Total/PerPage); }
            set { }
        }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            if (reader.LocalName != "suggestions")
            {
                UtilityMethods.CheckParsingException(reader);
                return;
            }

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "total":
                        Total = reader.ReadContentAsInt();
                        break;
                    case "page":
                        Page = reader.ReadContentAsInt();
                        break;
                    case "per_page":
                        PerPage = reader.ReadContentAsInt();
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "suggestion")
            {
                var suggestion = new Suggestion();
                ((IFlickrParsable) suggestion).Load(reader);
                Add(suggestion);
            }

            return;
        }

        #endregion
    }
}