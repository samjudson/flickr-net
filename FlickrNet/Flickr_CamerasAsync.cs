using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Gets a list of camera brands.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        /// <returns></returns>
        public void CamerasGetBrandsAsync(Action<FlickrResult<BrandCollection>> callback )
        {
            var parameters = new Dictionary<string, string> { { "method", "flickr.cameras.getBrands" } };
            GetResponseAsync(parameters, callback);
        }

        /// <summary>
        /// Get a list of camera models for a particular brand id.
        /// </summary>
        /// <param name="brandId">The ID of the brand you want the models of.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        /// <returns></returns>
        public void CamerasGetBrandModelsAsync(string brandId, Action<FlickrResult<CameraCollection>> callback)
        {
            var parameters = new Dictionary<string, string>
                                 {
                                     {"method", "flickr.cameras.getBrandModels"},
                                     {"brand", brandId}
                                 };
            GetResponseAsync(parameters, callback);
        }
    }
}
