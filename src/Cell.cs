using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace SudokuSharp
{
    /// <summary>
    /// <see cref="CellType"/>s are used to define how a cell on a Sudoku board was filled in.
    /// Possible values are Empty, Solution, or Guess.
    /// </summary>
    public enum CellType {
        /// <summary>
        /// The Empty value represents a cell which is blank.
        /// </summary>
        Empty = 0,
        /// <summary>
        /// The Given value represents a cell which is filled in at the start of the puzzle. In some terminologies this is called a Hint.
        /// </summary>
        Given,
        /// <summary>
        /// The Guess value represents a cell which was filled in by the user. This may or may not be the correct value for the cell.
        /// </summary>
        Guess
    }

    [DataContract]
    public class Cell
    {
        public Cell(Location Where, CellType Type, int Value, bool[] Pencils)
        {
            this.Where = Where;
            this.Value = Value;
            this.Type = Type;
            _pencilMarks = new bool[10];
            Array.Copy(Pencils, _pencilMarks, 10);
        }
        [DataMember]
        public readonly Location Where;
        [DataMember]
        public readonly int Value;
        [DataMember]
        public readonly  CellType Type;
        [DataMember]
        private bool[] _pencilMarks;

        public ReadOnlyCollection<bool> PencilMarks { get { return new ReadOnlyCollection<bool>(_pencilMarks); }  }
    }
}
