using System;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    [TestFixture]
    public class MachinetagsTests : BaseTest
    {
        [Test]
        public void MachinetagsGetNamespacesBasicTest()
        {
            NamespaceCollection col = Instance.MachineTagsGetNamespaces();

            Assert.IsTrue(col.Count > 10, "Should be greater than 10 namespaces.");

            foreach (var n in col)
            {
                Assert.IsNotNull(n.NamespaceName, "NamespaceName should not be null.");
                Assert.AreNotEqual(0, n.Predicates, "Predicates should not be zero.");
                Assert.AreNotEqual(0, n.Usage, "Usage should not be zero.");
            }
        }

        [Test]
        public void MachinetagsGetPredicatesBasicTest()
        {
            var col = Instance.MachineTagsGetPredicates();

            Assert.IsTrue(col.Count > 10, "Should be greater than 10 namespaces.");

            foreach (var n in col)
            {
                Assert.IsNotNull(n.PredicateName, "PredicateName should not be null.");
                Assert.AreNotEqual(0, n.Namespaces, "Namespaces should not be zero.");
                Assert.AreNotEqual(0, n.Usage, "Usage should not be zero.");
            }
        }

        [Test]
        public void MachinetagsGetPairsBasicTest()
        {
            var pairs = Instance.MachineTagsGetPairs(null, null, 0, 0);
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


        [Test]
        public void MachinetagsGetPairsNamespaceTest()
        {
            var pairs = Instance.MachineTagsGetPairs("dc", null, 0, 0);
            Assert.IsNotNull(pairs);

            Assert.AreNotEqual(0, pairs.Count, "Count should not be zero.");

            foreach (Pair p in pairs)
            {
                Assert.AreEqual("dc", p.NamespaceName, "NamespaceName should be 'dc'.");
                Assert.IsNotNull(p.PairName, "PairName should not be null.");
                Assert.IsTrue(p.PairName.StartsWith("dc:", StringComparison.Ordinal), "PairName should start with 'dc:'.");
                Assert.IsNotNull(p.PredicateName, "PredicateName should not be null.");
                Assert.AreNotEqual(0, p.Usage, "Usage should be greater than zero.");

            }
        }

        [Test]
        public void MachinetagsGetPairsPredicateTest()
        {
            var pairs = Instance.MachineTagsGetPairs(null, "author", 0, 0);
            Assert.IsNotNull(pairs);

            Assert.AreNotEqual(0, pairs.Count, "Count should not be zero.");

            foreach (Pair p in pairs)
            {
                Assert.AreEqual("author", p.PredicateName, "PredicateName should be 'dc'.");
                Assert.IsNotNull(p.PairName, "PairName should not be null.");
                Assert.IsTrue(p.PairName.EndsWith(":author", StringComparison.Ordinal), "PairName should end with ':author'.");
                Assert.IsNotNull(p.NamespaceName, "NamespaceName should not be null.");
                Assert.AreNotEqual(0, p.Usage, "Usage should be greater than zero.");

            }
        }

        [Test]
        public void MachinetagsGetPairsDcAuthorTest()
        {
            var pairs = Instance.MachineTagsGetPairs("dc", "author", 0, 0);
            Assert.IsNotNull(pairs);

            Assert.AreEqual(1, pairs.Count, "Count should be 1.");

            foreach (Pair p in pairs)
            {
                Assert.AreEqual("author", p.PredicateName, "PredicateName should be 'author'.");
                Assert.AreEqual("dc", p.NamespaceName, "NamespaceName should be 'dc'.");
                Assert.AreEqual("dc:author", p.PairName, "PairName should be 'dc:author'.");
                Assert.AreNotEqual(0, p.Usage, "Usage should be greater than zero.");

            }
        }

        [Test]
        public void MachinetagsGetValuesTest()
        {
            var items = Instance.MachineTagsGetValues("dc", "author");
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
            }
        }

        [Test]
        public void MachinetagsGetRecentValuesTest()
        {
            var items = Instance.MachineTagsGetRecentValues(DateTime.Now.AddHours(-5));
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
            }
        }
    }
}
