using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SudokuSharp;
using System.Linq;

namespace Tests
{
    public partial class BoardTests
    {
        [TestMethod, TestCategory("Creation")]
        public void CreateSolutions()
        {
            for (int i = 0; i < Iterations; i++)
            {
                Assert.IsTrue(Factory.Solution(rnd).IsSolved);
            }
        }

        [TestMethod, TestCategory("Creation")]
        public void CreatePuzzle()
        {
            for (int iter = 0; iter < Iterations; iter++)
            {
                var Work = Factory.Solution(rnd);
                for (int i = 0; i < 3; i++)
                {
                    var step = new Board(Work);

                    for (int j = 0; j < 3; j++)
                    {
                        Location loc = rnd.Next(81);
                        step[loc] = 0;
                        step[loc.FlipHorizontal()] = 0;
                        step[loc.FlipVertical()] = 0;
                        step[loc.FlipVertical().FlipHorizontal()] = 0;
                    }

                    if (step.ExistsUniqueSolution)
                        Work = step;
                }

                givens[0].Add(Work.Find.FilledLocations().Count());
            }

            WriteStatistics("Givens in generated puzzles: ", givens[0]);
        }
    }
}
