using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNet.Internals
{
    internal class MimeBody : MimePart
    {
        public string Boundary { get; set; }
        public List<MimePart> MimeParts { get; set; }

        public override void WriteTo(Stream stream)
        {
            var boundaryBytes = Encoding.UTF8.GetBytes("--" + Boundary + "\r\n");

            foreach (var part in MimeParts)
            {
                stream.Write(boundaryBytes, 0, boundaryBytes.Length);
                part.WriteTo(stream);
            }

            var endBoundaryBytes = Encoding.UTF8.GetBytes("--" + Boundary + "--\r\n");
            stream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
        }
    }
}
