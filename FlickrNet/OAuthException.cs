using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace FlickrNet
{
    /// <summary>
    /// An OAuth error occurred when calling one of the OAuth authentication flow methods.
    /// </summary>
    public class OAuthException : Exception
    {
        private string mess;
        private Exception exception;

        /// <summary>
        /// The list of error parameters returned by the OAuth exception.
        /// </summary>
        public Dictionary<string, string> OAuthErrorPameters { get; set; }

        /// <summary>
        /// Constructor for the OAuthException class.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="innerException"></param>
        public OAuthException(string response, Exception innerException) : base("OAuth Exception", innerException)
        {
            OAuthErrorPameters = UtilityMethods.StringToDictionary(response);

            mess = "OAuth Exception occurred: " + OAuthErrorPameters["oauth_problem"];
        }

        public OAuthException(Exception innerException) : base("OAuth Exception", innerException)
        {
            WebException exception = innerException as WebException;
            if (exception == null) return;

            HttpWebResponse res = exception.Response as HttpWebResponse;
            if (res == null) return;

            using(StreamReader sr = new StreamReader(res.GetResponseStream()))
            {

                string response = sr.ReadToEnd();
                OAuthErrorPameters = UtilityMethods.StringToDictionary(response);
                mess = "OAuth Exception occurred: " + OAuthErrorPameters["oauth_problem"];
                sr.Close();
            }
        }

        /// <summary>
        /// The message for the exception.
        /// </summary>
        public override string Message
        {
            get
            {
                return mess;
            }
        }
    }
}
