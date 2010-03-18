using System;
using System.Xml;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlickrNet
{
	/// <remarks/>
	public sealed class Collection : IFlickrParsable
	{
		private string _CollectionId;
		private string _title;
		private string _description;
		private string _iconlarge;
		private string _iconsmall;

        private Collection<CollectionSet> _subsets = new Collection<CollectionSet>();
        private Collection<Collection> _subcollections = new Collection<Collection>();

		/// <remarks/>
		public string CollectionId { get { return _CollectionId; } set { _CollectionId = value; } }

		/// <remarks/>
		public string Title { get { return _title; } set { _title = value; } }

		/// <remarks/>
		public string Description { get { return _description; } set { _description = value; } }

		/// <remarks/>
		public string IconLarge { get { return _iconlarge; } set { _iconlarge = value; } }

		/// <remarks/>
		public string IconSmall { get { return _iconsmall; } set { _iconsmall = value; } }

		/// <summary>
		/// An array of <see cref="CollectionSet"/> objects.
		/// </summary>
		public Collection<CollectionSet> Sets
		{
			get { return _subsets; }
		}

		/// <summary>
		/// An array of <see cref="Collection"/> objects.
		/// </summary>
        public Collection<Collection> Collections
		{
			get { return _subcollections; }
		}

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "collection")
                throw new ParsingException("Unknown element found: " + reader.LocalName);

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
                    default:
                        throw new ParsingException("Unknown element: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Read();

            while (reader.LocalName == "collection" || reader.LocalName == "set")
            {
                if (reader.LocalName == "collection")
                {
                    Collection c = new Collection();
                    ((IFlickrParsable)c).Load(reader);
                    _subcollections.Add(c);

                }
                else
                {
                    CollectionSet s = new CollectionSet();
                    ((IFlickrParsable)s).Load(reader);
                    _subsets.Add(s);
                }
            }

            // Skip to next element (if any)
            reader.Skip();

        }
    }
}
