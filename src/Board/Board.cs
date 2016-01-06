using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    /// <summary>
    /// The basic Sudoku class.
    /// It contains a grid of cells with values of 0-9; 0 corresponds to an empty cell, and the other digits the possible values.
    /// </summary>
    [DataContract]
    public partial class Board
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class, completely blank (ie every cell <see cref="Location"/> is empty).
        /// </summary>
        public Board() { }

        /// <summary>
        /// Copies an instance of the <see cref="Board"/> class.
        /// </summary>
        /// <param name="src">The source.</param>
        public Board(Board src)
        {
            Array.Copy(src.data, this.data, 81);
        }
        #endregion

        /// <summary>
        /// Gets the value of a given cell.
        /// </summary>
        /// <param name="Where">The cell to check; may be provided as either an instance of <see cref="Location"/> or the integer index of the cell.</param>
        /// <returns></returns>
        public int GetCell(Location Where) { return this[Where]; }
        /// <summary>
        /// Fills a cell in.
        /// </summary>
        /// <param name="Where">The <see cref="Location"/> of the cell to fill.</param>
        /// <param name="value">The value to place; 0 for clear, or 1-9.</param>
        public void PutCell(Location Where, int value) { this[Where] = value; }

        /// <summary>
        /// Overrides array indexing (suare brackets []) for accessing locations in the Grid.
        /// Essentially, it's another way to access GetCell and PutCell.
        /// 
        /// You may use
        ///   <see cref="Location"/> where = new <see cref="Location"/>(x,y);
        ///   <see cref="int"/> result = myGrid[where];
        ///   myGrid[where] = result;
        /// </summary>
        /// <value>
        /// The <see cref="int"/> representing the value of the cell (0 for empty, 1-9 for a value).
        /// </value>
        /// <param name="where">The <see cref="Location"/> to access.</param>
        /// <returns></returns>
        public int this[Location where]
        {
            get { return data[where]; }
            set { data[where] = value; }
        }

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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var loc in Location.All)
            {
                sb.Append((this[loc] > 0) ? this[loc].ToString() : "-");
                if (loc.Column % 3 == 2) sb.Append(" ");
                if (loc.Column == 8) sb.Append("\n");
                if (loc.Row % 3 == 2) sb.Append("\n");
            }
            return sb.ToString();
        }

        [DataMember]
        private int[] data = new int[81];
        private static int[] ZoneIndices = { 0, 3, 6, 27, 30, 33, 54, 57, 60 };
    }
}
