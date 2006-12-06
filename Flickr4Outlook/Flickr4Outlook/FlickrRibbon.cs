using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using Word = Microsoft.Office.Interop.Word;

namespace Flickr4Outlook
{
    public partial class ThisAddIn
    {
        private FlickrRibbon ribbon;

        protected override object RequestService(Guid serviceGuid)
        {
            if (serviceGuid == typeof(Office.IRibbonExtensibility).GUID)
            {
                if (ribbon == null)
                    ribbon = new FlickrRibbon();
                return ribbon;
            }

            return base.RequestService(serviceGuid);
        }
    }

    [ComVisible(true)]
    public class FlickrRibbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        public FlickrRibbon()
        {
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            string markup = string.Empty;

            switch (ribbonID)
            {
                case "Microsoft.Outlook.Mail.Compose":
                    markup = Properties.Resources.Flickr4OutlookRibbon;
                    break;
            }

            return markup;
        }

        #endregion

        #region Ribbon Callbacks

        public void OnLoad(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        public void OnFlickrInsert(Office.IRibbonControl control)
        {
            Outlook.Inspector inspector = (Outlook.Inspector)control.Context;
            Outlook.MailItem item = (Outlook.MailItem)inspector.CurrentItem;

            Word.Document doc = (Word.Document)inspector.WordEditor;
            Word.Selection select = (Word.Selection)doc.Application.Selection;
            
            string imageUrl = string.Empty;

            using (InsertFlickrImageForm flickrForm = new InsertFlickrImageForm())
            {
                System.Windows.Forms.DialogResult response = flickrForm.ShowDialog();

                if (response == DialogResult.OK)
                {
                    // get the selected image properties
                    FlickrNet.Photo photo = flickrForm.SelectedPhoto;
                    imageUrl = flickrForm.ImageSourceUrl;

                    if (item.BodyFormat == Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatPlain || item.BodyFormat == Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatUnspecified)
                    {
                        select.Text = photo.WebUrl;
                    }
                    else
                    {
                        // TODO: Fix this to force an HTTP based image reference
                        //select.Text = FlickrPluginHelper.GenerateFlickrHtml(photo, imageUrl, string.Empty, "0", string.Empty, string.Empty, string.Empty, flickrForm.EnableHyperLink, flickrForm.FlickrUserId);
                        object missing = Type.Missing;
                        
                        Word.InlineShape shape = select.InlineShapes.AddPicture(imageUrl, ref missing, ref missing, ref missing);

                        if (flickrForm.EnableHyperLink)
                        {
                            object photoTitle = photo.Title;
                            object photoLink = photo.WebUrl;
                            doc.Hyperlinks.Add(shape, ref photoLink, ref missing, ref photoTitle, ref missing, ref missing);
                        }
                    }
                }
            }
        }

        public stdole.IPictureDisp GetImage(Office.IRibbonControl control)
        {
            stdole.IPictureDisp img = null;

            switch (control.Id)
            {
                case "flickrButton":
                    img = PictureDispMaker.ConvertImage(Properties.Resources.FlickrIcon);
                    break;
            }

            return img;
        }

        #endregion

        #region Helpers

        #endregion
    }

    internal class PictureDispMaker : AxHost
    {
        private PictureDispMaker() : base("") { }

        static public stdole.IPictureDisp ConvertImage(Image img)
        {
            return (stdole.IPictureDisp)GetIPictureDispFromPicture(img);
        }

        static public stdole.IPictureDisp ConvertIcon(Icon icn)
        {
            return ConvertImage(icn.ToBitmap());
        }
    }
}