using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Adds a photo to the logged in favourites.
        /// Requires authentication.
        /// </summary>
        /// <param name="photoId">The id of the photograph to add.</param>
        public void FavoritesAdd(string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.favorites.add");
            parameters.Add("photo_id", photoId);
            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Removes a photograph from the logged in users favourites.
        /// Requires authentication.
        /// </summary>
        /// <param name="photoId">The id of the photograph to remove.</param>
        public void FavoritesRemove(string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.favorites.remove");
            parameters.Add("photo_id", photoId);
            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Get a list of the currently logger in users favourites.
        /// Requires authentication.
        /// </summary>
        /// <returns><see cref="PhotoCollection"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
        public PhotoCollection FavoritesGetList()
        {
            return FavoritesGetList(null, DateTime.MinValue, DateTime.MinValue, PhotoSearchExtras.None, 0, 0);
        }

        /// <summary>
        /// Get a list of the currently logger in users favourites.
        /// Requires authentication.
        /// </summary>
        /// <param name="extras">The extras to return for each photo.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
        public PhotoCollection FavoritesGetList(PhotoSearchExtras extras)
        {
            return FavoritesGetList(null, DateTime.MinValue, DateTime.MinValue, extras, 0, 0);
        }

        /// <summary>
        /// Get a list of the currently authenticated user's favourites.
        /// Requires authentication.
        /// </summary>
        /// <param name="perPage">Number of photos to include per page.</param>
        /// <param name="page">The page to download this time.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
        public PhotoCollection FavoritesGetList(int page, int perPage)
        {
            return FavoritesGetList(null, DateTime.MinValue, DateTime.MinValue, PhotoSearchExtras.None, page, perPage);
        }

        /// <summary>
        /// Get a list of the currently authenticated user's favourites.
        /// Requires authentication.
        /// </summary>
        /// <param name="extras">The extras to return for each photo.</param>
        /// <param name="perPage">Number of photos to include per page.</param>
        /// <param name="page">The page to download this time.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
        public PhotoCollection FavoritesGetList(PhotoSearchExtras extras, int page, int perPage)
        {
            return FavoritesGetList(null, DateTime.MinValue, DateTime.MinValue, extras, page, perPage);
        }

        /// <summary>
        /// Get a list of favourites for the specified user.
        /// </summary>
        /// <param name="userId">The user id of the user whose favourites you wish to retrieve.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
        public PhotoCollection FavoritesGetList(string userId)
        {
            return FavoritesGetList(userId, DateTime.MinValue, DateTime.MinValue, PhotoSearchExtras.None, 0, 0);
        }

        /// <summary>
        /// Get a list of favourites for the specified user.
        /// </summary>
        /// <param name="userId">The user id of the user whose favourites you wish to retrieve.</param>
        /// <param name="extras">The extras to return for each photo.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
        public PhotoCollection FavoritesGetList(string userId, PhotoSearchExtras extras)
        {
            return FavoritesGetList(userId, DateTime.MinValue, DateTime.MinValue, extras, 0, 0);
        }

        /// <summary>
        /// Get a list of favourites for the specified user.
        /// </summary>
        /// <param name="userId">The user id of the user whose favourites you wish to retrieve.</param>
        /// <param name="perPage">Number of photos to include per page.</param>
        /// <param name="page">The page to download this time.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
        public PhotoCollection FavoritesGetList(string userId, int page, int perPage)
        {
            return FavoritesGetList(userId, DateTime.MinValue, DateTime.MinValue, PhotoSearchExtras.None, page, perPage);
        }

        /// <summary>
        /// Get a list of favourites for the specified user.
        /// </summary>
        /// <param name="userId">The user id of the user whose favourites you wish to retrieve.</param>
        /// <param name="extras">The extras to return for each photo.</param>
        /// <param name="perPage">Number of photos to include per page.</param>
        /// <param name="page">The page to download this time.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
        public PhotoCollection FavoritesGetList(string userId, PhotoSearchExtras extras, int page, int perPage)
        {
            return FavoritesGetList(userId, DateTime.MinValue, DateTime.MinValue, extras, page, perPage);
        }

        /// <summary>
        /// Get a list of favourites for the specified user.
        /// </summary>
        /// <param name="userId">The user id of the user whose favourites you wish to retrieve.</param>
        /// <param name="minFavoriteDate">Minimum date that a photo was favorited on.</param>
        /// <param name="maxFavoriteDate">Maximum date that a photo was favorited on. </param>
        /// <param name="extras">The extras to return for each photo.</param>
        /// <param name="perPage">Number of photos to include per page.</param>
        /// <param name="page">The page to download this time.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
        public PhotoCollection FavoritesGetList(string userId, DateTime minFavoriteDate, DateTime maxFavoriteDate, PhotoSearchExtras extras, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.favorites.getList");
            if (userId != null) parameters.Add("user_id", userId);
            if (minFavoriteDate != DateTime.MinValue) parameters.Add("min_fav_date", UtilityMethods.DateToUnixTimestamp(minFavoriteDate));
            if (maxFavoriteDate != DateTime.MinValue) parameters.Add("max_fav_date", UtilityMethods.DateToUnixTimestamp(maxFavoriteDate));
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<PhotoCollection>(parameters);
        }

        /// <summary>
        /// Gets the public favourites for a specified user.
        /// </summary>
        /// <remarks>This function difers from <see cref="Flickr.FavoritesGetList(string)"/> in that the user id 
        /// is not optional.</remarks>
        /// <param name="userId">The is of the user whose favourites you wish to return.</param>
        /// <returns>A <see cref="PhotoCollection"/> object containing a collection of <see cref="Photo"/> objects.</returns>
        public PhotoCollection FavoritesGetPublicList(string userId)
        {
            return FavoritesGetPublicList(userId, DateTime.MinValue, DateTime.MinValue, PhotoSearchExtras.None, 0, 0);
        }

        /// <summary>
        /// Gets the public favourites for a specified user.
        /// </summary>
        /// <remarks>This function difers from <see cref="Flickr.FavoritesGetList(string)"/> in that the user id 
        /// is not optional.</remarks>
        /// <param name="userId">The is of the user whose favourites you wish to return.</param>
        /// <param name="minFavoriteDate">Minimum date that a photo was favorited on.</param>
        /// <param name="maxFavoriteDate">Maximum date that a photo was favorited on. </param>
        /// <param name="extras">The extras to return for each photo.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="page">The specific page to return.</param>
        /// <returns>A <see cref="PhotoCollection"/> object containing a collection of <see cref="Photo"/> objects.</returns>
        public PhotoCollection FavoritesGetPublicList(string userId, DateTime minFavoriteDate, DateTime maxFavoriteDate, PhotoSearchExtras extras, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.favorites.getPublicList");
            parameters.Add("user_id", userId);
            if (minFavoriteDate != DateTime.MinValue) parameters.Add("min_fav_date", UtilityMethods.DateToUnixTimestamp(minFavoriteDate));
            if (maxFavoriteDate != DateTime.MinValue) parameters.Add("max_fav_date", UtilityMethods.DateToUnixTimestamp(maxFavoriteDate));
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<PhotoCollection>(parameters);
        }
    }
}
