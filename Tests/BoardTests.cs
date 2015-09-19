using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class BoardTests
    {
        static int Iterations = 0;
        static bool Randomize = false;
        static Random rnd = null;

        [ClassInitialize]
        public static void BoardTestsInitialize(TestContext context)
        {
            Iterations = int.Parse(context.Properties["Iterations"].ToString());
            Randomize = bool.Parse(context.Properties["Randomize"].ToString());

            int seed = int.Parse(context.Properties["Seed"].ToString());
            rnd = new Random(seed);
        }

        [TestMethod]
        public void CreateSolution()
        {
            if (Randomize)
            {
                for (int i = 0; i < Iterations; i++)
                    SudokuSharp.Board.CreateSolution(rnd.Next());
            }
            else
            {
                for (int i = 0; i < Iterations; i++)
                    SudokuSharp.Board.CreateSolution(i);
            }
        }

        private void RunCreateTest(int Givens, int BatchSize)
        {
            if (Randomize)
            {
                for (int i = 0; i < Iterations; i++)
                    SudokuSharp.Board.CreatePuzzle(
                        SudokuSharp.Board.CreateSolution(rnd.Next()),
                        rnd.Next(),
                        Givens, BatchSize);
            }
            else
            {
                for (int i = 0; i < Iterations; i++)
                    SudokuSharp.Board.CreatePuzzle(SudokuSharp.Board.CreateSolution(i), i, Givens, BatchSize);
            }
        }

        [TestMethod]
        public void CreatePuzzleEasyLarge()
        {
            RunCreateTest(60, 8);
        }
        [TestMethod]
        public void CreatePuzzleEasyMedium()
        {
            RunCreateTest(60, 4);
        }
        [TestMethod]
        public void CreatePuzzleEasySmall()
        {
            RunCreateTest(60, 2);
        }
        [TestMethod]
        public void CreatePuzzleMediumLarge()
        {
            RunCreateTest(45, 8);
        }
        [TestMethod]
        public void CreatePuzzleMediumMedium()
        {
            RunCreateTest(45, 8);
        }
        [TestMethod]
        public void CreatePuzzleMediumSmall()
        {
            RunCreateTest(45, 8);
        }
        [TestMethod]
        public void CreatePuzzleHardLarge()
        {
            RunCreateTest(25, 8);
        }
        [TestMethod]
        public void CreatePuzzleHardMedium()
        {
            RunCreateTest(25, 4);
        }
        [TestMethod]
        public void CreatePuzzleHardSmall()
        {
            RunCreateTest(25, 2);
        }
    }
}
