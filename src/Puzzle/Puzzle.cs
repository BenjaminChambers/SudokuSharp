using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SudokuSharp
{
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
            _solution = Board.CreateSolution(Seed);
            _givens = Board.CreatePuzzle(_solution, Seed);
        }
        public Puzzle(Board Solution, Board Givens)
        {
            _solution = new Board(Solution);
            _givens = new Board(Givens);
        }

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

        public void PutCell(Location Where, int Value)
        {
            List<History.IHistoryAction> historyGroup = new List<History.IHistoryAction>();
            historyGroup.Add(new History.Guess(_work, Where, Value));
            if (AutoPencilMarkClearing)
            {
                foreach (Location loc in Where.GetConflictingIndices())
                {
                    if (_scratchPad.Get(Where, Value))
                        historyGroup.Add(new History.PencilClear(_scratchPad, Where, Value));
                }
            }

            foreach (History.IHistoryAction action in historyGroup)
                action.Apply();

            _undoList.Push(historyGroup);
            _redoList.Clear();
        }

        public bool IsCorrect(Location Where)
        {
            return (_work[Where] == _solution[Where]);
        }

        public void Undo()
        {
            List<History.IHistoryAction> step = _undoList.Pop();

            foreach (var item in step)
                item.Undo();

            _redoList.Push(step);
        }

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

        [DataMember]
        public bool AutoPencilMarkClearing { get; set; }
    }
}
