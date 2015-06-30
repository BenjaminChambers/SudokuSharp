using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
    /// <summary>
    /// The basic Sudoku class.
    /// It contains a grid of cells with values of 0-9; 0 corresponds to an empty cell, and the other digits the possible values.
    /// </summary>
    public partial class Puzzle
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Puzzle"/> class, completely blank (ie every cell <see cref="Location"/> is empty).
        /// </summary>
        public Puzzle() { }
        /// <summary>
        /// Copies an instance of the <see cref="Puzzle"/> class.
        /// </summary>
        /// <param name="src">The source.</param>
        public Puzzle(Puzzle src)
        {
            Array.Copy(src.data, this.data, 81);
        }
        #endregion

        /// <summary>
        /// Gets the value of a given cell.
        /// </summary>
        /// <param name="Where">The cell to check; may be provided as either an instance of <see cref="Location"/> or the integer index of the cell.</param>
        /// <returns></returns>
        public int GetCell(Location Where) { return data[Where]; }
        /// <summary>
        /// Fills a cell in.
        /// </summary>
        /// <param name="Where">The <see cref="Location"/> of the cell to fill.</param>
        /// <param name="value">The value to place; 0 for clear, or 1-9.</param>
        public void PutCell(Location Where, int value) { data[Where] = value; }

        private int[] GetRow(int Row)
        {
            int[] result = new int[9];

            Array.Copy(data, Row * 9, result, 0, 9);

            return result;
        }
        private int[] GetColumn(int Column)
        {
            int[] result = new int[9];
            int idx = Column;
            for (int i = 0; i < 9; i++)
            {
                result[i] = data[idx];
                idx += 9;
            }
            return result;
        }
        private int[] GetZone(int Zone)
        {
            int[] result = new int[9];
            Array.Copy(data, ZoneIndices[Zone], result, 0, 3);
            Array.Copy(data, ZoneIndices[Zone] + 9, result, 3, 3);
            Array.Copy(data, ZoneIndices[Zone] + 18, result, 6, 3);

            return result;
        }

        /// <summary>
        /// Gets a list of the values which may go here.
        /// </summary>
        /// <param name="Where">The <see cref="Location"/> of the cell to check.</param>
        /// <returns>A <see cref="List{T}"/> of possible values. We start with a list of digits 1-9, and remove any which are found in the same row, column, or zone.</returns>
        public List<int> GetCandidates(Location Where)
        {
            List<int> result = new List<int>();
            if (data[Where] > 0)
                return result;

            bool[] blocking = new bool[10];
            int[] blockingIndices = Where.GetConflictingIndices();

            foreach (int idx in blockingIndices)
            {
                blocking[data[idx]] = true;
            }

            for (int i = 1; i < 10; i++)
                if (!blocking[i])
                    result.Add(i);

            return result;
        }

        private int[] data = new int[81];
        private static int[] ZoneIndices = { 0, 3, 6, 27, 30, 33, 54, 57, 60 };
    }
}
