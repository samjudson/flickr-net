using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="HotTag"/> instances.
    /// </summary>
    public sealed class HotTagCollection : Collection<HotTag>, IFlickrParsable
    {
        /// <summary>
        /// The period that was used for the query.
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// The count that was used for the query.
        /// </summary>
        public int TagCount { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "period":
                        Period = reader.Value;
                        break;
                    case "count":
                        TagCount = reader.ReadContentAsInt();
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "tag")
            {
                HotTag item = new HotTag();
                ((IFlickrParsable)item).Load(reader);
                Add(item);
            }

            reader.Skip();
        }
    }
}
