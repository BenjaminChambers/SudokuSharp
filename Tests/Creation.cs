using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SudokuSharp;
using System.Linq;

namespace Tests
{
    public partial class BoardTests
    {
        private void WriteGradeStats()
        {
            for (int i = 1; i <= 5; i++)
                Console.WriteLine("Grade " + i + ":\t\t" + grades[0].Where(x => x == i).Count());
        }

        [TestMethod, TestCategory("Creation")]
        public void CreateSolutions()
        {
            for (int i = 0; i < Iterations; i++)
                Factory.Solution(rnd);
        }

        [TestMethod, TestCategory("Creation")]
        public void BruteForceFill()
        {
            for (int i = 0; i < Iterations; i++)
                new Board().Fill.Sequential();

            var data = new Board().Fill.Sequential();
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                    Console.Write(data[new Location(x, y)]);
                Console.Write("\n");
            }
        }

        [TestMethod, TestCategory("Creation")]
        public void BruteForceTracked()
        {
            for (int i = 0; i < Iterations; i++)
                new Board().Fill.SeqTracked();

            var data = new Board().Fill.SeqTracked();
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                    Console.Write(data[new Location(x, y)]);
                Console.Write("\n");
            }
        }


        [TestMethod, TestCategory("Creation")]
        public void GradedPuzzle1()
        {
            var Source = Factory.Solution(rnd);

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.GradedPuzzle(Source, rnd, 1);
                givens[0].Add(work.Find.FilledLocations().Count());
                grades[0].Add(work.Solve.Grade());
            }

            WriteStatistics("Givens in a Grade 1 puzzle: ", givens[0]);
            WriteGradeStats();
        }

        [TestMethod, TestCategory("Creation")]
        public void GradedPuzzle2()
        {
            var Source = Factory.Solution(rnd);

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.GradedPuzzle(Source, rnd, 2);
                givens[0].Add(work.Find.FilledLocations().Count());
                grades[0].Add(work.Solve.Grade());
            }

            WriteStatistics("Givens in a Grade 2 puzzle: ", givens[0]);
            WriteGradeStats();
        }

        [TestMethod, TestCategory("Creation")]
        public void GradedPuzzle3()
        {
            var Source = Factory.Solution(rnd);

            for (int iter = 0; iter < Iterations; iter++)
            {
                var work = Factory.GradedPuzzle(Source, rnd, 3);
                givens[0].Add(work.Find.FilledLocations().Count());
                grades[0].Add(work.Solve.Grade());
            }

            WriteStatistics("Givens in a Grade 3 puzzle: ", givens[0]);
            WriteGradeStats();
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
                grades[0].Add(Work.Solve.Grade());
            }

            WriteStatistics("Givens in generated puzzles: ", givens[0]);
            WriteGradeStats();
        }
    }
}
