using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Provides details of a particular group.
    /// </summary>
    /// <remarks>Used by <see cref="Flickr.GroupsBrowse()"/> and
    /// <see cref="Flickr.GroupsBrowse(string)"/>.</remarks>
    public class Group
    {
        /// <summary>
        /// The id of the group.
        /// </summary>
        [XmlAttribute("nsid", Form = XmlSchemaForm.Unqualified)]
        public string GroupId { get; set; }

        /// <summary>
        /// The name of the group
        /// </summary>
        [XmlAttribute("name", Form = XmlSchemaForm.Unqualified)]
        public string GroupName { get; set; }

        /// <summary>
        /// The number of memebers of the group.
        /// </summary>
        [XmlAttribute("members", Form = XmlSchemaForm.Unqualified)]
        public int Members { get; set; }
    }

    /// <summary>
    /// Throttle information about a group (i.e. posting limit)
    /// </summary>
    public sealed class GroupThrottleInfo : IFlickrParsable
    {
        /// <summary>
        /// The number of posts in each period allowed to this group.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The posting limit mode for a group.
        /// </summary>
        public GroupThrottleMode Mode { get; set; }

        private static GroupThrottleMode ParseMode(string mode)
        {
            switch (mode)
            {
                case "day":
                    return GroupThrottleMode.PerDay;
                case "week":
                    return GroupThrottleMode.PerWeek;
                case "month":
                    return GroupThrottleMode.PerMonth;
                case "ever":
                    return GroupThrottleMode.Ever;
                case "none":
                    return GroupThrottleMode.NoLimit;
                case "disabled":
                    return GroupThrottleMode.Disabled;
                default:
                    throw new ArgumentException(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Unknown mode found {0}", mode), "mode");
            }
        }

        /// <summary>
        /// The number of remainging posts allowed by this user. If unauthenticated then this will be zero.
        /// </summary>
        public int Remaining { get; set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "count":
                        Count = reader.ReadContentAsInt();
                        break;
                    case "mode":
                        Mode = ParseMode(reader.Value);
                        break;
                    case "remaining":
                        Remaining = reader.ReadContentAsInt();
                        break;
                }
            }
            reader.Read();
        }
    }

    /// <summary>
    /// The restrictions that apply to a group's pool.
    /// </summary>
    public sealed class GroupInfoRestrictions : IFlickrParsable
    {
        /// <summary>
        /// Are photos allowed to be added to this pool.
        /// </summary>
        public bool PhotosAccepted { get; set; }
        /// <summary>
        /// Are videos allowed to be added to this pool.
        /// </summary>
        public bool VideosAccepted { get; set; }

        /// <summary>
        /// Are Photo/Video images allowed to be added to this pool.
        /// </summary>
        public bool ImagesAccepted { get; set; }
        /// <summary>
        /// Are Screenshots/Screencasts allowed to be added to this pool.
        /// </summary>
        public bool ScreenshotsAccepted { get; set; }
        /// <summary>
        /// Are Illustrations/Art/Animation/CGI allowed to be added to this pool.
        /// </summary>
        public bool ArtIllustrationsAccepted { get; set; }

        /// <summary>
        /// Are safe items allowed to be added to this pool.
        /// </summary>
        public bool SafeItemsAccepted { get; set; }
        /// <summary>
        /// Are moderated items allowed to be added to this pool.
        /// </summary>
        public bool ModeratedItemsAccepted { get; set; }
        /// <summary>
        /// Are restricted items allowed to be added to this pool.
        /// </summary>
        public bool RestrictedItemsAccepted { get; set; }
        /// <summary>
        /// Must the item have geo information.
        /// </summary>
        public bool GeoInfoRequired { get; set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "photos_ok":
                        PhotosAccepted = reader.Value == "1";
                        break;
                    case "videos_ok":
                        VideosAccepted = reader.Value == "1";
                        break;
                    case "images_ok":
                        ImagesAccepted = reader.Value == "1";
                        break;
                    case "screens_ok":
                        ScreenshotsAccepted = reader.Value == "1";
                        break;
                    case "art_ok":
                        ArtIllustrationsAccepted = reader.Value == "1";
                        break;
                    case "safe_ok":
                        SafeItemsAccepted = reader.Value == "1";
                        break;
                    case "moderate_ok":
                        ModeratedItemsAccepted = reader.Value == "1";
                        break;
                    case "resitricted_ok":
                        RestrictedItemsAccepted = reader.Value == "1";
                        break;
                    case "has_geo":
                        GeoInfoRequired = reader.Value == "1";
                        break;
                }
            }

            reader.Read();
        }
    }


    /// <summary>
    /// The posting limit most for a group.
    /// </summary>
    public enum GroupThrottleMode
    {
        /// <summary>
        /// Per day posting limit.
        /// </summary>
        PerDay,
        /// <summary>
        /// Per week posting limit.
        /// </summary>
        PerWeek,
        /// <summary>
        /// Per month posting limit.
        /// </summary>
        PerMonth,
        /// <summary>
        /// No posting limit.
        /// </summary>
        NoLimit,
        /// <summary>
        /// Posting limit is total number of photos in the group.
        /// </summary>
        Ever,
        /// <summary>
        /// Posting is disabled to this group.
        /// </summary>
        Disabled

    }


}
