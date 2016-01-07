using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SudokuSharp;
using System.Linq;

namespace Tests
{
    public partial class BoardTests
    {
        [TestMethod, TestCategory("Cutting")]
        public void CutQuads()
        {
            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd);

                for (int batch = 0; batch < NumBatches; batch++)
                {
                    for (int test = 0; test < BatchSize; test++)
                        work = work.Cut.Quad(rnd);

                    givens[batch].Add(work.Find.FilledLocations().Count());
                }
            }

            for (int i = 0; i < NumBatches; i++)
                WriteStatistics("Givens after " + (i + 1) * BatchSize + " cuts: ", givens[i]);
        }

        [TestMethod, TestCategory("Cutting")]
        public void CutPairs()
        {
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
                WriteStatistics("Givens after " + (i + 1) * BatchSize + " cuts: ", givens[i]);
        }

        [TestMethod, TestCategory("Cutting")]
        public void CutSingles()
        {
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
                WriteStatistics("Givens after " + (i + 1) * BatchSize + " cuts: ", givens[i]);
        }
    }
}
