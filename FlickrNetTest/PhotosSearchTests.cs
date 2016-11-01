// ReSharper disable SuggestUseVarKeywordEvident
using System.Linq;
using NUnit.Framework;
using System;
using System.Text;
using FlickrNet;
using System.Collections.Generic;
using Shouldly;

namespace FlickrNetTest
{
    [TestFixture]
    public class PhotosSearchTests : BaseTest
    {
        [Test]
        public void PhotosSearchBasicSearch()
        {
            var o = new PhotoSearchOptions {Tags = "Test"};
            var photos = Instance.PhotosSearch(o);

            Assert.IsTrue(photos.Total > 0, "Total Photos should be greater than zero.");
            Assert.IsTrue(photos.Pages > 0, "Pages should be greaters than zero.");
            Assert.AreEqual(100, photos.PerPage, "PhotosPerPage should be 100.");
            Assert.AreEqual(1, photos.Page, "Page should be 1.");

            Assert.IsTrue(photos.Count > 0, "Photos.Count should be greater than 0.");
            Assert.AreEqual(photos.PerPage, photos.Count);
        }

        [Test]
        public void PhotosSearchSignedTest()
        {
            Flickr f = TestData.GetSignedInstance();
            var o = new PhotoSearchOptions {Tags = "Test", PerPage = 5};
            PhotoCollection photos = f.PhotosSearch(o);

            Assert.AreEqual(5, photos.PerPage, "PerPage should equal 5.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosSearchFavorites()
        {
            var o = new PhotoSearchOptions {UserId = "me", Faves = true, Tags = "cat"};

            PhotoCollection p = AuthInstance.PhotosSearch(o);

            Assert.IsTrue(p.Count > 5, "Should have returned more than 5 result returned. Count = " + p.Count);
            Assert.IsTrue(p.Count < 100, "Should be less than 100 results returned. Count = " + p.Count);
        }

        [Test, Ignore("Currently 'Camera' searches are not working.")]
        [Category("AccessTokenRequired")]
        public void PhotosSearchCameraIphone()
        {
            var o = new PhotoSearchOptions
                                       {
                                           Camera = "iPhone 5S",
                                           MinUploadDate = DateTime.Now.AddDays(-7),
                                           MaxUploadDate = DateTime.Now,
                                           Extras = PhotoSearchExtras.Tags
                                       };

            var ps = AuthInstance.PhotosSearch(o);

            Assert.IsNotNull(ps);
            Assert.AreNotEqual(0, ps.Count);
        }

        [Test]
        public void PhotoSearchByPathAlias()
        {
            var o = new PhotoSearchOptions
            {
                GroupPathAlias = "api",
                PerPage = 10
            };

            var ps = Instance.PhotosSearch(o);

            Assert.IsNotNull(ps);
            Assert.AreNotEqual(0, ps.Count);
        }

        [Test]
        public void PhotosSearchPerPage()
        {
            var o = new PhotoSearchOptions {PerPage = 10, Tags = "Test"};
            var photos = Instance.PhotosSearch(o);

            Assert.IsTrue(photos.Total > 0, "TotalPhotos should be greater than 0.");
            Assert.IsTrue(photos.Pages > 0, "TotalPages should be greater than 0.");
            Assert.AreEqual(10, photos.PerPage, "PhotosPerPage should be 10.");
            Assert.AreEqual(1, photos.Page, "PageNumber should be 1.");
            Assert.AreEqual(10, photos.Count, "Count should be 10.");
            Assert.AreEqual(photos.PerPage, photos.Count);
        }

        [Test]
        public void PhotosSearchUserIdTest()
        {
            var o = new PhotoSearchOptions {UserId = TestData.TestUserId};

            PhotoCollection photos = Instance.PhotosSearch(o);

            foreach (Photo photo in photos)
            {
                Assert.AreEqual(TestData.TestUserId, photo.UserId);
            }
        }

        [Test]
        public void PhotosSearchNoApiKey()
        {
            Instance.ApiKey = "";
            Should.Throw<ApiKeyRequiredException>(() => Instance.PhotosSearch(new PhotoSearchOptions()));
        }

        [Test]
        public void GetOauthRequestTokenNoApiKey()
        {
            Instance.ApiKey = "";
            Should.Throw<ApiKeyRequiredException>(() => Instance.OAuthGetRequestToken("oob"));
        }

        [Test]
        [Ignore("Flickr still doesn't seem to sort correctly by date posted.")]
        public void PhotosSearchSortDateTakenAscending()
        {
            var o = new PhotoSearchOptions
                        {
                            Tags = "microsoft",
                            SortOrder = PhotoSearchSortOrder.DateTakenAscending,
                            Extras = PhotoSearchExtras.DateTaken
                        };

            var p = Instance.PhotosSearch(o);

            for (var i = 1; i < p.Count; i++)
            {
                Assert.AreNotEqual(default(DateTime), p[i].DateTaken);
                Assert.IsTrue(p[i].DateTaken >= p[i - 1].DateTaken, "Date taken should increase");
            }
        }

        [Test]
        [Ignore("Flickr still doesn't seem to sort correctly by date posted.")]
        public void PhotosSearchSortDateTakenDescending()
        {
            var o = new PhotoSearchOptions
                        {
                            Tags = "microsoft",
                            SortOrder = PhotoSearchSortOrder.DateTakenDescending,
                            Extras = PhotoSearchExtras.DateTaken
                        };

            var p = Instance.PhotosSearch(o);

            for (var i = 1; i < p.Count; i++)
            {
                Assert.AreNotEqual(default(DateTime), p[i].DateTaken);
                Assert.IsTrue(p[i].DateTaken <= p[i - 1].DateTaken, "Date taken should decrease.");
            }
        }

        [Test]
        [Ignore("Flickr still doesn't seem to sort correctly by date posted.")]
        public void PhotosSearchSortDatePostedAscending()
        {
            var o = new PhotoSearchOptions
                        {
                            Tags = "microsoft",
                            SortOrder = PhotoSearchSortOrder.DatePostedAscending,
                            Extras = PhotoSearchExtras.DateUploaded
                        };

            var p = Instance.PhotosSearch(o);

            for (var i = 1; i < p.Count; i++)
            {
                Assert.AreNotEqual(default(DateTime), p[i].DateUploaded);
                Assert.IsTrue(p[i].DateUploaded >= p[i - 1].DateUploaded, "Date taken should increase.");
            }
        }

        [Test]
        [Ignore("Flickr still doesn't seem to sort correctly by date posted.")]
        public void PhotosSearchSortDataPostedDescending()
        {
            var o = new PhotoSearchOptions
                        {
                            Tags = "microsoft",
                            SortOrder = PhotoSearchSortOrder.DatePostedDescending,
                            Extras = PhotoSearchExtras.DateUploaded
                        };

            var p = Instance.PhotosSearch(o);

            for (int i = 1; i < p.Count; i++)
            {
                Assert.AreNotEqual(default(DateTime), p[i].DateUploaded);
                Assert.IsTrue(p[i].DateUploaded <= p[i - 1].DateUploaded, "Date taken should increase.");
            }
        }

        [Test]
        public void PhotosSearchGetLicenseNotNull()
        {
            var o = new PhotoSearchOptions
                                       {
                                           Tags = "microsoft",
                                           SortOrder = PhotoSearchSortOrder.DatePostedDescending,
                                           Extras = PhotoSearchExtras.License
                                       };

            PhotoCollection photos = Instance.PhotosSearch(o);

            foreach (Photo photo in photos)
            {
                Assert.IsNotNull(photo.License);
            }
        }

        [Test]
        public void PhotosSearchGetLicenseAttributionNoDerivs()
        {
            var o = new PhotoSearchOptions
                                       {
                                           Tags = "microsoft",
                                           SortOrder = PhotoSearchSortOrder.DatePostedDescending
                                       };
            o.Licenses.Add(LicenseType.AttributionNoDerivativesCC);
            o.Extras = PhotoSearchExtras.License;

            PhotoCollection photos = Instance.PhotosSearch(o);

            foreach (Photo photo in photos)
            {
                Assert.AreEqual(LicenseType.AttributionNoDerivativesCC, photo.License);
            }
        }

        [Test]
        public void PhotosSearchGetMultipleLicenses()
        {
            var o = new PhotoSearchOptions
                                       {
                                           Tags = "microsoft",
                                           PerPage = 500,
                                           SortOrder = PhotoSearchSortOrder.DatePostedDescending
                                       };
            o.Licenses.Add(LicenseType.AttributionNoDerivativesCC);
            o.Licenses.Add(LicenseType.AttributionNoncommercialNoDerivativesCC);
            o.Extras = PhotoSearchExtras.License | PhotoSearchExtras.OwnerName;

            PhotoCollection photos = Instance.PhotosSearch(o);

            foreach (Photo photo in photos)
            {
                if (photo.License != LicenseType.AttributionNoDerivativesCC &&
                    photo.License != LicenseType.AttributionNoncommercialNoDerivativesCC)
                {
                    Assert.Fail("License not one of selected licenses: " + photo.License.ToString());
                }
            }
        }

        [Test]
        public void PhotosSearchGetLicenseNoKnownCopright()
        {
            var o = new PhotoSearchOptions
                                       {
                                           Tags = "microsoft",
                                           SortOrder = PhotoSearchSortOrder.DatePostedDescending
                                       };
            o.Licenses.Add(LicenseType.NoKnownCopyrightRestrictions);
            o.Extras = PhotoSearchExtras.License;

            PhotoCollection photos = Instance.PhotosSearch(o);

            foreach (Photo photo in photos)
            {
                Assert.AreEqual(LicenseType.NoKnownCopyrightRestrictions, photo.License);
            }
        }

        [Test]
        public void PhotosSearchSearchTwice()
        {
            var o = new PhotoSearchOptions {Tags = "microsoft", PerPage = 10};

            PhotoCollection photos = Instance.PhotosSearch(o);

            Assert.AreEqual(10, photos.PerPage, "Per page is not 10");

            o.PerPage = 50;
            photos = Instance.PhotosSearch(o);
            Assert.AreEqual(50, photos.PerPage, "Per page has not changed?");

            photos = Instance.PhotosSearch(o);
            Assert.AreEqual(50, photos.PerPage, "Per page has changed!");
        }

        [Test]
        public void PhotosSearchPageTest()
        {
            var o = new PhotoSearchOptions {Tags = "colorful", PerPage = 10, Page = 3};

            PhotoCollection photos = Instance.PhotosSearch(o);

            Assert.AreEqual(3, photos.Page);
        }

        [Test]
        public void PhotosSearchByColorCode()
        {
            var o = new PhotoSearchOptions
                    {
                        ColorCodes = new List<string> { "orange" },
                        Tags = "colorful"
                    };

            var photos = Instance.PhotosSearch(o);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Count should not be zero.");

            foreach (var photo in photos)
            {
                Console.WriteLine(photo.WebUrl);
            }
        }

        [TestCase(Style.BlackAndWhite)]
        [TestCase(Style.DepthOfField)]
        [TestCase(Style.Minimalism)]
        [TestCase(Style.Pattern)]
        public void PhotoSearchByStyles(Style style)
        {
            var o = new PhotoSearchOptions
            {
                Text = "nature",
                Page = 1,
                PerPage = 10,
                Styles = new[] { style }
            };

            var photos = Instance.PhotosSearch(o);

            Assert.IsNotNull(photos);
            Assert.IsNotEmpty(photos);
        }

        [Test]
        public void PhotosSearchIsCommons()
        {
            var o = new PhotoSearchOptions
                                       {
                                           IsCommons = true,
                                           Tags = "newyork",
                                           PerPage = 10,
                                           Extras = PhotoSearchExtras.License
                                       };

            PhotoCollection photos = Instance.PhotosSearch(o);

            foreach (Photo photo in photos)
            {
                Assert.AreEqual(LicenseType.NoKnownCopyrightRestrictions, photo.License);
            }
        }

        [Test]
        public void PhotosSearchDateTakenGranualityTest()
        {
            var o = new PhotoSearchOptions
                                       {
                                           UserId = "8748614@N05",
                                           Tags = "primavera",
                                           PerPage = 500,
                                           Extras = PhotoSearchExtras.DateTaken
                                       };

            Instance.PhotosSearch(o);
        }

        [Test]
        public void PhotosSearchDetailedTest()
        {
            var o = new PhotoSearchOptions
                                       {
                                           Tags = "applestore",
                                           UserId = "41888973@N00",
                                           Extras = PhotoSearchExtras.All
                                       };
            PhotoCollection photos = Instance.PhotosSearch(o);

            Assert.AreEqual(100, photos.PerPage);
            Assert.AreEqual(1, photos.Page);

            Assert.AreNotEqual(0, photos.Count, "PhotoCollection.Count should not be zero.");

            Assert.AreEqual("3547139066", photos[0].PhotoId);
            Assert.AreEqual("41888973@N00", photos[0].UserId);
            Assert.AreEqual("bf4e11b1cb", photos[0].Secret);
            Assert.AreEqual("3311", photos[0].Server);
            Assert.AreEqual("Apple Store!", photos[0].Title);
            Assert.AreEqual("4", photos[0].Farm);
            Assert.AreEqual(false, photos[0].IsFamily);
            Assert.AreEqual(true, photos[0].IsPublic);
            Assert.AreEqual(false, photos[0].IsFriend);

            var dateTaken = new DateTime(2009, 5, 18, 4, 21, 46);
            var dateUploaded = new DateTime(2009, 5, 19, 21, 21, 46);
            Assert.IsTrue(photos[0].LastUpdated > dateTaken, "Last updated date was not correct.");
            Assert.AreEqual(dateTaken, photos[0].DateTaken, "Date taken date was not correct.");
            Assert.AreEqual(dateUploaded, photos[0].DateUploaded, "Date uploaded date was not correct.");

            Assert.AreEqual("jpg", photos[0].OriginalFormat, "OriginalFormat should be JPG");
            Assert.AreEqual("JjXZOYpUV7IbeGVOUQ", photos[0].PlaceId, "PlaceID not set correctly.");

            Assert.IsNotNull(photos[0].Description, "Description should not be null.");

            foreach (Photo photo in photos)
            {
                Assert.IsNotNull(photo.PhotoId);
                Assert.IsTrue(photo.IsPublic);
                Assert.IsFalse(photo.IsFamily);
                Assert.IsFalse(photo.IsFriend);
            }
        }

        [Test]
        public void PhotosSearchTagsTest()
        {
            var o = new PhotoSearchOptions {PerPage = 10, Tags = "test", Extras = PhotoSearchExtras.Tags};

            PhotoCollection photos = Instance.PhotosSearch(o);

            photos.Total.ShouldBeGreaterThan(0);
            photos.Pages.ShouldBeGreaterThan(0);
            photos.PerPage.ShouldBe(10);
            photos.Page.ShouldBe(1);
            photos.Count.ShouldBeInRange(9, 10, "Ideally should be 10, but sometimes returns 9");

            foreach (Photo photo in photos)
            {
                Assert.IsTrue(photo.Tags.Count > 0, "Should be some tags");
                Assert.IsTrue(photo.Tags.Contains("test"), "At least one should be 'test'");
            }
        }

        // Flickr sometimes returns different totals for the same search when a different perPage value is used.
        // As I have no control over this, and I am correctly setting the properties as returned I am ignoring this test.
        [Test]
        [Ignore("Flickr often returns different totals than requested.")]
        public void PhotosSearchPerPageMultipleTest()
        {
            var o = new PhotoSearchOptions {Tags = "microsoft"};
            o.Licenses.Add(LicenseType.AttributionCC);
            o.Licenses.Add(LicenseType.AttributionNoDerivativesCC);
            o.Licenses.Add(LicenseType.AttributionNoncommercialCC);
            o.Licenses.Add(LicenseType.AttributionNoncommercialNoDerivativesCC);
            o.Licenses.Add(LicenseType.AttributionNoncommercialShareAlikeCC);
            o.Licenses.Add(LicenseType.AttributionShareAlikeCC);

            o.MinUploadDate = DateTime.Today.AddDays(-4);
            o.MaxUploadDate = DateTime.Today.AddDays(-2);

            o.PerPage = 1;

            PhotoCollection photos = Instance.PhotosSearch(o);

            int totalPhotos1 = photos.Total;

            o.PerPage = 10;

            photos = Instance.PhotosSearch(o);

            int totalPhotos2 = photos.Total;

            o.PerPage = 100;

            photos = Instance.PhotosSearch(o);

            int totalPhotos3 = photos.Total;

            Assert.AreEqual(totalPhotos1, totalPhotos2, "Total Photos 1 & 2 should be equal");
            Assert.AreEqual(totalPhotos2, totalPhotos3, "Total Photos 2 & 3 should be equal");
        }

        [Test]
        public void PhotosSearchPhotoSearchBoundaryBox()
        {
            var b = new BoundaryBox(103.675997, 1.339811, 103.689456, 1.357764, GeoAccuracy.World);
            var o = new PhotoSearchOptions
                        {
                            HasGeo = true,
                            BoundaryBox = b,
                            Accuracy = b.Accuracy,
                            MinUploadDate = DateTime.Now.AddYears(-1),
                            MaxUploadDate = DateTime.Now,
                            Extras = PhotoSearchExtras.Geo | PhotoSearchExtras.PathAlias,
                            Tags = "colorful"
                        };

            var ps = Instance.PhotosSearch(o);

            foreach (var p in ps)
            {
                // Annoying, but sometimes Flickr doesn't return the geo properties for a photo even for this type of search.
                if (Math.Abs(p.Latitude - 0) < 1e-8 && Math.Abs(p.Longitude - 0) < 1e-8) continue;

                Assert.IsTrue(p.Latitude > b.MinimumLatitude && b.MaximumLatitude > p.Latitude,
                              "Latitude is not within the boundary box. {0} outside {1} and {2} for photo {3}", p.Latitude, b.MinimumLatitude, b.MaximumLatitude, p.WebUrl);
                Assert.IsTrue(p.Longitude > b.MinimumLongitude && b.MaximumLongitude > p.Longitude,
                              "Longitude is not within the boundary box. {0} outside {1} and {2} for photo {3}", p.Longitude, b.MinimumLongitude, b.MaximumLongitude, p.WebUrl);
            }
        }

        [Test]
        public void PhotosSearchLatCultureTest()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nb-NO");

            var o = new PhotoSearchOptions {HasGeo = true};
            o.Extras |= PhotoSearchExtras.Geo;
            o.Tags = "colorful";
            o.TagMode = TagMode.AllTags;
            o.PerPage = 10;

            Instance.PhotosSearch(o);
        }


        [Test]
        public void PhotosSearchTagCollectionTest()
        {
            var o = new PhotoSearchOptions
                                       {
                                           UserId = TestData.TestUserId,
                                           PerPage = 10,
                                           Extras = PhotoSearchExtras.Tags
                                       };

            PhotoCollection photos = Instance.PhotosSearch(o);

            foreach (Photo p in photos)
            {
                Assert.IsNotNull(p.Tags, "Tag Collection should not be null");
                Assert.IsTrue(p.Tags.Count > 0, "Should be more than one tag for all photos");
                Assert.IsNotNull(p.Tags[0]);
            }
        }

        [Test]
        public void PhotosSearchMultipleTagsTest()
        {
            var o = new PhotoSearchOptions();
            o.Tags = "art,collection";
            o.TagMode = TagMode.AllTags;
            o.PerPage = 10;
            o.Extras = PhotoSearchExtras.Tags;

            PhotoCollection photos = Instance.PhotosSearch(o);

            foreach (Photo p in photos)
            {
                Assert.IsNotNull(p.Tags, "Tag Collection should not be null");
                Assert.IsTrue(p.Tags.Count > 0, "Should be more than one tag for all photos");
                Assert.IsTrue(p.Tags.Contains("art"), "Should contain 'art' tag.");
                Assert.IsTrue(p.Tags.Contains("collection"), "Should contain 'collection' tag.");
            }
        }

        [Test]
        public void PhotosSearchInterestingnessBasicTest()
        {
            var o = new PhotoSearchOptions
                        {
                            SortOrder = PhotoSearchSortOrder.InterestingnessDescending,
                            Tags = "colorful",
                            PerPage = 500
                        };

            var ps = Instance.PhotosSearch(o);

            Assert.IsNotNull(ps, "Photos should not be null");
            Assert.AreEqual(500, ps.PerPage, "PhotosPerPage should be 500");
            Assert.AreNotEqual(0, ps.Count, "Count should be greater than zero.");
        }

        [Test]
        public void PhotosSearchGroupIdTest()
        {
            var o = new PhotoSearchOptions();
            o.GroupId = TestData.GroupId;
            o.PerPage = 10;

            var photos = Instance.PhotosSearch(o);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count);

            foreach (var photo in photos)
            {
                Assert.IsNotNull(photo.PhotoId);
            }
        }

