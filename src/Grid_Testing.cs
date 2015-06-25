using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
	public partial class Grid
	{
		public bool IsSolved
		{
			get
			{
				const int checksum = (1 + 4 + 9 + 16 + 25 + 36 + 49 + 64 + 81);

				for (int area = 0; area < 9; area++)
				{
					int sum = 0;
					foreach (int check in GetRow(area)) sum += (check*check);
					if (sum != checksum) return false;

					sum = 0;
					foreach (int check in GetColumn(area)) sum += (check * check);
					if (sum != checksum) return false;

					sum = 0;
					foreach (int check in GetZone(area)) sum += (check * check);
					if (sum != checksum) return false;

				}
				return true;
			}
		}

		public bool IsValid
		{
			get
			{
				if (DuplicateValuesPresent) return false;
				if (!AreEnoughDigitsPresent) return false;
				if (CanSwapColumnsOrRows) return false;

				return true;
			}
		}

		public bool DuplicateValuesPresent
		{
			get
			{
				int[,] CountByRow = new int[9, 10];
				int[,] CountByColumn = new int[9, 10];
				int[,] CountByZone = new int[9, 10];

				for (int i = 0; i < 81; i++)
				{
					Location loc = (Location)i;
					int value = GetCell(loc);

					CountByRow[loc.Row, value]++;
					CountByColumn[loc.Column, value]++;
					CountByZone[loc.Zone, value]++;
				}

				for (int area = 0; area < 9; area++)
				{
					for (int digit = 1; digit < 10; digit++)
					{
						if ((CountByRow[area, digit] > 1) || (CountByColumn[area, digit] > 1) || (CountByZone[area, digit] > 1))
							return true;
					}
				}

				return false;
			}
		}

		public bool ExistsUniqueSolution
		{
			get
			{
				if (IsSolved)
					return true;

				if (!IsValid)
					return false;

				for (int i = 0; i < 81; i++)
				{
					if (GetCell((Location)i) == 0)
					{ // Only test against empty cells
						List<int> Candidates = GetCandidates((Location)i);

						if (Candidates.Count > 1)
						{ // Only test where there's more than one option
							bool foundSolution=false;

							foreach (int test in Candidates)
							{
								Grid working = new Grid(this);
								working.PutCell((Location)i, test);
								try
								{
									working.BruteForceFill(); // Will trigger an exception if this doesn't work

									if (foundSolution) // solution already found
										return false;
									else
										foundSolution = true;
								}
								catch (Grid.CouldNotFillException e)
								{ }
							}
						}
					}
				}

				return true;
			}
		}

		private bool AreEnoughDigitsPresent
		{
			get
			{
				// Easy check: every 8 of the 9 digits must be present on the board
				// If two digits are completely missing, then those two digits may be swapped and you have two
				// valid solutions.
				// Also checking for at least 16 clues total... just so it's not completely insane

				bool[] present = new bool[10];
				for (int i = 0; i < 81; i++)
				{
					present[data[i]] = true;
				}

				int presentCount = 0;
				for (int i = 1; i < 10; i++)
					if (present[i])
						presentCount++;

				if (presentCount > 7)
					return true;

				return false;
			}
		}
		private bool CanSwapColumnsOrRows
		{
			get
			{
				// If two columns within the same zone are both completely empty, they may be swapped and you have
				// two unique solutions
				bool[] isColPopulated = new bool[9];
				bool[] isRowPopulated = new bool[9];

				for (int i = 0; i < 81; i++)
				{
					if (data[i] > 0)
					{
						isColPopulated[((Location)i).Column] = true;
						isRowPopulated[((Location)i).Row] = true;
					}
				}

				for (int i = 0; i < 3; i++)
				{
					int count = 0;
					if (isColPopulated[i * 3]) count++;
					if (isColPopulated[i * 3 + 1]) count++;
					if (isColPopulated[i * 3 + 2]) count++;
					if (count < 2) return true;
					count = 0;
					if (isRowPopulated[i * 3]) count++;
					if (isRowPopulated[i * 3 + 1]) count++;
					if (isRowPopulated[i * 3 + 2]) count++;
					if (count < 2) return true;
				}
				return false;
			}
		}
	}
}

