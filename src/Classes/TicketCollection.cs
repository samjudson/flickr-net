using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="Ticket"/> instances.
    /// </summary>
    public sealed class TicketCollection : Collection<Ticket>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "ticket")
            {
                var ticket = new Ticket();
                ((IFlickrParsable) ticket).Load(reader);
                Add(ticket);
            }

            reader.Skip();
        }

        #endregion
    }
}