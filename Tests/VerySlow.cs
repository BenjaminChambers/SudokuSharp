using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SudokuSharp;
using System.Linq;

namespace Tests
{
    public partial class BoardTests
    {
        [TestMethod, TestCategory("Very Slow")]
        public void CutAllSingles()
        {
            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd).Cut.AllSingles();
                givens[0].Add(work.Find.FilledLocations().Count());
            }

            WriteStatistics("", givens[0]);
        }

        [TestMethod, TestCategory("Very Slow")]
        public void CutAllSinglesRandomized()
        {
            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.Solution(rnd).Cut.AllSingles(rnd);
                givens[0].Add(work.Find.FilledLocations().Count());
            }

            WriteStatistics("", givens[0]);
        }
    }
}
