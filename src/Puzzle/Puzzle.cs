using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SudokuSharp
{
    [DataContract]
    public class Puzzle
    {
        public Puzzle(Grid Solution)
        {
            _solution = new Grid(Solution);
            _work = new Grid();
            _scratchPad = new PencilGrid();
            _history = new List<List<History.IHistoryAction>>();
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
            else if (_solution[Where] > 0)
            {
                type = CellType.Given;
                value = _solution[Where];
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

            _history.Add(historyGroup);
        }

        
        [DataMember]
        private Grid _solution;
        [DataMember]
        private Grid _work;
        [DataMember]
        private PencilGrid _scratchPad;
        [DataMember]
        List<List<History.IHistoryAction>> _history;

        [DataMember]
        public bool AutoPencilMarkClearing { get; set; }
    }
}
