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
        /// <returns>An array of <see cref="XmlElement"/> instances which is the expected response.</returns>
        public XmlElement[] TestGeneric(string method, NameValueCollection parameters)
        {
            Hashtable _parameters = new Hashtable();
            if (parameters != null)
            {
                foreach (string key in parameters.AllKeys)
                {
                    _parameters.Add(key, parameters[key]);
                }
            }
            _parameters.Add("method", method);

            FlickrNet.Response response = GetResponseNoCache(_parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.AllElements;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }
        /// <summary>
        /// Runs the flickr.test.echo method and returned an array of <see cref="XmlElement"/> items.
        /// </summary>
        /// <param name="echoParameter">The parameter to pass to the method.</param>
        /// <param name="echoValue">The value to pass to the method with the parameter.</param>
        /// <returns>An array of <see cref="XmlElement"/> items.</returns>
        /// <remarks>
        /// The APi Key has been removed from the returned array and will not be shown.
        /// </remarks>
        /// <example>
        /// <code>
        /// XmlElement[] elements = flickr.TestEcho("&amp;param=value");
        /// foreach(XmlElement element in elements)
        /// {
        ///		if( element.Name = "method" )
        ///			Console.WriteLine("Method = " + element.InnerXml);
        ///		if( element.Name = "param" )
        ///			Console.WriteLine("Param = " + element.InnerXml);
        /// }
        /// </code>
        /// </example>
        public XmlElement[] TestEcho(string echoParameter, string echoValue)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.test.echo");
            parameters.Add("api_key", _apiKey);
            if (echoParameter != null && echoParameter.Length > 0)
            {
                parameters.Add(echoParameter, echoValue);
            }

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                // Remove the api_key element from the array.
                XmlElement[] elements = new XmlElement[response.AllElements.Length - 1];
                int c = 0;
                foreach (XmlElement element in response.AllElements)
                {
                    if (element.Name != "api_key")
                        elements[c++] = element;
                }
                return elements;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Test the logged in state of the current Filckr object.
        /// </summary>
        /// <returns>The <see cref="FoundUser"/> object containing the username and userid of the current user.</returns>
        public FoundUser TestLogin()
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.test.login");

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return new FoundUser(response.AllElements[0]);
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
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


    }
}
