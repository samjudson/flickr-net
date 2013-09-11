using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNet.Internals
{
    internal class FormDataPart : MimePart
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override void WriteTo(Stream stream)
        {
            var sw = new StreamWriter(stream);
            sw.WriteLine("Content-Disposition: form-data; name=\"" + Name + "\"");
            sw.WriteLine();
            sw.WriteLine(Value);
            sw.Flush();
        }
    }
}
