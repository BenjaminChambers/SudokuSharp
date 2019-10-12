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
            Common.CompareIEnum((from x in Location.RowIndices(2, 0) select Common.Constructed2[x]), new[] { 1, 2, 3, 4 });
            Common.CompareIEnum((from x in Location.RowIndices(2, 0) select Common.Constructed2[x]), new[] { 1, 2, 3, 4 });
            Common.CompareIEnum((from x in Location.RowIndices(2, 1) select Common.Constructed2[x]), new[] { 3, 4, 1, 2 });
            Common.CompareIEnum((from x in Location.RowIndices(2, 2) select Common.Constructed2[x]), new[] { 2, 3, 4, 1 });
            Common.CompareIEnum((from x in Location.RowIndices(2, 3) select Common.Constructed2[x]), new[] { 4, 1, 2, 3 });

            Common.CompareIEnum((from x in Location.RowIndices(3, 0) select Common.Constructed3[x]), new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Common.CompareIEnum((from x in Location.RowIndices(3, 1) select Common.Constructed3[x]), new[] { 4, 5, 6, 7, 8, 9, 1, 2, 3 });
            Common.CompareIEnum((from x in Location.RowIndices(3, 2) select Common.Constructed3[x]), new[] { 7, 8, 9, 1, 2, 3, 4, 5, 6 });
            Common.CompareIEnum((from x in Location.RowIndices(3, 3) select Common.Constructed3[x]), new[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 });
            Common.CompareIEnum((from x in Location.RowIndices(3, 4) select Common.Constructed3[x]), new[] { 5, 6, 7, 8, 9, 1, 2, 3, 4 });
            Common.CompareIEnum((from x in Location.RowIndices(3, 5) select Common.Constructed3[x]), new[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 });
            Common.CompareIEnum((from x in Location.RowIndices(3, 6) select Common.Constructed3[x]), new[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 });
            Common.CompareIEnum((from x in Location.RowIndices(3, 7) select Common.Constructed3[x]), new[] { 6, 7, 8, 9, 1, 2, 3, 4, 5 });
            Common.CompareIEnum((from x in Location.RowIndices(3, 8) select Common.Constructed3[x]), new[] { 9, 1, 2, 3, 4, 5, 6, 7, 8 });
        }

        [TestMethod]
        public void GetColumn()
        {
            Common.CompareIEnum((from x in Location.ColumnIndices(2, 0) select Common.Constructed2[x]), new[] { 1, 3, 2, 4 });
            Common.CompareIEnum((from x in Location.ColumnIndices(2, 1) select Common.Constructed2[x]), new[] { 2, 4, 3, 1 });
            Common.CompareIEnum((from x in Location.ColumnIndices(2, 2) select Common.Constructed2[x]), new[] { 3, 1, 4, 2 });
            Common.CompareIEnum((from x in Location.ColumnIndices(2, 3) select Common.Constructed2[x]), new[] { 4, 2, 1, 3 });

            Common.CompareIEnum((from x in Location.ColumnIndices(3, 0) select Common.Constructed3[x]), new[] { 1, 4, 7, 2, 5, 8, 3, 6, 9 });
            Common.CompareIEnum((from x in Location.ColumnIndices(3, 1) select Common.Constructed3[x]), new[] { 2, 5, 8, 3, 6, 9, 4, 7, 1 });
            Common.CompareIEnum((from x in Location.ColumnIndices(3, 2) select Common.Constructed3[x]), new[] { 3, 6, 9, 4, 7, 1, 5, 8, 2 });
            Common.CompareIEnum((from x in Location.ColumnIndices(3, 3) select Common.Constructed3[x]), new[] { 4, 7, 1, 5, 8, 2, 6, 9, 3 });
            Common.CompareIEnum((from x in Location.ColumnIndices(3, 4) select Common.Constructed3[x]), new[] { 5, 8, 2, 6, 9, 3, 7, 1, 4 });
            Common.CompareIEnum((from x in Location.ColumnIndices(3, 5) select Common.Constructed3[x]), new[] { 6, 9, 3, 7, 1, 4, 8, 2, 5 });
            Common.CompareIEnum((from x in Location.ColumnIndices(3, 6) select Common.Constructed3[x]), new[] { 7, 1, 4, 8, 2, 5, 9, 3, 6 });
            Common.CompareIEnum((from x in Location.ColumnIndices(3, 7) select Common.Constructed3[x]), new[] { 8, 2, 5, 9, 3, 6, 1, 4, 7 });
            Common.CompareIEnum((from x in Location.ColumnIndices(3, 8) select Common.Constructed3[x]), new[] { 9, 3, 6, 1, 4, 7, 2, 5, 8 });
        }

        [TestMethod]
        public void GetZone()
        {
            Common.CompareIEnum((from x in Location.ZoneIndices(2, 0) select Common.Constructed2[x]), new[] { 1, 2, 3, 4 });
            Common.CompareIEnum((from x in Location.ZoneIndices(2, 1) select Common.Constructed2[x]), new[] { 3, 4, 1, 2 });
            Common.CompareIEnum((from x in Location.ZoneIndices(2, 2) select Common.Constructed2[x]), new[] { 2, 3, 4, 1 });
            Common.CompareIEnum((from x in Location.ZoneIndices(2, 3) select Common.Constructed2[x]), new[] { 4, 1, 2, 3 });

            Common.CompareIEnum((from x in Location.ZoneIndices(3, 0) select Common.Constructed3[x]), new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Common.CompareIEnum((from x in Location.ZoneIndices(3, 1) select Common.Constructed3[x]), new[] { 4, 5, 6, 7, 8, 9, 1, 2, 3 });
            Common.CompareIEnum((from x in Location.ZoneIndices(3, 2) select Common.Constructed3[x]), new[] { 7, 8, 9, 1, 2, 3, 4, 5, 6 });
            Common.CompareIEnum((from x in Location.ZoneIndices(3, 3) select Common.Constructed3[x]), new[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 });
            Common.CompareIEnum((from x in Location.ZoneIndices(3, 4) select Common.Constructed3[x]), new[] { 5, 6, 7, 8, 9, 1, 2, 3, 4 });
            Common.CompareIEnum((from x in Location.ZoneIndices(3, 5) select Common.Constructed3[x]), new[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 });
            Common.CompareIEnum((from x in Location.ZoneIndices(3, 6) select Common.Constructed3[x]), new[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 });
            Common.CompareIEnum((from x in Location.ZoneIndices(3, 7) select Common.Constructed3[x]), new[] { 6, 7, 8, 9, 1, 2, 3, 4, 5 });
            Common.CompareIEnum((from x in Location.ZoneIndices(3, 8) select Common.Constructed3[x]), new[] { 9, 1, 2, 3, 4, 5, 6, 7, 8 });
        }
    }
}
