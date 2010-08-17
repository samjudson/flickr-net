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

            parameters.Add("api_key", apiKey);
            parameters.Add("auth_token", apiToken);

            string responseXml = UploadData(stream, fileName, uploadUri, parameters);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(new StringReader(responseXml), settings);

            if (!reader.ReadToDescendant("rsp"))
            {
                throw new XmlException("Unable to find response element 'rsp' in Flickr response");
            }
            while (reader.MoveToNextAttribute())
            {
                if (reader.LocalName == "stat" && reader.Value == "fail")
                    throw ExceptionHandler.CreateResponseException(reader);
                continue;
            }

            reader.MoveToElement();
            reader.Read();

            UnknownResponse t = new UnknownResponse();
            ((IFlickrParsable)t).Load(reader);
            return t.GetElementValue("photoid");
        }

        private string UploadData(Stream imageStream, string fileName, Uri uploadUri, Dictionary<string, string> parameters)
        {
            string boundary = "FLICKR_MIME_" + DateTime.Now.ToString("yyyyMMddhhmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            byte[] dataBuffer = CreateUploadData(imageStream, fileName, parameters, boundary);
            
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uploadUri);
            //req.UserAgent = "Mozilla/4.0 FlickrNet API (compatible; MSIE 6.0; Windows NT 5.1)";
            req.Method = "POST";
            if (Proxy != null) req.Proxy = Proxy;
            //req.Timeout = HttpTimeout;
            req.ContentType = "multipart/form-data; boundary=" + boundary;
            //req.Expect = String.Empty;

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
            parameters.Add("api_key", apiKey);
            parameters.Add("auth_token", apiToken);

            string responseXml = UploadData(stream, fileName, replaceUri, parameters);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(new StringReader(responseXml), settings);

            if (!reader.ReadToDescendant("rsp"))
            {
                throw new XmlException("Unable to find response element 'rsp' in Flickr response");
            }
            while (reader.MoveToNextAttribute())
            {
                if (reader.LocalName == "stat" && reader.Value == "fail")
                    throw ExceptionHandler.CreateResponseException(reader);
                continue;
            }

            reader.MoveToElement();
            reader.Read();

            UnknownResponse t = new UnknownResponse();
            ((IFlickrParsable)t).Load(reader);
            return t.GetElementValue("photoid");
        }

    }
}
