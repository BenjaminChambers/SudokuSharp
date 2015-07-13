using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSharp
{
    public interface IHistoryAction
    {
        void Apply();
        void Undo();
    }
}
