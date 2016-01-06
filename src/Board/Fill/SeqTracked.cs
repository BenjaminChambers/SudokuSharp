using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class _FillClass
        {
            private class FillRecursionHelper
            {
                public FillRecursionHelper(Board src)
                {
                    _work = new Board(src);

                    foreach (var loc in Location.All)
                    {
                        _numInRow[_work[loc], loc.Row]++;
                        _numInColumn[_work[loc], loc.Column]++;
                        _numInZone[_work[loc], loc.Zone]++;
                    }
                }

                public static explicit operator Board(FillRecursionHelper h)
                {
                    return h._work;
                }

                public int this[Location Where]
                {
                    get { return _work[Where]; }
                    set
                    {
                        int old = _work[Where];
                        _work[Where] = value;

                        _numInRow[old, Where.Row]--;
                        _numInColumn[old, Where.Column]--;
                        _numInZone[old, Where.Zone]--;
                        _numInRow[value, Where.Row]++;
                        _numInColumn[value, Where.Column]++;
                        _numInZone[value, Where.Zone]++;
                    }
                }

                public List<int> Candidates(Location loc)
                {
                    /*
                    return from i in new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
                           where _numInRow[i, loc.Row] == 0
                           where _numInColumn[i, loc.Column] == 0
                           where _numInZone[i, loc.Zone] == 0
                           select i;
                    */

                    List<int> result = new List<int>();

                    for (int i = 1; i < 10; i++)
                        if ((_numInRow[i, loc.Row] == 0) && (_numInColumn[i, loc.Column] == 0) && (_numInZone[i, loc.Zone] == 0))
                            result.Add(i);

                    return result;
                }

                int[,] _numInRow = new int[10, 9];
                int[,] _numInColumn = new int[10, 9];
                int[,] _numInZone = new int[10, 9];
                Board _work;
            }
            Board _work;

            public Board SeqTracked()
            {
                FillRecursionHelper work = new FillRecursionHelper(_parent);

                if (BruteForceTracked(work, 0))
                    return (Board)work;

                return null;
            }

            private bool BruteForceTracked(FillRecursionHelper work, int Index)
            {
                if (Index == 81)
                    return true;

                if (work[Index] > 0)
                    return BruteForceTracked(work, Index + 1);
                else
                {
                    foreach (int test in work.Candidates(Index))
                    {
                        work[Index] = test;

                        if (BruteForceTracked(work, Index + 1))
                            return true;
                    }
                    work[Index] = 0;

                    return false;
                }
            }
        }
    }
}
