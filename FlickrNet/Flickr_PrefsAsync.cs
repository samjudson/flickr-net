using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Gets the currently authenticated users default content type.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PrefsGetContentTypeAsync(Action<FlickrResult<ContentType>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.prefs.getContentType");

            GetResponseAsync<UnknownResponse>(parameters, (r) =>
                {
                    FlickrResult<ContentType> result = new FlickrResult<ContentType>();
                    result.Error = r.Error;
                    if( !r.HasError )
                    {
                        result.Result = (ContentType)int.Parse(r.Result.GetAttributeValue("*", "content_type"), System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
                    callback(result);
                });
        }

        /// <summary>
        /// Returns the default privacy level for geographic information attached to the user's photos and whether or not the user has chosen to use geo-related EXIF information to automatically geotag their photos.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PrefsGetGeoPermsAsync(Action<FlickrResult<UserGeoPermissions>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.prefs.getGeoPerms");

            GetResponseAsync<UserGeoPermissions>(parameters, callback);
        }

        /// <summary>
        /// Gets the currently authenticated users default hidden from search setting.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PrefsGetHiddenAsync(Action<FlickrResult<HiddenFromSearch>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.prefs.getHidden");

            GetResponseAsync<UnknownResponse>(parameters, (r) =>
            {
                FlickrResult<HiddenFromSearch> result = new FlickrResult<HiddenFromSearch>();
                result.Error = r.Error;
                if (!r.HasError)
                {
                    result.Result = (HiddenFromSearch)int.Parse(r.Result.GetAttributeValue("*", "hidden"), System.Globalization.NumberFormatInfo.InvariantInfo);
                }
                callback(result);
            });
        }

        /// <summary>
        /// Returns the default privacy level preference for the user. 
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PrefsGetPrivacyAsync(Action<FlickrResult<PrivacyFilter>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.prefs.getPrivacy");

            GetResponseAsync<UnknownResponse>(parameters, (r) =>
            {
                FlickrResult<PrivacyFilter> result = new FlickrResult<PrivacyFilter>();
                result.Error = r.Error;
                if (!r.HasError)
                {
                    result.Result = (PrivacyFilter)int.Parse(r.Result.GetAttributeValue("*", "privacy"), System.Globalization.NumberFormatInfo.InvariantInfo);
                }
                callback(result);
            });

        }

        /// <summary>
        /// Gets the currently authenticated users default safety level.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PrefsGetSafetyLevelAsync(Action<FlickrResult<SafetyLevel>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.prefs.getSafetyLevel");

            GetResponseAsync<UnknownResponse>(parameters, (r) =>
            {
                FlickrResult<SafetyLevel> result = new FlickrResult<SafetyLevel>();
                result.Error = r.Error;
                if (!r.HasError)
                {
                    result.Result = (SafetyLevel)int.Parse(r.Result.GetAttributeValue("*", "safety_level"), System.Globalization.NumberFormatInfo.InvariantInfo);
                }
                callback(result);
            });
        }

    }
}
