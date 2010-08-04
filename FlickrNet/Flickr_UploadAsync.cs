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
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void UploadPictureAsync(Stream stream, string fileName, string title, string description, string tags, bool isPublic, bool isFamily, bool isFriend, ContentType contentType, SafetyLevel safetyLevel, HiddenFromSearch hiddenFromSearch, Action<FlickrResult<string>> callback)
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

            UploadDataAsync(stream, fileName, uploadUri, parameters, callback);
        }

        /// <summary>
        /// Replace an existing photo on Flickr.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> object containing the photo to be uploaded.</param>
        /// <param name="fileName">The filename of the file to replace the existing item with.</param>
        /// <param name="photoId">The ID of the photo to replace.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void ReplacePictureAsync(Stream stream, string fileName, string photoId, Action<FlickrResult<string>> callback)
        {
            Uri replaceUri = new Uri(ReplaceUrl);

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("photo_id", photoId);
            parameters.Add("api_key", apiKey);
            parameters.Add("auth_token", apiToken);

            UploadDataAsync(stream, fileName, replaceUri, parameters, callback);
        }

        private void UploadDataAsync(Stream imageStream, string fileName, Uri uploadUri, Dictionary<string, string> parameters, Action<FlickrResult<string>> callback)
        {
            string boundary = "FLICKR_MIME_" + DateTime.Now.ToString("yyyyMMddhhmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            byte[] dataBuffer = CreateUploadData(imageStream, fileName, parameters, boundary);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uploadUri);
            req.Method = "POST";
            req.ContentType = "multipart/form-data; boundary=" + boundary;

            req.BeginGetRequestStream(
                r =>
                {
                    Stream s = req.EndGetRequestStream(r);
                    s.Write(dataBuffer, 0, dataBuffer.Length);
                    s.Close();

                    req.BeginGetResponse(
                        r2 =>
                        {
                            FlickrResult<string> result = new FlickrResult<string>();

                            try
                            {
                                WebResponse res = req.EndGetResponse(r2);
                                StreamReader sr = new StreamReader(res.GetResponseStream());
                                string responseXml = sr.ReadToEnd();
                                sr.Close();

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
                                result.Result = t.GetElementValue("photoid");
                                result.HasError = false;
                            }
                            catch (Exception ex)
                            {
                                result.Error = ex;
                            }

                            callback(result);

                        }, 
                        this);
                }, 
                this);

        }

        private byte[] CreateUploadData(Stream imageStream, string fileName, Dictionary<string, string> parameters, string boundary)
        {
            string[] keys = new string[parameters.Keys.Count];
            parameters.Keys.CopyTo(keys, 0);
            Array.Sort(keys);

            StringBuilder hashStringBuilder = new StringBuilder(sharedSecret, 2 * 1024);
            StringBuilder contentStringBuilder = new StringBuilder();

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

            // Photo
            contentStringBuilder.Append("--" + boundary + "\r\n");
            contentStringBuilder.Append("Content-Disposition: form-data; name=\"photo\"; filename=\"" + Path.GetFileName(fileName) + "\"\r\n");
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
            return dataBuffer;
        }

    }
}
