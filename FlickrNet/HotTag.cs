using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A hot tag. Returned by <see cref="Flickr.TagsGetHotList()"/>.
    /// </summary>
    public sealed class HotTag : IFlickrParsable
    {
        /// <summary>
        /// The tag that is hot.
        /// </summary>
        public string Tag { get; private set; }

        /// <summary>
        /// The score for the tag.
        /// </summary>
        public int Score { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "score":
                        Score = reader.ReadContentAsInt();
                        break;
                }
            }

            reader.Read();

            if (reader.NodeType == System.Xml.XmlNodeType.Text)
                Tag = reader.ReadContentAsString();

            reader.Read();
        }
    }
}
