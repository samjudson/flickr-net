using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// The total number of favorites for a user, along with the next and previous favorite photos.
    /// </summary>
    public sealed class FavoriteContext : IFlickrParsable
    {
        /// <summary>
        /// The number of favorites the user has.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The list of previous photos for this favorite. Defaults to just a single photo.
        /// </summary>
        public Collection<FavoriteContextPhoto> PreviousPhotos { get; set; }

        /// <summary>
        /// The list of next photos for this favorite. Defaults to just a single photo.
        /// </summary>
        public Collection<FavoriteContextPhoto> NextPhotos { get; set; }

        /// <summary>
        /// Default constructor for <see cref="FavoriteContext"/>
        /// </summary>
        public FavoriteContext()
        {
            PreviousPhotos = new Collection<FavoriteContextPhoto>();
            NextPhotos = new Collection<FavoriteContextPhoto>();
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {

            if (reader.LocalName != "count" && !reader.ReadToFollowing("count"))
            {
                UtilityMethods.CheckParsingException(reader);
                return;
            }

            Count = reader.ReadElementContentAsInt();

            if( reader.LocalName != "prevphotos" ) reader.ReadToFollowing("prevphotos");
            reader.ReadToDescendant("photo");
            while (reader.LocalName == "photo")
            {
                FavoriteContextPhoto photo = new FavoriteContextPhoto();
                ((IFlickrParsable)photo).Load(reader);
                PreviousPhotos.Add(photo);
            }

            if (reader.LocalName != "nextphotos") reader.ReadToFollowing("nextphotos");
            reader.ReadToDescendant("photo");
            while (reader.LocalName == "photo")
            {
                FavoriteContextPhoto photo = new FavoriteContextPhoto();
                ((IFlickrParsable)photo).Load(reader);
                NextPhotos.Add(photo);
            }
        }
    }
}
