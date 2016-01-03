using System;
using System.Collections.Generic;

namespace SudokuSharp
{
    public partial class Board
	{
        /// <summary>
        /// Attempts to fill in all cells in a puzzle using brute force.
        /// The original puzzle remains unchanged.
        /// </summary>
        /// <returns>
        /// If successful, a new instance of <see cref="Board"/> will be returned with all cells filled in.
        /// If unsuccesful, a value of null will be returned.
        /// </returns>
        public Board Solve()
        {
            if (DuplicateValuesPresent)
                return null;

            Board work = new Board(this);

            if (work.BruteForceRecursion(0))
                return work;
            else
                return null;
        }

        private bool BruteForceRecursion(int Index)
		{
			if (Index == 81)
				return true;

			if (data[Index] > 0)
				return BruteForceRecursion(Index + 1);
			else
			{
				foreach (int test in Find.Candidates(Index))
				{
					data[Index]=test;
					if (BruteForceRecursion(Index + 1))
						return true;
				}
				data[Index]=0;

				return false;
			}
		}

		private bool RandomRecursion(Random stream, int Index)
		{
			if (Index == 81)
				return true;

			if (data[Index] > 0)
				return BruteForceRecursion(Index + 1);
			else
			{
				var possible = new List<int>();

				foreach (int digit in Find.Candidates(Index))
                    possible.Insert(stream.Next(possible.Count), digit);

				foreach (int test in possible)
				{
					data[Index] = test;
					if (BruteForceRecursion(Index + 1))
						return true;
				}
				data[Index] = 0;

				return false;
			}
		}
    }
}

