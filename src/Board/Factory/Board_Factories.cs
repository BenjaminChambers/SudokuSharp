using System;
using System.Collections.Generic;

namespace SudokuSharp
{
    public partial class Board
    {
        /// <summary>
        /// Creates a random Sudoku board, using the provided Seed for the randomizer.
        /// 
        /// If a board could not be created, it will return a Null value.
        /// </summary>
        /// <param name="Seed">The seed to use for the random number generator.</param>
        /// <returns>A <see cref="Board"/> with all cells filled in.</returns>
        public static Board CreateSolution(int Seed)
        {
            Board work = new Board();

            Random stream = new Random(Seed);

            if (!work.RandomRecursion(new Random(Seed), 0))
                return null;

            return work;
        }

        /// <summary>
        /// Creates a random Sudoku board, using the provided Seed for the randomizer.
        /// 
        /// If a board could not be created, it will return a Null value.
        /// </summary>
        /// <param name="Stream">The random number stream to use for filling in the puzzle.</param>
        /// <returns>A <see cref="Board"/> with all cells filled in.</returns>
        public static Board CreateSolution(Random Stream)
        {
            Board work = new Board();

            if (!work.RandomRecursion(Stream, 0))
                return null;

            return work;
        }


        /// <summary>
        /// Creates a puzzle with guaranteed to have your provided solution as the unique solution available.
        /// </summary>
        /// <param name="Solution">Your provided solution.</param>
        /// <param name="Seed">The seed for the random generator.</param>
        /// <param name="BatchSize">The number of locations to examine at a time. After removing this many spaces, the puzzle is checked for validity.
        /// As this puzzle check is the most intensive part of the creation process, processing batches of spaces together saves a great deal of time.</param>
        /// <param name="MinimumGivens">The minimum number of clues to include on the board.
        /// It is mathematically impossible to solve a given Sudoku puzzle with fewer than 17 givens.
        /// The puzzle creation process will end when the specified number of givens remain on the board.
        /// If a puzzle cannot be created with that number of givens, then one will be created with more.</param>
        /// <returns>A <see cref="Board"/> with many cells blanked out. It is not guaranteed to have the minimum number of clues (clue removal order could affect this), nor is it guaranteed to be any particular level of difficulty.</returns>
        public static Board CreatePuzzle(Board Solution, int Seed, int MinimumGivens = 20, int BatchSize = 4)
        {
            // Keeping a local array in case we make a mistake has empirically turned out to be faster than manually unrolling the changes
            int[] Restore = new int[81];
            Random Stream = new Random(Seed);
            Board Work = new Board(Solution);

            // I'm sure there's a faster way to do this than with a generic list, but why bother? I'll fix it if it's ever a problem.
            var OrderList = new List<int>();
            for (int i = 0; i < 81; i++)
            {
                OrderList.Insert(Stream.Next(OrderList.Count), i);
            }

            var OrderQueue = new Queue<int>(OrderList);

            #region Cutting quads
            // Removing cells four at a time, mirrored around both axes
            for (int i = 0; i < 5; i++)
            {
                Array.Copy(Work.data, Restore, 81);

                for (int j = 0; j < Math.Min(BatchSize, OrderQueue.Count); j++)
                {
                    Location loc = OrderQueue.Dequeue();

                    int x = loc.Column;
                    int y = loc.Row;
                    Work.PutCell(loc, 0);
                    Work.PutCell(new Location(8 - x, y), 0);
                    Work.PutCell(new Location(x, 8 - y), 0);
                    Work.PutCell(new Location(8 - x, 8 - y), 0);
                }

                // If it resulted in an unsolvable puzzle, then roll it back
                if (!Work.ExistsUniqueSolution)
                    Array.Copy(Restore, Work.data, 81);
            }
            #endregion

            #region Cutting pairs
            // Same thing, just removing pairs mirrored around a single axis
            for (int i = 5; i < 10; i++)
            {
                Array.Copy(Work.data, Restore, 81);

                for (int j = 0; j < 2; j++)
                {
                    Location loc = OrderQueue.Dequeue();

                    int x = loc.Column;
                    int y = loc.Row;
                    Work.PutCell(loc, 0);
                    if (Stream.Next(2) == 0)
                        Work.PutCell(new Location(8 - x, y), 0);
                    else
                        Work.PutCell(new Location(x, 8 - y), 0);
                }

                if (!Work.ExistsUniqueSolution)
                    Array.Copy(Restore, Work.data, 81);
            }
            #endregion

            #region Cutting singles
            // We'll now test each individual cell to see if it's possible to remove.
            // We don't need to reflect about axes, so we'll use the raw location Index instead of the Location class
            // As we're only modifying one value at a time, we also won't use the full backup array, but a single int value

            // If this is too slow I could short circuit it once we get down to only 17 clues, as it's mathematically impossible to solve a sudoku with
            // fewer than that... But I'll wait until I need more speed to get to that level of optimization
            OrderList.Clear();
            for (int i = 0; i < 81; i++)
            {
                int num = Work.data[i];
                if (num != 0)
                    OrderList.Insert(Stream.Next(OrderList.Count), num);
            }
            int givens = 81 - OrderList.Count;

            if (givens < MinimumGivens)
            {
                // We've cut too many, and need to give a few back

                for (int i = 0; i < (MinimumGivens - givens); i++)
                {
                    int loc;
                    do
                    {
                        loc = Stream.Next(81);
                    } while (Work[loc] > 0);
                    Work[loc] = Solution[loc];
                }

                return Work;
            }

            foreach (int i in OrderList)
            {
                int backup = Work.data[OrderList[i]];
                Work.data[OrderList[i]] = 0;

                if (!Work.ExistsUniqueSolution)
                    Work.data[OrderList[i]] = backup;
                else
                    givens--;

                if (givens == MinimumGivens)
                    return Work;
            }
            #endregion

            return Work;
        }
    }
}
