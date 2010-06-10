using System;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Contains details of a user
	/// </summary>
	public sealed class FoundUser : IFlickrParsable
	{
		/// <summary>
		/// The ID of the found user.
		/// </summary>
		public string UserId { get; private set; }

		/// <summary>
		/// The username of the found user.
		/// </summary>
		public string UserName { get; private set; }

        /// <summary>
        /// The full name of the user. Only returned by <see cref="Flickr.AuthGetToken"/>.
        /// </summary>
        public string FullName { get; private set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public FoundUser()
		{
		}

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "user")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "nsid":
                    case "id":
                        UserId = reader.Value;
                        break;
                    case "username":
                        UserName = reader.Value;
                        break;
                    case "fullname":
                        FullName = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            if (reader.NodeType != XmlNodeType.EndElement)
            {
                UserName = reader.ReadElementContentAsString();
                reader.Skip();
            }
        }
    }

	/// <summary>
	/// The upload status of the user, as returned by <see cref="Flickr.PeopleGetUploadStatus"/>.
	/// </summary>
    public sealed class UserStatus : IFlickrParsable
	{
		/// <summary>
		/// The id of the user object.
		/// </summary>
		public string UserId { get; private set; }

		/// <summary>
		/// The Username of the selected user.
		/// </summary>
		public string UserName { get; private set; }

		/// <summary>
		/// Is the current user a Pro account.
		/// </summary>
		public bool IsPro { get; private set; }

        /// <summary>
        /// The maximum bandwidth (in bytes) that the user can use each month.
        /// </summary>
        public long BandwidthMax { get; private set; }

        /// <summary>
        /// The maximum bandwidth (in kilobytes) that the user can use each month.
        /// </summary>
        public long BandwidthMaxKB { get; private set; }

        /// <summary>
        /// The remaining bandwidth (in bytes) that the user can use this month.
        /// </summary>
        public long BandwidthRemaining { get; private set; }

        /// <summary>
        /// The remaining bandwidth (in kilobytes) that the user can use this month.
        /// </summary>
        public long BandwidthRemainingKB { get; private set; }

        /// <summary>
        /// The number of bytes of the current months bandwidth that the user has used.
        /// </summary>
        public long BandwidthUsed { get; private set; }

        /// <summary>
        /// The number of kilobytes of the current months bandwidth that the user has used.
        /// </summary>
        public long BandwidthUsedKB { get; private set; }

        /// <summary>
        /// Is the upload bandwidth unlimited (i.e. a Pro user).
        /// </summary>
        public bool IsUnlimited { get; private set; }

        /// <summary>
        /// The maximum filesize (in bytes) that the user is allowed to upload.
        /// </summary>
        public long FileSizeMax { get; private set; }

        /// <summary>
        /// The maximum filesize (in kilobytes) that the user is allowed to upload.
        /// </summary>
        public long FileSizeMaxKB { get; private set; }

        /// <summary>
        /// The maximum filesize (in MB) that the user is allowed to upload.
        /// </summary>
        public long FileSizeMaxMB { get; private set; }

        /// <summary>
        /// The maximum filesize (in bytes) that the user is allowed to upload.
        /// </summary>
        public long VideoSizeMax { get; private set; }

        /// <summary>
        /// The maximum filesize (in kilobytes) that the user is allowed to upload.
        /// </summary>
        public long VideoSizeMaxKB { get; private set; }

        /// <summary>
        /// The maximum filesize (in MB) that the user is allowed to upload.
        /// </summary>
        public long VideoSizeMaxMB { get; private set; }

        /// <summary>
        /// The number of sets the user has created. Will be null for Pro users.
        /// </summary>
        public int? SetsCreated { get; private set; }

        /// <summary>
        /// The number of sets the user can still created. Will be null for Pro users.
        /// </summary>
        public int? SetsRemaining { get; private set; }

        /// <summary>
        /// The number of videos the user has uploaded. Will be null or zero for Pro users.
        /// </summary>
        public int? VideosUploaded { get; private set; }

        /// <summary>
        /// The number of videos the user can upload. Will be null for Pro users.
        /// </summary>
        public int? VideosRemaining { get; private set; }

        /// <summary>
		/// <see cref="Double"/> representing the percentage bandwidth used so far. Will range from 0 to 1.
		/// </summary>
		public Double PercentageUsed
		{
			get { return BandwidthUsed * 1.0 / BandwidthMax; }
		}

        void IFlickrParsable.Load(XmlReader reader)
        {
            LoadAttributes(reader);

            LoadElements(reader);
        }

        private void LoadElements(XmlReader reader)
        {
            while (reader.LocalName != "user")
            {
                switch (reader.LocalName)
                {
                    case "username":
                        UserName = reader.ReadElementContentAsString();
                        break;
                    case "bandwidth":
                        ParseBandwidth(reader);
                        break;
                    case "filesize":
                        ParseFileSize(reader);
                        break;
                    case "sets":
                        ParseSets(reader);
                        break;
                    case "videosize":
                        ParseVideoSize(reader);
                        break;
                    case "videos":
                        ParseVideos(reader);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }
        }

        private void LoadAttributes(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                    case "nsid":
                        UserId = reader.Value;
                        break;
                    case "ispro":
                        IsPro = reader.Value == "1";
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }

        private void ParseVideos(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "uploaded":
                        if (!String.IsNullOrEmpty(reader.Value))
                            VideosUploaded = reader.ReadContentAsInt();
                        break;
                    case "remaining":
                        if (!String.IsNullOrEmpty(reader.Value) && reader.Value != "lots")
                            VideosRemaining = reader.ReadContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }
            reader.Read();
        }

        private void ParseVideoSize(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "maxbytes":
                        VideoSizeMax = reader.ReadContentAsLong();
                        break;
                    case "maxkb":
                        VideoSizeMaxKB = reader.ReadContentAsLong();
                        break;
                    case "maxmb":
                        VideoSizeMaxMB = reader.ReadContentAsLong();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }
            reader.Read();
        }

        private void ParseSets(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "created":
                        if (!String.IsNullOrEmpty(reader.Value))
                            SetsCreated = reader.ReadContentAsInt();
                        break;
                    case "remaining":
                        if (!String.IsNullOrEmpty(reader.Value) && reader.Value != "lots")
                            SetsRemaining = reader.ReadContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }
            reader.Read();
        }

        private void ParseFileSize(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "maxbytes":
                    case "max":
                        FileSizeMax = reader.ReadContentAsLong();
                        break;
                    case "maxkb":
                        FileSizeMaxKB = reader.ReadContentAsLong();
                        break;
                    case "maxmb":
                        FileSizeMaxMB = reader.ReadContentAsLong();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }
            reader.Read();
        }

        private void ParseBandwidth(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "maxbytes":
                    case "max":
                        BandwidthMax = reader.ReadContentAsLong();
                        break;
                    case "maxkb":
                        BandwidthMaxKB = reader.ReadContentAsLong();
                        break;
                    case "used":
                    case "usedbytes":
                        BandwidthUsed = reader.ReadContentAsLong();
                        break;
                    case "usedkb":
                        BandwidthUsedKB = reader.ReadContentAsLong();
                        break;
                    case "remainingbytes":
                        BandwidthRemaining = reader.ReadContentAsLong();
                        break;
                    case "remainingkb":
                        BandwidthRemainingKB = reader.ReadContentAsLong();
                        break;
                    case "unlimited":
                        IsUnlimited = reader.Value == "1";
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }
            reader.Read();
        }
    }
}