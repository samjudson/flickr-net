using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="Cluster"/> instances.
    /// </summary>
    public sealed class ClusterCollection : Collection<Cluster>, IFlickrParsable
    {
        /// <summary>
        /// The source tag for this cluster collection.
        /// </summary>
        public string SourceTag { get; set; }

        /// <summary>
        /// The total number of clusters for this tag.
        /// </summary>
        public int TotalClusters { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "source":
                        SourceTag = reader.Value;
                        break;
                    case "total":
                        TotalClusters = reader.ReadContentAsInt();
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "cluster")
            {
                Cluster item = new Cluster();
                ((IFlickrParsable)item).Load(reader);
                item.SourceTag = SourceTag;
                Add(item);
            }

            reader.Skip();

        }
    }
}
