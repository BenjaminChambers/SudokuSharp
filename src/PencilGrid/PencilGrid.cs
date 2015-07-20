using System;
using System.Runtime.Serialization;

namespace SudokuSharp
{
    /// <summary>
    /// A grid for keeping track of all the pencil marks.
    /// Many players will mark in a cell the values that may be placed there; this is represented by an array of <see cref="bool"/> values for every <see cref="Location"/> on the board.
    /// </summary>
    [DataContract]
    public class PencilGrid
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PencilGrid"/> class with all fields blank.
        /// </summary>
        public PencilGrid()
        {
            data = new bool[81][];
            for (int i=0; i<81; i++)
                data[i] = new bool[10];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PencilGrid"/> class, with pencil marks for everywhere a number is a candidate.
        /// </summary>
        /// <param name="src">The <see cref="Board"/> to work off of.</param>
        public PencilGrid(Board src) : base()
        {
            foreach (Location where in Location.All)
            {
                var candidates = src.GetCandidates(where);
                foreach (int value in candidates)
                    data[where][value] = true;
            }
        }
        #endregion

        /// <summary>
        /// Returns whether a pencil mark is present.
        /// </summary>
        /// <param name="where">The <see cref="Location"/> to check.</param>
        /// <param name="value">The number to check.</param>
        /// <returns></returns>
        public bool Get(Location where, int value) { return data[where][value]; }
        /// <summary>
        /// Sets a pencil mark.
        /// </summary>
        /// <param name="where">The <see cref="Location"/> to set.</param>
        /// <param name="value">The number to set.</param>
        /// <returns></returns>
        public bool Set(Location where, int value) { return data[where][value] = true; }
        /// <summary>
        /// Clears the specified pencil mark.
        /// </summary>
        /// <param name="where">The <see cref="Location"/> to clear.</param>
        /// <param name="value">The number to clear.</param>
        /// <returns></returns>
        public bool Clear(Location where, int value) { return data[where][value] = false; }
        /// <summary>
        /// Toggles the specified pencil mark.
        /// </summary>
        /// <param name="where">The <see cref="Location"/> to toggle.</param>
        /// <param name="value">The number to toggle.</param>
        /// <returns></returns>
        public bool Toggle(Location where, int value) { return (data[where][value] = !data[where][value]); }

        /// <summary>
        /// Returns an array of all the pencil marks at a given <see cref="Location"/>.
        /// It is an array of 10 booleans, so you may access it using 1-9 for the given cell values.
        /// (I know, it doesn't make sense to have a pencil mark for 0, just ignore that index)
        /// </summary>
        /// <param name="where">The <see cref="Location"/>.</param>
        /// <returns></returns>
        public bool[] GetMarks(Location where)
        {
            bool[] result = new bool[10];
            Array.Copy(data[where], result, 10);

            return result;
        }

        #region Internals
        [DataMember]
        bool[][] data;
        #endregion
    }
}
