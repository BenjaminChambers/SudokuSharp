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

        [TestMethod, TestCategory("Very Slow")]
        public void CountSolutions()
        {
            for (int iter=0; iter< Iterations; iter++)
            {
                var work = Factory.Solution(rnd);

                List<Location> order = new List<Location>();
                for (int i = 0; i < 81; i++)
                    order.Insert(rnd.Next(order.Count), i);

                for (int i = 0; i < 40; i++)
                    work[order[i]] = 0;

                givens[0].Add(work.CountSolutions());
            }

            WriteStatistics("Solutions found after cutting 40 singles:", givens[0]);
        }
    }
}
