using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The limits for a person returned by <see cref="Flickr.PeopleGetLimits"/>.
    /// </summary>
    /// <remarks>
    /// For more details on limits see help here: http://www.flickr.com/help/limits/
    /// </remarks>
    public class PersonLimits : IFlickrParsable
    {
        /// <summary>
        /// Maximum size in pixels for photos displayed on the site (0 means that no limit is in place). No limit is placed on the dimension of photos uploaded
        /// </summary>
        public int MaximumDisplayPixels { get; set; }
        /// <summary>
        /// Maximum file size in bytes for photo uploads.
        /// </summary>
        public long MaximumPhotoUpload { get; set; }
        /// <summary>
        /// Maximum file size in bytes for video uploads.
        /// </summary>
        public long MaximumVideoUpload { get; set; }
        /// <summary>
        /// Maximum duration in seconds of a video.
        /// </summary>
        public int MaximumVideoDuration { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");

            if (!reader.ReadToFollowing("photos")) throw new ResponseXmlException("Unable to find \"photos\" element in response.");

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "maxdisplaypx":
                        MaximumDisplayPixels = reader.ReadContentAsInt();
                        break;
                    case "maxupload":
                        MaximumPhotoUpload = reader.ReadContentAsLong();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            if (!reader.ReadToFollowing("videos")) throw new ResponseXmlException("Unable to find \"videos\" element in response.");

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "maxduration":
                        MaximumVideoDuration = reader.ReadContentAsInt();
                        break;
                    case "maxupload":
                        MaximumVideoUpload = reader.ReadContentAsLong();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Skip();
        }
    }
}
