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
                var empties = work.Find.EmptyLocations().ToList();

                List<int>[] possibilities = new List<int>[81];
                foreach (var loc in Location.All)
                    possibilities[loc] = work.Find.Candidates(loc);

                if (ConstraintsRecurse(work, empties, possibilities, 0))
                    return work;

                return null;
            }

            private static bool ConstraintsRecurse(Board work, List<Location> EmptyLocations, List<int>[] PossibleValues, int Index)
            {
                if (Index == EmptyLocations.Count)
                    return true;

                if (PossibleValues[EmptyLocations[Index]].Count == 0)
                    return ConstraintsRecurse(work, EmptyLocations, PossibleValues, Index + 1);

                foreach (int test in PossibleValues[EmptyLocations[Index]])
                {
                    work[EmptyLocations[Index]] = test;
                    foreach (var blocked in EmptyLocations[Index].Blocking)
                        PossibleValues[blocked].Remove(test);
                    if (ConstraintsRecurse(work, EmptyLocations, PossibleValues, Index + 1))
                        return true;
                    foreach (var blocked in EmptyLocations[Index].Blocking)
                        PossibleValues[blocked].Add(test);
                }
                work[EmptyLocations[Index]] = 0;
                return false;
            }
        }
    }
}
