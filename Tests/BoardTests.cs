using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class BoardTests
    {
        static int Iterations = 0;

        static List<SudokuSharp.Board> Work = new List<SudokuSharp.Board>();

        [ClassInitialize]
        public static void BoardTestsInitialize(TestContext context)
        {
            Iterations = int.Parse(context.Properties["Iterations"].ToString());
        }

        [TestMethod]
        public void CreateSolution()
        {
            for (int i = 0; i < Iterations; i++)
                Work.Add(SudokuSharp.Board.CreateSolution(i));
        }

        [TestMethod]
        public void CreatePuzzle()
        {
            for (int i = 0; i < Iterations; i++)
                SudokuSharp.Board.CreatePuzzle(Work[i], i, 30, 4);
        }
    }
}
