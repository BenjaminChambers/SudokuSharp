using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class _FillClass
        {
            public Board Sequential()
            {
                var result = new Board(_parent);

                if (BruteForceRecursion(result, 0))
                    return result;

                return null;
            }

            private static bool BruteForceRecursion(Board work, int Index)
            {
                if (Index == 81)
                    return true;

                if (work[Index] > 0)
                    return BruteForceRecursion(work, Index + 1);
                else
                {
                    foreach (int test in work.Find.Candidates(Index))
                    {
                        work[Index] = test;
                        if (BruteForceRecursion(work, Index + 1))
                            return true;
                    }
                    work[Index] = 0;

                    return false;
                }
            }
        }
    }
}
