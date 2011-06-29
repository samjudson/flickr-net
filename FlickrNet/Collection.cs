using System;
using System.Xml;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <remarks/>
    public sealed class Collection : IFlickrParsable
    {
        private Collection<CollectionSet> subsets = new Collection<CollectionSet>();
        private Collection<Collection> subcollections = new Collection<Collection>();

        /// <remarks/>
        public string CollectionId { get; set; }

        /// <remarks/>
        public string Title { get; set; }

        /// <remarks/>
        public string Description { get; set; }

        /// <remarks/>
        public string IconLarge { get; set; }

        /// <remarks/>
        public string IconSmall { get; set; }

        /// <summary>
        /// The URL of the collection. Only returned when creating a new collection.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// An array of <see cref="CollectionSet"/> objects.
        /// </summary>
        public Collection<CollectionSet> Sets
        {
            get { return subsets; }
        }

        /// <summary>
        /// An array of <see cref="Collection"/> objects.
        /// </summary>
        public Collection<Collection> Collections
        {
            get { return subcollections; }
        }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "collection")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        CollectionId = reader.Value;
                        break;
                    case "title":
                        Title = reader.Value;
                        break;
                    case "description":
                        Description = reader.Value;
                        break;
                    case "iconlarge":
                        IconLarge = reader.Value;
                        break;
                    case "iconsmall":
                        IconSmall = reader.Value;
                        break;
                    case "url":
                        Url = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;

                }
            }

            reader.Read();

            while (reader.NodeType == XmlNodeType.Element && (reader.LocalName == "collection" || reader.LocalName == "set"))
            {
                if (reader.LocalName == "collection")
                {
                    Collection c = new Collection();
                    ((IFlickrParsable)c).Load(reader);
                    subcollections.Add(c);

                }
                else
                {
                    CollectionSet s = new CollectionSet();
                    ((IFlickrParsable)s).Load(reader);
                    subsets.Add(s);
                }
            }

            // Skip to next element (if any)
            reader.Skip();

        }
    }
}
