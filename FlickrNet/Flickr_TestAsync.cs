using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Collections.Specialized;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Can be used to call unsupported methods in the Flickr API.
        /// </summary>
        /// <remarks>
        /// Use of this method is not supported. 
        /// The way the FlickrNet API Library works may mean that some methods do not return an expected result 
        /// when using this method.
        /// </remarks>
        /// <param name="method">The method name, e.g. "flickr.test.null".</param>
        /// <param name="parameters">A list of parameters. Note, api_key is added by default and is not included. Can be null.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TestGenericAsync(string method, Dictionary<string, string> parameters, Action<FlickrResult<UnknownResponse>> callback)
        {
            if (parameters == null) parameters = new Dictionary<string, string>();

            parameters.Add("method", method);
            GetResponseAsync<UnknownResponse>(parameters, callback);
        }

        /// <summary>
        /// Test the logged in state of the current Filckr object.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TestLoginAsync(Action<FlickrResult<FoundUser>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.test.login");

            GetResponseAsync<FoundUser>(parameters, callback);
        }

        /// <summary>
        /// Null test.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TestNullAsync(Action<FlickrResult<NoResponse>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.test.null");

            GetResponseAsync<NoResponse>(parameters, callback);
        }

        /// <summary>
        /// Echos back all parameters passed in.
        /// </summary>
        /// <param name="parameters">A dictionary of extra parameters to pass in. Note, the "method" and "api_key" parameters will always be passed in.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TestEchoAsync(Dictionary<string, string> parameters, Action<FlickrResult<EchoResponseDictionary>> callback)
        {
            parameters.Add("method", "flickr.test.echo");
            GetResponseAsync<EchoResponseDictionary>(parameters, callback);
        }
    }
}
