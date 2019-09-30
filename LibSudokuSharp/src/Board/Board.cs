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

        public Board(IEnumerable<int> src)
        {
            int i = 0;
            foreach (var c in src)
            {
                data[i++] = c;
                if (i > 80)
                    break;
            }
        }

        /// <summary>
        /// Create a board from a string description
        /// </summary>
        /// <param name="src">A string of at least 81 characters. Shorter strings will throw an exception, as will non-numeric numbers. Longer strings will have the excess ignored.</param>
        public Board(string src)
        {
            if (src.Length < 81)
                throw new ArgumentException("Parameter src not long enough to describe complete board.");

            foreach (var loc in Location.All)
                PutCell(loc, int.Parse(src[loc.Index].ToString()));
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

        /// <summary>
        /// Provides a pretty string representation of the Board instance.
        /// 3x3 blocks have one empty column and row between them, and empty cells are represented as '-'
        /// The resulting string is then 11x11 when printed on a terminal
        /// </summary>
        /// <returns>A <see cref="string"/> value.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var loc in Location.All)
            {
                sb.Append((this[loc] > 0) ? this[loc].ToString() : "-");
                if (loc.Column % 3 == 2) sb.Append(" ");
                if (loc.Column == 8) sb.Append("\n");
                if ((loc.Column == 8) && (loc.Row % 3 == 2)) sb.Append("\n");
            }
            return sb.ToString();
        }

        [DataMember]
        private int[] data = new int[81];
        private static int[] ZoneIndices = { 0, 3, 6, 27, 30, 33, 54, 57, 60 };
    }
}
