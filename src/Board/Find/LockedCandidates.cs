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
        /// <returns><see cref="IEnumerable{T}"/>, where T is <see cref="KeyValuePair{Location, Int}"/></returns>
        public IEnumerable<KeyValuePair<Location, int>> FindLockedCandidates()
        {
            var candidates = FindCandidatesForAllEmptyCells().ToDictionary(x=>x.Key, x=>x.Value);

            for (int number = 1; number<9; number++)
            {
                var numberLocations = from item in candidates
                                      where item.Value.Contains(number)
                                      select item.Key;

                for (int zone=0; zone<9; zone++)
                {
                    var numberLocationsInThisZone = from loc in numberLocations
                                                    where loc.Zone == zone
                                                    select loc;

                    if (numberLocationsInThisZone.Count() > 0)
                    {
                        int lockedRow = numberLocationsInThisZone.First().Row;
                        int lockedColumn = numberLocationsInThisZone.First().Column;

                        foreach (var loc in numberLocationsInThisZone)
                        {
                            if (loc.Row != lockedRow) lockedRow = -1;
                            if (loc.Column != lockedColumn) lockedColumn = -1;
                        }
                        if (lockedRow != -1)
                        {
                            foreach (var loc in
                                (from item in numberLocations
                                 where item.Zone != zone
                                 where item.Row == lockedRow
                                 select item))
                                candidates[loc].Remove(number);
                        }
                        if (lockedColumn != -1)
                        {
                            foreach (var loc in
                                (from item in numberLocations
                                 where item.Zone != zone
                                 where item.Column == lockedColumn
                                 select item))
                                candidates[loc].Remove(number);
                        }
                    }
                }
            }

            return FindAllSingles(candidates);
        }
    }
}
