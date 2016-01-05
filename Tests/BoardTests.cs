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

        public static void WriteStatistics(string Message, List<int> data)
        {
            data.Sort();

            int min = data.Min();
            int max = data.Max();
            double mean = (double)(data.Sum()) / (double)data.Count();
            int qLow = data[data.Count / 4];
            int qHigh = data[(data.Count * 3) / 4];

            double stDev = 0;

            foreach (var item in data)
            {
                double delta = item - mean;
                stDev += (delta * delta);
            }

            stDev = Math.Sqrt(stDev / data.Count);

            Console.WriteLine(Message + "\t\tMin: " + min + "\t\t25%: " + qLow + "\t\tMean: " + mean + "\t\t75%: " + qHigh + "\t\tMax: " + max + "\t\tSt Dev: " + stDev);
        }

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
            List<int>[] givens = new List<int>[NumBatches];
            List<int>[] grades = new List<int>[NumBatches];

            for (int i = 0; i < NumBatches; i++)
            {
                givens[i] = new List<int>();
                grades[i] = new List<int>();
            }

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd);

                for (int batch = 0; batch < NumBatches; batch++)
                {
                    for (int test = 0; test < BatchSize; test++)
                        work = work.Cut.Quad(rnd);

                    givens[batch].Add(work.Find.FilledLocations().Count());
                    grades[batch].Add(work.Solve.Grade());
                }
            }

            for (int i = 0; i < NumBatches; i++)
                WriteStatistics("Givens after " + (i + 1) * BatchSize + " cuts: ", givens[i]);
            for (int i = 0; i < NumBatches; i++)
                WriteStatistics("Grades after " + (i + 1) * BatchSize + " cuts: ", grades[i]);
        }

        [TestMethod]
        public void CutPairs()
        {
            List<int>[] givens = new List<int>[NumBatches];
            for (int i = 0; i < NumBatches; i++)
                givens[i] = new List<int>();

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd);

                for (int batch = 0; batch < NumBatches; batch++)
                {
                    for (int test = 0; test < BatchSize; test++)
                        work = work.Cut.Pair(rnd);

                    givens[batch].Add(work.Find.FilledLocations().Count());
                }
            }

            for (int i = 0; i < NumBatches; i++)
                WriteStatistics("After " + (i + 1) * BatchSize + " cuts, ", givens[i]);
        }

        [TestMethod]
        public void CutSingles()
        {
            List<int>[] givens = new List<int>[NumBatches];
            for (int i = 0; i < NumBatches; i++)
                givens[i] = new List<int>();

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd);

                for (int batch = 0; batch < NumBatches; batch++)
                {
                    for (int test = 0; test < BatchSize; test++)
                        work = work.Cut.Single(rnd);

                    givens[batch].Add(work.Find.FilledLocations().Count());
                }
            }

            for (int i = 0; i < NumBatches; i++)
                WriteStatistics("After " + (i + 1) * BatchSize + " cuts, ", givens[i]);
        }

        [TestMethod]
        public void CutAllSingles()
        {
            List<int> givens = new List<int>();

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd).Cut.AllSingles();
                givens.Add(work.Find.FilledLocations().Count());
            }

            WriteStatistics("", givens);
        }

        [TestMethod]
        public void CutAllSinglesRandomized()
        {
            List<int> givens = new List<int>();

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd).Cut.AllSingles(rnd);
                givens.Add(work.Find.FilledLocations().Count());
            }

            WriteStatistics("", givens);
        }
    }
}
