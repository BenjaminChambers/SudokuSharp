using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SudokuSharp;
using System.Linq;

namespace Tests
{
    [TestClass]
    public partial class BoardTests
    {
        static int Iterations = 0;
        static int BatchSize = 0;
        static int NumBatches = 0;

        static Random rnd = null;

        static List<int>[] givens;

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

            Console.WriteLine(Message + "\t\tMin: " + min.ToString("00.000") + "\t\t25%: " + qLow.ToString("00.000") + "\t\tMean: " + mean.ToString("00.000") + "\t\t75%: " + qHigh.ToString("00.000") + "\t\tMax: " + max.ToString("00.000") + "\t\tSt Dev: " + stDev.ToString("00.000"));
        }

        [ClassInitialize]
        public static void ClassInitialization(TestContext context)
        {
            Iterations = int.Parse(context.Properties["Number Of Puzzles"].ToString());
            BatchSize = int.Parse(context.Properties["Batch Size"].ToString());
            NumBatches = int.Parse(context.Properties["Number of Batches"].ToString());

            int seed = int.Parse(context.Properties["Seed"].ToString());
            rnd = new Random(seed);

            givens = new List<int>[NumBatches];
        }

        [TestInitialize]
        public void TestInitialization()
        {
            for (int i = 0; i < NumBatches; i++)
            {
                givens[i] = new List<int>();
            }
        }
    }
}
