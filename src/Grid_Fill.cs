using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
	public partial class Grid
	{
		public class CouldNotFillException : Exception
		{
			public CouldNotFillException() : base() { }
			public CouldNotFillException(string Message = "") : base(Message) { }
		}

		public void BruteForceFill()
		{
			if (DuplicateValuesPresent)
				throw new CouldNotFillException("Puzzle has duplicates, could not fill.");

			if (!BruteForceRecursion(0))
				throw new CouldNotFillException("No solution found.");
		}

		private bool BruteForceRecursion(int Index)
		{
			if (Index == 81)
				return true;

			if (data[Index] > 0)
				return BruteForceRecursion(Index + 1);
			else
			{
				List<int> Candidates = GetCandidates((Location)Index);

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

		public void RandomFill(int Seed)
		{
			Random rnd = new Random(Seed);

			if (DuplicateValuesPresent)
				throw new CouldNotFillException("Puzzle has duplicates, could not fill.");

			if (!RandomRecursion(rnd, 0))
				throw new CouldNotFillException("No solution found.");
		}

		private bool RandomRecursion(Random stream, int Index)
		{
			if (Index == 81)
				return true;

			if (data[Index] > 0)
				return BruteForceRecursion(Index + 1);
			else
			{
				List<int> UnsortedCandidates = GetCandidates((Location)Index);
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

