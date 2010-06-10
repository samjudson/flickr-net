using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// List containing <see cref="PublicGroupInfo"/> items.
    /// </summary>
    public sealed class PublicGroupInfoCollection : System.Collections.ObjectModel.Collection<PublicGroupInfo>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "groups")
                UtilityMethods.CheckParsingException(reader);

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
