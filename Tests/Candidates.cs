using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class Candidates
    {
        [TestMethod]
        public void SingleCellCandidates()
        {
            for (int i=0; i<16; i++)
            {
                var work = Common.Constructed2.PutCell(i, 0);
                var c = work.FindCandidates(i);
                Assert.AreEqual(c.Count, 1);
                Assert.IsTrue(c.Contains(Common.Constructed2[i]));
            }

            for (int i=0; i<81; i++)
            {
                var work = Common.Constructed3.PutCell(i, 0);
                var c = work.FindCandidates(i);
                Assert.AreEqual(c.Count, 1);
                Assert.IsTrue(c.Contains(Common.Constructed3[i]));
            }
        }
    }
}
