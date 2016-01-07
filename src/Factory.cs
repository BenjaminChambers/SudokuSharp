using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSharp
{
    /// <summary>
    /// Provides functions for generating games
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// Provides a completely filled, randomly generated, Sudoku <see cref="Board"/>
        /// </summary>
        /// <param name="Seed">An integer seed for the random number generator</param>
        /// <returns><see cref="Board"/></returns>
        public static Board Solution(int Seed)
        {
            return Solution(new Random(Seed));
        }

        /// <summary>
        /// Provides a completely filled, randomly generated, Sudoku <see cref="Board"/>
        /// </summary>
        /// <param name="Stream">An existing <see cref="Random"/> number generator</param>
        /// <returns><see cref="Board"/></returns>
        public static Board Solution(Random Stream)
        {
            Board work = null;

            do
            {
                work = new Board().Fill.Randomized(Stream);
            } while (work == null);

            return work;
        }

        /// <summary>
        /// First calls <see cref="Solution(int)"/> with the provided Seed, then calls Cut.Quad, .Pair, and .Single the specified number of times
        /// </summary>
        /// <param name="Seed">An integer seed for the <see cref="Random"/> number generator</param>
        /// <param name="QuadsToCut">The number of times to call Cut.Quad</param>
        /// <param name="PairsToCut">The number of times to call Cut.Pair</param>
        /// <param name="SinglesToCut">The number of times to call Cut.Single</param>
        /// <returns></returns>
        public static Board Puzzle(int Seed, int QuadsToCut, int PairsToCut, int SinglesToCut)
        {
            Random rnd = new Random(Seed);
            return Puzzle(Solution(rnd), rnd, QuadsToCut, PairsToCut, SinglesToCut);
        }

        /// <summary>
        /// Calls Cut.Quad, .Pair, and .Single the specified number of times on the provided <see cref="Board"/>
        /// </summary>
        /// <param name="Source">The <see cref="Board"/> to be modified</param>
        /// <param name="Stream">An existing <see cref="Random"/> number generator</param>
        /// <param name="QuadsToCut">The number of times to call Cut.Quad</param>
        /// <param name="PairsToCut">The number of times to call Cut.Pair</param>
        /// <param name="SinglesToCut">The number of times to call Cut.Single</param>
        /// <returns></returns>
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
