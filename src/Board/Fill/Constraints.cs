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

                List<int>[] possibilities = new List<int>[81];
                foreach (var loc in Location.All)
                    possibilities[loc] = work.Find.Candidates(loc);

                if (ConstraintsRecurse(work, possibilities, 0))
                    return work;

                return null;
            }

            private static bool ConstraintsRecurse(Board work, List<int>[] PossibleValues, int Index)
            {
                if (Index == 81)
                    return true;

                if (PossibleValues[Index].Count == 0)
                    return ConstraintsRecurse(work, PossibleValues, Index + 1);

                foreach (int test in PossibleValues[Index])
                {
                    work[Index] = test;
                    foreach (var blocked in new Location(Index).Blocking)
                        PossibleValues[blocked].Remove(test);
                    if (ConstraintsRecurse(work, PossibleValues, Index + 1))
                        return true;
                    foreach (var blocked in new Location(Index).Blocking)
                        PossibleValues[blocked].Add(test);
                }
                work[Index] = 0;
                return false;
            }
        }
    }
}
