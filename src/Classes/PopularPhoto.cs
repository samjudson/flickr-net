using System.Globalization;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Details about a popular photo, including the statistics for its views, comments and favourites for the date.
    /// </summary>
    public class PopularPhoto : Photo, IFlickrParsable
    {
        /// <summary>
        /// The number of views this photo has received in the context of the calling time period.
        /// </summary>
        public int StatViews { get; set; }

        /// <summary>
        /// The number of comments this photo has received in the context of the calling time period.
        /// </summary>
        public int StatComments { get; set; }

        /// <summary>
        /// The number of favorites this photo has received in the context of the calling time period.
        /// </summary>
        public int StatFavorites { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            Load(reader, false);

            if (reader.LocalName != "stats")
            {
                UtilityMethods.CheckParsingException(reader);
            }

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "views":
                        StatViews = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "comments":
                        StatComments = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "favorites":
                        StatFavorites = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            if (reader.LocalName == "description")
                Description = reader.ReadElementContentAsString();

            reader.Skip();
        }

        #endregion
    }
}