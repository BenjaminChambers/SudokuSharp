using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class _FillClass
        {
            /// <summary>
            /// Attempts to fill the calling <see cref="Board"/> instance with numbers.
            /// The original instance remains unchanged
            /// </summary>
            /// <returns>Either a new instance of <see cref="Board"/> or, if unsuccessful, null</returns>
            public Board Sequential()
            {
                var work = new Board(_parent);

                ConstraintData data = new ConstraintData(work);

                if (BruteForceRecursion(work, data, 0))
                    return work;

                return null;
            }

            private static bool BruteForceRecursion(Board work, ConstraintData data, int Index)
            {
                if (Index == 81)
                    return true;

                if (work[Index] > 0)
                    return BruteForceRecursion(work, data, Index + 1);

                var loc = new Location(Index);

                for (int i = 1; i < 10; i++)
                {
                    if (!data.DigitInRow[i, loc.Row] && !data.DigitInColumn[i, loc.Column] && !data.DigitInZone[i, loc.Zone])
                    {
                        work[loc] = i;
                        data.DigitInRow[i, loc.Row] = data.DigitInColumn[i, loc.Column] = data.DigitInZone[i, loc.Zone] = true;
                        if (BruteForceRecursion(work, data, Index + 1))
                            return true;
                        data.DigitInRow[i, loc.Row] = data.DigitInColumn[i, loc.Column] = data.DigitInZone[i, loc.Zone] = false;
                    }
                }

                work[Index] = 0;
                return false;
            }
        }
    }
}
