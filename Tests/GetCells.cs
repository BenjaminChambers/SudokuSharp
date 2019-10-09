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
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetRow(5), new[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 }));
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

            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetColumn(0), new[] { 1, 4, 7, 2, 5, 8, 3, 6, 9 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetColumn(1), new[] { 2, 5, 8, 3, 6, 9, 4, 7, 1 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetColumn(2), new[] { 3, 6, 9, 4, 7, 1, 5, 8, 2 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetColumn(3), new[] { 4, 7, 1, 5, 8, 2, 6, 9, 3 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetColumn(4), new[] { 5, 8, 2, 6, 9, 3, 7, 1, 4 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetColumn(5), new[] { 6, 9, 3, 7, 1, 4, 8, 2, 5 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetColumn(6), new[] { 7, 1, 4, 8, 2, 5, 9, 3, 6 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetColumn(7), new[] { 8, 2, 5, 9, 3, 6, 1, 4, 7 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetColumn(8), new[] { 9, 3, 6, 1, 4, 7, 2, 5, 8 }));
        }

        [TestMethod]
        public void GetZone()
        {
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetZone(0), new[] { 1, 2, 3, 4 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetZone(1), new[] { 3, 4, 1, 2 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetZone(2), new[] { 2, 3, 4, 1 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed2.GetZone(3), new[] { 4, 1, 2, 3 }));

            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetZone(0), new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetZone(1), new[] { 4, 5, 6, 7, 8, 9, 1, 2, 3 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetZone(2), new[] { 7, 8, 9, 1, 2, 3, 4, 5, 6 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetZone(3), new[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetZone(4), new[] { 5, 6, 7, 8, 9, 1, 2, 3, 4 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetZone(5), new[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetZone(6), new[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetZone(7), new[] { 6, 7, 8, 9, 1, 2, 3, 4, 5 }));
            Assert.IsTrue(Common.CompareArrays(Common.Constructed3.GetZone(8), new[] { 9, 1, 2, 3, 4, 5, 6, 7, 8 }));
        }
    }
}
