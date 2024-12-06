using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BandOfPearl.Test
{
    /// <summary>
    ///This is a test class for PearlTest and is intended
    ///to contain all PearlTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PearlTest
    {
        private TestContext? testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext? TestContext
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

        [TestMethod()]
        public void Constructor_NewRedPearl_ShouldReturnCorrectColor()
        {
            Pearl pearl = new Pearl("Red", 1.7);
            Assert.AreEqual("Red", pearl.Color);
        }

        [TestMethod()]
        public void Constructor_NewGreenPearl_ShouldReturnCorrectColor()
        {
            Pearl pearl = new Pearl("Green", 1.7);
            Assert.AreEqual("Green", pearl.Color);
        }

        [TestMethod()]
        public void Constructor_NewBluePearl_ShouldReturnCorrectColor()
        {
            Pearl pearl = new Pearl("Blue", 1.7);
            Assert.AreEqual("Blue", pearl.Color);
        }

        [TestMethod()]
        public void Constructor_NewPearl_ShouldReturnCorrectWeight()
        {
            Pearl pearl = new Pearl("Red", 1.7);
            Assert.AreEqual(1.7, pearl.Weight);
        }

        [TestMethod()]
        public void Constructor_InvalidColor_ShouldReturnUnknownColor()
        {
            Pearl pearl = new Pearl("Yellow", -10);
            Assert.AreEqual("Unknown", pearl.Color);
        }

        [TestMethod()]
        public void Constructor_WrongWeight_ShouldReturnZero()
        {
            Pearl pearl = new Pearl("Yellow", -10);
            Assert.AreEqual(0, pearl.Weight);
        }

        [TestMethod()]
        public void Constructor_EmptyColor_ShouldReturnUnknownColor()
        {
            Pearl pearl = new Pearl("", 17);
            Assert.AreEqual("Unknown", pearl.Color);
        }

        [TestMethod()]
        public void Constructor_NegativeWeight_ShouldReturnZero()
        {
            Pearl pearl = new Pearl("red", -10);
            Assert.AreEqual(0, pearl.Weight);
        }
    }
}
