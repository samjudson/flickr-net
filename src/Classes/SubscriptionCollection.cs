using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="Subscription"/> instances for the calling user.
    /// </summary>
    public sealed class SubscriptionCollection : Collection<Subscription>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            if (reader.LocalName != "subscriptions")
            {
                UtilityMethods.CheckParsingException(reader);
                return;
            }

            reader.Read();

            while (reader.LocalName == "subscription")
            {
                var item = new Subscription();
                ((IFlickrParsable) item).Load(reader);
                Add(item);
            }

            reader.Skip();
        }

        #endregion
    }
}