using System.Collections.Generic;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Get a users profile properties.
        /// </summary>
        /// <param name="userId">The id of the user to get the profile for.</param>
        /// <returns>A <see cref="Profile"/> instance containing the details of the users profile.</returns>
        public Profile ProfileGetProfile(string userId)
        {
            var parameters = new Dictionary<string, string> {
                { "method", "flickr.profile.getProfile" },
                { "user_id", userId }
            };

            return GetResponseCache<Profile>(parameters);
        }
    }
}
