using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;
using System.Xml.Serialization;

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
            string file = Path.GetFileName(fileName);
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return UploadPicture(stream, file, title, description, tags, false, false, false, ContentType.None, SafetyLevel.None, HiddenFromSearch.None);
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
            string file = Path.GetFileName(fileName);
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return UploadPicture(stream, file, title, description, tags, isPublic, isFamily, isFriend, ContentType.None, SafetyLevel.None, HiddenFromSearch.None);
        }

        /// <summary>
        /// UploadPicture method that does all the uploading work.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> object containing the pphoto to be uploaded.</param>
        /// <param name="fileName">The filename of the file to upload. Used as the title if title is null.</param>
        /// <param name="title">The title of the photo (optional).</param>
        /// <param name="description">The description of the photograph (optional).</param>
        /// <param name="tags">The tags for the photograph (optional).</param>
        /// <param name="isPublic">0 for private, 1 for public.</param>
        /// <param name="isFamily">1 if family, 0 is not.</param>
        /// <param name="isFriend">1 if friend, 0 if not.</param>
        /// <param name="contentType">The content type of the photo, i.e. Photo, Screenshot or Other.</param>
        /// <param name="safetyLevel">The safety level of the photo, i.e. Safe, Moderate or Restricted.</param>
        /// <param name="hiddenFromSearch">Is the photo hidden from public searches.</param>
        /// <returns>The id of the photograph after successful uploading.</returns>
        public string UploadPicture(Stream stream, string fileName, string title, string description, string tags, bool isPublic, bool isFamily, bool isFriend, ContentType contentType, SafetyLevel safetyLevel, HiddenFromSearch hiddenFromSearch)
        {
            CheckRequiresAuthentication();
            /*
             * 
             * Modified UploadPicture code taken from the Flickr.Net library
             * URL: http://workspaces.gotdotnet.com/flickrdotnet
             * It is used under the terms of the Common Public License 1.0
             * URL: http://www.opensource.org/licenses/cpl.php
             * 
             * */

            string boundary = "FLICKR_MIME_" + DateTime.Now.ToString("yyyyMMddhhmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(UploadUrl);
            req.UserAgent = "Mozilla/4.0 FlickrNet API (compatible; MSIE 6.0; Windows NT 5.1)";
            req.Method = "POST";
            if (Proxy != null) req.Proxy = Proxy;
            //req.Referer = "http://www.flickr.com";
            req.KeepAlive = true;
            req.Timeout = HttpTimeout;
            req.ContentType = "multipart/form-data; boundary=" + boundary + "";
            req.Expect = "";

            StringBuilder sb = new StringBuilder();

            Hashtable parameters = new Hashtable();

            if (title != null && title.Length > 0)
            {
                parameters.Add("title", title);
            }
            if (description != null && description.Length > 0)
            {
                parameters.Add("description", description);
            }
            if (tags != null && tags.Length > 0)
            {
                parameters.Add("tags", tags);
            }

            parameters.Add("is_public", isPublic ? "1" : "0");
            parameters.Add("is_friend", isFriend ? "1" : "0");
            parameters.Add("is_family", isFamily ? "1" : "0");

            if (safetyLevel != SafetyLevel.None)
            {
                parameters.Add("safety_level", (int)safetyLevel);
            }
            if (contentType != ContentType.None)
            {
                parameters.Add("content_type", (int)contentType);
            }
            if (hiddenFromSearch != HiddenFromSearch.None)
            {
                parameters.Add("hidden", (int)hiddenFromSearch);
            }

            parameters.Add("api_key", _apiKey);
            parameters.Add("auth_token", _apiToken);

            string[] keys = new string[parameters.Keys.Count];
            parameters.Keys.CopyTo(keys, 0);
            Array.Sort(keys);

            StringBuilder HashStringBuilder = new StringBuilder(_sharedSecret, 2 * 1024);

            foreach (string key in keys)
            {
                HashStringBuilder.Append(key);
                HashStringBuilder.Append(parameters[key]);
                sb.Append("--" + boundary + "\r\n");
                sb.Append("Content-Disposition: form-data; name=\"" + key + "\"\r\n");
                sb.Append("\r\n");
                sb.Append(parameters[key] + "\r\n");
            }

            sb.Append("--" + boundary + "\r\n");
            sb.Append("Content-Disposition: form-data; name=\"api_sig\"\r\n");
            sb.Append("\r\n");
            sb.Append(UtilityMethods.MD5Hash(HashStringBuilder.ToString()) + "\r\n");

            // Photo
            sb.Append("--" + boundary + "\r\n");
            sb.Append("Content-Disposition: form-data; name=\"photo\"; filename=\"" + fileName + "\"\r\n");
            sb.Append("Content-Type: image/jpeg\r\n");
            sb.Append("\r\n");

            UTF8Encoding encoding = new UTF8Encoding();

            byte[] postContents = encoding.GetBytes(sb.ToString());

            byte[] photoContents = new byte[stream.Length];
            stream.Read(photoContents, 0, photoContents.Length);
            stream.Close();

            byte[] postFooter = encoding.GetBytes("\r\n--" + boundary + "--\r\n");

            byte[] dataBuffer = new byte[postContents.Length + photoContents.Length + postFooter.Length];
            Buffer.BlockCopy(postContents, 0, dataBuffer, 0, postContents.Length);
            Buffer.BlockCopy(photoContents, 0, dataBuffer, postContents.Length, photoContents.Length);
            Buffer.BlockCopy(postFooter, 0, dataBuffer, postContents.Length + photoContents.Length, postFooter.Length);

            req.ContentLength = dataBuffer.Length;

            Stream resStream = req.GetRequestStream();

            int j = 1;
            int uploadBit = Math.Max(dataBuffer.Length / 100, 50 * 1024);
            int uploadSoFar = 0;

            for (int i = 0; i < dataBuffer.Length; i = i + uploadBit)
            {
                int toUpload = Math.Min(uploadBit, dataBuffer.Length - i);
                uploadSoFar += toUpload;

                resStream.Write(dataBuffer, i, toUpload);

                if ((OnUploadProgress != null) && ((j++) % 5 == 0 || uploadSoFar == dataBuffer.Length))
                {
                    OnUploadProgress(this, new UploadProgressEventArgs(i + toUpload, uploadSoFar == dataBuffer.Length));
                }
            }
            resStream.Close();

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            XmlSerializer serializer = _uploaderSerializer;

            StreamReader sr = new StreamReader(res.GetResponseStream());
            string s = sr.ReadToEnd();
            sr.Close();

            StringReader str = new StringReader(s);

            FlickrNet.UploadResponse uploader = (FlickrNet.UploadResponse)serializer.Deserialize(str);

            if (uploader.Status == ResponseStatus.Ok)
            {
                return uploader.PhotoId;
            }
            else
            {
                throw new FlickrApiException(uploader.Error.Code, uploader.Error.Message);
            }
        }

        /// <summary>
        /// Replace an existing photo on Flickr.
        /// </summary>
        /// <param name="fileName">The filename of the photo to upload.</param>
        /// <param name="photoId">The ID of the photo to replace.</param>
        /// <returns>The id of the photograph after successful uploading.</returns>
        public string ReplacePicture(string fileName, string photoId)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                return ReplacePicture(stream, photoId);
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
        /// <param name="photoId">The ID of the photo to replace.</param>
        /// <returns>The id of the photograph after successful uploading.</returns>
        public string ReplacePicture(Stream stream, string photoId)
        {
            string boundary = "FLICKR_MIME_" + DateTime.Now.ToString("yyyyMMddhhmmss", System.Globalization.NumberFormatInfo.InvariantInfo);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(ReplaceUrl);
            req.UserAgent = "Mozilla/4.0 FlickrNet API (compatible; MSIE 6.0; Windows NT 5.1)";
            req.Method = "POST";
            if (Proxy != null) req.Proxy = Proxy;
            req.Referer = "http://www.flickr.com";
            req.KeepAlive = false;
            req.Timeout = HttpTimeout;
            req.ContentType = "multipart/form-data; boundary=" + boundary + "";

            StringBuilder sb = new StringBuilder();

            Hashtable parameters = new Hashtable();

            parameters.Add("photo_id", photoId);
            parameters.Add("api_key", _apiKey);
            parameters.Add("auth_token", _apiToken);

            string[] keys = new string[parameters.Keys.Count];
            parameters.Keys.CopyTo(keys, 0);
            Array.Sort(keys);

            StringBuilder HashStringBuilder = new StringBuilder(_sharedSecret, 2 * 1024);

            foreach (string key in keys)
            {
                HashStringBuilder.Append(key);
                HashStringBuilder.Append(parameters[key]);
                sb.Append("--" + boundary + "\r\n");
                sb.Append("Content-Disposition: form-data; name=\"" + key + "\"\r\n");
                sb.Append("\r\n");
                sb.Append(parameters[key] + "\r\n");
            }

            sb.Append("--" + boundary + "\r\n");
            sb.Append("Content-Disposition: form-data; name=\"api_sig\"\r\n");
            sb.Append("\r\n");
            sb.Append(UtilityMethods.MD5Hash(HashStringBuilder.ToString()) + "\r\n");

            // Photo
            sb.Append("--" + boundary + "\r\n");
            sb.Append("Content-Disposition: form-data; name=\"photo\"; filename=\"image.jpeg\"\r\n");
            sb.Append("Content-Type: image/jpeg\r\n");
            sb.Append("\r\n");

            UTF8Encoding encoding = new UTF8Encoding();

            byte[] postContents = encoding.GetBytes(sb.ToString());

            byte[] photoContents = new byte[stream.Length];
            stream.Read(photoContents, 0, photoContents.Length);
            stream.Close();

            byte[] postFooter = encoding.GetBytes("\r\n--" + boundary + "--\r\n");

            byte[] dataBuffer = new byte[postContents.Length + photoContents.Length + postFooter.Length];
            Buffer.BlockCopy(postContents, 0, dataBuffer, 0, postContents.Length);
            Buffer.BlockCopy(photoContents, 0, dataBuffer, postContents.Length, photoContents.Length);
            Buffer.BlockCopy(postFooter, 0, dataBuffer, postContents.Length + photoContents.Length, postFooter.Length);

            req.ContentLength = dataBuffer.Length;

            Stream resStream = req.GetRequestStream();

            int j = 1;
            int uploadBit = Math.Max(dataBuffer.Length / 100, 50 * 1024);
            int uploadSoFar = 0;

            for (int i = 0; i < dataBuffer.Length; i = i + uploadBit)
            {
                int toUpload = Math.Min(uploadBit, dataBuffer.Length - i);
                uploadSoFar += toUpload;

                resStream.Write(dataBuffer, i, toUpload);

                if ((OnUploadProgress != null) && ((j++) % 5 == 0 || uploadSoFar == dataBuffer.Length))
                {
                    OnUploadProgress(this, new UploadProgressEventArgs(i + toUpload, uploadSoFar == dataBuffer.Length));
                }
            }
            resStream.Close();

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            XmlSerializer serializer = _uploaderSerializer;

            StreamReader sr = new StreamReader(res.GetResponseStream());
            string s = sr.ReadToEnd();
            sr.Close();

            StringReader str = new StringReader(s);

            FlickrNet.UploadResponse uploader = (FlickrNet.UploadResponse)serializer.Deserialize(str);

            if (uploader.Status == ResponseStatus.Ok)
            {
                return uploader.PhotoId;
            }
            else
            {
                throw new FlickrApiException(uploader.Error.Code, uploader.Error.Message);
            }
        }

    }
}
