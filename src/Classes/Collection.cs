using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <remarks/>
    public sealed class Collection : IFlickrParsable
    {
        private readonly Collection<Collection> subcollections = new Collection<Collection>();
        private readonly Collection<CollectionSet> subsets = new Collection<CollectionSet>();

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

        #region IFlickrParsable Members

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

            reader.MoveToElement();

            // If this is an empty collection then skip to next item, which wont be a child, but may be a sibling.
            if (reader.IsEmptyElement)
            {
                reader.Skip();
                return;
            }

            reader.Read();

            while (reader.NodeType == XmlNodeType.Element &&
                   (reader.LocalName == "collection" || reader.LocalName == "set"))
            {
                if (reader.LocalName == "collection")
                {
                    var c = new Collection();
                    ((IFlickrParsable) c).Load(reader);
                    subcollections.Add(c);
                }
                else
                {
                    var s = new CollectionSet();
                    ((IFlickrParsable) s).Load(reader);
                    subsets.Add(s);
                }
            }

            // Skip to next element (if any)
            reader.Skip();
        }

        #endregion
    }
}