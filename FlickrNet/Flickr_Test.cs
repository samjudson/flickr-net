using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Collections.Specialized;
using System.Xml.XPath;

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
        /// <returns>An array of <see cref="XmlElement"/> instances which is the expected response.</returns>
        public UnknownResponse TestGeneric(string method, Dictionary<string, object> parameters)
        {
            if (parameters == null) parameters = new Dictionary<string, object>();

            parameters.Add("method", method);
            return GetResponseNoCache<UnknownResponse>(parameters);
        }

        /// <summary>
        /// Test the logged in state of the current Filckr object.
        /// </summary>
        /// <returns>The <see cref="FoundUser"/> object containing the username and userid of the current user.</returns>
        public FoundUser TestLogin()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.test.login");

            return GetResponseCache<FoundUser>(parameters);
        }

        /// <summary>
        /// Null test.
        /// </summary>
        public void TestNull()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.test.null");

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Echos back all parameters passed in.
        /// </summary>
        /// <param name="parameters">A dictionary of extra parameters to pass in. Note, the "method" and "api_key" parameters will always be passed in.</param>
        /// <returns></returns>
        public EchoResponseDictionary TestEcho(Dictionary<string, object> parameters)
        {
            parameters.Add("method", "flickr.test.echo");
            return GetResponseNoCache<EchoResponseDictionary>(parameters);
        }
    }
}
