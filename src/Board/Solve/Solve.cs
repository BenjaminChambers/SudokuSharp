using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        private _SolveClass _solveClass;

        public _SolveClass Solve
        {
            get
            {
                if (_solveClass == null)
                    _solveClass = new _SolveClass(this);

                return _solveClass;
            }
        }

        public partial class _SolveClass
        {
            Board _parent;

            public _SolveClass(Board Parent)
            {
                _parent = Parent;
            }

            public Board Singles()
            {
                Board work = new Board(_parent);

                var singles = _parent.Find.AllSingles();

                while (singles.Count() > 0)
                {
                    foreach (var item in singles)
                        work[item.Key] = item.Value;

                    singles = work.Find.AllSingles();
                }

                return work;
            }

            public Board LockedCandidates()
            {
                Board work = new Board(_parent);

                var locked = _parent.Find.LockedCandidates();

                while (locked.Count() > 0)
                {
                    foreach (var item in locked)
                        work[item.Key] = item.Value;

                    locked = work.Find.LockedCandidates();
                }

                return work;
            }

            public int Grade()
            {
                var work = Singles();
                if (work.IsSolved) return 1;

                work = work.Solve.LockedCandidates().Solve.Singles();
                if (work.IsSolved) return 2;

                return 3;
            }
        }
    }
}
