using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <remarks/>
	public class Collection : IFlickrParsable
	{
		private string _CollectionId;
		private string _title;
		private string _description;
		private string _iconlarge;
		private string _iconsmall;

        private List<CollectionSet> _subsets = new List<CollectionSet>();
        private List<Collection> _subcollections = new List<Collection>();

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
		public CollectionSet[] Sets
		{
			get { return _subsets.ToArray(); }
		}

		/// <summary>
		/// An array of <see cref="Collection"/> objects.
		/// </summary>
		[XmlElement("collection", Form = XmlSchemaForm.Unqualified)]
		public Collection[] Collections
		{
			get { return _subcollections.ToArray(); }
		}

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "collection")
                throw new FlickrException("Unknown element found: " + reader.LocalName);

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
                        throw new Exception("Unknown element: " + reader.Name + "=" + reader.Value);

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
