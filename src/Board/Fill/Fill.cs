using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        private _FillClass _fillClass;

        /// <summary>
        /// Hack to allow a namespace inside a class. Allows access to the Fill functions.
        /// </summary>
        public _FillClass Fill
        {
            get
            {
                if (_fillClass == null)
                    _fillClass = new _FillClass(this);

                return _fillClass;
            }
        }

        /// <summary>
        /// Hack to allow a namespace inside a class.
        /// </summary>
        public partial class _FillClass
        {
            Board _parent;

            /// <summary>
            /// Hack to allow a namespace inside a class.
            /// </summary>
            public _FillClass(Board Parent)
            {
                _parent = Parent;
            }

            // This class is not made public because it only checks for the presence of a digit in a row or column.
            // It does nothing to guard against duplicates, so is useless for the general board class.
            class ConstraintData
            {
                public ConstraintData(Board Src)
                {
                    foreach (var loc in Location.All)
                    {
                        DigitInRow[Src[loc], loc.Row] =
                            DigitInColumn[Src[loc], loc.Column] =
                            DigitInZone[Src[loc], loc.Zone] = true;
                    }
                }
                public bool[,] DigitInRow = new bool[10, 9];
                public bool[,] DigitInColumn = new bool[10, 9];
                public bool[,] DigitInZone = new bool[10, 9];
            }
        }
    }
}
