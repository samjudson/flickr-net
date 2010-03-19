using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for MachinetagsTests
    /// </summary>
    [TestClass]
    public class MachinetagsTests
    {
        public MachinetagsTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void MachinetagsGetNamespacesBasicTest()
        {
            Flickr f = TestData.GetInstance();

            NamespaceCollection col = f.MachinetagsGetNamespaces();

            Assert.IsTrue(col.Count > 10, "Should be greater than 10 namespaces.");

            foreach (var n in col)
            {
                Assert.IsNotNull(n.NamespaceName, "NamespaceName should not be null.");
                Assert.AreNotEqual(0, n.Predicates, "Predicates should not be zero.");
                Assert.AreNotEqual(0, n.Usage, "Usage should not be zero.");
                Console.WriteLine(n.NamespaceName + " has " + n.Predicates + " predicates and a usage of " + n.Usage);
            }
        }

        [TestMethod]
        public void MachinetagsGetPredicatesBasicTest()
        {
            Flickr f = TestData.GetInstance();

            var col = f.MachinetagsGetPredicates();

            Assert.IsTrue(col.Count > 10, "Should be greater than 10 namespaces.");

            foreach (var n in col)
            {
                Assert.IsNotNull(n.PredicateName, "PredicateName should not be null.");
                Assert.AreNotEqual(0, n.Namespaces, "Namespaces should not be zero.");
                Assert.AreNotEqual(0, n.Usage, "Usage should not be zero.");
                Console.WriteLine(n.PredicateName + " has " + n.Namespaces + " predicates and a usage of " + n.Usage);
            }
        }

        [TestMethod]
        public void MachinetagsGetPairsBasicTest()
        {
            var pairs = TestData.GetInstance().MachinetagsGetPairs(null, null, 0, 0);
            Assert.IsNotNull(pairs);

            Assert.AreNotEqual(0, pairs.Count, "Count should not be zero.");

            foreach (Pair p in pairs)
            {
                Assert.IsNotNull(p.NamespaceName, "NamespaceName should not be null.");
                Assert.IsNotNull(p.PairName, "PairName should not be null.");
                Assert.IsNotNull(p.PredicateName, "PredicateName should not be null.");
                Assert.AreNotEqual(0, p.Usage, "Usage should be greater than zero.");
            }
        }


        [TestMethod]
        public void MachinetagsGetPairsNamespaceTest()
        {
            var pairs = TestData.GetInstance().MachinetagsGetPairs("dc", null, 0, 0);
            Assert.IsNotNull(pairs);

            Assert.AreNotEqual(0, pairs.Count, "Count should not be zero.");

            foreach (Pair p in pairs)
            {
                Assert.AreEqual("dc", p.NamespaceName, "NamespaceName should be 'dc'.");
                Assert.IsNotNull(p.PairName, "PairName should not be null.");
                Assert.IsTrue(p.PairName.StartsWith("dc:"), "PairName should start with 'dc:'.");
                Assert.IsNotNull(p.PredicateName, "PredicateName should not be null.");
                Assert.AreNotEqual(0, p.Usage, "Usage should be greater than zero.");

            }
        }

        [TestMethod]
        public void MachinetagsGetPairsPredicateTest()
        {
            var pairs = TestData.GetInstance().MachinetagsGetPairs(null, "author", 0, 0);
            Assert.IsNotNull(pairs);

            Assert.AreNotEqual(0, pairs.Count, "Count should not be zero.");

            foreach (Pair p in pairs)
            {
                Assert.AreEqual("author", p.PredicateName, "PredicateName should be 'dc'.");
                Assert.IsNotNull(p.PairName, "PairName should not be null.");
                Assert.IsTrue(p.PairName.EndsWith(":author"), "PairName should end with ':author'.");
                Assert.IsNotNull(p.NamespaceName, "NamespaceName should not be null.");
                Assert.AreNotEqual(0, p.Usage, "Usage should be greater than zero.");

                Console.WriteLine(p.PairName + " used " + p.Usage + " times");

            }
        }

        [TestMethod]
        public void MachinetagsGetPairsDcAuthorTest()
        {
            var pairs = TestData.GetInstance().MachinetagsGetPairs("dc", "author", 0, 0);
            Assert.IsNotNull(pairs);

            Assert.AreEqual(1, pairs.Count, "Count should be 1.");

            foreach (Pair p in pairs)
            {
                Assert.AreEqual("author", p.PredicateName, "PredicateName should be 'author'.");
                Assert.AreEqual("dc", p.NamespaceName, "NamespaceName should be 'dc'.");
                Assert.AreEqual("dc:author", p.PairName, "PairName should be 'dc:author'.");
                Assert.AreNotEqual(0, p.Usage, "Usage should be greater than zero.");

                Console.WriteLine(p.PairName + " used " + p.Usage + " times");
            }
        }

        [TestMethod]
        public void MachinetagsGetValuesTest()
        {
            var items = TestData.GetInstance().MachinetagsGetValues("dc", "author");
            Assert.IsNotNull(items);

            Assert.AreNotEqual(0, items.Count, "Count should be not be zero.");
            Assert.AreEqual("dc", items.NamespaceName);
            Assert.AreEqual("author", items.PredicateName);

            foreach (var item in items)
            {
                Assert.AreEqual("author", item.PredicateName, "PredicateName should be 'author'.");
                Assert.AreEqual("dc", item.NamespaceName, "NamespaceName should be 'dc'.");
                Assert.IsNotNull(item.ValueText, "ValueText should not be null.");
                Assert.AreNotEqual(0, item.Usage, "Usage should be greater than zero.");

                Console.WriteLine(item.ValueText + " used " + item.Usage + " times");
            }
        }

        [TestMethod]
        public void MachinetagsGetRecentValuesTest()
        {
            var items = TestData.GetInstance().MachinetagsGetRecentValues(DateTime.Now.AddDays(-1));
            Assert.IsNotNull(items);

            Assert.AreNotEqual(0, items.Count, "Count should be not be zero.");

            foreach (var item in items)
            {
                Assert.IsNotNull(item.NamespaceName, "NamespaceName should not be null.");
                Assert.IsNotNull(item.PredicateName, "PredicateName should not be null.");
                Assert.IsNotNull(item.ValueText, "ValueText should not be null.");
                Assert.IsNotNull(item.DateFirstAdded, "DateFirstAdded should not be null.");
                Assert.IsNotNull(item.DateLastUsed, "DateLastUsed should not be null.");
                Assert.AreNotEqual(0, item.Usage, "Usage should be greater than zero.");

                Console.WriteLine(item.ValueText + " used " + item.Usage + " times");
            }
        }
    }
}
