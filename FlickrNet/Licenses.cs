using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;

namespace FlickrNet
{
    /// <summary>
    /// A enumeration containing the list of current license types.
    /// </summary>
    public enum LicenseType
    {
		/// <summary>
		/// All Rights Reserved.
		/// </summary>
        AllRightsReserved = 0,
		/// <summary>
		/// Creative Commons: Attribution Non-Commercial, Share-alike License.
		/// </summary>
        AttributionNoncommercialShareAlikeCC = 1,
		/// <summary>
		/// Creative Commons: Attribution Non-Commercial License.
		/// </summary>
        AttributionNoncommercialCC = 2,
		/// <summary>
		/// Creative Commons: Attribution Non-Commercial, No Derivatives License.
		/// </summary>
        AttributionNoncommercialNoDerivativesCC = 3,
		/// <summary>
		/// Creative Commons: Attribution License.
		/// </summary>
        AttributionCC = 4,
		/// <summary>
		/// Creative Commons: Attribution Share-alike License.
		/// </summary>
        AttributionShareAlikeCC = 5,
		/// <summary>
		/// Creative Commons: Attribution No Derivatives License.
		/// </summary>
        AttributionNoDerivativesCC = 6,
		/// <summary>
		/// No Known Copyright Resitrctions (Flickr Commons).
		/// </summary>
        NoKnownCopyrightRestrictions = 7
    }

	/// <summary>
	/// A class which encapsulates a single property, an array of
	/// <see cref="License"/> objects in its <see cref="LicenseCollection"/> property.
	/// </summary>
    public sealed class LicenseCollection : System.Collections.ObjectModel.Collection<License>, IFlickrParsable
	{
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "license")
            {
                License license = new License();
                ((IFlickrParsable)license).Load(reader);
                Add(license);
            }

            reader.Skip();
        }
    }

	/// <summary>
	/// Details of a particular license available from Flickr.
	/// </summary>
    public sealed class License : IFlickrParsable
	{
		/// <summary>
        ///     The ID of the license. Used by <see cref="Flickr.PhotosGetInfo(string)"/> and 
        ///     <see cref="Flickr.PhotosGetInfo(string, string)"/>.
        /// </summary>
        public LicenseType LicenseId { get; private set; }

		/// <summary>The name of the license.</summary>
        public string LicenseName { get; private set; }

		/// <summary>The URL for the license text.</summary>
        public Uri LicenseUrl { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        LicenseId = (LicenseType)reader.ReadContentAsInt();
                        break;
                    case "name":
                        LicenseName = reader.Value;
                        break;
                    case "url":
                        LicenseUrl = new Uri(reader.Value);
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();
        }
    }
}
