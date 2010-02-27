using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// 
	/// </summary>
	public class CollectionSet : IFlickrParsable
	{
		private string _SetId;
		private string _title;
		private string _description;

		/// <remarks/>
		public string SetId { get { return _SetId; } set { _SetId = value; } }

		/// <remarks/>
		public string Title { get { return _title; } set { _title = value; } }

		/// <remarks/>
		public string Description { get { return _description; } set { _description = value; } }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "set")
                throw new FlickrException("Unknown element found: " + reader.LocalName);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        SetId = reader.Value;
                        break;
                    case "title":
                        Title = reader.Value;
                        break;
                    case "description":
                        Description = reader.Value;
                        break;
                    default:
                        throw new Exception("Unknown element: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Skip();
        }
    }
}
