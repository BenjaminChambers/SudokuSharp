using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SudokuSharp;
using System.Linq;

namespace Tests
{
    [TestClass]
    class GetCells
    {
        static int[] Raw = new int[] {
                1,2,3,4,
                3,4,1,2,
                2,3,4,1,
                4,1,2,3
            };
        static Board Work = new Board(2, Raw);

        [TestMethod]
        public void TestGetCell()
        {
            for (int i=0; i<16; i++)
            {
                Assert.AreEqual(Raw[i], Work.GetCell(i));
                Assert.AreEqual(Raw[i], Work[i]);
            }
        }

    }
}
