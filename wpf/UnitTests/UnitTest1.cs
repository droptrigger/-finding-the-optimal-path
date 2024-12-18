using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using universitycollege.finding.model;
using universitycollege.finding.view;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreatingMap2x2()
        {
            Map map = new Map(2, 2);
            string final = "";
            foreach (sbyte temp in map.MapArr) { final += temp; }

            sbyte[,] testArr = new sbyte[,] { { 0, 0 }, { 0, 0 } };
            string expected = "";
            foreach (sbyte temp in testArr) { expected += temp; }
            Assert.AreEqual(expected, final);
        }

        [TestMethod]
        public void TestEmptyMapAmount10x10()
        {
            Map map = new Map(10, 10);
            Path optimalPath = new Path(map);

            Assert.AreEqual(500, map.GetAmount(optimalPath));
        }

        [TestMethod]
        public void TestMapHorizontalZeroAmount1x5()
        {
            Map map = new Map(1, 5);
            Path optimalPath = new Path(map);

            Assert.AreEqual(250, map.GetAmount(optimalPath));
        }

        [TestMethod]
        public void TestAddPatternToMap()
        {
            Map map = new Map(7, 7);
            TopologyGenerator generator = new TopologyGenerator(map);

            Pattern square = new Pattern("square_four.txt");

            generator.AddPatternTopology(square, new Map.Coords(3, 3));
            string final = "";
            foreach (sbyte temp in map.MapArr) { final += temp; }

            string expected = "";
            foreach (sbyte temp in square.PatternArr) { expected += temp; };

            Assert.AreEqual(expected, final);
        }

        [TestMethod]
        public void TestOptimalEqualLinnearPath()
        {
            Map map = new Map(10, 10);
            Path optimalPath = new Path(map);
            LinnearPath linnearPath = new LinnearPath(map);

            Assert.AreEqual(map.GetAmount(linnearPath), map.GetAmount(optimalPath));
        }

        [TestMethod]
        public void OptimalPathEqualCoords()
        {
            Map map = new Map(10, 10);
            Path optimalPath = new Path(map, new Map.Coords(0, 0), new Map.Coords(0, 0));

            Assert.AreEqual(50, map.GetAmount(optimalPath));
        }

        [TestMethod]
        public void OptimalPathLeftDownToTopRight()
        {
            Map map = new Map(10, 10);
            Path optimalPath = new Path(map, new Map.Coords(0, 9), new Map.Coords(9, 0));

            Assert.AreEqual(500, map.GetAmount(optimalPath));
        }

        [TestMethod]
        public void TestEqualPattern()
        {
            Pattern pattern = new Pattern("square_four.txt");
            sbyte[,] tempArr = ReaderPatternFile.GetPatternArr("square_four.txt");

            string final = "";
            foreach (sbyte temp in pattern.PatternArr) { final += temp; };

            string expected = "";
            foreach (sbyte temp in tempArr) { expected += temp; };

            Assert.AreEqual(expected, final);
        }

        [TestMethod]
        public void TestCoordsIsNotInAMap()
        {
            Map map = new Map(10, 10);
            bool not = map.IsInAMap(new Map.Coords(-10, -10));

            Assert.IsFalse(not);
        }

        [TestMethod]
        public void TestPointAreHiger()
        {
            Map map = new Map(10, 10);
            bool yes = map.IsPointAreHigher(new Map.Coords(0, 0), 1);

            Assert.IsTrue(yes);
        }
    }
}
