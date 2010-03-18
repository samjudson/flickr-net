using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.XPath;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Get a list of current 'Pandas' supported by Flickr.
        /// </summary>
        /// <returns>An array of panda names.</returns>
        public string[] PandaGetList()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.panda.getList");

            UnknownResponse response = GetResponseCache<UnknownResponse>(parameters);

            List<string> pandas = new List<string>();
            foreach (XPathNavigator n in response.GetXPathNavigator().Select("//panda/text()"))
            {
                pandas.Add(n.Value);
            }
            return pandas.ToArray();
        }

        /// <summary>
        /// Gets a list of photos for the given panda.
        /// </summary>
        /// <param name="pandaName">The name of the panda to return photos for.</param>
        /// <returns>A list of photos for the panda.</returns>
        public PandaPhotoCollection PandaGetPhotos(string pandaName)
        {
            return PandaGetPhotos(pandaName, PhotoSearchExtras.None, 0, 0);
        }

        /// <summary>
        /// Gets a list of photos for the given panda.
        /// </summary>
        /// <param name="pandaName">The name of the panda to return photos for.</param>
        /// <param name="extras">The extras to return with the photos.</param>
        /// <returns>A list of photos for the panda.</returns>
        public PandaPhotoCollection PandaGetPhotos(string pandaName, PhotoSearchExtras extras)
        {
            return PandaGetPhotos(pandaName, extras, 0, 0);
        }

        /// <summary>
        /// Gets a list of photos for the given panda.
        /// </summary>
        /// <param name="pandaName">The name of the panda to return photos for.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="page">The age to return.</param>
        /// <returns>A list of photos for the panda.</returns>
        public PandaPhotoCollection PandaGetPhotos(string pandaName, int page, int perPage)
        {
            return PandaGetPhotos(pandaName, PhotoSearchExtras.None, page, perPage);
        }

        /// <summary>
        /// Gets a list of photos for the given panda.
        /// </summary>
        /// <param name="pandaName">The name of the panda to return photos for.</param>
        /// <param name="extras">The extras to return with the photos.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="page">The age to return.</param>
        /// <returns>A list of photos for the panda.</returns>
        public PandaPhotoCollection PandaGetPhotos(string pandaName, PhotoSearchExtras extras, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.panda.getPhotos");
            parameters.Add("panda_name", pandaName);
            if( extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));
            if( perPage > 0 ) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<PandaPhotoCollection>(parameters);
        }
    }
}
