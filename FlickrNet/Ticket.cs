using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The status of an upload ticket.
    /// </summary>
    public sealed class Ticket : IFlickrParsable
    {
        /// <summary>
        /// The ID of the ticket asked for.
        /// </summary>
        public string TicketId { get; set; }

        /// <summary>
        /// If the ticket is complete then this contains the photo ID of the uploaded photo.
        /// </summary>
        public string PhotoId { get; set; }

        /// <summary>
        /// Is the ticket ID supplied a valid ticket. True if it is invalid.
        /// </summary>
        public bool InvalidTicketId { get; set; }

        /// <summary>
        /// The status of a valid ticket. 0 = Incomplete, 1 = Complete, 2 = Error processing the image/video.
        /// </summary>
        public int CompleteStatus { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        TicketId = reader.Value;
                        break;
                    case "invalid":
                        InvalidTicketId = true;
                        break;
                    case "photoid":
                        PhotoId = reader.Value;
                        break;
                    case "complete":
                        CompleteStatus = reader.ReadContentAsInt();
                        break;
                }
            }

            reader.Skip();
        }
    }
}
