using System;
using GildedRose;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        Mock<ILogger> _logger = new Mock<ILogger>();

        [TestMethod]
        public void TestMethod1()
        {
            var db = new SqlLite(_logger.Object);
            Assert.AreEqual(db.Name, "Sql");
            _logger.Verify(c=>c.Log("Name requested"), Times.Once);
        }
    }
}
