using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A list of photos contained within a photoset.
    /// </summary>
    public sealed class PhotosetPhotoCollection : PagedPhotoCollection, IFlickrParsable
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

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photoset")
                UtilityMethods.CheckParsingException(reader);

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
                        UtilityMethods.CheckParsingException(reader);
                        break;
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
