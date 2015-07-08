using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
    public partial class Puzzle
    {
        /// <summary>
        /// Creates a random Sudoku board, using the provided Seed for the randomizer.
        /// 
        /// If a board could not be created, it will return a Null value.
        /// </summary>
        /// <param name="Seed">The seed to use for the random number generator.</param>
        /// <returns>A <see cref="Puzzle"/> with all cells filled in.</returns>
        public static async Task<Puzzle> CreateSolutionAsync(int Seed)
        {
            return await Task.Factory.StartNew(() => Puzzle.CreateSolution(Seed));
        }

        /// <summary>
        /// Creates a puzzle with guaranteed to have your provided solution as the unique solution available.
        /// </summary>
        /// <param name="Solution">Your provided solution.</param>
        /// <param name="Seed">The seed for the random generator.</param>
        /// <returns>A <see cref="Puzzle"/> with many cells blanked out. It is not guaranteed to have the minimum number of clues (clue removal order could affect this), nor is it guaranteed to be any particular level of difficulty.</returns>
        public static async Task<Puzzle> CreatePuzzleAsync(Puzzle Solution, int Seed)
        {
            return await Task.Factory.StartNew(() => Puzzle.CreatePuzzle(Solution, Seed));
        }

        /// <summary>
        /// Creates a random Sudoku board, using the provided Seed for the randomizer.
        /// 
        /// If a board could not be created, it will return a Null value.
        /// </summary>
        /// <param name="Seed">The seed to use for the random number generator.</param>
        /// <returns>A <see cref="Puzzle"/> with all cells filled in.</returns>
        public static Puzzle CreateSolution(int Seed)
        {
            Puzzle work = new Puzzle();

            if (!work.RandomRecursion(new Random(Seed), 0))
                return null;

            return work;
        }

        /// <summary>
        /// Creates a puzzle with guaranteed to have your provided solution as the unique solution available.
        /// </summary>
        /// <param name="Solution">Your provided solution.</param>
        /// <param name="Seed">The seed for the random generator.</param>
        /// <returns>A <see cref="Puzzle"/> with many cells blanked out. It is not guaranteed to have the minimum number of clues (clue removal order could affect this), nor is it guaranteed to be any particular level of difficulty.</returns>
        public static Puzzle CreatePuzzle(Puzzle Solution, int Seed)
        {
            // Keeping a local array in case we make a mistake has empirically turned out to be faster than manually unrolling the changes
            int[] Restore = new int[81];
            Random Stream = new Random(Seed);
            Puzzle Work = new Puzzle(Solution);

            // I'm sure there's a faster way to do this than with a generic linked list, but why bother? I'll fix it if it's ever a problem.
            List<int> srcOrder = new List<int>();
            for (int i = 0; i < 81; i++)
                srcOrder.Add(i);

            List<int> Order = new List<int>();
            foreach (int i in srcOrder)
                Order.Insert(Stream.Next(Order.Count), i);

            #region Cutting quads
            // Removing cells four at a time, mirrored around both axes
            for (int i = 0; i < 5; i++)
            {
                Array.Copy(Work.data, Restore, 81);

                for (int j=0; j<2; j++)
                {
                    Location loc = Order[i*3+j];

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

                for (int j=0; j<2; j++)
                {
                    Location loc = Order[i*3+j];

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
            Order.Clear();
            for (int i=0; i<81; i++)
            {
                if (Work.data[i] != 0)
                    Order.Insert(Stream.Next(Order.Count), Work.data[i]);
            }
            int backup;
            int givens = 81 - Order.Count;
            foreach (int i in Order)
            {
                if (givens > 18)
                {
                    backup = Work.data[Order[i]];
                    Work.data[Order[i]] = 0;

                    if (!Work.ExistsUniqueSolution)
                        Work.data[Order[i]] = backup;
                    else
                        givens--;
                }
            }
            #endregion

            return Work;
        }
    }
}
