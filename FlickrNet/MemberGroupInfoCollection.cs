using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A list of <see cref="MemberGroupInfo"/> items.
    /// </summary>
    public class MemberGroupInfoCollection : List<MemberGroupInfo>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "groups")
                throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");

            reader.Read();

            while (reader.LocalName == "group")
            {
                MemberGroupInfo member = new MemberGroupInfo();
                ((IFlickrParsable)member).Load(reader);
                Add(member);
            }

            reader.Skip();
        }
    }
}
