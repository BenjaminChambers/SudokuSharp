using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
	public partial class Puzzle
	{
		public void Shuffle(Random Stream, int Iterations)
		{
			for (int i = 0; i < Iterations; i++)
			{
				int type = Stream.Next(3);
				switch (type)
				{
					case 0: SwapRow(Stream.Next(9), Stream.Next(2)); break;
					case 1: SwapColumn(Stream.Next(9), Stream.Next(2)); break;
					case 2: SwapDigits(Stream.Next(9), Stream.Next(9)); break;
				}
			}
		}

		private void SwapRow(int Row, int Offset)
		{
			int section = Row / 3;
			int target = ((Row % 3) + Offset + 1) % 3 + section*3;

			for (int i = 0; i < 9; i++)
			{
				Location from = new Location(i, Row);
				Location to = new Location(i, target);
				int value = GetCell(from);
				PutCell(from, GetCell(to));
				PutCell(to, value);
			}
		}

		private void SwapColumn(int Column, int Offset)
		{
			int section = Column / 3;
			int target = ((Column % 3) + Offset + 1) % 3 + section*3;

			for (int i = 0; i < 9; i++)
			{
				Location from = new Location(Column, i);
				Location to = new Location(target, i);
				int value = GetCell(from);
				PutCell(from, GetCell(to));
				PutCell(to, value);
			}
		}

		private void SwapDigits(int first, int second)
		{
			// Summing and modulating means they will only be identical if one or the other is zero
			if (first == 0) first++;
			if (second == 0) second++;

			int src = first + 1; // Now it's 1-9;
			int dst = (first + second) % 9 + 1; // Now it's ALSO 1-9

			for (int idx = 0; idx < 81; idx++)
			{
				if (data[idx] == src)
					data[idx] = dst;
				else
					if (data[idx] == dst)
						data[idx] = src;
			}
		}
	}
}
