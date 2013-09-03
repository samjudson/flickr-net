using System.Collections.ObjectModel;
using System.Xml;

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

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
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
                var item = new HotTag();
                ((IFlickrParsable) item).Load(reader);
                Add(item);
            }

            reader.Skip();
        }

        #endregion
    }
}