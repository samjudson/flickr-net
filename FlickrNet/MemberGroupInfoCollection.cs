using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A list of <see cref="MemberGroupInfo"/> items.
    /// </summary>
    public sealed class MemberGroupInfoCollection : System.Collections.ObjectModel.Collection<MemberGroupInfo>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "groups")
                UtilityMethods.CheckParsingException(reader);

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
