using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BrandCollection CamerasGetBrands()
        {
            var parameters = new Dictionary<string, string> {{"method", "flickr.cameras.getBrands"}};
            return GetResponseCache<BrandCollection>(parameters);
        }

        /// <summary>
        /// Get a list of camera models for a particular brand id.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public CameraCollection CamerasGetBrandModels(string brandId)
        {
            var parameters = new Dictionary<string, string>
                                 {
                                     {"method", "flickr.cameras.getBrandModels"},
                                     {"brand", brandId}
                                 };
            return GetResponseCache<CameraCollection>(parameters);
        }
    }
}
