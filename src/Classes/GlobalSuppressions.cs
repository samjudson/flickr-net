// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project. 
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc. 
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File". 
// You do not need to add suppressions to this file manually. 

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Flickr",
        Scope = "namespace", Target = "FlickrNet")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Scope = "member",
        Target = "FlickrNet.PointD.#.ctor(System.Double,System.Double)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Scope = "member",
        Target = "FlickrNet.PointD.#.ctor(System.Double,System.Double)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Scope = "member",
        Target = "FlickrNet.PointD.#X")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Scope = "member",
        Target = "FlickrNet.PointD.#Y")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Scope = "type",
        Target = "FlickrNet.Collection")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Scope = "member",
        Target = "FlickrNet.Flickr.#TestLogin()")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Scope = "member",
        Target = "FlickrNet.Method.#NeedsLogin")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Scope = "type",
        Target = "FlickrNet.MethodPermission")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved", Scope = "member",
        Target = "FlickrNet.LicenseType.#AllRightsReserved")]
[assembly:
    SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member",
        Target = "FlickrNet.UtilityMethods.#MD5Hash(System.String)")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member",
        Target = "FlickrNet.UtilityMethods.#UrlEncode(System.String)")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member",
        Target = "FlickrNet.ResponseCacheItem.#Url")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target = "FlickrNet.ShapeData.#PolyLines")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
        Target = "FlickrNet.UnknownResponse.#GetXPathNavigator()")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Scope = "member",
        Target = "FlickrNet.Flickr.#PlacesGetInfoByUrl(System.String)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HotList",
        Scope = "member", Target = "FlickrNet.Flickr.#TagsGetHotList()")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HotList",
        Scope = "member", Target = "FlickrNet.Flickr.#TagsGetHotList(System.String,System.Int32)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Namespace",
        Scope = "type", Target = "FlickrNet.Namespace")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member",
        Target = "FlickrNet.PhotoInfoUrl.#UrlType")]