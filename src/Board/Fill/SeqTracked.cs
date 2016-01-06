using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class _FillClass
        {
            int[,] _numInRow = new int[10, 9];
            int[,] _numInColumn = new int[10, 9];
            int[,] _numInZone = new int[10, 9];
            Board _work;

            public Board SeqTracked()
            {
                _work = new Board(_parent);
                _numInRow = new int[10, 9];
                _numInColumn = new int[10, 9];
                _numInZone = new int[10, 9];

                foreach (var loc in Location.All)
                {
                    _numInRow[_work[loc], loc.Row]++;
                    _numInColumn[_work[loc], loc.Column]++;
                    _numInZone[_work[loc], loc.Zone]++;
                }

                if (BruteForceTracked(0))
                    return _work;

                return null;
            }

            IEnumerable<int> Possible(Location loc)
            {
                return from i in new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
                       where _numInRow[i, loc.Row] == 0
                       where _numInColumn[i, loc.Column] == 0
                       where _numInZone[i, loc.Zone] == 0
                       select i;
            }

            private void ChangeNumber(Location loc, int value)
            {
                int remove = _work[loc];
                _numInRow[remove, loc.Row]--;
                _numInColumn[remove, loc.Column]--;
                _numInZone[remove, loc.Zone]--;
                _work[loc] = value;
                _numInRow[value, loc.Row]++;
                _numInColumn[value, loc.Column]++;
                _numInZone[value, loc.Zone]++;
            }

            private bool BruteForceTracked(int Index)
            {
                if (Index == 81)
                    return true;

                if (_work[Index] > 0)
                    return BruteForceTracked(Index + 1);
                else
                {
                    foreach (int test in Possible(Index))
                    {
                        ChangeNumber(Index, test);

                        if (BruteForceTracked(Index + 1))
                            return true;
                    }

                    return false;
                }
            }
        }
    }
}
