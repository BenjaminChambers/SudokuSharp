using System.Collections.Generic;

namespace SudokuSharp
{
    public partial class Board
    {
        // TODO:
        // Add pairs, triples, quads
        // Add Locked Candidates
        // Add X-Wing, XY-Wing and XYZ-Wing
        // Add Swordfish
        // Add Nishio (proof by contradiction)

        public int FillSingles()
        {
            int count = 0;

            int countThisRun = 0;
            do
            {
                countThisRun = 0;

                foreach (Location loc in Location.All)
                {
                    if (GetCell(loc)==0)
                    {
                        var possible = GetCandidates(loc);
                        if (possible.Count == 1)
                        {
                            PutCell(loc, possible[0]);
                            countThisRun++;
                        }
                    }
                }
                count += countThisRun;
            } while (countThisRun > 0);

            return count;
        }

        public int FillDoubles()
        {
            // Find any three blocking cells (same row, column, or zone)
            // If two of them have the same two possibilities, then those two possibilities may be removed from the third
            // The key here is to focus on those cells with three possibilities, and look for doubles to be removed from them.


            // Begin Run
            var empties = GetEmptyCells();
            Dictionary<Location, List<int>> possible = new Dictionary<Location, List<int>>();

            foreach (Location loc in empties)
                possible.Add(loc, GetCandidates(loc));

            foreach (KeyValuePair<Location, List<int>> cell in possible)
            {
                if (cell.Value.Count == 3)
                {

                }
            }

            return 0;
        }
    }
}
