using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNet
{
    public sealed class PandaCollection : Collection<string>,IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            reader.ReadToFollowing("panda");

            while (reader.LocalName == "panda")
            {
                Add(reader.ReadElementContentAsString());
            }
        }
    }
}
