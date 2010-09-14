using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A machine tag value and its usage.
    /// </summary>
    public sealed class Value : IFlickrParsable
    {
        /// <summary>
        /// The usage of this machine tag value.
        /// </summary>
        public int Usage { get; set; }

        /// <summary>
        /// The namespace for this value.
        /// </summary>
        public string NamespaceName { get; set; }

        /// <summary>
        /// The predicate name for this value.
        /// </summary>
        public string PredicateName { get; set; }

        /// <summary>
        /// The text of this value.
        /// </summary>
        public string ValueText { get; set; }

        /// <summary>
        /// The date this machine tag was first used.
        /// </summary>
        public DateTime? DateFirstAdded { get; set; }

        /// <summary>
        /// The date this machine tag was last added.
        /// </summary>
        public DateTime? DateLastUsed { get; set; }

        /// <summary>
        /// The full machine tag for this value.
        /// </summary>
        public string MachineTag
        {
            get { return NamespaceName + ":" + PredicateName + "=" + ValueText; }
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "usage":
                        Usage = reader.ReadContentAsInt();
                        break;
                    case "predicate":
                        PredicateName = reader.Value;
                        break;
                    case "namespace":
                        NamespaceName = reader.Value;
                        break;
                    case "first_added":
                        DateFirstAdded = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "last_added":
                        DateLastUsed = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                }
            }

            reader.Read();

            if (reader.NodeType == System.Xml.XmlNodeType.Text)
                ValueText = reader.ReadContentAsString();

            reader.Read();

        }
    }
}
