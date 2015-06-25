using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
	public partial class Puzzle
	{
		public void PatternedCut(int Seed)
		{
			int[] Restore = new int[81];

			Random stream = new Random(Seed);

			Array.Copy(data, Restore, 81);
			do
			{
				Array.Copy(Restore, data, 81);

				for (int i = 0; i < 5; i++)
				{
					int x = stream.Next(9);
					int y = stream.Next(9);
					PutCell(new Location(x, y), 0);
					PutCell(new Location(8 - x, y), 0);
					PutCell(new Location(x, 8 - y), 0);
					PutCell(new Location(8 - x, 8 - y), 0);
				}
			} while (!ExistsUniqueSolution);

			Array.Copy(data, Restore, 81);
			do
			{
				Array.Copy(Restore, data, 81);
				for (int i = 0; i < 5; i++)
				{
					int x = stream.Next(9);
					int y = stream.Next(9);
					PutCell(new Location(x, y), 0);
					if (stream.Next(2) == 1)
						PutCell(new Location(8 - x, y), 0);
					else
						PutCell(new Location(x, 8 - y), 0);
				}
			} while (!ExistsUniqueSolution);

			int givens = 0;
			for (int i = 0; i < 81; i++)
				if (GetCell((Location)i) > 0)
					givens++;

			for (int i = 0; i < 81; i++)
			{
				int value = GetCell((Location)i);

				if (value != 0)
				{
					PutCell((Location)i, 0);

					if (ExistsUniqueSolution)
						givens--;
					else
						PutCell((Location)i, value);

					if (givens < 40)
						i = 81;
				}
			}

		}
	}
}
