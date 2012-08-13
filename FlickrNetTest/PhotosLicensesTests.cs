using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    [TestClass]
    public class PhotosLicensesTests
    {
        [TestMethod]
        public void PhotosLicensesGetInfoBasicTest()
        {
            LicenseCollection col = TestData.GetInstance().PhotosLicensesGetInfo();

            foreach (License lic in col)
            {
                if (!Enum.IsDefined(typeof(LicenseType), lic.LicenseId))
                {
                    Assert.Fail("License with ID " + (int)lic.LicenseId + ", " + lic.LicenseName + " dooes not exist.");
                }
            }
        }

        [TestMethod]
        public void PhotosLicensesSetLicenseTest()
        {
            Flickr f = TestData.GetAuthInstance();
            string photoId = "7176125763";

            var photoInfo = f.PhotosGetInfo(photoId); // Rainbow Rose
            var origLicense = photoInfo.License;

            var newLicense = origLicense == LicenseType.AttributionCC ? LicenseType.AttributionNoDerivativesCC : LicenseType.AttributionCC;
            f.PhotosLicensesSetLicense(photoId, newLicense);

            var newPhotoInfo = f.PhotosGetInfo(photoId);

            Assert.AreEqual(newLicense, newPhotoInfo.License, "License has not changed");

            // Reset license 
            f.PhotosLicensesSetLicense(photoId, origLicense);
        }

    }
}
