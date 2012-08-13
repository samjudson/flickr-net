using System;
using FlickrNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FlickrNetTest
{
    /// <summary>
    ///This is a test class for FlickrNet.Utils and is intended
    ///to contain all FlickrNet.Utils Unit Tests
    ///</summary>
    [TestClass()]
    public class UtilityMethodsTests
    {
        private Dictionary<DateTime, string> timestampTests = new Dictionary<DateTime, string>()
            {
                { new DateTime(1970, 1, 1), "0" },
                { new DateTime(2011, 1, 1), "1293840000" },
                { new DateTime(2011,1, 1, 0, 20, 31), "1293841231" }
            };

        [TestMethod]
        public void CleanCollectionIdTest()
        {
            string orig = "78188-72157600072406095";
            string expected = "72157600072406095";

            string actual = UtilityMethods.CleanCollectionId(orig);
            Assert.AreEqual(expected, actual);

            actual = UtilityMethods.CleanCollectionId(expected);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void DateToUnixTimestampTests()
        {
            foreach (var pair in timestampTests)
            {
                var orig = pair.Key;
                var expected = pair.Value;
                var actual = UtilityMethods.DateToUnixTimestamp(orig);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void DateToMySqlTests()
        {
            var tests = new Dictionary<DateTime, string>()
            {
                { new DateTime(2009, 1, 12), "2009-01-12 00:00:00" },
                { new DateTime(2009, 7, 12), "2009-07-12 00:00:00" },
                { new DateTime(2009, 12, 12), "2009-12-12 00:00:00" }
            };

            foreach (var pair in tests)
            {
                var orig = pair.Key;
                var expected = pair.Value;
                var actual = UtilityMethods.DateToMySql(orig);
                Assert.AreEqual<string>(expected, actual, orig + " should have converted to " + expected);
            }
        }
        [TestMethod()]
        public void ExtrasToStringTestNoExtras()
        {
            PhotoSearchExtras extras = PhotoSearchExtras.None; // TODO: Initialize to an appropriate value

            string expected = String.Empty;
            string actual;

            actual = FlickrNet.UtilityMethods.ExtrasToString(extras);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.ExtrasToString did not return the expected value.");
        }

        [TestMethod()]
        public void ExtrasToStringTestTags()
        {
            PhotoSearchExtras extras = PhotoSearchExtras.Tags; // TODO: Initialize to an appropriate value

            string expected = "tags";
            string actual;

            actual = FlickrNet.UtilityMethods.ExtrasToString(extras);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.ExtrasToString did not return the expected value.");
        }

        [TestMethod()]
        public void ExtrasToStringTestMultiple()
        {
            PhotoSearchExtras extras = PhotoSearchExtras.Tags | PhotoSearchExtras.OriginalFormat; // TODO: Initialize to an appropriate value

            string expected = "original_format,tags";
            string actual;

            actual = FlickrNet.UtilityMethods.ExtrasToString(extras);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.ExtrasToString did not return the expected value.");
        }

        [TestMethod]
        public void MachineTagModeToStringTests()
        {
            Dictionary<MachineTagMode, string> test = new Dictionary<MachineTagMode, string>() { 
                { MachineTagMode.AllTags, "all" } ,
                { MachineTagMode.AnyTag, "any" },
                { MachineTagMode.None, "" },
                { (MachineTagMode)99, "" }
            };

            foreach (var pair in test)
            {
                var expected = pair.Value;
                var orig = pair.Key;

                var actual = UtilityMethods.MachineTagModeToString(orig);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var tests = new Dictionary<string, DateTime>()
            {
                { "2009-07-12", new DateTime(2009, 7, 12) },
                { "2009-12-12", new DateTime(2009, 12, 12) },
                { "2009-01-12 00:00:00", new DateTime(2009, 1, 12) }
            };

            foreach (var pair in tests)
            {
                var expected = pair.Value;
                var orig = pair.Key;

                var actual = UtilityMethods.MySqlToDate(orig);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void ParseDateWithGranularityOK()
        {
            string d = "2010-01-17 12:43:23";
            DateTime d2 = UtilityMethods.ParseDateWithGranularity(d);

            Assert.AreEqual(2010, d2.Year);
            Assert.AreEqual(1, d2.Month);
            Assert.AreEqual(17, d2.Day);
            Assert.AreEqual(12, d2.Hour);
            Assert.AreEqual(43, d2.Minute);
            Assert.AreEqual(23, d2.Second);
        }

        [TestMethod]
        public void ParseDateWithGranularityZeroMonth()
        {
            string d = "2010-00-01 00:00:00";
            DateTime d2 = UtilityMethods.ParseDateWithGranularity(d);

            Assert.AreEqual(2010, d2.Year);
            Assert.AreEqual(1, d2.Month);
            Assert.AreEqual(1, d2.Day);
            Assert.AreEqual(0, d2.Hour);
            Assert.AreEqual(0, d2.Minute);
            Assert.AreEqual(0, d2.Second);
        }

        [TestMethod]
        public void ParseIdToMemberTypeTests()
        {
            var tests = new Dictionary<string, MemberTypes>()
            {
                { "1", MemberTypes.Narwhal },
                { "2", MemberTypes.Member },
                { "3", MemberTypes.Moderator },
                { "4", MemberTypes.Admin },
                { "99", MemberTypes.None }
            };

            foreach (var pair in tests)
            {
                var orig = pair.Key;
                var expected = pair.Value;

                var actual = UtilityMethods.ParseIdToMemberType(orig);

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void MemberTypeToStringTests()
        {
            var tests = new Dictionary<MemberTypes, string>()
            {
                { MemberTypes.Admin, "4" },
                { MemberTypes.Member, "2" },
                { MemberTypes.Member | MemberTypes.Admin, "2,4" },
                { MemberTypes.Narwhal | MemberTypes.Member, "1,2" }
            };

            foreach (var pair in tests)
            {
                var orig = pair.Key;
                var expected = pair.Value;

                var actual = UtilityMethods.MemberTypeToString(orig);
                
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void SortOrderToStringPhotoSearchSortOrderTest()
        {
            var tests = new Dictionary<PhotoSearchSortOrder, string>()
            {
                { PhotoSearchSortOrder.DatePostedAscending, "date-posted-asc"},
                { PhotoSearchSortOrder.DatePostedDescending, "date-posted-desc"},
                { PhotoSearchSortOrder.DateTakenAscending, "date-taken-asc"},
                { PhotoSearchSortOrder.DateTakenDescending, "date-taken-desc"},
                { PhotoSearchSortOrder.InterestingnessAscending, "interestingness-asc"},
                { PhotoSearchSortOrder.InterestingnessDescending, "interestingness-desc"},
                { PhotoSearchSortOrder.Relevance, "relevance"},
                { PhotoSearchSortOrder.None, ""},
                { (PhotoSearchSortOrder)99, ""}
            };

            foreach (var pair in tests)
            {
                var orig = pair.Key;
                var expected = pair.Value;

                var actual = UtilityMethods.SortOrderToString(orig);

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void SortOrderToStringPopularitySortTest()
        {
            var tests = new Dictionary<PopularitySort, string>()
            {
                { PopularitySort.Comments, "comments"},
                { PopularitySort.Favorites, "favorites"},
                { PopularitySort.Views, "views"},
                { PopularitySort.None, ""},
                { (PopularitySort)99, ""}
            };

            foreach (var pair in tests)
            {
                var orig = pair.Key;
                var expected = pair.Value;

                var actual = UtilityMethods.SortOrderToString(orig);

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void TagModeToStringTests()
        {
            Dictionary<TagMode, string> test = new Dictionary<TagMode, string>() { 
                { TagMode.AllTags, "all" } ,
                { TagMode.AnyTag, "any" },
                { TagMode.Boolean, "bool" },
                { TagMode.None, "" },
                { (TagMode)99, "" }
            };

            foreach (var pair in test)
            {
                var expected = pair.Value;
                var orig = pair.Key;

                var actual = UtilityMethods.TagModeToString(orig);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void UnixTimestampToDateTests()
        {
            foreach (var pair in timestampTests)
            {
                var expected = pair.Key;
                var orig = pair.Value;
                var actual = UtilityMethods.UnixTimestampToDate(orig);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void UnixTimestampToDateInvalidStringTest()
        {
            string invalidTimestamp = "kjhkjh0987";
            DateTime expectedResult = DateTime.MinValue;
            DateTime actualResult = UtilityMethods.UnixTimestampToDate(invalidTimestamp);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void UrlEncodeTestEmpty()
        {
            string data = String.Empty;

            string expected = String.Empty;
            string actual;

            actual = FlickrNet.UtilityMethods.UrlEncode(data);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.UrlEncode did not return the expected value.");
        }

        [TestMethod()]
        public void UrlEncodeTestAmpersand()
        {
            string data = "A&B";

            string expected = "A%26B";
            string actual;

            actual = FlickrNet.UtilityMethods.UrlEncode(data);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.UrlEncode did not return the expected value.");
        }

        [TestMethod()]
        public void UrlEncodeTestPlus()
        {
            string data = "A+B";

            string expected = "A%2BB";
            string actual;

            actual = FlickrNet.UtilityMethods.UrlEncode(data);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.UrlEncode did not return the expected value.");
        }

        [TestMethod()]
        public void UrlEncodeTestSpace()
        {
            string data = "A B";

            string expected = "A%20B";
            string actual;

            actual = FlickrNet.UtilityMethods.UrlEncode(data);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.UrlEncode did not return the expected value.");
        }

        [TestMethod]
        public void UrlFormatTest()
        {
            var farm = "1";
            var server = "2";
            var photoid = "3";
            var secret = "4";
            var extension = "jpg";

            var sizeTests = new Dictionary<string, string>()
            {
                { "square", "http://farm1.staticflickr.com/2/3_4_s.jpg"},
                { "thumbnail", "http://farm1.staticflickr.com/2/3_4_t.jpg"},
                { "small", "http://farm1.staticflickr.com/2/3_4_m.jpg"},
                { "medium", "http://farm1.staticflickr.com/2/3_4.jpg"},
                { "large", "http://farm1.staticflickr.com/2/3_4_b.jpg"},
                { "original", "http://farm1.staticflickr.com/2/3_4_o.jpg"}
            };

            foreach(var pair in sizeTests)
            {
                var size = pair.Key;
                var expected = pair.Value;
                var actual = UtilityMethods.UrlFormat(farm, server, photoid, secret, size, extension);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void UrlFormatPhotoInfoTest()
        {
            var photoId = "7176125763"; // Rainbow rose
            var size = "medium";
            var extension = "jpg";
            var expected = "http://farm9.staticflickr.com/8162/7176125763_7eac68f450.jpg";

            var photoInfo = TestData.GetAuthInstance().PhotosGetInfo(photoId);

            var actual = UtilityMethods.UrlFormat(photoInfo, size, extension);

            Assert.AreEqual(expected, actual);


        }

        [TestMethod]
        public void UriCreationTest()
        {
            Uri u = new Uri("/Test", UriKind.Relative);

            Uri u2 = new Uri(new Uri("http://www.test.com"), u);

            Console.WriteLine(u2.AbsoluteUri);
        }

        [TestMethod]
        public void UtilityAuthToStringTest()
        {
            AuthLevel a = AuthLevel.Delete;
            var b = UtilityMethods.AuthLevelToString(a);
            Assert.AreEqual("delete", b);

            a = AuthLevel.Read;
            b = UtilityMethods.AuthLevelToString(a);
            Assert.AreEqual("read", b);

            a = AuthLevel.Write;
            b = UtilityMethods.AuthLevelToString(a);
            Assert.AreEqual("write", b);

            a = AuthLevel.None;
            b = UtilityMethods.AuthLevelToString(a);
            Assert.AreEqual("none", b);

            // Invalid auth level
            a = (AuthLevel)99;
            b = UtilityMethods.AuthLevelToString(a);
            Assert.AreEqual(String.Empty, b);
        }

    }
}
