using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class Full
    {
        [TestMethod]
        public void EmptyBoard()
        {
            for (int i = 2; i < 6; i++)
                Assert.IsFalse(new Board(i).Full);
        }

        [TestMethod]
        public void FullBoard()
        {
            Assert.IsTrue(Common.Constructed2.Full);
            Assert.IsTrue(Common.Constructed3.Full);
        }

        [TestMethod]
        public void MissingOneCell()
        {
            for (int i = 0; i < 16; i++)
                Assert.IsFalse(Common.Constructed2.PutCell(i, 0).Full);

            for (int i = 0; i < 25; i++)
                Assert.IsFalse(Common.Constructed3.PutCell(i, 0).Full);
        }
    }
}
