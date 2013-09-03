using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// List containing <see cref="GroupInfo"/> items.
    /// </summary>
    public sealed class GroupInfoCollection : Collection<GroupInfo>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "groups")
                UtilityMethods.CheckParsingException(reader);

            reader.Read();

            while (reader.LocalName == "group")
            {
                var member = new GroupInfo();
                ((IFlickrParsable) member).Load(reader);
                Add(member);
            }

            reader.Skip();
        }

        #endregion
    }
}