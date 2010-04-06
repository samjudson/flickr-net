using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A machine tag pair made up of a namespace and predicate. "namespace:predicate=value".
    /// </summary>
    public sealed class Pair : IFlickrParsable
    {
        /// <summary>
        /// The name of the pair.
        /// </summary>
        public string PairName { get; private set; }

        /// <summary>
        /// The usage of the namespace.
        /// </summary>
        public int Usage { get; private set; }

        /// <summary>
        /// The predicate part of this pair.
        /// </summary>
        public string PredicateName { get; private set; }

        /// <summary>
        /// The namespace part of this pair.
        /// </summary>
        public string NamespaceName { get; private set; }

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
                }
            }

            reader.Read();

            if (reader.NodeType == System.Xml.XmlNodeType.Text)
                PairName = reader.ReadContentAsString();

            reader.Read();
        }
    }
}
