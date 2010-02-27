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
        public string[] PandaGetList()
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.panda.getList");

            Response response = GetResponseCache(parameters);
            if (response.Status == ResponseStatus.OK)
            {
                List<string> pandas = new List<string>();
                foreach (System.Xml.XmlNode node in response.AllElements[0].ChildNodes)
                    pandas.Add(node.InnerText);
                return pandas.ToArray();
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Gets a list of photos for the given panda.
        /// </summary>
        /// <param name="pandaName">The name of the panda to return photos for.</param>
        /// <returns>A list of photos for the panda.</returns>
        public PandaPhotos PandaGetPhotos(string pandaName)
        {
            return PandaGetPhotos(pandaName, PhotoSearchExtras.None, 0, 0);
        }

        /// <summary>
        /// Gets a list of photos for the given panda.
        /// </summary>
        /// <param name="pandaName">The name of the panda to return photos for.</param>
        /// <param name="extras">The extras to return with the photos.</param>
        /// <returns>A list of photos for the panda.</returns>
        public PandaPhotos PandaGetPhotos(string pandaName, PhotoSearchExtras extras)
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
        public PandaPhotos PandaGetPhotos(string pandaName, int perPage, int page)
        {
            return PandaGetPhotos(pandaName, PhotoSearchExtras.None, perPage, page);
        }

        /// <summary>
        /// Gets a list of photos for the given panda.
        /// </summary>
        /// <param name="pandaName">The name of the panda to return photos for.</param>
        /// <param name="extras">The extras to return with the photos.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="page">The age to return.</param>
        /// <returns>A list of photos for the panda.</returns>
        public PandaPhotos PandaGetPhotos(string pandaName, PhotoSearchExtras extras, int perPage, int page)
        {
            Dictionary<string, object> parameters = new Dictionary<string,object>();
            parameters.Add("method", "flickr.panda.getPhotos");
            parameters.Add("panda_name", pandaName);
            if( extras != PhotoSearchExtras.None) parameters.Add("extras", Utils.ExtrasToString(extras));
            if( perPage > 0 ) parameters.Add("per_page", perPage);
            if( page > 0 ) parameters.Add("page", page);

            return GetResponseCache<PandaPhotos>(parameters);
        }
    }
}
