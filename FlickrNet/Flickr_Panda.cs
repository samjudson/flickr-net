using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
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

        public PandaPhotos PandaGetPhotos(string pandaName)
        {
            return PandaGetPhotos(pandaName, PhotoSearchExtras.None, 0, 0);
        }

        public PandaPhotos PandaGetPhotos(string pandaName, PhotoSearchExtras extras)
        {
            return PandaGetPhotos(pandaName, extras, 0, 0);
        }

        public PandaPhotos PandaGetPhotos(string pandaName, int perPage, int page)
        {
            return PandaGetPhotos(pandaName, PhotoSearchExtras.None, perPage, page);
        }

        public PandaPhotos PandaGetPhotos(string pandaName, PhotoSearchExtras extra, int perPage, int page)
        {
            Dictionary<string, object> parameters = new Dictionary<string,object>();
            parameters.Add("method", "flickr.panda.getPhotos");
            parameters.Add("panda_name", pandaName);
            if( extra != PhotoSearchExtras.None) parameters.Add("extras", Utils.ExtrasToString(extra));
            if( perPage > 0 ) parameters.Add("per_page", perPage);
            if( page > 0 ) parameters.Add("page", page);

            return GetResponseCache<PandaPhotos>(parameters);
        }
    }
}
