using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SudokuSharp;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class BoardTests
    {
        static int Iterations = 0;
        static int BatchSize = 0;
        static int NumBatches = 0;

        static Random rnd = null;

        [ClassInitialize]
        public static void BoardTestsInitialize(TestContext context)
        {
            Iterations = int.Parse(context.Properties["Number Of Puzzles"].ToString());
            BatchSize = int.Parse(context.Properties["Batch Size"].ToString());
            NumBatches = int.Parse(context.Properties["Number of Batches"].ToString());

            int seed = int.Parse(context.Properties["Seed"].ToString());
            rnd = new Random(seed);
        }

        [TestMethod]
        public void CreateSolutions()
        {
            for (int i = 0; i < Iterations; i++)
                Factory.Solution(rnd);
        }

        [TestMethod]
        public void CutQuads()
        {
            int[] givens = new int[NumBatches];

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd);

                for (int batch = 0; batch < NumBatches; batch++)
                {
                    for (int test = 0; test < BatchSize; test++)
                        work = work.Cut.Quad(rnd);

                    givens[batch] += work.Find.FilledLocations().Count();
                }
            }

            for (int i = 0; i < NumBatches; i++)
                Console.WriteLine("After " + (i + 1) * BatchSize + " cuts, an average of " + ((double)givens[i] / (double)Iterations) + " givens per board.");
        }

        [TestMethod]
        public void CutPairs()
        {
            int[] givens = new int[NumBatches];

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd);

                for (int batch = 0; batch < NumBatches; batch++)
                {
                    for (int test = 0; test < BatchSize; test++)
                        work = work.Cut.Pair(rnd);

                    givens[batch] += work.Find.FilledLocations().Count();
                }
            }

            for (int i = 0; i < NumBatches; i++)
                Console.WriteLine("After " + (i + 1) * BatchSize + " cuts, an average of " + ((double)givens[i] / (double)Iterations) + " givens per board.");
        }

        [TestMethod]
        public void CutSingles()
        {
            int[] givens = new int[NumBatches];

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd);

                for (int batch = 0; batch < NumBatches; batch++)
                {
                    for (int test = 0; test < BatchSize; test++)
                        work = work.Cut.Single(rnd);

                    givens[batch] += work.Find.FilledLocations().Count();
                }
            }

            for (int i = 0; i < NumBatches; i++)
                Console.WriteLine("After " + (i + 1) * BatchSize + " cuts, an average of " + ((double)givens[i] / (double)Iterations) + " givens per board.");
        }

        [TestMethod]
        public void CutAllSingles()
        {
            int givens = 0;

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd).Cut.AllSingles();
                givens += work.Find.FilledLocations().Count();
            }

            Console.WriteLine("An average of " + ((double)givens / (double)Iterations) + " givens per board.");
        }

        [TestMethod]
        public void CutAllSinglesRandomized()
        {
            int givens = 0;

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd).Cut.AllSingles(rnd);
                givens += work.Find.FilledLocations().Count();
            }

            Console.WriteLine("An average of " + ((double)givens / (double)Iterations) + " givens per board.");
        }

        [TestMethod]
        public void CutComprehensive()
        {
            int[] givens = new int[NumBatches];

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd);

                for (int i = 0; i < 25; i++)
                    work = work.Cut.Quad(rnd);

                for (int batch = 0; batch < NumBatches; batch++)
                {
                    for (int test = 0; test < BatchSize; test++)
                        work = work.Cut.Pair(rnd);

                    givens[batch] += work.Find.FilledLocations().Count();
                }
            }

            for (int i = 0; i < NumBatches; i++)
                Console.WriteLine("After " + (i + 1) * BatchSize + " cuts, an average of " + ((double)givens[i] / (double)Iterations) + " givens per board.");
        }
    }
}
