using BandOfPearl.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BandOfPearl.Test
{
    /// <summary>
    ///This is a test class for PearlNecklaceTest and is intended
    ///to contain all PearlNecklaceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BandTest
    {
        private TestContext? testContextInstance;

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

        [TestMethod()]
        public void Count_NewEmptyBand_ShouldReturnZero()
        {
            Band band = new Band();
            Assert.AreEqual(0, band.Count);
        }

        [TestMethod()]
        public void Count_AddOne_ShouldReturn1()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            Assert.AreEqual(1, band.Count);
        }

        [TestMethod()]
        public void Count_InsertMany_ShouldReturnCorrectNumber()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));
            Assert.AreEqual(7, band.Count);
        }

        [TestMethod()]
        public void Count_InsertNull_ShouldReturnCorrectNumber()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));

            band.AddPearl(null);
            Assert.AreEqual(7, band.Count);
        }

        [TestMethod()]
        public void GetPearlAtPosition_FirstPos_ShouldReturnPearlAtFirstPos()
        {
            Band band = new Band();
            Pearl pearl = new Pearl("Red", 1.5);
            band.AddPearl(pearl);
            Pearl getPearl = band.GetPearlAtPosition(0);
            Assert.AreSame(pearl, getPearl);
        }

        [TestMethod()]
        public void GetNumberOfColoredPearls_InsertOneOfSameColor_ShouldReturn1()
        {
            Band band = new Band();
            Pearl pearl = new Pearl("Red", 1.5);
            band.AddPearl(pearl);
            int count = band.GetNumberOfColoredPearls("Red");
            Assert.AreEqual(1, count);
        }

        [TestMethod()]
        public void GetNumberOfColoredPearls_InsertDifferentColor_ShouldReturn0()
        {
            Band band = new Band();
            Pearl pearl = new Pearl("Red", 1.5);
            band.AddPearl(pearl);
            int count = band.GetNumberOfColoredPearls("Blue");
            Assert.AreEqual(0, count);
        }

        [TestMethod()]
        public void GetNumberOfColoredPearls_InsertManyOfColorRed_ShouldReturnCorrectNumber()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));
            int count = band.GetNumberOfColoredPearls("Red");
            Assert.AreEqual(3, count);
        }

        [TestMethod()]
        public void GetNumberOfColoredPearls_InsertManyOfColorBlue_ShouldReturnCorrectNumber()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));
            int count = band.GetNumberOfColoredPearls("Blue");
            Assert.AreEqual(3, count);
        }

        [TestMethod()]
        public void GetNumberOfColoredPearls_InsertOneOfColorGreen_ShouldReturnCorrectNumber()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));
            int count = band.GetNumberOfColoredPearls("Green");
            Assert.AreEqual(1, count);
        }

        [TestMethod()]
        public void GetNumberOfColoredPearls_ColorIsNull_ShouldReturn0()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));

            int count = band.GetNumberOfColoredPearls(null);
            Assert.AreEqual(0, count);
        }

        [TestMethod()]
        public void GetPearlAtPosition_InsertMany_ShouldReturnCorrectPearlAtPos0()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            Pearl pearl = new Pearl("Red", 7);
            band.AddPearl(pearl);
            Pearl getPearl = band.GetPearlAtPosition(0);
            Assert.AreSame(pearl, getPearl);
        }

        [TestMethod()]
        public void GetPearlAtPosition_InsertMany_ShouldReturnCorrectPearlAtPos4()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            Pearl pearl = new Pearl("Blue", 3);
            band.AddPearl(pearl);
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));
            Pearl getPearl;
            getPearl = band.GetPearlAtPosition(4);
            Assert.AreSame(pearl, getPearl);
        }

        [TestMethod()]
        public void GetPearlAtPosition_InsertMany_ShouldReturnCorrectPearlAtPos6()
        {
            Band band = new Band();
            Pearl pearl = new Pearl("Red", 1.5);
            band.AddPearl(pearl);
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));
            Pearl getPearl;
            getPearl = band.GetPearlAtPosition(6);
            Assert.AreSame(pearl, getPearl);
        }

        [TestMethod()]
        public void GetPearlAtPosition_OutOfIndex_ShouldReturnNull()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));

            Pearl getPearl = band.GetPearlAtPosition(7);
            Assert.IsNull(getPearl);
        }

        [TestMethod()]
        public void GetPearlAtPosition_NegativIndex_ShouldReturnNull()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));

            Pearl getPearl = band.GetPearlAtPosition(-1);
            Assert.IsNull(getPearl);
        }

        [TestMethod()]
        public void RemovePearl_RemoveFirst_ShouldReturnPearlWith7Weight()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));

            Pearl pearl = band.RemovePearl();
            Assert.AreEqual(7, pearl.Weight,0.001);
        }

        [TestMethod()]
        public void RemovePearl_RemoveFromEmptyList_ShouldReturnNull()
        {
            Band band = new Band();
           

            Pearl pearl = band.RemovePearl();
            Assert.IsNull(pearl);
        }

        [TestMethod()]
        public void GetTotalWeight_FromEmptyList_ShouldReturn0()
        {
            Band band = new Band();


            double totalWeight = band.GetTotalWeight();
            Assert.AreEqual(0,totalWeight);
        }

        [TestMethod()]
        public void GetTotalWeight_FromFilledList_ShouldReturnCorrectWeight()
        {
            Band band = new Band();
            band.AddPearl(new Pearl("Red", 1.5));
            band.AddPearl(new Pearl("Blue", 2));
            band.AddPearl(new Pearl("Blue", 3));
            band.AddPearl(new Pearl("Red", 4.0));
            band.AddPearl(new Pearl("Green", 5.1));
            band.AddPearl(new Pearl("Blue", 6));
            band.AddPearl(new Pearl("Red", 7));

            double totalWeight = band.GetTotalWeight();
            Assert.AreEqual(28.6, totalWeight,0.01);
        }
    }
}
