using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSharp
{
    public class PencilGrid
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PencilGrid"/> class with all fields blank.
        /// </summary>
        public PencilGrid() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PencilGrid"/> class, with pencil marks for everywhere a number is a candidate.
        /// </summary>
        /// <param name="src">The <see cref="Grid"/> to work off of.</param>
        public PencilGrid(Grid src)
        {
            for (Location where = 0; where < 81; where++)
            {
                var candidates = src.GetCandidates(where);
                foreach (int value in candidates)
                    data[where, value] = true;
            }
        }
        #endregion

        bool GetMark(Location where, int value) { return data[where, value]; }
        bool PutMark(Location where, int value) { return data[where, value] = true; }
        bool ClearMark(Location where, int value) { return data[where, value] = false; }
        bool ToggleMark(Location where, int value) { return (data[where, value] = !data[where, value]); }

        bool[] GetMarks(Location where)
        {
            bool[] result = new bool[10];
            for (int i = 1; i < 10; i++)
                result[i] = data[where, i];
            return result;
        }

        #region Internals
        bool[,] data = new bool[81, 10];
        #endregion
    }
}
