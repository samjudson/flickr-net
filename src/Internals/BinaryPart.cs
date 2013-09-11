using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNet.Internals
{
    internal class BinaryPart : MimePart
    {
        public string Name { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; private set; }

        public void LoadContent(Stream stream)
        {
            Content = new byte[stream.Length];
            stream.Read(Content, 0, Content.Length);
        }

        public override void WriteTo(Stream stream)
        {
            var sw = new StreamWriter(stream);
            sw.WriteLine("Content-Disposition: form-data; name=\"" + Name + "\"; filename=\"" + Filename + "\"");
            sw.WriteLine("Content-Type: " + ContentType);
            sw.WriteLine();
            sw.Flush();

            stream.Write(Content, 0, Content.Length);
            
            sw.WriteLine();
            sw.Flush();
        }
    }
}
