using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class Solved
    {
        [TestMethod]
        public void EmptyBoard()
        {
            for (int i = 2; i < 6; i++)
                Assert.IsFalse(new Board(i).Solved);
        }

        [TestMethod]
        public void FullBoard()
        {
            Assert.IsTrue(Common.Constructed2.Solved);
            Assert.IsTrue(Common.Constructed3.Solved);
        }

        [TestMethod]
        public void MissingOneCell()
        {
            for (int i = 0; i < 16; i++)
                Assert.IsFalse(Common.Constructed2.PutCell(i, 0).Solved);

            for (int i = 0; i < 25; i++)
                Assert.IsFalse(Common.Constructed3.PutCell(i, 0).Solved);
        }
    }
}
