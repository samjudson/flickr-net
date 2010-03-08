using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Rotates a photo on Flickr.
        /// </summary>
        /// <remarks>
        /// Does not rotate the original photo.
        /// </remarks>
        /// <param name="photoId">The ID of the photo.</param>
        /// <param name="degrees">The number of degrees to rotate by. Valid values are 90, 180 and 270.</param>
        public void PhotosTransformRotate(string photoId, int degrees)
        {
            if (photoId == null)
                throw new ArgumentNullException("photoId");
            if (degrees != 90 && degrees != 180 && degrees != 270)
                throw new ArgumentException("Must be 90, 180 or 270", "degrees");

            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.transform.rotate");
            parameters.Add("photo_id", photoId);
            parameters.Add("degrees", degrees.ToString("0"));

            FlickrNet.Response response = GetResponseNoCache(parameters);
            if (response.Status == ResponseStatus.OK)
            {
                return;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

    }
}
