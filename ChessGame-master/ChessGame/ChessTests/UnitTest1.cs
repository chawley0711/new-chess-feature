using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ChessTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGoodKingPlacement()
        {
            int king = 3;
            int badValue1 = 0;
            int badValue2 = 7;

            Assert.IsTrue(king != badValue1 && king != badValue2);
        }
        [TestMethod]
        public void TestBadKingPlacement()
        {
            int king = 7;
            int badValue1 = 0;
            int badValue2 = 7;

            Assert.IsTrue(king != badValue1 && king != badValue2);
        }
        [TestMethod]
        public void TestGoodRook2Placement()
        {
            int rook1 = 2;
            int king = 3;
            int rook2 = 6;
    
            Assert.IsTrue(((rook2 < king) && (king < rook1)) || ((rook1 < king) && (king < rook2)));            
        }
        [TestMethod]
        public void TestBadRook2Placement()
        {
            int rook1 = 2;
            int king = 3;
            int rook2 = 0;

            Assert.IsTrue(((rook2 < king) && (king < rook1)) || ((rook1 < king) && (king < rook2)));
        }
        [TestMethod]
        public void TestGoodBishopPlacement()
        {
            int bishop1 = 2;
            int bishop2 = 5;
            List<int> evens = new List<int>();
            evens.AddRange(new int[]
            {
                0, 2, 4, 6
            });
            List<int> odds = new List<int>();
            odds.AddRange(new int[]
            {
                1, 3, 5, 7
            });

            Assert.IsTrue((evens.Contains(bishop1) && odds.Contains(bishop2)) || (evens.Contains(bishop2) && odds.Contains(bishop1)));
        }
        [TestMethod]
        public void TestBadBishopPlacement()
        {
            int bishop1 = 2;
            int bishop2 = 6;
            List<int> evens = new List<int>();
            evens.AddRange(new int[]
            {
                0, 2, 4, 6
            });
            List<int> odds = new List<int>();
            odds.AddRange(new int[]
            {
                1, 3, 5, 7
            });

            Assert.IsTrue((evens.Contains(bishop1) && odds.Contains(bishop2)) || (evens.Contains(bishop2) && odds.Contains(bishop1)));
        }
    }
}
