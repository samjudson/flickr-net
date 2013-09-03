using System;
using System.Xml;

namespace FlickrNet
{
    internal sealed class StringHolder : IFlickrParsable
    {
        public string Value { get; set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            // Find first attribute or element with a none-empty contents;

            while (reader.Read())
            {
                while (reader.MoveToNextAttribute())
                {
                    if (String.IsNullOrEmpty(reader.Value)) continue;

                    Value = reader.Value;
                    return;
                }
            }
        }
    }
}