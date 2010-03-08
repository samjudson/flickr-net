using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Gets an array of supported method names for Flickr.
        /// </summary>
        /// <remarks>
        /// Note: Not all methods might be supported by the FlickrNet Library.</remarks>
        /// <returns></returns>
        public string[] ReflectionGetMethods()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.reflection.getMethods");

            return GetResponseCache<Methods>(parameters).ToArray();
        }

        /// <summary>
        /// Gets the method details for a given method.
        /// </summary>
        /// <param name="methodName">The name of the method to retrieve.</param>
        /// <returns>Returns a <see cref="Method"/> instance for the given method name.</returns>
        public Method ReflectionGetMethodInfo(string methodName)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.reflection.getMethodInfo");
            parameters.Add("api_key", _apiKey);
            parameters.Add("method_name", methodName);

            return GetResponseCache<Method>(parameters);
        }

    }
}
