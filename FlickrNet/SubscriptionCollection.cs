using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="Subscription"/> instances for the calling user.
    /// </summary>
    public sealed class SubscriptionCollection : Collection<Subscription>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            if (reader.LocalName != "subscriptions") { UtilityMethods.CheckParsingException(reader); return; }

            reader.Read();

            while (reader.LocalName == "subscription")
            {
                Subscription item = new Subscription();
                ((IFlickrParsable)item).Load(reader);
                Add(item);
            }

            reader.Skip();
        }
    }
}
