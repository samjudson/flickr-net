using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosGetInfoTests
    /// </summary>
    [TestClass]
    public class PhotosGetInfoTests
    {
        public PhotosGetInfoTests()
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
        public void PhotosGetInfoBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            PhotoInfo info = f.PhotosGetInfo("4268023123");

            Assert.IsNotNull(info);

            Assert.AreEqual("4268023123", info.PhotoId);
            Assert.AreEqual("a4283bac01", info.Secret);
            Assert.AreEqual("2795", info.Server);
            Assert.AreEqual("3", info.Farm);
            Assert.AreEqual(UtilityMethods.UnixTimestampToDate("1263291891"), info.DateUploaded);
            Assert.AreEqual(false, info.IsFavorite);
            Assert.AreEqual(LicenseType.AttributionNoncommercialShareAlikeCC, info.License);
            Assert.AreEqual(0, info.Rotation);
            Assert.AreEqual("9d3d4bf24a", info.OriginalSecret);
            Assert.AreEqual("jpg", info.OriginalFormat);
            Assert.IsTrue(info.ViewCount > 87, "ViewCount should be greater than 87.");
            Assert.AreEqual(MediaType.Photos, info.Media);

            Assert.AreEqual("12. Sudoku", info.Title);
            Assert.AreEqual("It scares me sometimes how much some of my handwriting reminds me of Dad's - in this photo there is one 5 that especially reminds me of his handwriting.", info.Description);

            //Owner
            Assert.AreEqual("41888973@N00", info.OwnerUserId);

            //Dates
            Assert.AreEqual(new DateTime(2010, 01, 12, 11, 01, 20), info.DateTaken, "DateTaken is not set correctly.");

            //Editability
            Assert.IsTrue(info.CanComment, "CanComment should be true when not authenticated.");
            Assert.IsTrue(info.CanAddMeta, "CanAddMeta should be true when not authenticated.");

            //Permissions
            Assert.AreEqual(PermissionComment.Everybody, info.PermissionComment);
            Assert.AreEqual(PermissionAddMeta.Everybody, info.PermissionAddMeta);

            //Visibility

            // Notes

            Assert.AreEqual(1, info.Notes.Count, "Notes.Count should be one.");
            Assert.AreEqual("72157623069944527", info.Notes[0].NoteId);
            Assert.AreEqual("41888973@N00", info.Notes[0].AuthorId);
            Assert.AreEqual("Sam Judson", info.Notes[0].AuthorName);
            Assert.AreEqual(267, info.Notes[0].XPosition);
            Assert.AreEqual(238, info.Notes[0].YPosition);

            // Tags

            Assert.AreEqual(5, info.Tags.Count);
            Assert.AreEqual("78188-4268023123-586", info.Tags[0].TagId);
            Assert.AreEqual("green", info.Tags[0].Raw);

            // URLs

            Assert.AreEqual(1, info.Urls.Count);
            Assert.AreEqual("photopage", info.Urls[0].UrlType);
            Assert.AreEqual(new Uri("http://www.flickr.com/photos/samjudson/4268023123/"), info.Urls[0].Url);

        }

        [TestMethod]
        public void PhotosGetInfoUnauthenticatedTest()
        {
            Flickr f = TestData.GetInstance();

            PhotoInfo info = f.PhotosGetInfo("4268023123");

            Assert.IsNotNull(info);

            Assert.AreEqual("4268023123", info.PhotoId);
            Assert.AreEqual("a4283bac01", info.Secret);
            Assert.AreEqual("2795", info.Server);
            Assert.AreEqual("3", info.Farm);
            Assert.AreEqual(UtilityMethods.UnixTimestampToDate("1263291891"), info.DateUploaded);
            Assert.AreEqual(false, info.IsFavorite);
            Assert.AreEqual(LicenseType.AttributionNoncommercialShareAlikeCC, info.License);
            Assert.AreEqual(0, info.Rotation);
            Assert.AreEqual("9d3d4bf24a", info.OriginalSecret);
            Assert.AreEqual("jpg", info.OriginalFormat);
            Assert.IsTrue(info.ViewCount > 87, "ViewCount should be greater than 87.");
            Assert.AreEqual(MediaType.Photos, info.Media);

            Assert.AreEqual("12. Sudoku", info.Title);
            Assert.AreEqual("It scares me sometimes how much some of my handwriting reminds me of Dad's - in this photo there is one 5 that especially reminds me of his handwriting.", info.Description);

            //Owner
            Assert.AreEqual("41888973@N00", info.OwnerUserId);

            //Dates

            //Editability
            Assert.IsFalse(info.CanComment, "CanComment should be false when not authenticated.");
            Assert.IsFalse(info.CanAddMeta, "CanAddMeta should be false when not authenticated.");

            //Permissions
            Assert.IsNull(info.PermissionComment, "PermissionComment should be null when not authenticated.");
            Assert.IsNull(info.PermissionAddMeta, "PermissionAddMeta should be null when not authenticated.");

            //Visibility

            // Notes

            Assert.AreEqual(1, info.Notes.Count, "Notes.Count should be one.");
            Assert.AreEqual("72157623069944527", info.Notes[0].NoteId);
            Assert.AreEqual("41888973@N00", info.Notes[0].AuthorId);
            Assert.AreEqual("Sam Judson", info.Notes[0].AuthorName);
            Assert.AreEqual(267, info.Notes[0].XPosition);
            Assert.AreEqual(238, info.Notes[0].YPosition);

            // Tags

            Assert.AreEqual(5, info.Tags.Count);
            Assert.AreEqual("78188-4268023123-586", info.Tags[0].TagId);
            Assert.AreEqual("green", info.Tags[0].Raw);

            // URLs

            Assert.AreEqual(1, info.Urls.Count);
            Assert.AreEqual("photopage", info.Urls[0].UrlType);
            Assert.AreEqual(new Uri("http://www.flickr.com/photos/samjudson/4268023123/"), info.Urls[0].Url);

        }

        [TestMethod]
        public void PhotosGetInfoTestLocation()
        {
            string photoId = "4268756940";

            Flickr f = TestData.GetAuthInstance();

            PhotoInfo info = f.PhotosGetInfo(photoId);

            Assert.IsNotNull(info.Location);
        }


        [TestMethod]
        public void PhotosGetExifTest()
        {
            Flickr f = TestData.GetInstance();

            ExifTagCollection tags = f.PhotosGetExif("4268023123");

            Assert.IsNotNull(tags, "ExifTagCollection should not be null.");

            Assert.IsTrue(tags.Count > 20, "More than twenty parts of EXIF data should be returned.");

            Assert.AreEqual("System", tags[0].TagSpace, "First tags TagSpace is not set correctly.");
            Assert.AreEqual(0, tags[0].TagSpaceId, "First tags TagSpaceId is not set correctly.");
            Assert.AreEqual("FileName", tags[0].Tag, "First tags Tag is not set correctly.");
            Assert.AreEqual("FileName", tags[0].Label, "First tags Label is not set correctly.");
            Assert.AreEqual("ORI46620478284895704.img", tags[0].Raw, "First tags RAW is not correct.");
            Assert.IsNull(tags[0].Clean, "First tags Clean should be null.");
        }

    }
}
