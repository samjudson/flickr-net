using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosSerializationTests
    /// </summary>
    [TestClass]
    public class PhotosSerializationTests
    {
        public PhotosSerializationTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void PhotosSerializationSkipBlankPhotoRowTest()
        {
            string xml = @"<photos page=""1"" pages=""1"" perpage=""500"" total=""500"">
                    <photo id=""3662960087"" owner=""18499405@N00"" secret=""9f8fcf9269"" server=""3379"" farm=""4"" title=""gecko closeup"" ispublic=""1"" isfriend=""0"" isfamily=""0"" dateupload=""1246050291"" tags=""reptile jinaacom geckocloseup geckoanatomy jinaajinahibrahim"" latitude=""1.45"" />
                    <photo id="""" owner="""" secret="""" server="""" farm=""0"" title="""" ispublic="""" isfriend="""" isfamily="""" dateupload="""" tags="""" latitude="""" />
                    <photo id=""3662960087"" owner=""18499405@N00"" secret=""9f8fcf9269"" server=""3379"" farm=""4"" title=""gecko closeup"" ispublic=""1"" isfriend=""0"" isfamily=""0"" dateupload=""1246050291"" tags=""reptile jinaacom geckocloseup geckoanatomy jinaajinahibrahim"" latitude=""1.45"" />
                    </photos>";

            PhotoCollection photos = new PhotoCollection();

            StringReader sr = new StringReader(xml);
            XmlTextReader reader = new XmlTextReader(sr);
            reader.WhitespaceHandling = WhitespaceHandling.Significant;

            reader.ReadToDescendant("photos");

            ((IFlickrParsable)photos).Load(reader);

            Assert.IsNotNull(photos, "Photos should not be null");
            Assert.AreEqual(500, photos.Total, "Total photos should be 500");
            Assert.AreEqual(2, photos.Count, "Should only contain 2 photo");

        }
    }
}
