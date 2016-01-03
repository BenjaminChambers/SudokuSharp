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
                work = work.Cut.Singles(Stream, 1);

            return work;
        }
    }
}
