using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FlickrNet;
using System.Threading;
using System.IO;
using System.Net;

namespace Flickr4Outlook
{
    public class InsertFlickrImageForm : Form
    {
        #region Globals and Constants
        private const int PER_PAGE = 8;
        private const string FLICKR_SIGNUP = "http://www.flickr.com/signup";
        private const string FLICKR_URL = "http://www.flickr.com";
        #endregion

        #region Member Variables
        private RadioButton rdbUploadedBySet;
        private RadioButton rdbSearchTypeSet;
        private Label label2;
        private ComboBox dropDownImageSizes;
        private GroupBox groupLayout;
        private ComboBox dropDownAlignment;
        private Label label3;
        private NumericUpDown verticalPadding;
        private Label label6;
        private NumericUpDown borderThickness;
        private Label label5;
        private NumericUpDown horizontalPadding;
        private Label label4;
        private int _currentPage = 1;
        private FlickrNet.Flickr flickrProxy;
        private string _userId = string.Empty;
        private FlickrNet.Photo _currentPhoto;
        private string _srcUrl;
        private TextBox textBoxCssClass;
        private Label label7;
        private CheckBox checkHyperLink;
        private PictureBox flickrLogo;
        private Label label9;
        private LinkLabel flickrSignup;
        private ToolTip toolTip1;
        private PictureBox infoButton;
        private long _totalPages;
        private StatusStrip statusStrip1;
        private ToolTip toolTip2;
        private ImageList imageListing;
        private ListView retrievedImageList;
        private Button buttonGetImages;
        private Button pagePrevious;
        private Button pageNext;
        private bool _images = false;
        private ToolTip tipUser;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel statusPictureCount;
        private ToolStripStatusLabel statusPage;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private BackgroundWorker imageProcessor;
        private ToolStripProgressBar progressBar;
        private GroupBox groupUploadedBy;
        private RadioButton rdbUserName;
        private RadioButton rdbAnyone;
        private GroupBox groupSearchType;
        private RadioButton rdbTags;
        private RadioButton rdbPhotoset;
        private RadioButton rdbAllPhotos;
        private TextBox textBoxTagFilter;
        private ComboBox photosetList;
        #endregion

        #region Public Properties
        public string FlickrUserId
        {
            get { return _userId; }
        }
        public string Alignment
        {
            get { return dropDownAlignment.Text.Trim(); }
        }
        public bool EnableHyperLink
        {
            get { return checkHyperLink.Checked; }
        }
        public string CssClass
        {
            get { return textBoxCssClass.Text.Trim(); }
        }

        public string FlickrUserName
        {
            get { return textboxFlickrUserName.Text.Trim(); }
        }       

        public string BorderThickness
        {
            get { return borderThickness.Value.ToString().Trim(); }
        }        

        public string HorizontalPadding
        {
            get { return horizontalPadding.Value.ToString().Trim(); }
        }        

        public string VerticalPadding
        {
            get { return verticalPadding.Value.ToString().Trim(); }
        }       
    
        public FlickrNet.Photo SelectedPhoto
        {
            get { return _currentPhoto; }
        }

        public string ImageSourceUrl
        {
            get { return _srcUrl; }
        }
        #endregion

