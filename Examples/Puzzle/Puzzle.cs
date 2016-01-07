using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SudokuSharp.Examples
{
    /// <summary>
    /// The composite <see cref="Puzzle"/> class is an example of a higher-level Sudoku board.
    /// 
    /// If you are building a Sudoku application and wish to have as much functionality as possible provided, use this class.
    /// </summary>
    [DataContract]
    public class Puzzle
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Puzzle()
        {
            _solution = new Board();
            _givens = new Board();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Puzzle"/> class.
        /// 
        /// The solution and givens are both generated using the same random seed.
        /// </summary>
        /// <param name="Seed">The random seed to use.</param>
        public Puzzle(int Seed)
        {
            _solution = Factory.Solution(Seed);
            _givens = Factory.Puzzle(_solution, Seed, 4, 4, 4);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Puzzle"/> class, using the supplied solution and givens.
        /// </summary>
        /// <param name="Solution">A <see cref="Board"/> representing the solved puzzle.</param>
        /// <param name="Givens">A <see cref="Board"/> representing the initial state of the puzzle.</param>
        public Puzzle(Board Solution, Board Givens)
        {
            _solution = new Board(Solution);
            _givens = new Board(Givens);
        }

        /// <summary>
        /// Returns a <see cref="CellType"/> with relevant information about that <see cref="Location"/> on the board.
        /// </summary>
        /// <param name="Where">The <see cref="Location"/>.</param>
        /// <returns></returns>
        public Cell GetCell(Location Where)
        {
            CellType type;
            int value;

            if (_work[Where] > 0)
            {
                type = CellType.Guess;
                value = _work[Where];
            }
            else if (_givens[Where] > 0)
            {
                type = CellType.Given;
                value = _givens[Where];
            }
            else
            {
                type = CellType.Empty;
                value = 0;
            }

            return new Cell(Where, type, value, _scratchPad.GetMarks(Where));
        }

        /// <summary>
        /// Fills a cell in.
        /// It also creates a step in the Undo list.
        /// If the <see cref="AutoPencilMarkClearing"/> property is <c>true</c>, then the corresponding Pencil Marks are also cleared (and the step in the Undo list
        /// will contain the necessary information to restore them if necessary).
        /// </summary>
        /// <param name="Where">The where.</param>
        /// <param name="Value">The value.</param>
        public void PutCell(Location Where, int Value)
        {
            List<History.IHistoryAction> historyGroup = new List<History.IHistoryAction>();
            historyGroup.Add(new History.Guess(_work, Where, Value));
            if (AutoPencilMarkClearing)
            {
                foreach (Location loc in Where.Blocking)
                {
                    if (_scratchPad.Get(loc, Value))
                        historyGroup.Add(new History.PencilClear(_scratchPad, loc, Value));
                }
            }

            foreach (History.IHistoryAction action in historyGroup)
                action.Apply();

            _undoList.Push(historyGroup);
            _redoList.Clear();
        }

        /// <summary>
        /// Checks if the entire puzzle is solved
        /// </summary>
        /// <returns>True or False</returns>
        public bool IsSolved()
        {
            foreach (var loc in Location.All)
                if (!IsCorrect(loc))
                    return false;

            return true;
        }

        /// <summary>
        /// Toggles a pencil mark.
        /// It also creates an undo step in the history.
        /// </summary>
        /// <param name="where">The location to mark</param>
        /// <param name="value">The number to mark</param>
        public void TogglePencil(Location where, int value)
        {
            _scratchPad.Toggle(where, value);

            List<History.IHistoryAction> historyGroup = new List<History.IHistoryAction>();
            historyGroup.Add(new History.PencilToggle(_scratchPad, where, value));

            _undoList.Push(historyGroup);
            _redoList.Clear();
        }

        /// <summary>
        /// Determines whether the specified cell matches the solution.
        /// </summary>
        /// <param name="Where">The <see cref="Location"/> to check.</param>
        /// <returns></returns>
        public bool IsCorrect(Location Where)
        {
            if (_givens[Where] > 0)
                return true;

            return (_work[Where] == _solution[Where]);
        }

        /// <summary>
        /// Undoes one step of actions, and moves that step to the Redo stack.
        /// </summary>
        public void Undo()
        {
            List<History.IHistoryAction> step = _undoList.Pop();

            foreach (var item in step)
                item.Undo();

            _redoList.Push(step);
        }

        /// <summary>
        /// Redoes one step of actions, and moves that step back to the Undo stack.
        /// </summary>
        public void Redo()
        {
            List<History.IHistoryAction> step = _redoList.Pop();

            foreach (var item in step)
                item.Apply();

            _undoList.Push(step);
        }


        [DataMember]
        private Board _solution;
        [DataMember]
        private Board _givens;
        [DataMember]
        private Board _work = new Board();
        [DataMember]
        private PencilGrid _scratchPad = new PencilGrid();
        [DataMember]
        Stack<List<History.IHistoryAction>> _undoList = new Stack<List<History.IHistoryAction>>();
        [DataMember]
        Stack<List<History.IHistoryAction>> _redoList = new Stack<List<History.IHistoryAction>>();

        /// <summary>
        /// Indicates whether conflicting pencil marks should be automatically cleared when placing values in cells.
        /// </summary>
        /// <value>
        /// <c>true</c> if [automatic pencil mark clearing]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool AutoPencilMarkClearing { get; set; }
    }
}
