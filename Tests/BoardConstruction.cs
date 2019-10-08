using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class BoardConstruction
    {
        [TestMethod]
        public void ConstructorDefault()
        {
            var work = new Board(2);
            for (int i = 0; i < 16; i++)
                Assert.AreEqual(work[i], 0);

            work = new Board(3);
            for (int i = 0; i < 81; i++)
                Assert.AreEqual(work[i], 0);

            work = new Board(4);
            for (int i = 0; i < 256; i++)
                Assert.AreEqual(work[i], 0);
        }

        [TestMethod]
        public void ConstructorIEnumerable()
        {
            for (int i = 0; i < 16; i++)
                Assert.AreEqual(Common.Constructed2[i], Common.Raw2[i]);

            for (int i = 0; i < 81; i++)
                Assert.AreEqual(Common.Constructed3[i], Common.Raw3[i]);
        }

        [TestMethod]
        public void ConstructorCopy()
        {
            for (int i = 0; i < 16; i++)
                Assert.AreEqual(Common.Constructed2[i], Common.Copied2[i]);

            for (int i = 0; i < 81; i++)
                Assert.AreEqual(Common.Constructed3[i], Common.Copied3[i]);
        }
    }
}
