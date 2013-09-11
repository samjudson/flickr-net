using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNet.Internals
{
    internal abstract class MimePart
    {
        public abstract void WriteTo(Stream stream);
    }
}
