using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Get a list of current 'Pandas' supported by Flickr.
        /// </summary>
        /// <returns>An array of panda names.</returns>
        public void PandaGetListAsync(Action<FlickrResult<string[]>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.panda.getList");

            GetResponseAsync<UnknownResponse>(parameters, (r) =>
            {
                FlickrResult<string[]> result = new FlickrResult<string[]>();
                result.HasError = r.HasError;
                if (r.HasError)
                {
                    result.Error = r.Error;
                }
                else
                {
                    result.Result = r.Result.GetElementArray("panda");
                }
                callback(result);
            });

        }

        /// <summary>
        /// Gets a list of photos for the given panda.
        /// </summary>
        /// <param name="pandaName">The name of the panda to return photos for.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PandaGetPhotosAsync(string pandaName, Action<FlickrResult<PandaPhotoCollection>> callback)
        {
            PandaGetPhotosAsync(pandaName, PhotoSearchExtras.None, 0, 0, callback);
        }

        /// <summary>
        /// Gets a list of photos for the given panda.
        /// </summary>
        /// <param name="pandaName">The name of the panda to return photos for.</param>
        /// <param name="extras">The extras to return with the photos.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PandaGetPhotosAsync(string pandaName, PhotoSearchExtras extras, Action<FlickrResult<PandaPhotoCollection>> callback)
        {
            PandaGetPhotosAsync(pandaName, extras, 0, 0, callback);
        }

        /// <summary>
        /// Gets a list of photos for the given panda.
        /// </summary>
        /// <param name="pandaName">The name of the panda to return photos for.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="page">The age to return.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PandaGetPhotosAsync(string pandaName, int page, int perPage, Action<FlickrResult<PandaPhotoCollection>> callback)
        {
            PandaGetPhotosAsync(pandaName, PhotoSearchExtras.None, page, perPage, callback);
        }

        /// <summary>
        /// Gets a list of photos for the given panda.
        /// </summary>
        /// <param name="pandaName">The name of the panda to return photos for.</param>
        /// <param name="extras">The extras to return with the photos.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="page">The age to return.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PandaGetPhotosAsync(string pandaName, PhotoSearchExtras extras, int page, int perPage, Action<FlickrResult<PandaPhotoCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.panda.getPhotos");
            parameters.Add("panda_name", pandaName);
            if( extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));
            if( perPage > 0 ) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<PandaPhotoCollection>(parameters, callback);
        }
    }
}
