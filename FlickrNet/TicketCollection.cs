using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="Ticket"/> instances.
    /// </summary>
    public sealed class TicketCollection : Collection<Ticket>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "ticket")
            {
                Ticket ticket = new Ticket();
                ((IFlickrParsable)ticket).Load(reader);
                Add(ticket);
            }

            reader.Skip();

        }
    }
}
