using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Gets the currently authenticated users default safety level.
        /// </summary>
        /// <returns></returns>
        public SafetyLevel PrefsGetSafetyLevel()
        {
            CheckRequiresAuthentication();

            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.prefs.getSafetyLevel");

            Response res = GetResponseCache(parameters);
            if (res.Status == ResponseStatus.OK)
            {
                string s = res.AllElements[0].GetAttribute("safety_level");
                return (SafetyLevel)int.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                throw new FlickrApiException(res.Error);
            }
        }

        /// <summary>
        /// Gets the currently authenticated users default hidden from search setting.
        /// </summary>
        /// <returns></returns>
        public HiddenFromSearch PrefsGetHidden()
        {
            CheckRequiresAuthentication();

            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.prefs.getHidden");

            Response res = GetResponseCache(parameters);
            if (res.Status == ResponseStatus.OK)
            {
                string s = res.AllElements[0].GetAttribute("hidden");
                return (HiddenFromSearch)int.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                throw new FlickrApiException(res.Error);
            }
        }

        /// <summary>
        /// Gets the currently authenticated users default content type.
        /// </summary>
        /// <returns></returns>
        public ContentType PrefsGetContentType()
        {
            CheckRequiresAuthentication();

            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.prefs.getContentType");

            Response res = GetResponseCache(parameters);
            if (res.Status == ResponseStatus.OK)
            {
                string s = res.AllElements[0].GetAttribute("content_type");
                return (ContentType)int.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                throw new FlickrApiException(res.Error);
            }
        }
    }
}
