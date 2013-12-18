using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for CommonsTests
    /// </summary>
    [TestFixture]
    public class CommonsTests
    {
       
        [Test]
        public void CommonsGetInstitutions()
        {
            InstitutionCollection insts = TestData.GetInstance().CommonsGetInstitutions();

            Assert.IsNotNull(insts);
            Assert.IsTrue(insts.Count > 5);

            foreach (var i in insts)
            {
                Assert.IsNotNull(i);
                Assert.IsNotNull(i.InstitutionId);
                Assert.IsNotNull(i.InstitutionName);
            }
        }
    }
}
