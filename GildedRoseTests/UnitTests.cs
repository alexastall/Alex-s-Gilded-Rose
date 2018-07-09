using System;
using System.Collections.Generic;
using GildedRose;
using GildedRose.Interfaces;
using GildedRose.Models;
using GildedRose.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GildedRoseTests
{
    [TestClass]
    public class UnitTests
    {
        readonly Mock<ILogger> _logger = new Mock<ILogger>();

        [TestMethod]
        public void TestLoggerWorks()
        {
            _logger.Object.Log("Nothing done");
            // Can use Mock to simulate logger and test that a specific message has been logged.
            _logger.Verify(c => c.Log("Nothing done"), Times.Once);
        }

        [TestMethod]
        public void TestDoNothingCalculator()
        {
            var calculator = new DoNothingUpdate();

            var testItem = new Item { Name = "Test", SellIn = 1, Quality = 1 };
            calculator.UpdateQuality(testItem);

            var expectedItem = new Item { Name = "Test", SellIn = 1, Quality = 1 };
            Assert.AreEqual(testItem.Quality, expectedItem.Quality);
            Assert.AreEqual(testItem.SellIn, expectedItem.SellIn);
        }

        [TestMethod]
        public void TestAgedBrieQualityCalculator()
        {
            var calculator = new AgedBrieQualityCalculator();

            var testItem = new Item { Name = "Test", SellIn = 1, Quality = 1 };
            calculator.UpdateQuality(testItem);

            var expectedItem = new Item { Name = "Test", SellIn = 0, Quality = 2 };
            Assert.AreEqual(testItem.Quality, expectedItem.Quality);
            Assert.AreEqual(testItem.SellIn, expectedItem.SellIn);
        }

        [TestMethod]
        public void TestBackstagePassesQualityCalculator()
        {
            var calculator = new BackstagePassesQualityCalculator();

            var testItems = new List<Item>
            {
                new Item {Name = "Test 1", SellIn = -1, Quality = 2},
                new Item {Name = "Test 2", SellIn = 9, Quality = 2}
            };

            var expectedItems = new List<Item>
            {
                new Item {Name = "Test 1", SellIn = -2, Quality = 0},
                new Item {Name = "Test 2", SellIn = 8, Quality = 4}
            };

            foreach (var item in testItems)
            {
                calculator.UpdateQuality(item);
            }
            
            for (int i = 0; i < testItems.Count; i++)
            {
                Assert.AreEqual(testItems[i].Quality, expectedItems[i].Quality);
                Assert.AreEqual(testItems[i].SellIn, expectedItems[i].SellIn);
            }
        }


        /*
         * THIS TEST FAILS, Either the expected result is incorrect or my calculator logic is wrong. :-(
         */
        [TestMethod]
        public void TestStandardQualityCalculator1()
        {
            var calculator = new StandardQualityCalculator();

            var testItem = new Item { Name = "Test", SellIn = -1, Quality = 55 };
            calculator.UpdateQuality(testItem);

            var expectedItem = new Item { Name = "Test", SellIn = -2, Quality = 50 };
            Assert.AreEqual(testItem.Quality, expectedItem.Quality);
            Assert.AreEqual(testItem.SellIn, expectedItem.SellIn);
        }

        [TestMethod]
        public void TestStandardQualityCalculator2()
        {
            var calculator = new StandardQualityCalculator();

            var testItem = new Item { Name = "Test", SellIn = 2, Quality = 2 };
            calculator.UpdateQuality(testItem);

            var expectedItem = new Item { Name = "Test", SellIn = 1, Quality = 1 };
            Assert.AreEqual(testItem.Quality, expectedItem.Quality);
            Assert.AreEqual(testItem.SellIn, expectedItem.SellIn);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestInvalidItem()
        {
            var calculator = new StandardQualityCalculator();

            var testItem = new Item { Name = "INVALID ITEM", SellIn = 1, Quality = 1 };
            calculator.UpdateQuality(testItem);
        }

        [TestMethod]
        public void TestConjuredQualityCalculator()
        {
            var calculator = new StandardQualityCalculator(2);

            var testItems = new List<Item>
            {
                new Item {Name = "Test 1", SellIn = 2, Quality = 2},
                new Item {Name = "Test 2", SellIn = -1, Quality = 5}
            };

            var expectedItems = new List<Item>
            {
                new Item {Name = "Test 1", SellIn = 1, Quality = 0},
                new Item {Name = "Test 2", SellIn = -2, Quality = 1}
            };

            foreach (var item in testItems)
            {
                calculator.UpdateQuality(item);
            }

            for (int i = 0; i < testItems.Count; i++)
            {
                Assert.AreEqual(testItems[i].Quality, expectedItems[i].Quality);
                Assert.AreEqual(testItems[i].SellIn, expectedItems[i].SellIn);
            }
        }
    }
}
