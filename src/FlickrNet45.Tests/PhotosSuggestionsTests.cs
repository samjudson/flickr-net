using System;
using System.Linq;
using NUnit.Framework;
using FlickrNet;

namespace FlickrNet45.Tests
{
    [TestFixture]
    public class PhotosSuggestionsTests : BaseTest
    {
        private const string PhotoId = "6282363572";

        [SetUp]
        public void TestInitialize()
        {
            Flickr.CacheDisabled = true;
        }
        
        [Test]
        [Category("AccessTokenRequired")]
        public void GetListTest()
        {
            // Remove any pending suggestions
            var suggestions = AuthInstance.PhotosSuggestionsGetList(PhotoId, SuggestionStatus.Pending);
            Assert.IsNotNull(suggestions, "SuggestionCollection should not be null.");

            foreach (var s in suggestions)
            {
                Assert.IsNotNull(s.SuggestionId, "Suggestion ID should not be null.");
                AuthInstance.PhotosSuggestionsRemoveSuggestion(s.SuggestionId);
            }

            // Add test suggestion
            AddSuggestion();

            // Get list of suggestions and check
            suggestions = AuthInstance.PhotosSuggestionsGetList(PhotoId, SuggestionStatus.Pending);

            Assert.IsNotNull(suggestions, "SuggestionCollection should not be null.");
            Assert.AreNotEqual(0, suggestions.Count, "Count should not be zero.");

            var suggestion = suggestions.First();

            Assert.AreEqual("41888973@N00", suggestion.SuggestedByUserId);
            Assert.AreEqual("Sam Judson", suggestion.SuggestedByUserName);
            Assert.AreEqual("I really think this is a good suggestion.", suggestion.Note);
            Assert.AreEqual(54.977, suggestion.Latitude, "Latitude should be the same.");

            AuthInstance.PhotosSuggestionsRemoveSuggestion(suggestion.SuggestionId);

            // Add test suggestion
            AddSuggestion();
            suggestion = AuthInstance.PhotosSuggestionsGetList(PhotoId, SuggestionStatus.Pending).First();
            AuthInstance.PhotosSuggestionsApproveSuggestion(suggestion.SuggestionId);
            AuthInstance.PhotosSuggestionsRemoveSuggestion(suggestion.SuggestionId);

        }

        public void AddSuggestion()
        {
            var lat = 54.977;
            var lon = -1.612;
            var accuracy = GeoAccuracy.Street;
            var woeId = "30079";
            var placeId = "X9sTR3BSUrqorQ";
            var note = "I really think this is a good suggestion.";

            AuthInstance.PhotosSuggestionsSuggestLocation(PhotoId, lat, lon, accuracy, woeId, placeId, note);
        }
    }
}
