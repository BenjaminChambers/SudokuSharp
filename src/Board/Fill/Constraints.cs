using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class _FillClass
        {
            public Board Constraints()
            {
                var work = new Board(_parent);

                ConstraintData data = new ConstraintData(work);

                if (ConstraintsRecurse(work, data, 0))
                    return work;

                return null;
            }

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

            private static bool ConstraintsRecurse(Board work, ConstraintData data, int Index)
            {
                if (Index == 81)
                    return true;

                if (work[Index] > 0)
                    return ConstraintsRecurse(work, data, Index + 1);

                var loc = new Location(Index);

                for (int i = 1; i < 10; i++)
                {
                    if (!data.DigitInRow[i, loc.Row] && !data.DigitInColumn[i, loc.Column] && !data.DigitInZone[i, loc.Zone])
                    {
                        work[loc] = i;
                        data.DigitInRow[i, loc.Row] = data.DigitInColumn[i, loc.Column] = data.DigitInZone[i, loc.Zone] = true;
                        if (ConstraintsRecurse(work, data, Index + 1))
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
