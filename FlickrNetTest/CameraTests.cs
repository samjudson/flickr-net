using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlickrNetTest
{
    [TestClass]
    public class CameraTests
    {
        [TestMethod]
        public void ShouldReturnListOfCameraBrands()
        {
            var brands = TestData.GetInstance().CamerasGetBrands();

            Assert.IsNotNull((brands));
            Assert.AreNotEqual(0, brands.Count);

            Assert.IsTrue(brands.Any(b => b.BrandId == "canon" && b.BrandName == "Canon"));
            Assert.IsTrue(brands.Any(b => b.BrandId == "nikon" && b.BrandName == "Nikon"));
        }

        [TestMethod]
        public void ShouldReturnListOfCanonCameraModels()
        {
            var models = TestData.GetInstance().CamerasGetBrandModels("canon");

            Assert.IsNotNull((models));
            Assert.AreNotEqual(0, models.Count);

            Assert.IsTrue(models.Any(c => c.CameraId == "eos_5d_mark_ii" && c.CameraName == "Canon EOS 5D Mark II"));
            Assert.IsTrue(models.Any(c => c.CameraId == "powershot_a620" && c.CameraName == "Canon PowerShot A620"));
            
        }
    }
}
