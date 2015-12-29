using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public Dictionary<Location, int> FindNakedSingles()
        {
            return (Dictionary < Location, int> )
                from item in FindCandidatesForAllEmptyCell()
                where item.Value.Count == 0
                select new KeyValuePair<Location, int>(item.Key, item.Value.First());
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
