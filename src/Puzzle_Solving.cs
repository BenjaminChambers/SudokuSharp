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
        /// Attempts to fill in all cells in a puzzle using brute force.
        /// The original puzzle remains unchanged.
        /// </summary>
        /// <returns>
        /// If successful, a new instance of <see cref="Puzzle"/> will be returned with all cells filled in.
        /// If unsuccesful, a value of null will be returned.
        /// </returns>
        public Puzzle Solve()
        {
            if (DuplicateValuesPresent)
                return null;

            Puzzle work = new Puzzle(this);

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

