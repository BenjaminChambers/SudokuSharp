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

        public static Board Puzzle(int Seed, int QuadsToCut, int PairsToCut, int SinglesToCut)
        {
            Random rnd = new Random(Seed);
            return Puzzle(Solution(rnd), rnd, QuadsToCut, PairsToCut, SinglesToCut);
        }

        public static Board Puzzle(Board Source, Random Stream, int QuadsToCut, int PairsToCut, int SinglesToCut)
        {
            var work = new Board(Source);

            for (int i = 0; i < QuadsToCut; i++)
                work = work.Cut.Quad(Stream);
            for (int i = 0; i < PairsToCut; i++)
                work = work.Cut.Pair(Stream);
            for (int i = 0; i < SinglesToCut; i++)
                work = work.Cut.Single(Stream);

            return work;
        }
    }
}
