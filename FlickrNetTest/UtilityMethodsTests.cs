using System;
using FlickrNet;
using NUnit.Framework;
using System.Collections.Generic;

namespace FlickrNetTest
{
    /// <summary>
    ///This is a test class for FlickrNet.Utils and is intended
    ///to contain all FlickrNet.Utils Unit Tests
    ///</summary>
    [TestFixture]
    public class UtilityMethodsTests : BaseTest
    {
        readonly Dictionary<DateTime, string> _timestampTests = new Dictionary<DateTime, string>
                                                                            {
                { new DateTime(1970, 1, 1), "0" },
                { new DateTime(2011, 1, 1), "1293840000" },
                { new DateTime(2011,1, 1, 0, 20, 31), "1293841231" }
            };

        [Test]
        public void CleanCollectionIdTest()
        {
            const string orig = "78188-72157600072406095";
            const string expected = "72157600072406095";

            var actual = UtilityMethods.CleanCollectionId(orig);
            Assert.AreEqual(expected, actual);

            actual = UtilityMethods.CleanCollectionId(expected);
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void DateToUnixTimestampTests()
        {
            foreach (var pair in _timestampTests)
            {
                var orig = pair.Key;
                var expected = pair.Value;
                var actual = UtilityMethods.DateToUnixTimestamp(orig);
                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        public void DateToMySqlTests()
        {
            var tests = new Dictionary<DateTime, string>
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
                Assert.AreEqual(expected, actual, orig + " should have converted to " + expected);
            }
        }
        [Test]
        public void ExtrasToStringTestNoExtras()
        {
            const PhotoSearchExtras extras = PhotoSearchExtras.None; 

            var expected = string.Empty;

            var actual = UtilityMethods.ExtrasToString(extras);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.ExtrasToString did not return the expected value.");
        }

        [Test]
        public void ExtrasToStringTestTags()
        {
            const PhotoSearchExtras extras = PhotoSearchExtras.Tags; 

            const string expected = "tags";

            var actual = UtilityMethods.ExtrasToString(extras);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.ExtrasToString did not return the expected value.");
        }

        [Test]
        public void ExtrasToStringTestMultiple()
        {
            const PhotoSearchExtras extras = PhotoSearchExtras.Tags | PhotoSearchExtras.OriginalFormat; 

            const string expected = "original_format,tags";

            var actual = UtilityMethods.ExtrasToString(extras);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.ExtrasToString did not return the expected value.");
        }

        [Test]
        public void ExtrasToStringTestAll()
        {
            const PhotoSearchExtras extras = PhotoSearchExtras.All;

            const string expected =
                "license,date_upload,date_taken,owner_name,icon_server,original_format,last_update,tags," +
                "geo,machine_tags,o_dims,views,media,path_alias,url_sq,url_t,url_s,url_m,url_l,url_o," +
                "description,usage,visibility,url_q,url_n,rotation,url_h,url_k,url_c,url_z,count_faves,count_comments";

            var actual = UtilityMethods.ExtrasToString(extras);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.ExtrasToString did not return the expected value.");
        }

        [Test]
        public void MachineTagModeToStringTests()
        {
            var test = new Dictionary<MachineTagMode, string>
                           { 
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

        [Test]
        public void MyTestMethod()
        {
            var tests = new Dictionary<string, DateTime>
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

        [Test]
        public void ParseDateWithGranularityOK()
        {
            const string d = "2010-01-17 12:43:23";
            var d2 = UtilityMethods.ParseDateWithGranularity(d);

            Assert.AreEqual(2010, d2.Year);
            Assert.AreEqual(1, d2.Month);
            Assert.AreEqual(17, d2.Day);
            Assert.AreEqual(12, d2.Hour);
            Assert.AreEqual(43, d2.Minute);
            Assert.AreEqual(23, d2.Second);
        }

        [Test]
        public void ParseDateWithGranularityZeroMonth()
        {
            const string d = "2010-00-01 00:00:00";
            DateTime d2 = UtilityMethods.ParseDateWithGranularity(d);

            Assert.AreEqual(2010, d2.Year);
            Assert.AreEqual(1, d2.Month);
            Assert.AreEqual(1, d2.Day);
            Assert.AreEqual(0, d2.Hour);
            Assert.AreEqual(0, d2.Minute);
            Assert.AreEqual(0, d2.Second);
        }

        [Test]
        public void ParseIdToMemberTypeTests()
        {
            var tests = new Dictionary<string, MemberTypes>
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

        [Test]
        public void MemberTypeToStringTests()
        {
            var tests = new Dictionary<MemberTypes, string>
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

        [Test]
        public void SortOrderToStringPhotoSearchSortOrderTest()
        {
            var tests = new Dictionary<PhotoSearchSortOrder, string>
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

        [Test]
        public void SortOrderToStringPopularitySortTest()
        {
            var tests = new Dictionary<PopularitySort, string>
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

        [Test]
        public void TagModeToStringTests()
        {
            var test = new Dictionary<TagMode, string>
                           { 
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

        [Test]
        public void UnixTimestampToDateTests()
        {
            foreach (var pair in _timestampTests)
            {
                var expected = pair.Key;
                var orig = pair.Value;
                var actual = UtilityMethods.UnixTimestampToDate(orig);
                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        public void UnixTimestampToDateInvalidStringTest()
        {
            const string invalidTimestamp = "kjhkjh0987";
            DateTime expectedResult = DateTime.MinValue;
            DateTime actualResult = UtilityMethods.UnixTimestampToDate(invalidTimestamp);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void UrlEncodeTestEmpty()
        {
            string data = string.Empty;

            string expected = string.Empty;
            string actual;

            actual = UtilityMethods.UrlEncode(data);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.UrlEncode did not return the expected value.");
        }

        [Test]
        public void UrlEncodeTestAmpersand()
        {
            string data = "A&B";

            string expected = "A%26B";
            string actual;

            actual = UtilityMethods.UrlEncode(data);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.UrlEncode did not return the expected value.");
        }

        [Test]
        public void UrlEncodeTestPlus()
        {
            string data = "A+B";

            string expected = "A%2BB";
            string actual;

            actual = UtilityMethods.UrlEncode(data);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.UrlEncode did not return the expected value.");
        }

        [Test]
        public void UrlEncodeTestSpace()
        {
            string data = "A B";

            string expected = "A%20B";
            string actual;

            actual = UtilityMethods.UrlEncode(data);

            Assert.AreEqual(expected, actual, "FlickrNet.Utils.UrlEncode did not return the expected value.");
        }

        [Test]
        public void UrlFormatTest()
        {
            var farm = "1";
            var server = "2";
            var photoid = "3";
            var secret = "4";
            var extension = "jpg";

            var sizeTests = new Dictionary<string, string>
                                {
                { "square", "https://farm1.staticflickr.com/2/3_4_s.jpg"},
                { "thumbnail", "https://farm1.staticflickr.com/2/3_4_t.jpg"},
                { "small", "https://farm1.staticflickr.com/2/3_4_m.jpg"},
                { "medium", "https://farm1.staticflickr.com/2/3_4.jpg"},
                { "large", "https://farm1.staticflickr.com/2/3_4_b.jpg"},
                { "original", "https://farm1.staticflickr.com/2/3_4_o.jpg"}
            };

            foreach(var pair in sizeTests)
            {
                var size = pair.Key;
                var expected = pair.Value;
                var actual = UtilityMethods.UrlFormat(farm, server, photoid, secret, size, extension);
                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void UrlFormatPhotoInfoTest()
        {
            var photoId = "7176125763"; // Rainbow rose
            var size = "medium";
            var extension = "jpg";
            var expected = "https://farm9.staticflickr.com/8162/7176125763_7eac68f450.jpg";

            var photoInfo = AuthInstance.PhotosGetInfo(photoId);

            var actual = UtilityMethods.UrlFormat(photoInfo, size, extension);

            Assert.AreEqual(expected, actual);


        }

        [Test]
        public void UriCreationTest()
        {
            var u = new Uri("/Test", UriKind.Relative);

            var u2 = new Uri(new Uri("http://www.test.com"), u);

            Console.WriteLine(u2.AbsoluteUri);
        }

        [Test]
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
            Assert.AreEqual(string.Empty, b);
        }

        [Test]
        public void StylesToString_ParameterIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => UtilityMethods.StylesToString(null));
        }

        [Test]
        public void StyleToString_ParameterIsEmpty_ReturnsEmptyString()
        {
            Assert.AreEqual(UtilityMethods.StylesToString(new List<Style>()), string.Empty);
        }

        [TestCase(Style.BlackAndWhite, "blackandwhite")]
        [TestCase(Style.DepthOfField, "depthoffield")]
        [TestCase(Style.Minimalism, "minimalism")]
        [TestCase(Style.Pattern, "pattern")]
        public void StylesToString_ConvertsSingletonCollectionIntoString(Style style, string excepted)
        {
            Assert.AreEqual(UtilityMethods.StylesToString(new[] { style }), excepted);
        }

        [TestCase("blackandwhite,depthoffield", Style.BlackAndWhite, Style.DepthOfField)]
        [TestCase("depthoffield,minimalism,pattern", Style.DepthOfField, Style.Minimalism, Style.Pattern)]
        [TestCase("minimalism,pattern,depthoffield,blackandwhite", Style.Minimalism, Style.Pattern, Style.DepthOfField, Style.BlackAndWhite)]
        public void StylesToString_SeparatesValuesByComma(string expected, params Style[] styles)
        {
            Assert.AreEqual(UtilityMethods.StylesToString(styles), expected);
        }

        [TestCase("blackandwhite", Style.BlackAndWhite, Style.BlackAndWhite)]
        [TestCase("blackandwhite,minimalism", Style.BlackAndWhite, Style.BlackAndWhite, Style.Minimalism, Style.Minimalism)]
        [TestCase("blackandwhite,pattern,minimalism", Style.BlackAndWhite, Style.BlackAndWhite, Style.Pattern, Style.Minimalism, Style.Minimalism)]
        public void StylesToString_FiltersOutRecurrences(string expected, params Style[] styles)
        {
            Assert.AreEqual(UtilityMethods.StylesToString(styles), expected);
        }
    }
}
