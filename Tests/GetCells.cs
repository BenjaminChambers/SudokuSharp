using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SudokuSharp;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class GetCells
    {
        [TestMethod]
        public void GetRow()
        {
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetRow(0), new[] { 1, 2, 3, 4 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetRow(1), new[] { 3, 4, 1, 2 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetRow(2), new[] { 2, 3, 4, 1 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetRow(3), new[] { 4, 1, 2, 3 }));

            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetRow(0), new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetRow(1), new[] { 4, 5, 6, 7, 8, 9, 1, 2, 3 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetRow(2), new[] { 7, 8, 9, 1, 2, 3, 4, 5, 6 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetRow(3), new[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetRow(4), new[] { 5, 6, 7, 8, 9, 1, 2, 3, 4 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetRow(5), new[] { 8, 9, 1, 9, 1, 2, 3, 4, 5 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetRow(6), new[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetRow(7), new[] { 6, 7, 8, 9, 1, 2, 3, 4, 5 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetRow(8), new[] { 9, 1, 2, 3, 4, 5, 6, 7, 8 }));
        }

        [TestMethod]
        public void GetColumn()
        {
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetColumn(0), new[] { 1, 3, 2, 4 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetColumn(1), new[] { 2, 4, 3, 1 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetColumn(2), new[] { 3, 1, 4, 2 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetColumn(3), new[] { 4, 2, 1, 3 }));
        }

        [TestMethod]
        public void GetZone()
        {
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetZone(0), new[] { 1, 2, 3, 4 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetZone(1), new[] { 3, 4, 1, 2 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetZone(2), new[] { 2, 3, 4, 1 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetZone(3), new[] { 4, 1, 2, 3 }));
        }
    }
}
