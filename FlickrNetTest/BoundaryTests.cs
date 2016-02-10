using FlickrNet;
using NUnit.Framework;

namespace FlickrNetTest
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Potential Code Quality Issues", "RECS0026:Possible unassigned object created by 'new'")]
    public class BoundaryTests : BaseTest
    {
        [Test]
        public void BoundaryBoxCalculateSizesUkNewcastle()
        {
            var b = BoundaryBox.UKNewcastle;

            var e = b.DiagonalDistanceInMiles();

            Assert.AreNotEqual(0, e);
        }

        [Test]
        [ExpectedException]
        public void BoundaryBoxThrowExceptionOnInvalidMinLat()
        {
            new BoundaryBox(-180, -91, 180, 90);
        }

        [Test]
        [ExpectedException]
        public void BoundaryBoxThrowExceptionOnInvalidMinLong()
        {
            new BoundaryBox(-181, -90, 180, 90);
        }

        [Test]
        [ExpectedException]
        public void BoundaryBoxThrowExceptionOnInvalidMaxLat()
        {
            new BoundaryBox(-180, -90, 180, 91);
        }

        [Test]
        [ExpectedException]
        public void BoundaryBoxThrowExceptionOnInvalidMaxLon()
        {
            new BoundaryBox(-180, -90, 181, 90);
        }


        [Test]
        public void BoundaryBoxCalculateSizesFrankfurtToBerlin()
        {
            var b = new BoundaryBox(8.68194, 50.11222, 13.29750, 52.52222);

            var e = b.DiagonalDistanceInMiles();
            Assert.IsTrue(259.9 < e && e < 260.0);
        }

        [Test]
        [ExpectedException]
        public void BoundaryBoxWithNullPointStringThrows()
        {
            new BoundaryBox(null);
        }

        [Test]
        [ExpectedException]
        public void BoundaryBoxWithInvalidPointStringThrows()
        {
            new BoundaryBox("1,2,3");
        }

        [Test]
        [ExpectedException]
        public void BoundaryBoxWithNoneNumericPointStringThrows()
        {
            new BoundaryBox("1,2,3,A");
        }

        [Test]
        public void BoundaryBoxWithValidPointStringSetCorrectly()
        {
            var b = new BoundaryBox("1,2,3,4");

            Assert.That(b.MinimumLongitude, Is.EqualTo(1M));
            Assert.That(b.MinimumLatitude, Is.EqualTo(2M));
            Assert.That(b.MaximumLongitude, Is.EqualTo(3M));
            Assert.That(b.MaximumLatitude, Is.EqualTo(4M));
        }

    }
}
