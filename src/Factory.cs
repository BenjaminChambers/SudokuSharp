using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSharp
{
    public class Factory
    {
        public static Board Solution(int Seed)
        {
            return Solution(new Random(Seed));
        }

        public static Board Solution(Random Stream)
        {
            Board work = null;

            do
            {
                work = new Board().Fill.Randomized(Stream);
            } while (work == null);

            return work;
        }

        public static Board Puzzle(Board Source, int Seed, int Quads, int Pairs, int Singles)
        {
            return Puzzle(Source, new Random(Seed), Quads, Pairs, Singles);
        }

        public static Board Puzzle(Board Source, Random Stream, int Quads, int Pairs, int Singles)
        {
            var work = new Board(Source);

            for (int i = 0; i < Quads; i++)
                work = work.Cut.Quad(Stream);
            for (int i = 0; i < Pairs; i++)
                work = work.Cut.Pair(Stream);
            for (int i = 0; i < Singles; i++)
                work = work.Cut.Single(Stream);

            return work;
        }

        public static Board GradedPuzzle(Board Source, Random Stream, int Grade)
        {
            var work = new Board(Source);

            int minCuts = 8;
            int maxCuts = 20;

            int g = 0;
            for (int i = 0; (i < minCuts) || ((i < maxCuts) && (g < Grade - 1)); i++)
            {
                var test = work.Cut.Quad(Stream);
                int g2 = test.Solve.Grade();
                if (g2 <= Grade)
                {
                    work = test;
                    g = g2;
                }
            }

            for (int i = 0; (i < minCuts) || ((i < maxCuts) && (g < Grade)); i++)
            {
                var test = work.Cut.Pair(Stream);
                int g2 = test.Solve.Grade();
                if (g2 <= Grade)
                {
                    work = test;
                    g = g2;
                }
            }

            /*
            if (g < Grade)
            {
                var filled = work.Find.FilledLocations().ToList();
                var rList = new List<Location>();
                foreach (var loc in filled)
                    rList.Insert(Stream.Next(rList.Count), loc);
                var rQueue = new Queue<Location>();
                foreach (var loc in rList)
                    rQueue.Enqueue(loc);

                while ((g < Grade) && (rQueue.Count > 0))
                {
                    var loc = rQueue.Dequeue();
                    work[loc] = 0;
                    g = work.Solve.Grade();
                    if (g > Grade)
                        work[loc] = Source[loc];
                }
            }
            */

            return work;
        }
    }
}
