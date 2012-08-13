using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    [TestClass]
    public class PhotosSuggestionsTests
    {
        private string photoId = "6282363572";

        [TestInitialize]
        public void TestInitialize()
        {
            Flickr.CacheDisabled = true;
        }
        
        [TestMethod]
        public void GetListTest()
        {
            var f = TestData.GetAuthInstance();

            // Remove any pending suggestions
            var suggestions = f.PhotosSuggestionsGetList(photoId, SuggestionStatus.Pending);
            Assert.IsNotNull(suggestions, "SuggestionCollection should not be null.");

            foreach (var s in suggestions)
            {
                if (s.SuggestionId == null)
                {
                    Console.WriteLine(f.LastRequest);
                    Console.WriteLine(f.LastResponse);
                }
                Assert.IsNotNull(s.SuggestionId, "Suggestion ID should not be null.");
                f.PhotosSuggestionsRemoveSuggestion(s.SuggestionId);
            }

            // Add test suggestion
            AddSuggestion();

            // Get list of suggestions and check
            suggestions = f.PhotosSuggestionsGetList(photoId, SuggestionStatus.Pending);

            Assert.IsNotNull(suggestions, "SuggestionCollection should not be null.");
            Assert.AreNotEqual(0, suggestions.Count, "Count should not be zero.");

            var suggestion = suggestions.First();

            Assert.AreEqual("41888973@N00", suggestion.SuggestedByUserId);
            Assert.AreEqual("Sam Judson", suggestion.SuggestedByUserName);
            Assert.AreEqual("I really think this is a good suggestion.", suggestion.Note);
            Assert.AreEqual(54.977, suggestion.Latitude, "Latitude should be the same.");

            // Reject then remove - For some reason rejection is not working
//            f.PhotosSuggestionsRejectSuggestion(suggestion.SuggestionId);
            f.PhotosSuggestionsRemoveSuggestion(suggestion.SuggestionId);

            // Add test suggestion
            AddSuggestion();
            suggestion = f.PhotosSuggestionsGetList(photoId, SuggestionStatus.Pending).First();
            f.PhotosSuggestionsApproveSuggestion(suggestion.SuggestionId);
            f.PhotosSuggestionsRemoveSuggestion(suggestion.SuggestionId);

        }

        public void AddSuggestion()
        {
            var f = TestData.GetAuthInstance();

            var lat = 54.977;
            var lon = -1.612;
            var accuracy = GeoAccuracy.Street;
            var woeId = "30079";
            var placeId = "X9sTR3BSUrqorQ";
            var note = "I really think this is a good suggestion.";

            f.PhotosSuggestionsSuggestLocation(photoId, lat, lon, accuracy, woeId, placeId, note);
        }
    }
}
