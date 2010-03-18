using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// List containing <see cref="PublicGroupInfo"/> items.
    /// </summary>
    public class PublicGroupInfoCollection : List<PublicGroupInfo>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "groups")
                throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");

            reader.Read();

            while (reader.LocalName == "group")
            {
                PublicGroupInfo member = new PublicGroupInfo();
                ((IFlickrParsable)member).Load(reader);
                Add(member);
            }

            reader.Skip();
        }
    }
}
