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
using Word = Microsoft.Office.Interop.Word;

namespace Flickr4Word
{
    public partial class ThisAddIn
    {
        private Flickr4WordRibbon ribbon;

        protected override object RequestService(Guid serviceGuid)
        {
            if (serviceGuid == typeof(Office.IRibbonExtensibility).GUID)
            {
                if (ribbon == null)
                    ribbon = new Flickr4WordRibbon();
                return ribbon;
            }

            return base.RequestService(serviceGuid);
        }
    }

    [ComVisible(true)]
    public class Flickr4WordRibbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        public Flickr4WordRibbon()
        {
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            string markup = Properties.Resources.Flickr4WordRibbonUI;

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
            Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;
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
                    
                    object missing = Type.Missing;
                                        
                    Word.InlineShape shape = select.InlineShapes.AddPicture(imageUrl, ref missing, ref missing, ref missing);
                    if (flickrForm.EnableHyperLink)
                    {
                        object photoLink = photo.WebUrl;
                        object photoScreenTip = photo.Title;
                        doc.Hyperlinks.Add(shape.Range, ref photoLink, ref missing, ref photoScreenTip, ref missing, ref missing);
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
