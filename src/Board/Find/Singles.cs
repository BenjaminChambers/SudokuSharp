using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public Dictionary<Location, int> FindNakedSingles()
        {
            var results = new Dictionary<Location, int>();

            foreach (var loc in Location.All)
            {
                var candidates = GetCandidates(loc);
                if (candidates.Count == 1)
                    results.Add(loc, candidates[0]);
            }

            return results;
        }

        public Dictionary<Location, int> FindHiddenSingles()
        {
            var results = new Dictionary<Location, int>();

            var empties = from loc in Location.All
                          where GetCell(loc) == 0
                          select loc;

            for (int test = 0; test < 9; test++)
            {
                for (int number = 1; number < 10; number++)
                {
                }
            }
        }
    }
}
