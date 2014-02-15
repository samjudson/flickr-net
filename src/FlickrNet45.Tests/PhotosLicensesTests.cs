using System;
using NUnit.Framework;
using FlickrNet;

namespace FlickrNet45.Tests
{
    [TestFixture]
    public class PhotosLicensesTests : BaseTest
    {
        [Test]
        public void PhotosLicensesGetInfoBasicTest()
        {
            LicenseCollection col = Instance.PhotosLicensesGetInfo();

            foreach (License lic in col)
            {
                if (!Enum.IsDefined(typeof(LicenseType), lic.LicenseId))
                {
                    Assert.Fail("License with ID " + (int)lic.LicenseId + ", " + lic.LicenseName + " dooes not exist.");
                }
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosLicensesSetLicenseTest()
        {
            string photoId = "7176125763";

            var photoInfo = AuthInstance.PhotosGetInfo(photoId); // Rainbow Rose
            var origLicense = photoInfo.License;

            var newLicense = origLicense == LicenseType.AttributionCC ? LicenseType.AttributionNoDerivativesCC : LicenseType.AttributionCC;
            AuthInstance.PhotosLicensesSetLicense(photoId, newLicense);

            var newPhotoInfo = IgnoreInstance.PhotosGetInfo(photoId);

            Assert.AreEqual(newLicense, newPhotoInfo.License, "License has not changed to correct value.");

            // Reset license 
            AuthInstance.PhotosLicensesSetLicense(photoId, origLicense);
        }

    }
}
