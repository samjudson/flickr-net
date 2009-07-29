using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <remarks/>
	[System.Serializable]
	public class Collection
	{
		private string _CollectionId;
		private string _title;
		private string _description;
		private string _iconlarge;
		private string _iconsmall;

		private CollectionSet[] _sets = new CollectionSet[0];
		private Collection[] _collections = new Collection[0];

		/// <remarks/>
		[XmlAttribute("id", Form = XmlSchemaForm.Unqualified)]
		public string CollectionId { get { return _CollectionId; } set { _CollectionId = value; } }

		/// <remarks/>
		[XmlAttribute("title", Form = XmlSchemaForm.Unqualified)]
		public string Title { get { return _title; } set { _title = value; } }

		/// <remarks/>
		[XmlAttribute("description", Form = XmlSchemaForm.Unqualified)]
		public string Description { get { return _description; } set { _description = value; } }

		/// <remarks/>
		[XmlAttribute("iconlarge", Form = XmlSchemaForm.Unqualified)]
		public string IconLarge { get { return _iconlarge; } set { _iconlarge = value; } }

		/// <remarks/>
		[XmlAttribute("iconsmall", Form = XmlSchemaForm.Unqualified)]
		public string IconSmall { get { return _iconsmall; } set { _iconsmall = value; } }

		/// <summary>
		/// An array of <see cref="CollectionSet"/> objects.
		/// </summary>
		[XmlElement("set", Form = XmlSchemaForm.Unqualified)]
		public CollectionSet[] Sets
		{
			get { return _sets; }
			set
			{
				if (value == null)
					_sets = new CollectionSet[0];
				else
					_sets = value;
			}
		}

		/// <summary>
		/// An array of <see cref="Collection"/> objects.
		/// </summary>
		[XmlElement("collection", Form = XmlSchemaForm.Unqualified)]
		public Collection[] Collections
		{
			get { return _collections; }
			set
			{
				if (value == null)
					_collections = new Collection[0];
				else
					_collections = value;
			}
		}
	}
}
