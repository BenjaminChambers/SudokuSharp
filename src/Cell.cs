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

    /// <summary>
    /// The <see cref="Cell"/> is returned by the Puzzle class. It contains useful information about every location in a Sudoku game, including:
    /// </summary>
    [DataContract]
    public class Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="Where">The <see cref="Location"/>.</param>
        /// <param name="Type">The <see cref="CellType"/>.</param>
        /// <param name="Value">The number that is there.</param>
        /// <param name="Pencils">A <see cref="bool"/> array of values representing the pencil marks; true for present, false for clear.
        /// Note that the array contains values for 0-9 even though a pencil mark for 0 (clear) doesn't make sense. This just makes the handling easier.</param>
        public Cell(Location Where, CellType Type, int Value, bool[] Pencils)
        {
            this.Where = Where;
            this.Value = Value;
            this.Type = Type;
            _pencilMarks = new bool[10];
            Pencils.CopyTo(_pencilMarks,0);
        }
        /// <summary>
        /// The <see cref="Location"/> of the <see cref="Cell"/>.
        /// </summary>
        [DataMember]
        public readonly Location Where;
        /// <summary>
        /// The number placed in the <see cref="Cell"/>.
        /// </summary>
        [DataMember]
        public readonly int Value;
        /// <summary>
        /// The <see cref="CellType"/> of the cell.
        /// </summary>
        [DataMember]
        public readonly  CellType Type;
        [DataMember]
        private bool[] _pencilMarks;

        /// <summary>
        /// Gets a <see cref="bool"/> array of the pencil marks.
        /// </summary>
        /// <value>
        /// An array of 10 <see cref="bool"/> values, for the numbers 0-9. True indicates the presence of a pencil mark, false indicates clear.
        /// In many Sudoku games, these marks indicate whether a number MAY be placed in the cell.
        /// </value>
        public ReadOnlyCollection<bool> PencilMarks { get { return new ReadOnlyCollection<bool>(_pencilMarks); }  }
    }
}
