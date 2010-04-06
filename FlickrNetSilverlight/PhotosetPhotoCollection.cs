using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A list of photos contained within a photoset.
    /// </summary>
    public sealed class PhotosetPhotoCollection : System.Collections.ObjectModel.Collection<Photo>, IFlickrParsable
    {
        /// <summary>
        /// The id for the photoset.
        /// </summary>
        public string PhotosetId { get; private set; }

        /// <summary>
        /// The ID of the primary photo for this photoset. May be contained within the list.
        /// </summary>
        public string PrimaryPhotoId { get; private set; }

        /// <summary>
        /// The NSID of the owner of this photoset.
        /// </summary>
        public string OwnerId { get; private set; }

        /// <summary>
        /// The real name of the owner of this photoset.
        /// </summary>
        public string OwnerName { get; private set; }

        /// <summary>
        /// The page of the results returned. Will be 1 even if no results are returned.
        /// </summary>
        public int Page { get; private set; }
        /// <summary>
        /// The number of pages of photos returned. Will likely be 1 if no photos are returned.
        /// </summary>
        public int Pages { get; private set; }
        /// <summary>
        /// The number of photos returned per page.
        /// </summary>
        public int PerPage { get; private set; }
        /// <summary>
        /// The total number of photos that match the query. Call the method again to retrieve each page of results if Total > PerPage.
        /// </summary>
        public int Total { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photoset")
                throw new System.Xml.XmlException("Unknown element name '" + reader.LocalName + "' found in Flickr response");

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        PhotosetId = reader.Value;
                        break;
                    case "primary":
                        PrimaryPhotoId = reader.Value;
                        break;
                    case "owner":
                        OwnerId = reader.Value;
                        break;
                    case "ownername":
                        OwnerName = reader.Value;
                        break;
                    case "page":
                        Page = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "total":
                        Total = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "perpage":
                    case "per_page":
                        PerPage = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();

            while (reader.LocalName == "photo")
            {
                Photo photo = new Photo();
                ((IFlickrParsable)photo).Load(reader);
                if (String.IsNullOrEmpty(photo.UserId)) photo.UserId = OwnerId;
                Add(photo);
            }

            reader.Skip();
        }
    }
}
