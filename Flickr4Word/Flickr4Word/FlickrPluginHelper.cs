using System;
using System.Windows.Forms;
using System.Drawing;
using FlickrNet;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;

namespace Flickr4Word
{
    class FlickrPluginHelper
    {
        internal static void GotoUrl(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }

        internal static FlickrNet.Flickr GetFlickrProxy()
        {
            FlickrNet.Flickr.CacheDisabled = true;
            FlickrNet.Flickr fproxy = null;

            fproxy = new FlickrNet.Flickr(Flickr4Word.Properties.Resources.FlickrApiKey);

            return fproxy;
        }

        internal static Image GetImageFromUrl(string url)
        {
            Image theImage;

            System.Net.WebRequest http = System.Net.WebRequest.Create(url);
            
            using (System.IO.Stream imageStream = http.GetResponse().GetResponseStream())
            {
                theImage = Image.FromStream(imageStream);
            }

            return theImage;
        }

        internal static string CleanTagFilter(string tagList)
        {
            string cleanedTagList = string.Empty;
            // TODO: use regex here

            // remove spaces
            cleanedTagList = tagList.Replace(" ", string.Empty);

            // replace semicolons with commas
            cleanedTagList = cleanedTagList.Replace(";", ",");

            return cleanedTagList;
        }

        internal static string GenerateFlickrHtml(FlickrNet.Photo selectedPhoto, string imageUrl, string cssClass, string border, string vSpace, string hSpace, string alignment, bool hyperLink, string userId)
        {
            StringBuilder imageTag = new StringBuilder();
            string imageHtml = string.Empty;

            imageTag.Append("<img "); // begin tag
            imageTag.Append(string.Format("src=\"{0}\" ", imageUrl));
            //imageTag.Append(string.Format("alt=\"{0}\" ", HtmlServices.HtmlEncode(selectedPhoto.Title))); // alt required for XHTML
            imageTag.Append(string.Format("border=\"{0}\" ", border));

            if (cssClass.Trim().Length > 0)
            {
                imageTag.Append(string.Format("class=\"{0}\" ", cssClass));
            }

            if (hSpace.Trim().Length > 0 && hSpace != "0")
            {
                imageTag.Append(string.Format("hspace=\"{0}\" ", hSpace));
            }

            if (vSpace.Trim().Length > 0 && vSpace != "0")
            {
                imageTag.Append(string.Format("vspace=\"{0}\" ", vSpace));
            }

            if (alignment.Trim().Length > 0 && alignment.ToLower() != "none")
            {
                imageTag.Append(string.Format("alignment=\"{0}\" ", alignment.ToLower()));
            }

            imageTag.Append("/>"); // end tag XHTML

            imageHtml = imageTag.ToString();

            if (hyperLink)
            {
                imageHtml = string.Format("<a href=\"{0}\" title=\"{2}\">{1}</a>", selectedPhoto.WebUrl, imageHtml, selectedPhoto.Title);
                // TODO: Fix this temp hack in FlicrkNet library -- photoset photos array in FlickrAPI does not include owner attribute
                // thus the serialization lacks the Userid attribute to properly input the WebUrl
                // THIS IS A TEMP HACK TO FIX PHOTOSET ONLY
                imageHtml = FixPhotosetWebUrl(imageHtml, userId);

            }

            return imageHtml;
        }

        // TEMP SHIM FUNCTION
        internal static string FixPhotosetWebUrl(string url, string userId)
        {
            string photosetUrlPattern = "(?<PhotosetWebUrlHack>photos//)";
            return Regex.Replace(url, photosetUrlPattern, string.Format("photos/{0}/", userId));
        }
        
        internal static FlickrNet.FoundUser FindUserByEmailOrName(FlickrNet.Flickr flickrProxy, string criteria)
        {
            FlickrNet.FoundUser user;

            Regex rxp = new Regex("(?<user>[^@]+)@(?<host>.+)");

            if (rxp.IsMatch(criteria))
            {
                user = flickrProxy.PeopleFindByEmail(criteria);
            }
            else
            {
                user = flickrProxy.PeopleFindByUsername(criteria);
            }

            return user;
        }

        internal static Image ScaleToFixedSize(Image imgPhoto, int width, int height, int offset, Color backgroundColor)
        {
            int num1 = imgPhoto.Width;
            int num2 = imgPhoto.Height;
            int num3 = 0;
            int num4 = 0;
            int num5 = offset;
            int num6 = offset;
            float single1 = 0f;
            float single2 = 0f;
            float single3 = 0f;
            single2 = ((float)width) / ((float)num1);
            single3 = ((float)height) / ((float)num2);
            if (single3 < single2)
            {
                single1 = single3;
                num5 = (int)((width - (num1 * single1)) / 2f);
            }
            else
            {
                single1 = single2;
                num6 = (int)((height - (num2 * single1)) / 2f);
            }
            int num7 = (int)(num1 * single1);
            int num8 = (int)(num2 * single1);
            Bitmap bitmap1 = new Bitmap(width, height);
            bitmap1.MakeTransparent(backgroundColor);
            bitmap1.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            Graphics graphics1 = Graphics.FromImage(bitmap1);
            graphics1.Clear(backgroundColor);
            graphics1.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics1.DrawImage(imgPhoto, new Rectangle(num5, num6, num7 - offset, num8 - offset), new Rectangle(num3, num4, num1, num2), GraphicsUnit.Pixel);
            graphics1.Dispose();
            return bitmap1;
        }
    }
}