        [Test]
        public void PhotosSearchGeoContext()
        {
            var o = new PhotoSearchOptions
                        {
                            HasGeo = true,
                            GeoContext = GeoContext.Outdoors,
                            Tags = "landscape"
                        };

            o.Extras |= PhotoSearchExtras.Geo;

            var col = Instance.PhotosSearch(o);

            foreach (var p in col)
            {
                Assert.AreEqual(GeoContext.Outdoors, p.GeoContext);
            }
        }

        [Test]
        public void PhotosSearchLatLongGeoRadiusTest()
        {
            var o = new PhotoSearchOptions();
            o.HasGeo = true;
            o.MinTakenDate = DateTime.Today.AddYears(-3);
            o.PerPage = 10;
            o.Latitude = 61.600447;
            o.Longitude = 5.035064;
            o.Radius = 4.75f;
            o.RadiusUnits = RadiusUnit.Kilometers;
            o.Extras |= PhotoSearchExtras.Geo;

            var photos = Instance.PhotosSearch(o);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "No photos returned by search.");

            foreach (var photo in photos)
            {
                Assert.IsNotNull(photo.PhotoId);
                Assert.AreNotEqual(0, photo.Longitude, "Longitude should not be zero.");
                Assert.AreNotEqual(0, photo.Latitude, "Latitude should not be zero.");
            }
        }

