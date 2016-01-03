using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        private _FillClass _fillClass;

        public _FillClass Fill
        {
            get
            {
                if (_fillClass == null)
                    _fillClass = new _FillClass(this);

                return _fillClass;
            }
        }

        public class CouldNotFillException : Exception { }

        public partial class _FillClass
        {
            Board _parent;

            public _FillClass(Board Parent)
            {
                _parent = Parent;
            }
        }
    }
}
