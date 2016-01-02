using System;
using System.Collections.Generic;

namespace SudokuSharp
{
    public partial class Board
    {
        /// <summary>
        /// Clears a number of cells, mirrored around both vertical and horizontal axes.
        /// After clearing the cells the board is checked to see if a unique solution exists.
        /// If no unique solution exists, the original board is returned instead.
        /// </summary>
        /// <param name="Source">A <see cref="Board"/> to operate on.</param>
        /// <param name="Seed">An <see cref="int"/> used as basis for a random number stream.</param>
        /// <param name="Rounds">The number of passes to make.</param>
        /// <returns>If a unique solution exists, the new <see cref="Board"/>; otherwise, the original Source.</returns>
        public static Board CutQuads(Board Source, int Seed, int Rounds)
        {
            Random stream = new Random(Seed);

            Board result = new Board(Source);

            for (int i = 0; i < Rounds; i++)
            {
                int x = stream.Next(9);
                int y = stream.Next(9);

                result[new Location(x, y)] = 0;
                result[new Location(8 - x, y)] = 0;
                result[new Location(x, 8 - y)] = 0;
                result[new Location(8 - x, 8 - y)] = 0;
            }

            if (result.ExistsUniqueSolution)
                return result;
            else
                return Source;
        }

        /// <summary>
        /// Clears a number of cells, mirrored around both vertical and horizontal axes.
        /// After clearing the cells the board is checked to see if a unique solution exists.
        /// If no unique solution exists, the original board is returned instead.
        /// </summary>
        /// <param name="Source">A <see cref="Board"/> to operate on.</param>
        /// <param name="Stream">A <see cref="Random"/> number stream.</param>
        /// <param name="Rounds">The number of passes to make.</param>
        /// <returns>If a unique solution exists, the new <see cref="Board"/>; otherwise, the original Source.</returns>
        public static Board CutQuads(Board Source, Random Stream, int Rounds)
        {
            Board result = new Board(Source);

            for (int i = 0; i < Rounds; i++)
            {
                int x = Stream.Next(9);
                int y = Stream.Next(9);

                result[new Location(x, y)] = 0;
                result[new Location(8 - x, y)] = 0;
                result[new Location(x, 8 - y)] = 0;
                result[new Location(8 - x, 8 - y)] = 0;
            }

            if (result.ExistsUniqueSolution)
                return result;
            else
                return Source;
        }

        /// <summary>
        /// Clears a number of cells, mirrored around either the vertical or horizontal axis.
        /// After clearing the cells the board is checked to see if a unique solution exists.
        /// If no unique solution exists, the original board is returned instead.
        /// </summary>
        /// <param name="Source">A <see cref="Board"/> to operate on.</param>
        /// <param name="Seed">An <see cref="int"/> used as basis for a random number stream.</param>
        /// <param name="Rounds">The number of passes to make.</param>
        /// <returns>If a unique solution exists, the new <see cref="Board"/>; otherwise, the original Source.</returns>
        public static Board CutPairs(Board Source, int Seed, int Rounds)
        {
            Board result = new Board(Source);
            Random Stream = new Random(Seed);

            for (int i = 0; i < Rounds; i++)
            {
                int x = Stream.Next(9);
                int y = Stream.Next(9);

                result[new Location(x, y)] = 0;
                if (Stream.Next(2) == 1)
                    result[new Location(8 - x, y)] = 0;
                else
                    result[new Location(x, 8 - y)] = 0;
            }

            if (result.ExistsUniqueSolution)
                return result;
            else
                return Source;
        }

        /// <summary>
        /// Clears a number of cells, mirrored around either the vertical or horizontal axis.
        /// After clearing the cells the board is checked to see if a unique solution exists.
        /// If no unique solution exists, the original board is returned instead.
        /// </summary>
        /// <param name="Source">A <see cref="Board"/> to operate on.</param>
        /// <param name="Stream">A <see cref="Random"/> number stream.</param>
        /// <param name="Rounds">The number of passes to make.</param>
        /// <returns>If a unique solution exists, the new <see cref="Board"/>; otherwise, the original Source.</returns>
        public static Board CutPairs(Board Source, Random Stream, int Rounds)
        {
            Board result = new Board(Source);

            for (int i = 0; i < Rounds; i++)
            {
                int x = Stream.Next(9);
                int y = Stream.Next(9);

                result[new Location(x, y)] = 0;
                if (Stream.Next(2) == 1)
                    result[new Location(8 - x, y)] = 0;
                else
                    result[new Location(x, 8 - y)] = 0;
            }

            if (result.ExistsUniqueSolution)
                return result;
            else
                return Source;
        }

        /// <summary>
        /// Clears a number of cells.
        /// After clearing the cells the board is checked to see if a unique solution exists.
        /// If no unique solution exists, the original board is returned instead.
        /// </summary>
        /// <param name="Source">A <see cref="Board"/> to operate on.</param>
        /// <param name="Seed">An <see cref="int"/> used as basis for a random number stream.</param>
        /// <param name="CellsToCut">The number of passes to make.</param>
        /// <returns>If a unique solution exists, the new <see cref="Board"/>; otherwise, the original Source.</returns>
        public static Board CutSingles(Board Source, int Seed, int CellsToCut)
        {
            Board result = new Board(Source);
            Random Stream = new Random(Seed);

            for (int i = 0; i < CellsToCut; i++)
            {
                int idx = Stream.Next(81);
                result[idx] = 0;
            }

            if (result.ExistsUniqueSolution)
                return result;
            else
                return Source;
        }

        /// <summary>
        /// Clears a number of cells.
        /// After clearing the cells the board is checked to see if a unique solution exists.
        /// If no unique solution exists, the original board is returned instead.
        /// </summary>
        /// <param name="Source">A <see cref="Board"/> to operate on.</param>
        /// <param name="Stream">A <see cref="Random"/> number stream.</param>
        /// <param name="CellsToCut">The number of passes to make.</param>
        /// <returns>If a unique solution exists, the new <see cref="Board"/>; otherwise, the original Source.</returns>
        public static Board CutSingles(Board Source, Random Stream, int CellsToCut)
        {
            Board result = new Board(Source);

            for (int i = 0; i < CellsToCut; i++)
            {
                int idx = Stream.Next(81);
                result[idx] = 0;
            }

            if (result.ExistsUniqueSolution)
                return result;
            else
                return Source;
        }
    }
}
