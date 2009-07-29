using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// 
	/// </summary>
	public class CollectionSet
	{
		private string _SetId;
		private string _title;
		private string _description;

		/// <remarks/>
		[XmlAttribute("id", Form = XmlSchemaForm.Unqualified)]
		public string SetId { get { return _SetId; } set { _SetId = value; } }

		/// <remarks/>
		[XmlAttribute("title", Form = XmlSchemaForm.Unqualified)]
		public string Title { get { return _title; } set { _title = value; } }

		/// <remarks/>
		[XmlAttribute("description", Form = XmlSchemaForm.Unqualified)]
		public string Description { get { return _description; } set { _description = value; } }
	}
}
