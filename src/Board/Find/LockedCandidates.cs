using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        /// <summary>
        /// For every zone, checks to see if a candidate is confined to only one row or column.
        /// If it is, then it checks for naked or hidden singles after removing that row or column as a possibility from other zones
        /// </summary>
        /// <returns><see cref="Dictionary{Location, Int}"/></returns>
        public Dictionary<Location, int> FindLockedCandidates()
        {
        }
    }
}
