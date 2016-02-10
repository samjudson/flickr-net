
using NUnit.Framework;
using FlickrNet;
using System.IO;
using System.Xml;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosSerializationTests
    /// </summary>
    [TestFixture]
    public class PhotosSerializationTests : BaseTest
    {
        [Test]
        public void PhotosSerializationSkipBlankPhotoRowTest()
        {
            const string xml = @"<photos page=""1"" pages=""1"" perpage=""500"" total=""500"">
                    <photo id=""3662960087"" owner=""18499405@N00"" secret=""9f8fcf9269"" server=""3379"" farm=""4"" title=""gecko closeup"" 
                        ispublic=""1"" isfriend=""0"" isfamily=""0"" dateupload=""1246050291"" 
                        tags=""reptile jinaacom geckocloseup geckoanatomy jinaajinahibrahim"" latitude=""1.45"" />
                    <photo id="""" owner="""" secret="""" server="""" farm=""0"" title="""" 
                        ispublic="""" isfriend="""" isfamily="""" dateupload="""" tags="""" latitude="""" />
                    <photo id=""3662960087"" owner=""18499405@N00"" secret=""9f8fcf9269"" server=""3379"" farm=""4"" title=""gecko closeup"" 
                        ispublic=""1"" isfriend=""0"" isfamily=""0"" dateupload=""1246050291"" tags=""reptile jinaacom geckocloseup geckoanatomy jinaajinahibrahim"" latitude=""1.45"" />
                    </photos>";

            var photos = new PhotoCollection();

            var sr = new StringReader(xml);
            var reader = new XmlTextReader(sr) {WhitespaceHandling = WhitespaceHandling.Significant};

            reader.ReadToDescendant("photos");

            ((IFlickrParsable) photos).Load(reader);

            Assert.IsNotNull(photos, "Photos should not be null");
            Assert.AreEqual(500, photos.Total, "Total photos should be 500");
            Assert.AreEqual(2, photos.Count, "Should only contain 2 photo");

        }
    }
}
