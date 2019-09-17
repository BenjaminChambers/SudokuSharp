using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SudokuSharp;
using System.Linq;

namespace Tests
{
    [TestClass]
    class BoardCreation
    {
        static int[] Data = new int[] {
                9, 2, 3, 4, 1, 5, 8, 7, 6,
                8, 7, 1, 6, 3, 9, 2, 5, 4,
                6, 5, 4, 8, 2, 7, 1, 9, 3,
                1, 3, 8, 9, 4, 2, 5, 6, 7,
                2, 6, 5, 3, 7, 8, 9, 4, 1,
                7, 4, 9, 1, 5, 6, 3, 2, 8,
                4, 1, 2, 7, 9, 3, 6, 8, 5,
                5, 8, 7, 2, 6, 1, 4, 3, 9,
                3, 9, 6, 5, 8, 4, 7, 1, 2
            };

        [TestMethod]
        public void TestFill()
        {
            var b = new SudokuSharp.Board(Data);

            foreach (var idx in Location.All)
                Assert.AreEqual(b[idx], Data[idx]);


        }
    }
}
