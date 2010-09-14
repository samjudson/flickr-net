using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CollectionSet : IFlickrParsable
    {
        /// <remarks/>
        public string SetId { get; set; }

        /// <remarks/>
        public string Title { get; set; }

        /// <remarks/>
        public string Description { get; set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "set")
                UtilityMethods.CheckParsingException(reader);

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
                        UtilityMethods.CheckParsingException(reader);
                        break;

                }
            }

            reader.Skip();
        }
    }
}
