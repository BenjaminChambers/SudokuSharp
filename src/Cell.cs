using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SudokuSharp
{
    public enum CellType { Empty=0, Solution, Guess }
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
        public readonly Location Where;
        public readonly int Value;
        public readonly  CellType Type;
        private bool[] _pencilMarks;
        public ReadOnlyCollection<bool> PencilMarks { get { return new ReadOnlyCollection<bool>(_pencilMarks); }  }
    }
}
