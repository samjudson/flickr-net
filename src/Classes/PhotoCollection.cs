using System;
using System.Globalization;
using System.Xml;

namespace FlickrNet
{
    /// <remarks/>
    public sealed class PhotoCollection : PagedPhotoCollection, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "photos")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "total":
                        if (reader.Value == "")
                            Total = -1;
                        else
                            Total = int.Parse(reader.Value, CultureInfo.InvariantCulture);
                        break;
                    case "perpage":
                    case "per_page":
                        PerPage = int.Parse(reader.Value, CultureInfo.InvariantCulture);
                        break;
                    case "page":
                        Page = int.Parse(reader.Value, CultureInfo.InvariantCulture);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, CultureInfo.InvariantCulture);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "photo")
            {
                var p = new Photo();
                ((IFlickrParsable) p).Load(reader);
                if (!String.IsNullOrEmpty(p.PhotoId)) Add(p);
            }

            // Skip to next element (if any)
            reader.Skip();
        }

        #endregion
    }
}