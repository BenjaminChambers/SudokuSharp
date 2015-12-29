using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        /// <summary>
        /// Returns a collection of all empty cells
        /// </summary>
        /// <returns><see cref="IEnumerable{Int}"/></returns>
        public IEnumerable<Location> FindEmptyCells()
        {
            return from loc in Location.All
                   where this[loc] == 0
                   select loc;
        }

        /// <summary>
        /// Returns a collection of all empty cells combined with the possible candidates for those cells
        /// </summary>
        /// <returns>A <see cref="Dictionary{Location, List}"/></returns>
        public Dictionary<Location, List<int>> FindCandidatesForAllEmptyCell()
        {
            return (Dictionary<Location, List<int>>)from loc in Location.All
            where this[loc] == 0
            select new KeyValuePair<Location, List<int>>(loc, GetCandidates(loc));
        }
    }
}
