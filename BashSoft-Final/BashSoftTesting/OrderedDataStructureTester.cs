using Bashsoft.DataStructures;
using Bashsoft.IO.Contracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BashSoftTesting
{
    public class OrderedDataStructureTester
    {
        private ISimpleOrderedBag<string> names;

        [SetUp]
        public void SetUp()
        {
            this.names = new SimpleSortedList<string>();
        }

        [Test]
        public void TestEmptyCtor()
        {
            this.names = new SimpleSortedList<string>();
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithInitialCapacity()
        {
            this.names = new SimpleSortedList<string>(20);
            Assert.AreEqual(this.names.Capacity, 20);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithAllParams()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase ,30);
            Assert.AreEqual(this.names.Capacity, 30);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithInitialComparer()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestAddIncreasesSize()
        {
            this.names.Add("Gosho");
            Assert.AreEqual(1, this.names.Size);
        }

        [Test]
        public void TestAddNullThrowsException()
        {
            Assert.That(() => this.names.Add(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestAddUnsortedDataIsHeldSorted()
        {
            string[] sortedTestData = new string[] {"Balkan", "Georgi", "Rosen" };

            string[] unsortedTestData = new string[] { "Rosen", "Georgi", "Balkan" };

            foreach (var item in unsortedTestData)
            {
                this.names.Add(item);
            }

            Assert.That(this.names, Is.EquivalentTo(sortedTestData));
        }

        [Test]
        public void TestAddingMoreThanInitialCapacity()
        {
            this.names = new SimpleSortedList<string>();

            for (int i = 0; i < 17; i++)
            {
                this.names.Add("Pesho");
            }
            Assert.AreEqual(17, this.names.Size);
            Assert.That(this.names.Capacity != 16);
        }

        [Test]
        public void TestAddingAllFromCollectionIncreasesSize()
        {
            List<string> testList = new List<string>(new string[] { "pesho", "gosho" });

            this.names = new SimpleSortedList<string>();

            this.names.AddAll(testList);

            Assert.AreEqual(2, this.names.Size);
        }

        [Test]
        public void TestAddingAllFromNullThrowsException()
        {
            this.names = new SimpleSortedList<string>();

            Assert.That(() => this.names.AddAll(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestAddAllKeepsSorted()
        {
            this.names = new SimpleSortedList<string>();

            string[] sortedData = new string[] { "a", "b", "c", "d" };

            string[] unsortedData = new string[] { "d", "a", "c", "b" };

            this.names.AddAll(unsortedData);

            Assert.That(this.names, Is.EquivalentTo(sortedData));
        }

        [Test]
        public void TestRemoveValidElementDecreasesSize()
        {
            this.names = new SimpleSortedList<string>();

            this.names.Add("Stamat");

            this.names.Remove("Stamat");

            Assert.AreEqual(0, this.names.Size);
        }

        [Test]
        public void TestRemoveValidElementRemovesSelectedOne()
        {
            this.names = new SimpleSortedList<string>();

            this.names.AddAll(new string[] { "Pesho", "Gosho" });

            this.names.Remove("Pesho");

            Assert.That(!this.names.Any(n => n == "Pesho"));
        }

        [Test]
        public void TestRemovingNullThrowsException()
        {
            this.names = new SimpleSortedList<string>();

            Assert.That(() => this.names.Remove(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestJoinWithNull()
        {
            this.names = new SimpleSortedList<string>();

            this.names.AddAll(new string[] { "Pesho", "Gosho" });

            Assert.That(() => this.names.JoinWith(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestJoinWorksFine()
        {
            this.names = new SimpleSortedList<string>();

            this.names.AddAll(new string[] { "Gosho", "Pesho" });

            string testResultString = "Gosho, Pesho";

            string result = this.names.JoinWith(", ");

            Assert.AreEqual(testResultString, result);
        }
    }
}
