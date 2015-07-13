﻿using System;
using System.Collections.Generic;

namespace SudokuSharp
{
    public partial class Grid
	{
        /// <summary>
        /// Attempts to fill in all cells in a puzzle using brute force.
        /// The original puzzle remains unchanged.
        /// </summary>
        /// <returns>
        /// If successful, a new instance of <see cref="Grid"/> will be returned with all cells filled in.
        /// If unsuccesful, a value of null will be returned.
        /// </returns>
        public Grid Solve()
        {
            if (DuplicateValuesPresent)
                return null;

            Grid work = new Grid(this);

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
				List<int> Candidates = GetCandidates(Index);

				foreach (int test in Candidates)
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
				List<int> UnsortedCandidates = GetCandidates(Index);
				List<int> Candidates = new List<int>();

				foreach (int digit in UnsortedCandidates)
					Candidates.Insert(stream.Next(Candidates.Count), digit);

				foreach (int test in Candidates)
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
