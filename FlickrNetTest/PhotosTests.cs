using System;
using System.Linq;
using System.Net;
using FlickrNet;
using NUnit.Framework;
using Shouldly;

namespace FlickrNetTest
{
    [TestFixture]
    public class PhotosTests : BaseTest
    {
        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosSetDatesTest()
        {
            var f = AuthInstance;
            var photoId = TestData.PhotoId;

            var info = f.PhotosGetInfo(photoId);

            f.PhotosSetDates(photoId, info.DatePosted, info.DateTaken, info.DateTakenGranularity);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosAddTagsTest()
        {
            Flickr f = AuthInstance;
            string testtag = "thisisatesttag";
            string photoId = "6282363572";

            // Add the tag
            f.PhotosAddTags(photoId, testtag);
            // Add second tag using different signature
            f.PhotosAddTags(photoId, new string[] { testtag + "2" });

            // Get list of tags
            var tags = f.TagsGetListPhoto(photoId);

            // Find the tag in the collection
            var tagsToRemove = tags.Where(t => t.TagText.StartsWith(testtag, StringComparison.Ordinal));

            foreach (var tag in tagsToRemove)
            {
                // Remove the tag
                f.PhotosRemoveTag(tag.TagId);
            }
        }

        [Test]
        public void PhotosGetAllContextsBasicTest()
        {
            var a = Instance.PhotosGetAllContexts("4114887196");

            Assert.IsNotNull(a);
            Assert.IsNotNull(a.Groups, "Groups should not be null.");
            Assert.IsNotNull(a.Sets, "Sets should not be null.");

            Assert.AreEqual(1, a.Groups.Count, "Groups.Count should be one.");
            Assert.AreEqual(1, a.Sets.Count, "Sets.Count should be one.");
        }

        [Test]
        public void PhotosGetPopular()
        {
            var photos = Instance.PhotosGetPopular(TestData.TestUserId);

            photos.ShouldNotBeEmpty();
        }

        [Test]
        public void PhotosGetExifTest()
        {
            Flickr f = Instance;

            ExifTagCollection tags = f.PhotosGetExif("4268023123");

            Console.WriteLine(f.LastResponse);

            Assert.IsNotNull(tags, "ExifTagCollection should not be null.");

            Assert.IsTrue(tags.Count > 20, "More than twenty parts of EXIF data should be returned.");

            Assert.AreEqual("IFD0", tags[0].TagSpace, "First tags TagSpace is not set correctly.");
            Assert.AreEqual(0, tags[0].TagSpaceId, "First tags TagSpaceId is not set correctly.");
            Assert.AreEqual("ImageDescription", tags[0].Tag, "First tags Tag is not set correctly.");
            Assert.AreEqual("Image Description", tags[0].Label, "First tags Label is not set correctly.");
            Assert.AreEqual(
                "It scares me sometimes how much some of my handwriting reminds me of Dad's " +
                "- in this photo there is one 5 that especially reminds me of his handwriting.",
                tags[0].Raw, "First tags RAW is not correct.");
            Assert.IsNull(tags[0].Clean, "First tags Clean should be null.");
        }

        [Test]
        public void PhotosGetContextBasicTest()
        {
            var context = Instance.PhotosGetContext("3845365350");

            Assert.IsNotNull(context);

            Assert.AreEqual("3844573707", context.PreviousPhoto.PhotoId);
            Assert.AreEqual("3992605178", context.NextPhoto.PhotoId);
        }

        [Test]
        public void PhotosGetExifIPhoneTest()
        {
            bool bFound = false;
            Flickr f = Instance;

            ExifTagCollection tags = f.PhotosGetExif("5899928191");

            Assert.AreEqual("Apple iPhone 4", tags.Camera, "Camera property should be set correctly.");

            foreach (ExifTag tag in tags)
            {
                if (tag.Tag == "Model")
                {
                    Assert.IsTrue(tag.Raw == "iPhone 4", "Model tag is not 'iPhone 4'");
                    bFound = true;
                    break;
                }
            }
            Assert.IsTrue(bFound, "Model tag not found.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetNotInSetAllParamsTest()
        {
            Flickr f = AuthInstance;

            var photos = f.PhotosGetNotInSet(1, 10, PhotoSearchExtras.All);

            Assert.IsNotNull(photos);
            Assert.AreEqual(10, photos.Count);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetNotInSetNoParamsTest()
        {
            Flickr f = AuthInstance;

            var photos = f.PhotosGetNotInSet();
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetNotInSetPagesTest()
        {
            Flickr f = AuthInstance;

            var photos = f.PhotosGetNotInSet(1, 11);

            Assert.IsNotNull(photos);
            Assert.AreEqual(11, photos.Count);

            // Overloads
            f.PhotosGetNotInSet();
            f.PhotosGetNotInSet(1);
            f.PhotosGetNotInSet(new PartialSearchOptions() { Page = 1, PerPage = 10, PrivacyFilter = PrivacyFilter.CompletelyPrivate });
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetPermsBasicTest()
        {
            var p = AuthInstance.PhotosGetPerms("4114887196");

            Assert.IsNotNull(p);
            Assert.AreEqual("4114887196", p.PhotoId);
            Assert.AreNotEqual(PermissionComment.Nobody, p.PermissionComment);
        }

        [Test]
        public void PhotosGetRecentBlankTest()
        {
            var photos = Instance.PhotosGetRecent();

            Assert.IsNotNull(photos);
        }

        [Test]
        public void PhotosGetRecentAllParamsTest()
        {
            var photos = Instance.PhotosGetRecent(1, 20, PhotoSearchExtras.All);

            Assert.IsNotNull(photos);
            Assert.AreEqual(20, photos.PerPage);
            Assert.AreEqual(20, photos.Count);
        }

        [Test]
        public void PhotosGetRecentPagesTest()
        {
            var photos = Instance.PhotosGetRecent(1, 20);

            Assert.IsNotNull(photos);
            Assert.AreEqual(20, photos.PerPage);
            Assert.AreEqual(20, photos.Count);
        }

        [Test]
        public void PhotosGetRecentExtrasTest()
        {
            var photos = Instance.PhotosGetRecent(PhotoSearchExtras.OwnerName);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count);

            var photo = photos.First();
            Assert.IsNotNull(photo.OwnerName);
        }

        [Test]
        public void PhotosGetSizes10Test()
        {
            var o = new PhotoSearchOptions {Tags = "microsoft", PerPage = 10};

            var photos = Instance.PhotosSearch(o);

            foreach (var p in photos)
            {
                var sizes = Instance.PhotosGetSizes(p.PhotoId);
                foreach (var s in sizes)
                {

                }
            }
        }

        [Test]
        public void PhotosGetSizesBasicTest()
        {
            var sizes = Instance.PhotosGetSizes("4114887196");

            Assert.IsNotNull(sizes);
            Assert.AreNotEqual(0, sizes.Count);

            foreach (Size s in sizes)
            {
                Assert.IsNotNull(s.Label, "Label should not be null.");
                Assert.IsNotNull(s.Source, "Source should not be null.");
                Assert.IsNotNull(s.Url, "Url should not be null.");
                Assert.AreNotEqual(0, s.Height, "Height should not be zero.");
                Assert.AreNotEqual(0, s.Width, "Width should not be zero.");
                Assert.AreNotEqual(MediaType.None, s.MediaType, "MediaType should be set.");
            }
        }

        [Test]
        public void PhotosGetSizesVideoTest()
        {
            //https://www.flickr.com/photos/tedsherarts/4399135415/
            var sizes = Instance.PhotosGetSizes("4399135415");

            sizes.ShouldContain(s => s.MediaType == MediaType.Videos, "At least one size should contain a Video media type.");
            sizes.ShouldContain(s => s.MediaType == MediaType.Photos, "At least one size should contain a Photo media type.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetUntaggedAllParamsTest()
        {
            Flickr f = AuthInstance;

            var photos = f.PhotosGetUntagged(1, 10, PhotoSearchExtras.All);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetUntaggedNoParamsTest()
        {
            Flickr f = AuthInstance;

            var photos = f.PhotosGetUntagged();

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count);

            var photo = photos.First();
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetUntaggedExtrasTest()
        {
            Flickr f = AuthInstance;

            var photos = f.PhotosGetUntagged(PhotoSearchExtras.OwnerName);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count);

            var photo = photos.First();

            Assert.IsNotNull(photo.OwnerName);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetUntaggedPagesTest()
        {
            Flickr f = AuthInstance;

            var photos = f.PhotosGetUntagged(1, 10);

            Assert.IsNotNull(photos);
            Assert.AreEqual(10, photos.Count);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosRecentlyUpdatedTests()
        {
            var sixMonthsAgo = DateTime.Today.AddMonths(-6);
            var f = AuthInstance;

            var photos = f.PhotosRecentlyUpdated(sixMonthsAgo, PhotoSearchExtras.All, 1, 20);

            Assert.IsNotNull(photos);
            Assert.AreEqual(20, photos.PerPage);
            Assert.AreNotEqual(0, photos.Count);

            // Overloads

            photos = f.PhotosRecentlyUpdated(sixMonthsAgo);
            photos = f.PhotosRecentlyUpdated(sixMonthsAgo, PhotoSearchExtras.DateTaken);
            photos = f.PhotosRecentlyUpdated(sixMonthsAgo, 1, 10);
        }

        [Test]
        public void PhotosSearchDoesLargeExist()
        {
            var o = new PhotoSearchOptions();
            o.Extras = PhotoSearchExtras.AllUrls;
            o.PerPage = 50;
            o.Tags = "test";

            PhotoCollection photos = Instance.PhotosSearch(o);

            foreach (Photo p in photos)
            {
                Assert.IsTrue(p.DoesLargeExist == true || p.DoesLargeExist == false);
                Assert.IsTrue(p.DoesMediumExist == true || p.DoesMediumExist == false);
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosSetMetaLargeDescription()
        {
            string description;

            using (WebClient wc = new WebClient())
            {
                description = wc.DownloadString("http://en.wikipedia.org/wiki/Scots_Pine");
                // Limit to size of a url to 65519 characters, so chop the description down to a large but not too large size.
                description = description.Substring(0, 6551);
            }

            string title = "Blacksway Cat";
            string photoId = "5279984467";

            Flickr f = AuthInstance;
            f.PhotosSetMeta(photoId, title, description);
        }

        [Test]
        public void PhotosUploadCheckTicketsTest()
        {
            Flickr f = Instance;

            string[] tickets = new string[3];
            tickets[0] = "invalid1";
            tickets[1] = "invalid2";
            tickets[2] = "invalid3";

            var t = f.PhotosUploadCheckTickets(tickets);

            Assert.AreEqual(3, t.Count);

            Assert.AreEqual("invalid1", t[0].TicketId);
            Assert.IsNull(t[0].PhotoId);
            Assert.IsTrue(t[0].InvalidTicketId);
        }

        [Test]
        public void PhotosPeopleGetListTest()
        {
            var photoId = "3547137580";

            var people = Instance.PhotosPeopleGetList(photoId);

            Assert.AreNotEqual(0, people.Total, "Total should not be zero.");
            Assert.AreNotEqual(0, people.Count, "Count should not be zero.");
            Assert.AreEqual(people.Count, people.Total, "Count should equal Total.");

            foreach (var person in people)
            {
                Assert.IsNotNull(person.UserId, "UserId should not be null.");
                Assert.IsNotNull(person.PhotostreamUrl, "PhotostreamUrl should not be null.");
                Assert.IsNotNull(person.BuddyIconUrl, "BuddyIconUrl should not be null.");
            }
        }

        [Test]
        public void PhotosPeopleGetListSpecificUserTest()
        {
            string photoId = "104267998"; // https://www.flickr.com/photos/thunderchild5/104267998/
            string userId = "41888973@N00"; //sam judsons nsid

            Flickr f = Instance;
            PhotoPersonCollection ppl = f.PhotosPeopleGetList(photoId);
            PhotoPerson pp = ppl[0];
            Assert.AreEqual(userId, pp.UserId);
            Assert.IsTrue(pp.BuddyIconUrl.Contains(".staticflickr.com/"), "Buddy icon doesn't contain correct details.");
        }

        [Test]
        public void WebUrlContainsUserIdIfPathAliasIsEmpty()
        {
            var options = new PhotoSearchOptions
                        {
                            UserId = "39858630@N06",
                            PerPage = 1,
                            Extras = PhotoSearchExtras.PathAlias
                        };

            var flickr = Instance;
            var photos = flickr.PhotosSearch(options);

            string webUrl = photos[0].WebUrl;
            string userPart = webUrl.Split('/')[4];

            Console.WriteLine("WebUrl is: " + webUrl);
            Assert.AreNotEqual(userPart, string.Empty, "User part of the URL cannot be empty");
        }

        [Test]
        public void PhotostreamUrlContainsUserIdIfPathAliasIsEmpty()
        {
            var photoPerson = new PhotoPerson()
                                  {
                                      PathAlias = string.Empty,
                                      UserId = "UserId",
                                  };

            string userPart = photoPerson.PhotostreamUrl.Split('/')[4];

            Assert.AreNotEqual(userPart, string.Empty, "User part of the URL cannot be empty");
        }

        [Test]
        public void PhotosTestLargeSquareSmall320()
        {
            var o = new PhotoSearchOptions();
            o.Extras = PhotoSearchExtras.LargeSquareUrl | PhotoSearchExtras.Small320Url;
            o.UserId = TestData.TestUserId;
            o.PerPage = 10;

            var photos = Instance.PhotosSearch(o);
            Assert.IsTrue(photos.Count > 0, "Should return more than zero photos.");

            foreach (var photo in photos)
            {
                Assert.IsNotNull(photo.Small320Url, "Small320Url should not be null.");
                Assert.IsNotNull(photo.LargeSquareThumbnailUrl, "LargeSquareThumbnailUrl should not be null.");
            }
        }

   }
}
