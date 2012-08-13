using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Details of a push subscription.
    /// </summary>
    public sealed class Subscription : IFlickrParsable
    {
        /// <summary>
        /// The topic the subscription is listening to.
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// The callback URL that the subscription is sending to.
        /// </summary>
        public string Callback { get; set; }
        /// <summary>
        /// True if the subscription has not yet been verified.
        /// </summary>
        public bool IsPending { get; set; }
        /// <summary>
        /// The date the subscription was created.
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// The number of seconds the subscription is valid for.
        /// </summary>
        public int LeaseSeconds { get; set; }
        /// <summary>
        /// The date the subscription will expire.
        /// </summary>
        public DateTime Expiry { get; set; }
        /// <summary>
        /// If pending and async then the number of times that verification has been attempted.
        /// </summary>
        public int VerifyAttempts { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "subscription")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "topic":
                        Topic = reader.Value;
                        break;
                    case "callback":
                        Callback = reader.Value;
                        break;
                    case "pending":
                        IsPending = reader.Value == "1";
                        break;
                    case "date_create":
                        DateCreated = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "lease_seconds":
                        LeaseSeconds = reader.ReadContentAsInt();
                        break;
                    case "expiry":
                        Expiry = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "verify_attempts":
                        VerifyAttempts = reader.ReadContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }
    }
}