        [Test]
        public void PhotosSearchLargeRadiusTest()
        {
            const double lat = 61.600447;
            const double lon = 5.035064;

            var o = new PhotoSearchOptions
                        {
                            PerPage = 100,
                            HasGeo = true,
                            MinTakenDate = DateTime.Today.AddYears(-3),
                            Latitude = lat,
                            Longitude = lon,
                            Radius = 5.432123456f,
                            RadiusUnits = RadiusUnit.Kilometers
                        };
            o.Extras |= PhotoSearchExtras.Geo;

            var photos = Instance.PhotosSearch(o);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "No photos returned by search.");

            foreach (var photo in photos)
            {
                Assert.IsNotNull(photo.PhotoId);
                Assert.AreNotEqual(0, photo.Longitude, "Longitude should not be zero.");
                Assert.AreNotEqual(0, photo.Latitude, "Latitude should not be zero.");

                LogOnError("Photo ID " + photo.PhotoId,
                           string.Format("Lat={0}, Long={1}", photo.Latitude, photo.Longitude));

                // Note: +/-1 is not an exact match to 5.4km, but anything outside of these bounds is definitely wrong.
                Assert.IsTrue(photo.Latitude > lat - 1 && photo.Latitude < lat + 1,
                              "Latitude not within acceptable range.");
                Assert.IsTrue(photo.Longitude > lon - 1 && photo.Longitude < lon + 1,
                              "Latitude not within acceptable range.");

            }
        }

        [Test]
        public void PhotosSearchFullParamTest()
        {
            var o = new PhotoSearchOptions
                        {
                            UserId = TestData.TestUserId,
                            Tags = "microsoft",
                            TagMode = TagMode.AllTags,
                            Text = "microsoft",
                            MachineTagMode = MachineTagMode.AllTags,
                            MachineTags = "dc:author=*",
                            MinTakenDate = DateTime.Today.AddYears(-1),
                            MaxTakenDate = DateTime.Today,
                            PrivacyFilter = PrivacyFilter.PublicPhotos,
                            SafeSearch = SafetyLevel.Safe,
                            ContentType = ContentTypeSearch.PhotosOnly,
                            HasGeo = false,
                            WoeId = "30079",
                            PlaceId = "X9sTR3BSUrqorQ"
                        };

            var photos = Instance.PhotosSearch(o);

            Assert.IsNotNull(photos);
            Assert.AreEqual(0, photos.Count);

        }

        [Test, Ignore("Not currently working for some reason.")]
        public void PhotosSearchGalleryPhotos()
        {
            var o = new PhotoSearchOptions {UserId = TestData.TestUserId, InGallery = true, Tags = "art"};
            var photos = Instance.PhotosSearch(o);

            Assert.AreEqual(1, photos.Count, "Only one photo should have been returned.");
        }

        [Test]
        public void PhotosSearchUrlLimitTest()
        {
            var o = new PhotoSearchOptions {Extras = PhotoSearchExtras.All, TagMode = TagMode.AnyTag};
            var sb = new StringBuilder();
            for (var i = 1; i < 200; i++) sb.Append("tagnumber" + i);
            o.Tags = sb.ToString();

            Instance.PhotosSearch(o);
        }

        [Test]
        public void PhotosSearchRussianCharacters()
        {
            var o = new PhotoSearchOptions();
            o.Tags = "снег";

            var photos = Instance.PhotosSearch(o);

            Assert.AreNotEqual(0, photos.Count, "Search should return some results.");
        }

        [Test]
        public void PhotosSearchRussianTagsReturned()
        {
            var o = new PhotoSearchOptions { PerPage = 200, Extras = PhotoSearchExtras.Tags, Tags = "фото" };

            var photos = Instance.PhotosSearch(o);

            photos.Count.ShouldNotBe(0);
            photos.ShouldContain(p => p.Tags.Any(t => t == "фото"));
        }

        [Test]
        public void PhotosSearchRussianTextReturned()
        {
            const string russian = "фото";

            var o = new PhotoSearchOptions { PerPage = 200, Extras = PhotoSearchExtras.Tags | PhotoSearchExtras.Description, Text = russian };

            var photos = Instance.PhotosSearch(o);

            photos.Count.ShouldNotBe(0);
            photos.ShouldContain(p => p.Tags.Any(t => t == russian) || p.Title.Contains(russian) || p.Description.Contains(russian));
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosSearchAuthRussianCharacters()
        {
            var o = new PhotoSearchOptions();
            o.Tags = "снег";

            var photos = AuthInstance.PhotosSearch(o);

            Assert.AreNotEqual(0, photos.Count, "Search should return some results.");
        }

        [Test]
        public void PhotosSearchRotation()
        {
            var o = new PhotoSearchOptions
                                       {
                                           Extras = PhotoSearchExtras.Rotation,
                                           UserId = TestData.TestUserId,
                                           PerPage = 100
                                       };
            var photos = Instance.PhotosSearch(o);
            foreach (var photo in photos)
            {
                Assert.IsTrue(photo.Rotation.HasValue, "Rotation should be set.");
                if (photo.PhotoId == "6861439677")
                    Assert.AreEqual(90, photo.Rotation, "Rotation should be 90 for this photo.");
                if (photo.PhotoId == "6790104907")
                    Assert.AreEqual(0, photo.Rotation, "Rotation should be 0 for this photo.");
            }
        }

        [Test]
        public void PhotosSearchLarge1600ImageSize()
        {
            var o = new PhotoSearchOptions
                        {
                            Extras = PhotoSearchExtras.AllUrls,
                            Tags = "colorful",
                            MinUploadDate = DateTime.UtcNow.AddDays(-1)
                        };

            var photos = Instance.PhotosSearch(o);

            Assert.IsNotNull(photos, "PhotosSearch should not return a null instance.");
            Assert.IsTrue(photos.Any(), "PhotoSearch should have returned some photos.");
            Assert.IsTrue(
                photos.Any(
                    p =>
                    !string.IsNullOrEmpty(p.Large1600Url) && p.Large1600Height.HasValue && p.Large1600Width.HasValue),
                "At least one photo should have a large1600 image url and height and width.");
        }

        [Test]
        public void PhotosSearchLarge2048ImageSize()
        {
            var o = new PhotoSearchOptions
                        {
                            Extras = PhotoSearchExtras.Large2048Url,
                            Tags = "colorful",
                            MinUploadDate = DateTime.UtcNow.AddDays(-1)
                        };

            var photos = Instance.PhotosSearch(o);

            Assert.IsNotNull(photos, "PhotosSearch should not return a null instance.");
            Assert.IsTrue(photos.Any(), "PhotoSearch should have returned some photos.");
            Assert.IsTrue(
                photos.Any(
                    p =>
                    !string.IsNullOrEmpty(p.Large2048Url) && p.Large2048Height.HasValue && p.Large2048Width.HasValue));
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosSearchContactsPhotos()
        {
            var contacts = AuthInstance.ContactsGetList(1, 1000).Select(c => c.UserId).ToList();

            // Test with user id = "me"
            var o = new PhotoSearchOptions
                        {
                            UserId = "me",
                            Contacts = ContactSearch.AllContacts,
                            PerPage = 50
                        };

            var photos = AuthInstance.PhotosSearch(o);

            Assert.IsNotNull(photos, "PhotosSearch should not return a null instance.");
            Assert.IsTrue(photos.Any(), "PhotoSearch should have returned some photos.");
            Assert.IsTrue(photos.All(p => p.UserId != TestData.TestUserId), "None of the photos should be mine.");
            Assert.IsTrue(photos.All(p => contacts.Contains(p.UserId)), "UserID not found in list of contacts.");

            // Retest with user id specified explicitly
            o.UserId = TestData.TestUserId;
            photos = AuthInstance.PhotosSearch(o);

            Assert.IsNotNull(photos, "PhotosSearch should not return a null instance.");
            Assert.IsTrue(photos.Any(), "PhotoSearch should have returned some photos.");
            Assert.IsTrue(photos.All(p => p.UserId != TestData.TestUserId), "None of the photos should be mine.");
            Assert.IsTrue(photos.All(p => contacts.Contains(p.UserId)), "UserID not found in list of contacts.");
        }

        [Test]
        public void SearchByUsername()
        {
            var user = Instance.PeopleFindByUserName("Jesus Solana");

            var photos = Instance.PhotosSearch(new PhotoSearchOptions {Username = "Jesus Solana"});

            Assert.AreEqual(user.UserId, photos.First().UserId);
        }

        [Test]
        public void SearchByExifExposure()
        {
            var options = new PhotoSearchOptions
                              {
                                  ExifMinExposure = 10,
                                  ExifMaxExposure = 30,
                                  Extras = PhotoSearchExtras.PathAlias,
                                  PerPage = 5
                              };

            var photos = Instance.PhotosSearch(options);

            foreach (var photo in photos)
            {
                Console.WriteLine(photo.WebUrl);
            }
        }

        [Test]
        public void SearchByExifAperture()
        {
            var options = new PhotoSearchOptions
            {
                ExifMinAperture = 0.0,
                ExifMaxAperture = 1/2,
                Extras = PhotoSearchExtras.PathAlias,
                PerPage = 5
            };

            var photos = Instance.PhotosSearch(options);

            foreach (var photo in photos)
            {
                Console.WriteLine(photo.WebUrl);
            }
        }

        [Test]
        public void SearchByExifFocalLength()
        {
            var options = new PhotoSearchOptions
            {
                ExifMinFocalLength = 400,
                ExifMaxFocalLength = 800,
                Extras = PhotoSearchExtras.PathAlias,
                PerPage = 5
            };

            var photos = Instance.PhotosSearch(options);

            foreach (var photo in photos)
            {
                Console.WriteLine(photo.WebUrl);
            }
        }

        [Test]
        public void ExcludeUserTest()
        {
            var options = new PhotoSearchOptions
            {
                Tags = "colorful",
                MinTakenDate = DateTime.Today.AddDays(-7),
                MaxTakenDate = DateTime.Today.AddDays(-1),
                PerPage = 10
            };

            var photos = Instance.PhotosSearch(options);


            var firstUserId = photos.First().UserId;

            options.ExcludeUserID = firstUserId;

            var nextPhotos = Instance.PhotosSearch(options);

            Assert.IsFalse(nextPhotos.Any(p => p.UserId == firstUserId), "Should not be any photos for user {0}", firstUserId);
        }

        [Test]
        public void GetPhotosByFoursquareVenueId()
        {
            var venueid = "4ac518cef964a520f6a520e3";

            var options = new PhotoSearchOptions
            {
                FoursquareVenueID = venueid
            };

            var photos = Instance.PhotosSearch(options);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Should have returned some photos for 'Big Ben'");
        }

        [Test]
        public void GetPhotosByFoursquareWoeId()
        {
            // Seems to be the same as normal WOE IDs, so not sure what is different about the foursquare ones.
            var woeId = "44417";

            var options = new PhotoSearchOptions
            {
                FoursquareWoeID = woeId
            };

            var photos = Instance.PhotosSearch(options);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Should have returned some photos for 'Big Ben'");
        }

        [Test]
        public void CountFavesAndCountComments()
        {
            var options = new PhotoSearchOptions
            {
                Extras = PhotoSearchExtras.CountFaves | PhotoSearchExtras.CountComments,
                Tags = "colorful"
            };

            var photos = Instance.PhotosSearch(options);

            Assert.IsFalse(photos.Any(p => p.CountFaves == null), "Should not have any null CountFaves");
            Assert.IsFalse(photos.Any(p => p.CountComments == null), "Should not have any null CountComments");
        }

        [Test]
        public void ExcessiveTagsShouldNotThrowUriFormatException()
        {
            var list = Enumerable.Range(1, 9000).Select(i => "reallybigtag" + i).ToList();
            var options = new PhotoSearchOptions{
                Tags = string.Join(",", list)
            };

            Should.Throw<FlickrApiException>(() => Instance.PhotosSearch(options));
        }
    }
}
// ReSharper restore SuggestUseVarKeywordEvident
