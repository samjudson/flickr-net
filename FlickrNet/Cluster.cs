using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// A tag cluster (a tag and a group of common sibling tags).
    /// </summary>
    public sealed class Cluster : IFlickrParsable
    {
        /// <summary>
        /// The tag for which this cluster belongs.
        /// </summary>
        public string SourceTag { get; internal set; }

        /// <summary>
        /// The number of tags in this cluster.
        /// </summary>
        public int TotalTags { get; private set; }

        /// <summary>
        /// The collection of tags in this cluster.
        /// </summary>
        public Collection<string> Tags { get; private set; }

        /// <summary>
        /// The cluster id for this cluster.
        /// </summary>
        public string ClusterId
        {
            get
            {
                if (Tags.Count >= 3)
                {
                    return Tags[0] + "-" + Tags[1] + "-" + Tags[2];
                }
                else
                {
                    List<string> ids = new List<string>();
                    foreach (string s in Tags) ids.Add(s);
                    return String.Join("-", ids.ToArray());
                }
            }
        }

        /// <summary>
        /// The URL for the clusters Flickr page.
        /// </summary>
        public Uri ClusterUrl
        {
            get
            {
                return new Uri(String.Format(System.Globalization.CultureInfo.InvariantCulture, "http://www.flickr.com/photos/tags/{0}/clusters/{1}/", SourceTag, ClusterId));
            }
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            Tags = new Collection<string>();

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "total":
                        TotalTags = reader.ReadContentAsInt();
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "tag")
            {
                Tags.Add(reader.ReadElementContentAsString());
            }

            reader.Read();
        }
    }
}
