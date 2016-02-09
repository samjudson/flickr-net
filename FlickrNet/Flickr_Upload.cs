using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Uploads a file to Flickr.
        /// </summary>
        /// <param name="fileName">The filename of the file to open.</param>
        /// <returns>The id of the photo on a successful upload.</returns>
        /// <exception cref="FlickrApiException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
        /// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
        public string UploadPicture(string fileName)
        {
            return UploadPicture(fileName, null, null, null, true, false, false);
        }

        /// <summary>
        /// Uploads a file to Flickr.
        /// </summary>
        /// <param name="fileName">The filename of the file to open.</param>
        /// <param name="title">The title of the photograph.</param>
        /// <returns>The id of the photo on a successful upload.</returns>
        /// <exception cref="FlickrApiException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
        /// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
        public string UploadPicture(string fileName, string title)
        {
            return UploadPicture(fileName, title, null, null, true, false, false);
        }

        /// <summary>
        /// Uploads a file to Flickr.
        /// </summary>
        /// <param name="fileName">The filename of the file to open.</param>
        /// <param name="title">The title of the photograph.</param>
        /// <param name="description">The description of the photograph.</param>
        /// <returns>The id of the photo on a successful upload.</returns>
        /// <exception cref="FlickrApiException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
        /// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
        public string UploadPicture(string fileName, string title, string description)
        {
            return UploadPicture(fileName, title, description, null, true, false, false);
        }

        /// <summary>
        /// Uploads a file to Flickr.
        /// </summary>
        /// <param name="fileName">The filename of the file to open.</param>
        /// <param name="title">The title of the photograph.</param>
        /// <param name="description">The description of the photograph.</param>
        /// <param name="tags">A comma seperated list of the tags to assign to the photograph.</param>
        /// <returns>The id of the photo on a successful upload.</returns>
        /// <exception cref="FlickrApiException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
        /// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
        public string UploadPicture(string fileName, string title, string description, string tags)
        {
            var file = Path.GetFileName(fileName);
            using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var photoId = UploadPicture(stream, file, title, description, tags, false, false, false, ContentType.None, SafetyLevel.None, HiddenFromSearch.None);
                stream.Close();
                return photoId;
            }
        }

        /// <summary>
        /// Uploads a file to Flickr.
        /// </summary>
        /// <param name="fileName">The filename of the file to open.</param>
        /// <param name="title">The title of the photograph.</param>
        /// <param name="description">The description of the photograph.</param>
        /// <param name="tags">A comma seperated list of the tags to assign to the photograph.</param>
        /// <param name="isPublic">True if the photograph should be public and false if it should be private.</param>
        /// <param name="isFriend">True if the photograph should be marked as viewable by friends contacts.</param>
        /// <param name="isFamily">True if the photograph should be marked as viewable by family contacts.</param>
        /// <returns>The id of the photo on a successful upload.</returns>
        /// <exception cref="FlickrApiException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
        /// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
        public string UploadPicture(string fileName, string title, string description, string tags, bool isPublic, bool isFamily, bool isFriend)
        {
            var file = Path.GetFileName(fileName);
            using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var photoId = UploadPicture(stream, file, title, description, tags, isPublic, isFamily, isFriend, ContentType.None, SafetyLevel.None, HiddenFromSearch.None);
                stream.Close();
                return photoId;
            }
        }

        /// <summary>
        /// UploadPicture method that does all the uploading work.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> object containing the pphoto to be uploaded.</param>
        /// <param name="fileName">The filename of the file to upload. Used as the title if title is null.</param>
        /// <param name="title">The title of the photo (optional).</param>
        /// <param name="description">The description of the photograph (optional).</param>
        /// <param name="tags">The tags for the photograph (optional).</param>
        /// <param name="isPublic">false for private, true for public.</param>
        /// <param name="isFamily">true if visible to family.</param>
        /// <param name="isFriend">true if visible to friends only.</param>
        /// <param name="contentType">The content type of the photo, i.e. Photo, Screenshot or Other.</param>
        /// <param name="safetyLevel">The safety level of the photo, i.e. Safe, Moderate or Restricted.</param>
        /// <param name="hiddenFromSearch">Is the photo hidden from public searches.</param>
        /// <returns>The id of the photograph after successful uploading.</returns>
        public string UploadPicture(Stream stream, string fileName, string title, string description, string tags,
                                    bool isPublic, bool isFamily, bool isFriend, ContentType contentType,
                                    SafetyLevel safetyLevel, HiddenFromSearch hiddenFromSearch)
        {
            CheckRequiresAuthentication();

            var uploadUri = new Uri(UploadUrl);

            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(title))
            {
                parameters.Add("title", title);
            }
            if (!string.IsNullOrEmpty(description))
            {
                parameters.Add("description", description);
            }
            if (!string.IsNullOrEmpty(tags))
            {
                parameters.Add("tags", tags);
            }

            parameters.Add("is_public", isPublic ? "1" : "0");
            parameters.Add("is_friend", isFriend ? "1" : "0");
            parameters.Add("is_family", isFamily ? "1" : "0");

            if (safetyLevel != SafetyLevel.None)
            {
                parameters.Add("safety_level", safetyLevel.ToString("D"));
            }
            if (contentType != ContentType.None)
            {
                parameters.Add("content_type", contentType.ToString("D"));
            }
            if (hiddenFromSearch != HiddenFromSearch.None)
            {
                parameters.Add("hidden", hiddenFromSearch.ToString("D"));
            }

            if (!string.IsNullOrEmpty(OAuthAccessToken))
            {
                OAuthGetBasicParameters(parameters);
                parameters.Add("oauth_token", OAuthAccessToken);

                string sig = OAuthCalculateSignature("POST", uploadUri.AbsoluteUri, parameters, OAuthAccessTokenSecret);
                parameters.Add("oauth_signature", sig);
            }
            else
            {
                parameters.Add("api_key", apiKey);
                parameters.Add("auth_token", apiToken);
            }

            string responseXml = UploadData(stream, fileName, uploadUri, parameters);

           var t = new UnknownResponse();
            ((IFlickrParsable) t).Load(responseXml);
            return t.GetElementValue("photoid");
        }

        private string UploadData(Stream imageStream, string fileName, Uri uploadUri, Dictionary<string, string> parameters)
        {
            var boundary = "FLICKR_MIME_" + DateTime.Now.ToString("yyyyMMddhhmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            var authHeader = FlickrResponder.OAuthCalculateAuthHeader(parameters);
            var dataBuffer = CreateUploadData(imageStream, fileName, parameters, boundary);
            
            var req = (HttpWebRequest)WebRequest.Create(uploadUri);
            req.Method = "POST";
            if (Proxy != null) req.Proxy = Proxy;
            req.Timeout = HttpTimeout;
            req.ContentType = "multipart/form-data; boundary=" + boundary;

            if (!string.IsNullOrEmpty(authHeader))
            {
                req.Headers["Authorization"] = authHeader;
            }

            req.AllowWriteStreamBuffering = false;
            req.SendChunked = true;
            //req.ContentLength = dataBuffer.Length;

            using (var reqStream = req.GetRequestStream())
            {
                var bufferSize = 32 * 1024;
                if (dataBuffer.Length / 100 > bufferSize) bufferSize = bufferSize * 2;
                dataBuffer.UploadProgress += (o, e) => { if( OnUploadProgress != null ) OnUploadProgress(this, e); };
                dataBuffer.CopyTo(reqStream, bufferSize);
                reqStream.Flush();
            }

            var res = (HttpWebResponse)req.GetResponse();
            var stream = res.GetResponseStream();
            if( stream == null) throw new FlickrWebException("Unable to retrieve stream from web response.");

            var sr = new StreamReader(stream);
            var s = sr.ReadToEnd();
            sr.Close();
            return s;
        }

        /// <summary>
        /// Replace an existing photo on Flickr.
        /// </summary>
        /// <param name="fullFileName">The full filename of the photo to upload.</param>
        /// <param name="photoId">The ID of the photo to replace.</param>
        /// <returns>The id of the photograph after successful uploading.</returns>
        public string ReplacePicture(string fullFileName, string photoId)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                return ReplacePicture(stream, fullFileName, photoId);
            }
            finally
            {
                if (stream != null) stream.Close();
            }

        }

        /// <summary>
        /// Replace an existing photo on Flickr.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> object containing the photo to be uploaded.</param>
        /// <param name="fileName">The filename of the file to replace the existing item with.</param>
        /// <param name="photoId">The ID of the photo to replace.</param>
        /// <returns>The id of the photograph after successful uploading.</returns>
        public string ReplacePicture(Stream stream, string fileName, string photoId)
        {

            var replaceUri = new Uri(ReplaceUrl);

            var parameters = new Dictionary<string, string>
                                 {
                                     {"photo_id", photoId}
                                 };

            if (!string.IsNullOrEmpty(OAuthAccessToken))
            {
                OAuthGetBasicParameters(parameters);
                parameters.Add("oauth_token", OAuthAccessToken);

                var sig = OAuthCalculateSignature("POST", replaceUri.AbsoluteUri, parameters, OAuthAccessTokenSecret);
                parameters.Add("oauth_signature", sig);
            }
            else
            {
                parameters.Add("api_key", apiKey);
                parameters.Add("auth_token", apiToken);
            }

            var responseXml = UploadData(stream, fileName, replaceUri, parameters);
            
            var t = new UnknownResponse();
            ((IFlickrParsable)t).Load(responseXml);
            return t.GetElementValue("photoid");
        }

    }
}