        #region Required Designer Code
        // NOTE: This is here because we are using VS2005 to develop and MSBee to compile
        // At this time, MSBee did not support partial classes

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsertFlickrImageForm));
            this.buttonInsert = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textboxFlickrUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dropDownImageSizes = new System.Windows.Forms.ComboBox();
            this.groupLayout = new System.Windows.Forms.GroupBox();
            this.checkHyperLink = new System.Windows.Forms.CheckBox();
            this.textBoxCssClass = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.verticalPadding = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.borderThickness = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.horizontalPadding = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.dropDownAlignment = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.flickrSignup = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonGetImages = new System.Windows.Forms.Button();
            this.infoButton = new System.Windows.Forms.PictureBox();
            this.flickrLogo = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusPictureCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusPage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.imageListing = new System.Windows.Forms.ImageList(this.components);
            this.retrievedImageList = new System.Windows.Forms.ListView();
            this.pagePrevious = new System.Windows.Forms.Button();
            this.pageNext = new System.Windows.Forms.Button();
            this.tipUser = new System.Windows.Forms.ToolTip(this.components);
            this.imageProcessor = new System.ComponentModel.BackgroundWorker();
            this.groupUploadedBy = new System.Windows.Forms.GroupBox();
            this.rdbUserName = new System.Windows.Forms.RadioButton();
            this.rdbAnyone = new System.Windows.Forms.RadioButton();
            this.groupSearchType = new System.Windows.Forms.GroupBox();
            this.textBoxTagFilter = new System.Windows.Forms.TextBox();
            this.photosetList = new System.Windows.Forms.ComboBox();
            this.rdbTags = new System.Windows.Forms.RadioButton();
            this.rdbPhotoset = new System.Windows.Forms.RadioButton();
            this.rdbAllPhotos = new System.Windows.Forms.RadioButton();
            this.groupLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.verticalPadding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.borderThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalPadding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flickrLogo)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupUploadedBy.SuspendLayout();
            this.groupSearchType.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonInsert
            // 
            this.buttonInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInsert.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonInsert.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInsert.Location = new System.Drawing.Point(432, 598);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(88, 23);
            this.buttonInsert.TabIndex = 9;
            this.buttonInsert.Text = "Insert Image";
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(526, 598);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textboxFlickrUserName
            // 
            this.textboxFlickrUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxFlickrUserName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxFlickrUserName.Location = new System.Drawing.Point(20, 67);
            this.textboxFlickrUserName.Name = "textboxFlickrUserName";
            this.textboxFlickrUserName.Size = new System.Drawing.Size(200, 21);
            this.textboxFlickrUserName.TabIndex = 2;
            this.textboxFlickrUserName.TextChanged += new System.EventHandler(this.textboxFlickrUserName_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(419, 534);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Picture Size:";
            // 
            // dropDownImageSizes
            // 
            this.dropDownImageSizes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dropDownImageSizes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownImageSizes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropDownImageSizes.Items.AddRange(new object[] {
            "Thumbnail",
            "Small",
            "Medium",
            "Large"});
            this.dropDownImageSizes.Location = new System.Drawing.Point(493, 531);
            this.dropDownImageSizes.Name = "dropDownImageSizes";
            this.dropDownImageSizes.Size = new System.Drawing.Size(121, 21);
            this.dropDownImageSizes.TabIndex = 8;
            this.dropDownImageSizes.SelectedIndexChanged += new System.EventHandler(this.GenericUpdateChange);
            // 
            // groupLayout
            // 
            this.groupLayout.Controls.Add(this.textBoxCssClass);
            this.groupLayout.Controls.Add(this.label7);
            this.groupLayout.Controls.Add(this.verticalPadding);
            this.groupLayout.Controls.Add(this.label6);
            this.groupLayout.Controls.Add(this.borderThickness);
            this.groupLayout.Controls.Add(this.label5);
            this.groupLayout.Controls.Add(this.horizontalPadding);
            this.groupLayout.Controls.Add(this.label4);
            this.groupLayout.Controls.Add(this.dropDownAlignment);
            this.groupLayout.Controls.Add(this.label3);
            this.groupLayout.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupLayout.Location = new System.Drawing.Point(16, 518);
            this.groupLayout.Name = "groupLayout";
            this.groupLayout.Size = new System.Drawing.Size(341, 106);
            this.groupLayout.TabIndex = 7;
            this.groupLayout.TabStop = false;
            this.groupLayout.Text = "Layout";
            this.groupLayout.Visible = false;
            // 
            // checkHyperLink
            // 
            this.checkHyperLink.Checked = true;
            this.checkHyperLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkHyperLink.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkHyperLink.Location = new System.Drawing.Point(469, 563);
            this.checkHyperLink.Name = "checkHyperLink";
            this.checkHyperLink.Size = new System.Drawing.Size(143, 17);
            this.checkHyperLink.TabIndex = 6;
            this.checkHyperLink.Text = "Include link to image";
            // 
            // textBoxCssClass
            // 
            this.textBoxCssClass.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCssClass.Location = new System.Drawing.Point(73, 74);
            this.textBoxCssClass.Name = "textBoxCssClass";
            this.textBoxCssClass.Size = new System.Drawing.Size(80, 21);
            this.textBoxCssClass.TabIndex = 5;
            this.textBoxCssClass.TextChanged += new System.EventHandler(this.GenericUpdateChange);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "CSS Class:";
            // 
            // verticalPadding
            // 
            this.verticalPadding.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verticalPadding.Location = new System.Drawing.Point(284, 45);
            this.verticalPadding.Name = "verticalPadding";
            this.verticalPadding.Size = new System.Drawing.Size(46, 21);
            this.verticalPadding.TabIndex = 4;
            this.verticalPadding.ValueChanged += new System.EventHandler(this.GenericUpdateChange);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(171, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Vertical spacing:";
            // 
            // borderThickness
            // 
            this.borderThickness.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.borderThickness.Location = new System.Drawing.Point(103, 45);
            this.borderThickness.Name = "borderThickness";
            this.borderThickness.Size = new System.Drawing.Size(50, 21);
            this.borderThickness.TabIndex = 3;
            this.borderThickness.ValueChanged += new System.EventHandler(this.GenericUpdateChange);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Border thickness:";
            // 
            // horizontalPadding
            // 
            this.horizontalPadding.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.horizontalPadding.Location = new System.Drawing.Point(284, 17);
            this.horizontalPadding.Name = "horizontalPadding";
            this.horizontalPadding.Size = new System.Drawing.Size(46, 21);
            this.horizontalPadding.TabIndex = 2;
            this.horizontalPadding.ValueChanged += new System.EventHandler(this.GenericUpdateChange);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(171, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Horizontal spacing:";
            // 
            // dropDownAlignment
            // 
            this.dropDownAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownAlignment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropDownAlignment.Items.AddRange(new object[] {
            "None",
            "Left",
            "Right",
            "TextTop",
            "Middle",
            "Bottom",
            "Baseline"});
            this.dropDownAlignment.Location = new System.Drawing.Point(73, 17);
            this.dropDownAlignment.Name = "dropDownAlignment";
            this.dropDownAlignment.Size = new System.Drawing.Size(80, 21);
            this.dropDownAlignment.TabIndex = 1;
            this.dropDownAlignment.SelectedIndexChanged += new System.EventHandler(this.GenericUpdateChange);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Alignment:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(481, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "New to Flickr?";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flickrSignup
            // 
            this.flickrSignup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flickrSignup.AutoSize = true;
            this.flickrSignup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flickrSignup.Location = new System.Drawing.Point(561, 9);
            this.flickrSignup.Name = "flickrSignup";
            this.flickrSignup.Size = new System.Drawing.Size(51, 13);
            this.flickrSignup.TabIndex = 0;
            this.flickrSignup.TabStop = true;
            this.flickrSignup.Text = "Signup...";
            this.flickrSignup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.flickrSignup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.flickrSignup_LinkClicked);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Searching";
            // 
            // buttonGetImages
            // 
            this.buttonGetImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetImages.Image = global::Flickr4Outlook.Properties.Resources.search16;
            this.buttonGetImages.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGetImages.Location = new System.Drawing.Point(550, 81);
            this.buttonGetImages.Name = "buttonGetImages";
            this.buttonGetImages.Size = new System.Drawing.Size(64, 44);
            this.buttonGetImages.TabIndex = 3;
            this.buttonGetImages.Text = "Search";
            this.buttonGetImages.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonGetImages.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonGetImages, "Click here to search the Flickr user account for images.\r\n\r\nYou can enter tags to" +
                    " filter (comma separated) or leave\r\nit blank to show all.");
            this.buttonGetImages.Click += new System.EventHandler(this.buttonGetImages_Click);
            // 
            // infoButton
            // 
            this.infoButton.Cursor = System.Windows.Forms.Cursors.Help;
            this.infoButton.Image = global::Flickr4Outlook.Properties.Resources.Info;
            this.infoButton.Location = new System.Drawing.Point(182, 12);
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size(16, 16);
            this.infoButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.infoButton.TabIndex = 20;
            this.infoButton.TabStop = false;
            this.infoButton.Click += new System.EventHandler(this.aboutPlugin_Click);
            // 
            // flickrLogo
            // 
            this.flickrLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flickrLogo.Image = global::Flickr4Outlook.Properties.Resources.flickr_gamma;
            this.flickrLogo.Location = new System.Drawing.Point(19, 13);
            this.flickrLogo.Name = "flickrLogo";
            this.flickrLogo.Size = new System.Drawing.Size(157, 43);
            this.flickrLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.flickrLogo.TabIndex = 15;
            this.flickrLogo.TabStop = false;
            this.flickrLogo.Click += new System.EventHandler(this.flickrLogo_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.toolStripStatusLabel1,
            this.statusLabel,
            this.statusPictureCount,
            this.statusPage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 629);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(631, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(252, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusPictureCount
            // 
            this.statusPictureCount.AutoSize = false;
            this.statusPictureCount.Name = "statusPictureCount";
            this.statusPictureCount.Size = new System.Drawing.Size(110, 17);
            // 
            // statusPage
            // 
            this.statusPage.AutoSize = false;
            this.statusPage.Name = "statusPage";
            this.statusPage.Size = new System.Drawing.Size(110, 17);
            this.statusPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolTip2
            // 
            this.toolTip2.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip2.ToolTipTitle = "Flickr Tags";
            // 
            // imageListing
            // 
            this.imageListing.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListing.ImageSize = new System.Drawing.Size(102, 102);
            this.imageListing.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // retrievedImageList
            // 
            this.retrievedImageList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.retrievedImageList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.retrievedImageList.BackColor = System.Drawing.Color.White;
            this.retrievedImageList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.retrievedImageList.CausesValidation = false;
            this.retrievedImageList.LargeImageList = this.imageListing;
            this.retrievedImageList.Location = new System.Drawing.Point(16, 186);
            this.retrievedImageList.Margin = new System.Windows.Forms.Padding(0);
            this.retrievedImageList.MultiSelect = false;
            this.retrievedImageList.Name = "retrievedImageList";
            this.retrievedImageList.ShowGroups = false;
            this.retrievedImageList.Size = new System.Drawing.Size(598, 326);
            this.retrievedImageList.TabIndex = 6;
            this.retrievedImageList.UseCompatibleStateImageBehavior = false;
            this.retrievedImageList.DoubleClick += new System.EventHandler(this.retrievedImageList_DoubleClick);
            this.retrievedImageList.SelectedIndexChanged += new System.EventHandler(this.retrievedImageList_SelectedIndexChanged);
            // 
            // pagePrevious
            // 
            this.pagePrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pagePrevious.Enabled = false;
            this.pagePrevious.Image = global::Flickr4Outlook.Properties.Resources.blueArrowLeft16;
            this.pagePrevious.Location = new System.Drawing.Point(550, 150);
            this.pagePrevious.Name = "pagePrevious";
            this.pagePrevious.Size = new System.Drawing.Size(26, 23);
            this.pagePrevious.TabIndex = 4;
            this.pagePrevious.UseVisualStyleBackColor = true;
            this.pagePrevious.Click += new System.EventHandler(this.pagePrevious_Click);
            // 
            // pageNext
            // 
            this.pageNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pageNext.Enabled = false;
            this.pageNext.Image = global::Flickr4Outlook.Properties.Resources.blueArrowRight16;
            this.pageNext.Location = new System.Drawing.Point(588, 150);
            this.pageNext.Name = "pageNext";
            this.pageNext.Size = new System.Drawing.Size(26, 23);
            this.pageNext.TabIndex = 5;
            this.pageNext.UseVisualStyleBackColor = true;
            this.pageNext.Click += new System.EventHandler(this.pageNext_Click);
            // 
            // tipUser
            // 
            this.tipUser.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tipUser.ToolTipTitle = "User Information";
            // 
            // imageProcessor
            // 
            this.imageProcessor.WorkerReportsProgress = true;
            this.imageProcessor.WorkerSupportsCancellation = true;
            this.imageProcessor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.imageProcessor_DoWork);
            this.imageProcessor.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.imageProcessor_RunWorkerCompleted);
            this.imageProcessor.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.imageProcessor_ProgressChanged);
            // 
            // groupUploadedBy
            // 
            this.groupUploadedBy.Controls.Add(this.rdbUserName);
            this.groupUploadedBy.Controls.Add(this.rdbAnyone);
            this.groupUploadedBy.Controls.Add(this.textboxFlickrUserName);
            this.groupUploadedBy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupUploadedBy.Location = new System.Drawing.Point(16, 73);
            this.groupUploadedBy.Name = "groupUploadedBy";
            this.groupUploadedBy.Size = new System.Drawing.Size(226, 100);
            this.groupUploadedBy.TabIndex = 1;
            this.groupUploadedBy.TabStop = false;
            this.groupUploadedBy.Text = "Uploaded By";
            // 
            // rdbUserName
            // 
            this.rdbUserName.AutoSize = true;
            this.rdbUserName.Checked = true;
            this.rdbUserName.Location = new System.Drawing.Point(7, 44);
            this.rdbUserName.Name = "rdbUserName";
            this.rdbUserName.Size = new System.Drawing.Size(77, 17);
            this.rdbUserName.TabIndex = 1;
            this.rdbUserName.TabStop = true;
            this.rdbUserName.Text = "Username:";
            this.rdbUserName.UseVisualStyleBackColor = true;
            this.rdbUserName.Click += new System.EventHandler(this.uploadedBy_Click);
            // 
            // rdbAnyone
            // 
            this.rdbAnyone.AutoSize = true;
            this.rdbAnyone.Location = new System.Drawing.Point(7, 21);
            this.rdbAnyone.Name = "rdbAnyone";
            this.rdbAnyone.Size = new System.Drawing.Size(62, 17);
            this.rdbAnyone.TabIndex = 0;
            this.rdbAnyone.Text = "Anyone";
            this.rdbAnyone.UseVisualStyleBackColor = true;
            this.rdbAnyone.Click += new System.EventHandler(this.uploadedBy_Click);
            // 
            // groupSearchType
            // 
            this.groupSearchType.Controls.Add(this.textBoxTagFilter);
            this.groupSearchType.Controls.Add(this.photosetList);
            this.groupSearchType.Controls.Add(this.rdbTags);
            this.groupSearchType.Controls.Add(this.rdbPhotoset);
            this.groupSearchType.Controls.Add(this.rdbAllPhotos);
            this.groupSearchType.Location = new System.Drawing.Point(249, 73);
            this.groupSearchType.Name = "groupSearchType";
            this.groupSearchType.Size = new System.Drawing.Size(295, 100);
            this.groupSearchType.TabIndex = 2;
            this.groupSearchType.TabStop = false;
            this.groupSearchType.Text = "Search Type";
            // 
            // textBoxTagFilter
            // 
            this.textBoxTagFilter.Enabled = false;
            this.textBoxTagFilter.Location = new System.Drawing.Point(85, 67);
            this.textBoxTagFilter.Name = "textBoxTagFilter";
            this.textBoxTagFilter.Size = new System.Drawing.Size(204, 21);
            this.textBoxTagFilter.TabIndex = 4;
            this.textBoxTagFilter.TextChanged += new System.EventHandler(this.textBoxTagFilter_TextChanged);
            // 
            // photosetList
            // 
            this.photosetList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.photosetList.Enabled = false;
            this.photosetList.FormattingEnabled = true;
            this.photosetList.Location = new System.Drawing.Point(85, 43);
            this.photosetList.Name = "photosetList";
            this.photosetList.Size = new System.Drawing.Size(204, 21);
            this.photosetList.TabIndex = 3;
            // 
            // rdbTags
            // 
            this.rdbTags.AutoSize = true;
            this.rdbTags.Location = new System.Drawing.Point(7, 67);
            this.rdbTags.Name = "rdbTags";
            this.rdbTags.Size = new System.Drawing.Size(52, 17);
            this.rdbTags.TabIndex = 2;
            this.rdbTags.Text = "Tags:";
            this.rdbTags.UseVisualStyleBackColor = true;
            this.rdbTags.Click += new System.EventHandler(this.searchType_Click);
            // 
            // rdbPhotoset
            // 
            this.rdbPhotoset.AutoSize = true;
            this.rdbPhotoset.Location = new System.Drawing.Point(7, 44);
            this.rdbPhotoset.Name = "rdbPhotoset";
            this.rdbPhotoset.Size = new System.Drawing.Size(72, 17);
            this.rdbPhotoset.TabIndex = 1;
            this.rdbPhotoset.Text = "Photoset:";
            this.rdbPhotoset.UseVisualStyleBackColor = true;
            this.rdbPhotoset.Click += new System.EventHandler(this.searchType_Click);
            // 
            // rdbAllPhotos
            // 
            this.rdbAllPhotos.AutoSize = true;
            this.rdbAllPhotos.Checked = true;
            this.rdbAllPhotos.Location = new System.Drawing.Point(7, 21);
            this.rdbAllPhotos.Name = "rdbAllPhotos";
            this.rdbAllPhotos.Size = new System.Drawing.Size(74, 17);
            this.rdbAllPhotos.TabIndex = 0;
            this.rdbAllPhotos.TabStop = true;
            this.rdbAllPhotos.Text = "All Images";
            this.rdbAllPhotos.UseVisualStyleBackColor = true;
            this.rdbAllPhotos.Click += new System.EventHandler(this.searchType_Click);
            // 
            // InsertFlickrImageForm
            // 
            this.AcceptButton = this.buttonInsert;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(631, 651);
            this.Controls.Add(this.checkHyperLink);
            this.Controls.Add(this.groupSearchType);
            this.Controls.Add(this.groupUploadedBy);
            this.Controls.Add(this.groupLayout);
            this.Controls.Add(this.pageNext);
            this.Controls.Add(this.pagePrevious);
            this.Controls.Add(this.retrievedImageList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.infoButton);
            this.Controls.Add(this.flickrSignup);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.flickrLogo);
            this.Controls.Add(this.dropDownImageSizes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonGetImages);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonInsert);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertFlickrImageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Insert Flickr Image";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InsertFlickrImageForm_FormClosed);
            this.groupLayout.ResumeLayout(false);
            this.groupLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.verticalPadding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.borderThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalPadding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flickrLogo)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupUploadedBy.ResumeLayout(false);
            this.groupUploadedBy.PerformLayout();
            this.groupSearchType.ResumeLayout(false);
            this.groupSearchType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void textBoxTagFilter_TextChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
        }

        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textboxFlickrUserName;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        #endregion

        #region Constructors
        public InsertFlickrImageForm()
        {
            InitializeComponent();
            this.dropDownAlignment.SelectedIndex = 0;
            this.dropDownImageSizes.SelectedIndex = 2;

            textboxFlickrUserName.Focus();
            textboxFlickrUserName.Text = Properties.Settings.Default.FlickrUserName.Trim();
            textBoxTagFilter.Text = Properties.Settings.Default.FlickrTags.Trim();

            // TODO: Consider threaded start of init
            System.Threading.Thread init = new System.Threading.Thread(new System.Threading.ThreadStart(EnsureFlickrNet));
            init.Start();
        }

        private void EnsureFlickrNet()
        {
            if (flickrProxy == null)
            {
                flickrProxy = FlickrPluginHelper.GetFlickrProxy();
            }
        }

        #endregion

        #region Event Handlers
        /// <summary>
        /// Cancels the modal dialog to insert a Flickr Image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// The OK function of the modal dialog resulting in letting the ContentSource know the action is complete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (_currentPhoto == null)
            {
                MessageBox.Show("No image selected.  Please choose an image or cancel to continue.", "No image selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                base.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// A generic update EventHandler that is leveraged by several OnChanged events throughout 
        /// the control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenericUpdateChange(object sender, EventArgs e)
        {
            if (_currentPhoto != null)
            {
                UpdateCurrentImageData();
            }
        }

        /// <summary>
        /// EventHandler for retrieving the initial set of Flickr images for the Flickr user name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetImages_Click(object sender, EventArgs e)
        {
            SaveSettings();
            EnsureFlickrNet();
            if (LoginUser())
            {
                StartImageProcessor();
            }
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.FlickrUserName = this.textboxFlickrUserName.Text.Trim();
            Properties.Settings.Default.FlickrTags = this.textBoxTagFilter.Text.Trim();
            Properties.Settings.Default.Save();
        }

        private void flickrSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FlickrPluginHelper.GotoUrl(FLICKR_SIGNUP);
        }

        private void flickrLogo_Click(object sender, EventArgs e)
        {
            FlickrPluginHelper.GotoUrl(FLICKR_URL);
        }

        private void aboutPlugin_Click(object sender, EventArgs e)
        {
            AboutFlickrPlugin about = new AboutFlickrPlugin();
            about.ShowDialog();
        }

        private void textboxFlickrUserName_TextChanged(object sender, EventArgs e)
        {
            _userId = string.Empty;
        }

        private void retrievedImageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.retrievedImageList.SelectedIndices.Count == 1)
            {
                FlickrNet.Photo p = (FlickrNet.Photo)retrievedImageList.Items[retrievedImageList.SelectedIndices[0]].Tag;
                _currentPhoto = p;
            }
            else
            {
                _currentPhoto = null;
            }
            ChangeImageSourceUrl();
        }

        private void pagePrevious_Click(object sender, EventArgs e)
        {
            if (_images)
            {
                UpdateStatus(string.Format("Getting previous {0} images...", PER_PAGE.ToString()));

                if (_currentPage == 1)
                {
                    _currentPage = Convert.ToInt32(_totalPages);
                }
                else
                {
                    _currentPage--;
                }

                StartImageProcessor();

                UpdateStatus(string.Empty);
            }
        }

        private void pageNext_Click(object sender, EventArgs e)
        {
            if (_images)
            {
                UpdateStatus(string.Format("Getting next {0} images...", PER_PAGE.ToString()));

                if (_currentPage == Convert.ToInt32(_totalPages))
                {
                    _currentPage = 1;
                }
                else
                {
                    _currentPage++;
                }

                StartImageProcessor();

                UpdateStatus(string.Empty);
            }
        }       

        private void imageProcessor_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateStatus("Retrieving images from Flickr...");
            try
            {
                DrawRetrievedImages();
            }
            finally
            {
                UpdateStatus(string.Empty);
            }
        }

        private void imageProcessor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!progressBar.IsDisposed)
            {
                progressBar.Visible = true;
                progressBar.Increment(1);

                if (e.ProgressPercentage == 100)
                {
                    UpdateStatus(string.Empty);
                }
            }
        }

        private void imageProcessor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!progressBar.IsDisposed)
            {
                progressBar.Value = 0;
                progressBar.Minimum = 0;
            }
        }

        private void setProcessor_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateStatus("Retrieving photoset information...");
            EnsureFlickrNet();
            if (LoginUser())
            {
                RefreshPhotosets();
            }
            UpdateStatus(string.Empty);
        }

        private void InsertFlickrImageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                imageProcessor.CancelAsync();
            }
            catch { }
        }        
        
        private void retrievedImageList_DoubleClick(object sender, EventArgs e)
        {
            if (retrievedImageList.SelectedIndices.Count == 1)
            {
                buttonInsert_Click(sender, e);
            }
        }
        protected void searchType_Click(object sender, EventArgs e)
        {
            rdbSearchTypeSet = (RadioButton)sender;
            ApplySearchTypeChanges();
        }

        protected void uploadedBy_Click(object sender, EventArgs e)
        {
            rdbUploadedBySet = (RadioButton)sender;
            ApplyUploadedByChanges();
        }

        #endregion

        #region Overrides
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Mini Helper Methods
        private bool LoginUser()
        {
            if (rdbUserName.Checked)
            {
                if (string.IsNullOrEmpty(_userId))
                {
                    UpdateStatus("Validating user...");
                    // make sure at least something is there
                    // let's not waste our time if nobody put a login name

                    string userName = GetValueFromUIThread<string>(delegate
                    {
                        return textboxFlickrUserName.Text.Trim();
                    });

                    if (string.IsNullOrEmpty(userName))
                    {
                        ExecuteOnUIThread(delegate
                        {
                            MessageBox.Show("Please enter a Flickr login (email address)", "No Flickr Login", MessageBoxButtons.OK);
                            textboxFlickrUserName.SelectAll();
                            textboxFlickrUserName.Focus();
                        });
                        return false;
                    }

                    try
                    {
                        string userId = string.Empty;
                        FlickrNet.FoundUser user;

                        user = FlickrPluginHelper.FindUserByEmailOrName(flickrProxy, textboxFlickrUserName.Text.Trim());
                        _userId = user.UserId;

                        return true;
                    }
                    catch (FlickrNet.FlickrException ex)
                    {
                        UpdateStatus(string.Empty);
                        if (ex.Code == 1)  // user not found
                        {
                            ExecuteOnUIThread(delegate
                            {
                                MessageBox.Show("Flickr user not found.  Please ensure it is either the Flickr email address or correct Flickr user name.", "Flickr User Not Found", MessageBoxButtons.OK);
                            });
                        }
                        else if (ex.Code == 9999) // HttpTimeout
                        {
                            ExecuteOnUIThread(delegate
                            {
                                MessageBox.Show("Flickr could not be contacted.  The requested timed out.  \nPlease ensure your internet connection is still valid.", "Flickr Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            });
                        }
                        return false;
                    }
                    finally
                    {
                        UpdateStatus(string.Empty);
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Simple method to help update the status area.
        /// 
        /// Can be called from any thread.
        /// </summary>
        /// <param name="message">The status message to display.</param>
        private void UpdateStatus(string message)
        {
            ExecuteOnUIThread(delegate
            {
                statusLabel.Text = message;
                if (message.Length < 1) // default the curser back
                {
                    Cursor.Current = Cursors.Default;
                    statusLabel.Text = "Idle";
                }
                else // we are actually doing something, let's pause the cursor
                {
                    Cursor.Current = Cursors.WaitCursor;
                }
            });
        }

        /// <summary>
        /// Method to update member variables with data regarding the selected image and the layout properties.
        /// </summary>
        private void UpdateCurrentImageData()
        {
            ChangeImageSourceUrl();
        }

        /// <summary>
        /// Method to change the image reference (src) url for the Html content.
        /// </summary>
        private void ChangeImageSourceUrl()
        {
            if (_currentPhoto == null)
                _srcUrl = null;
            else
            {
                switch (dropDownImageSizes.Text)
                {
                    case "Thumbnail":
                        _srcUrl = _currentPhoto.ThumbnailUrl;
                        break;
                    case "Small":
                        _srcUrl = _currentPhoto.SmallUrl;
                        break;
                    case "Large":
                        _srcUrl = _currentPhoto.LargeUrl;
                        break;
                    default:
                        _srcUrl = _currentPhoto.MediumUrl;
                        break;
                }
            }
        }
        #endregion

        #region Member Methods
        private void RefreshPhotosets()
        {
            EnsureFlickrNet();
            if (LoginUser())
            {
                if (photosetList.Items.Count < 1)
                {
                    ExecuteOnUIThread(delegate
                    {
                        photosetList.DataSource = null;
                        photosetList.Items.Clear();
                    });

                    Photosets sets = flickrProxy.PhotosetsGetList(_userId);

                    ExecuteOnUIThread(delegate
                    {
                        photosetList.DataSource = sets.PhotosetCollection;
                        photosetList.ValueMember = "PhotosetId";
                        photosetList.DisplayMember = "Title";
                    });
                }
            }
        }
        
        private void StartImageProcessor()
        {
            if (this.imageProcessor.IsBusy)
            {
                imageProcessor.CancelAsync();
            }
            while (imageProcessor.CancellationPending)
            {
                Application.DoEvents();
            }
            imageProcessor.RunWorkerAsync();
        }
        
        private void ClearImageList()
        {
            imageListing.Images.Clear();
            retrievedImageList.Items.Clear();
            _currentPhoto = null;
            ChangeImageSourceUrl();
        }

        private void DrawRetrievedImages()
        {
            FlickrNet.PhotoSearchOptions options = new FlickrNet.PhotoSearchOptions();
            ExecuteOnUIThread(delegate
            {
                if (rdbUserName.Checked)
                {
                    options.UserId = _userId;
                }
            });

            options.PerPage = PER_PAGE;
            options.Page = _currentPage;
            string tags = GetValueFromUIThread<string>(delegate { return textBoxTagFilter.Text.Trim(); });
            string photoSetId = string.Empty;

            ExecuteOnUIThread(delegate
            {
                if (rdbPhotoset.Checked)
                {
                    photoSetId = GetValueFromUIThread<string>(delegate { return photosetList.SelectedValue.ToString(); });
                }
            });

            options.Tags = FlickrPluginHelper.CleanTagFilter(tags);
            options.TagMode = TagMode.AllTags;

            // TODO: if tag options is selected, must containt one tag

            ExecuteOnUIThread(delegate
            {
                ClearImageList();
            });

            FlickrNet.Photos fPhotos = null;

            try
            {
                if (!string.IsNullOrEmpty(photoSetId))
                {
                    Photo[] photoArray = flickrProxy.PhotosetsGetPhotos(photoSetId);
                    if (imageProcessor.CancellationPending)
                        return;
                    Photos ps = new Photos();
                    for (int i = 0; i < photoArray.Length; i++)
                    {
                        ps.PhotoCollection.Add(photoArray[i]);
                    }
                    fPhotos = ps;
                }
                else
                {
                    fPhotos = flickrProxy.PhotosSearch(options);
                    if (imageProcessor.CancellationPending)
                        return;
                }
                ExecuteOnUIThread(delegate
                {
                    if (imageProcessor.CancellationPending)
                        return;
                    if (fPhotos.TotalPages == 0)
                    {
                        statusPage.Text = "";
                        statusPictureCount.Text = string.Format("Image Count: {0}", fPhotos.PhotoCollection.Count.ToString());
                    }
                    else
                    {
                        statusPage.Text = string.Format("Page {0} of {1}", _currentPage.ToString(), fPhotos.TotalPages.ToString());
                        statusPictureCount.Text = string.Format("Image Count: {0}", fPhotos.TotalPhotos.ToString());
                    }
                });
                if (imageProcessor.CancellationPending)
                    return;
            }
            catch (FlickrNet.FlickrException ex)
            {
                if (ex.Code == 9999)
                {
                    ExecuteOnUIThread(delegate
                    {
                        MessageBox.Show("Flickr could not be contacted.  The requested timed out.  \nPlease ensure your internet connection is still valid.", "Flickr Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                    return;
                }
            }

            _totalPages = fPhotos.TotalPages;
            ExecuteOnUIThread(delegate
            {
                pagePrevious.Enabled = _totalPages > 1;
                pageNext.Enabled = _totalPages > 1;
                progressBar.Maximum = fPhotos.PhotoCollection.Length;
            });

            if (fPhotos.PhotoCollection.Count > 0)
            {
                _images = true;

                for (int i = 0; i < fPhotos.PhotoCollection.Count; i++)
                {
                    if (imageProcessor.CancellationPending)
                        break;

                    ListViewItem item = null;

                    // HTTP request must happen on background thread
                    System.Net.WebRequest http = WebRequest.Create(fPhotos.PhotoCollection[i].ThumbnailUrl);
                    
                    MemoryStream ms = new MemoryStream();
                    using (System.IO.Stream imageStream = http.GetResponse().GetResponseStream())
                    {
                        byte[] buf = new byte[0x1000];
                        int cnt;
                        while ((cnt = imageStream.Read(buf, 0, buf.Length)) > 0)
                            ms.Write(buf, 0, cnt);
                    }
                    ms.Seek(0, SeekOrigin.Begin);

                    if (imageProcessor.CancellationPending)
                        break;

                    ExecuteOnUIThread(delegate
                    {
                        // Image that will be used on UI thread must be created on UI thread
                        Image img = Image.FromStream(ms);
                        img = FlickrPluginHelper.ScaleToFixedSize(img, imageListing.ImageSize.Width, imageListing.ImageSize.Height, 0, Color.Transparent);
                        imageListing.Images.Add(img);
                        item = new ListViewItem();
                        item.Tag = fPhotos.PhotoCollection[i];
                        item.Text = fPhotos.PhotoCollection[i].Title;
                        item.ImageIndex = i;
                        retrievedImageList.Items.Add(item);
                    });

                    imageProcessor.ReportProgress(i + 1);
                }
            }
        }

        private void ApplySearchTypeChanges()
        {
            if (rdbSearchTypeSet == rdbAllPhotos)
            {
                rdbPhotoset.Checked = false;
                rdbTags.Checked = false;
                textBoxTagFilter.Enabled = false;
                textBoxTagFilter.Text = string.Empty;
                photosetList.Enabled = false;
            }
            else if (rdbSearchTypeSet == rdbPhotoset)
            {
                RefreshPhotosets();
                if (photosetList.Items.Count > 0)
                {
                    rdbAllPhotos.Checked = false;
                    rdbTags.Checked = false;
                    textBoxTagFilter.Enabled = false;
                    textBoxTagFilter.Text = string.Empty;
                    photosetList.Enabled = true;
                }
                else
                {
                    // no photosets for the user
                    ExecuteOnUIThread(delegate
                    {
                        MessageBox.Show(string.Format("No Photosets exist for {0}.\nTry another username or different search type.", textboxFlickrUserName.Text.Trim()), "No Photosets", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                    return;
                }
            }
            else if (rdbSearchTypeSet == rdbTags)
            {
                rdbPhotoset.Checked = false;
                rdbAllPhotos.Checked = false;
                textBoxTagFilter.Enabled = true;
                photosetList.Enabled = false;
                textBoxTagFilter.Focus();
            }
        }

        private void ApplyUploadedByChanges()
        {
            if (rdbUploadedBySet == rdbAnyone)
            {
                rdbUserName.Checked = false;
                textboxFlickrUserName.Enabled = false;
                // all images and photosets should be disabled
                rdbAllPhotos.Enabled = false;
                rdbPhotoset.Enabled = false;
                rdbSearchTypeSet = rdbTags;
                rdbTags.Checked = true;
                ApplySearchTypeChanges();
            }
            else if (rdbUploadedBySet == rdbUserName)
            {
                rdbAnyone.Checked = false;
                textboxFlickrUserName.Enabled = true;
                rdbAllPhotos.Enabled = true;
                rdbPhotoset.Enabled = true;
                textboxFlickrUserName.Focus();
            }
        }

        #endregion

        #region Delegates
        private delegate void ExecuteOnUIThreadDelegate();
        /// <summary>
        /// Executes the given operation on the UI thread.  If called
        /// from the UI thread, the operation will simply be run.
        /// If called from a background thread, the operation will be
        /// Invoked on the UI thread.
        /// 
        /// This call is synchronous, it will block until the operation
        /// has finished running on the UI thread (or an exception is
        /// thrown).
        /// </summary>
        private void ExecuteOnUIThread(ExecuteOnUIThreadDelegate operation)
        {
            if (InvokeRequired)
                Invoke(operation);
            else
                operation();
        }

        private delegate T GetValueDelegate<T>();
        /// <summary>
        /// Same as ExecuteOnUIThread, but can return a value of type T.
        /// 
        /// For example:
        /// 
        /// string s = GetValueFromUIThread&lt;string&gt;(delegate { return textBox.Text; });
        /// </summary>
        private T GetValueFromUIThread<T>(GetValueDelegate<T> operation)
        {
            if (InvokeRequired)
                return (T)Invoke(operation);
            else
                return operation();
        }
        #endregion
    }
}