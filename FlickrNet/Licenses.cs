using System;
using System.Xml.Serialization;
using System.Xml.Schema;

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
        AttributionNonCommercialShareAlikeCC = 1,
		/// <summary>
		/// Creative Commons: Attribution Non-Commercial License.
		/// </summary>
        AttributionNonCommercialCC = 2,
		/// <summary>
		/// Creative Commons: Attribution Non-Commercial, No Derivatives License.
		/// </summary>
        AttributionNonCommercialNoDerivsCC = 3,
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
        AttributionNoDerivsCC = 6,
		/// <summary>
		/// No Known Copyright Resitrctions (Flickr Commons).
		/// </summary>
        NoKnownCopyrightRestrictions = 7
    }

	/// <summary>
	/// A class which encapsulates a single property, an array of
	/// <see cref="License"/> objects in its <see cref="LicenseCollection"/> property.
	/// </summary>
	[System.Serializable]
	public class Licenses
	{
		/// <summary>A collection of available licenses.</summary>
		/// <remarks/>
		[XmlElement("license", Form=XmlSchemaForm.Unqualified)]
		public License[] LicenseCollection;
    
	}

	/// <summary>
	/// Details of a particular license available from Flickr.
	/// </summary>
	[System.Serializable]
	public class License
	{
		/// <summary>
        ///     The ID of the license. Used by <see cref="Flickr.PhotosGetInfo(string)"/> and 
        ///     <see cref="Flickr.PhotosGetInfo(string, string)"/>.
        /// </summary>
		/// <remarks/>
		[XmlAttribute("id", Form=XmlSchemaForm.Unqualified)]
		public LicenseType LicenseId;

		/// <summary>The name of the license.</summary>
		/// <remarks/>
		[XmlAttribute("name", Form=XmlSchemaForm.Unqualified)]
		public string LicenseName;

		/// <summary>The URL for the license text.</summary>
		/// <remarks/>
		[XmlAttribute("url", Form=XmlSchemaForm.Unqualified)]
		public string LicenseUrl;

	}
}
