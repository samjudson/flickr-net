using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;
using System.Xml.Serialization;
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
        /// <param name="isPublic">false for private, true for public.</param>
        /// <param name="isFamily">true if visible to family.</param>
        /// <param name="isFriend">true if visible to friends only.</param>
        /// <param name="contentType">The content type of the photo, i.e. Photo, Screenshot or Other.</param>
        /// <param name="safetyLevel">The safety level of the photo, i.e. Safe, Moderate or Restricted.</param>
        /// <param name="hiddenFromSearch">Is the photo hidden from public searches.</param>
        /// <returns>The id of the photograph after successful uploading.</returns>
        public string UploadPicture(Stream stream, string fileName, string title, string description, string tags, bool isPublic, bool isFamily, bool isFriend, ContentType contentType, SafetyLevel safetyLevel, HiddenFromSearch hiddenFromSearch)
        {
            CheckRequiresAuthentication();

            Uri uploadUri = new Uri(UploadUrl);

            Dictionary<string, string> parameters = new Dictionary<string, string>();

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
                parameters.Add("safety_level", safetyLevel.ToString("d"));
            }
            if (contentType != ContentType.None)
            {
                parameters.Add("content_type", contentType.ToString("d"));
            }
            if (hiddenFromSearch != HiddenFromSearch.None)
            {
                parameters.Add("hidden", hiddenFromSearch.ToString("d"));
            }

            parameters.Add("api_key", _apiKey);
            parameters.Add("auth_token", _apiToken);

            string s = UploadData(stream, fileName, uploadUri, parameters);

            StringReader str = new StringReader(s);
            XmlReader reader = XmlReader.Create(str);

            UploadResponse response = new UploadResponse();
            ((IFlickrParsable)response).Load(reader);

            return response.PhotoId;
        }

        private string UploadData(Stream imageStream, string fileName, Uri uploadUri, Dictionary<string, string> parameters)
        {
            string[] keys = new string[parameters.Keys.Count];
            parameters.Keys.CopyTo(keys, 0);
            Array.Sort(keys);

            StringBuilder hashStringBuilder = new StringBuilder(_sharedSecret, 2 * 1024);
            StringBuilder contentStringBuilder = new StringBuilder();
            string boundary = "FLICKR_MIME_" + DateTime.Now.ToString("yyyyMMddhhmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            foreach (string key in keys)
            {
                hashStringBuilder.Append(key);
                hashStringBuilder.Append(parameters[key]);
                contentStringBuilder.Append("--" + boundary + "\r\n");
                contentStringBuilder.Append("Content-Disposition: form-data; name=\"" + key + "\"\r\n");
                contentStringBuilder.Append("\r\n");
                contentStringBuilder.Append(parameters[key] + "\r\n");
            }

            contentStringBuilder.Append("--" + boundary + "\r\n");
            contentStringBuilder.Append("Content-Disposition: form-data; name=\"api_sig\"\r\n");
            contentStringBuilder.Append("\r\n");
            contentStringBuilder.Append(UtilityMethods.MD5Hash(hashStringBuilder.ToString()) + "\r\n");

            fileName = Path.GetFileName(fileName);

            // Photo
            contentStringBuilder.Append("--" + boundary + "\r\n");
            contentStringBuilder.Append("Content-Disposition: form-data; name=\"photo\"; filename=\"" + fileName + "\"\r\n");
            contentStringBuilder.Append("Content-Type: image/jpeg\r\n");
            contentStringBuilder.Append("\r\n");

            UTF8Encoding encoding = new UTF8Encoding();

            byte[] postContents = encoding.GetBytes(contentStringBuilder.ToString());

            byte[] photoContents = new byte[imageStream.Length];
            imageStream.Read(photoContents, 0, photoContents.Length);
            imageStream.Close();

            byte[] postFooter = encoding.GetBytes("\r\n--" + boundary + "--\r\n");

            byte[] dataBuffer = new byte[postContents.Length + photoContents.Length + postFooter.Length];
            Buffer.BlockCopy(postContents, 0, dataBuffer, 0, postContents.Length);
            Buffer.BlockCopy(photoContents, 0, dataBuffer, postContents.Length, photoContents.Length);
            Buffer.BlockCopy(postFooter, 0, dataBuffer, postContents.Length + photoContents.Length, postFooter.Length);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uploadUri);
            req.Method = "POST";
            req.ContentType = "multipart/form-data; boundary=" + boundary + "";

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

            StreamReader sr = new StreamReader(res.GetResponseStream());
            string s = sr.ReadToEnd();
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

            Uri replaceUri = new Uri(ReplaceUrl);

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("photo_id", photoId);
            parameters.Add("api_key", _apiKey);
            parameters.Add("auth_token", _apiToken);

            string s = UploadData(stream, fileName, replaceUri, parameters);

            StringReader str = new StringReader(s);
            XmlReader reader = XmlReader.Create(str);

            UploadResponse response = new UploadResponse();
            ((IFlickrParsable)response).Load(reader);

            return response.PhotoId;
        }

    }
}
