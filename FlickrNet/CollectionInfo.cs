using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// Summary description for CollectionInfo.
    /// </summary>
    public sealed class CollectionInfo : IFlickrParsable
    {
        private Collection<Photo> iconPhotos = new Collection<Photo>();

        /// <summary>
        /// The ID for the collection.
        /// </summary>
        public string CollectionId { get; private set; }

        /// <summary>
        /// The number of child collections this collection contains. Call <see cref="Flickr.CollectionsGetTree()"/> for children.
        /// </summary>
        public int ChildCount { get; private set; }

        /// <summary>
        /// The date the collection was created.
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// The large mosaic icon for the collection.
        /// </summary>
        public string IconLarge { get; private set; }

        /// <summary>
        /// The small mosaix icon for the collection.
        /// </summary>
        public string IconSmall { get; private set; }

        /// <summary>
        /// The server for the icons.
        /// </summary>
        public string Server { get; private set; }

        /// <summary>
        /// The secret for the icons.
        /// </summary>
        public string Secret { get; private set; }

        /// <summary>
        /// The description for the collection.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The title of the description.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// An array of the 12 photos used to create a collection's mosaic.
        /// </summary>
        public Collection<Photo> IconPhotos { get { return iconPhotos; } }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "collection")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "id":
                        CollectionId = reader.Value;
                        break;
                    case "child_count":
                        ChildCount = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "datecreate":
                        DateCreated = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "iconlarge":
                        IconLarge = reader.Value;
                        break;
                    case "iconsmall":
                        IconSmall = reader.Value;
                        break;
                    case "server":
                        Server = reader.Value;
                        break;
                    case "secret":
                        Secret = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "title":
                        Title = reader.ReadElementContentAsString();
                        break;
                    case "description":
                        Description = reader.ReadElementContentAsString();
                        break;
                    case "iconphotos":
                        reader.Read();

                        while (reader.LocalName == "photo")
                        {
                            Photo p = new Photo();
                            ((IFlickrParsable)p).Load(reader);

                            iconPhotos.Add(p);
                        }
                        reader.Read();
                        return;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Skip();

        }
    }
}
