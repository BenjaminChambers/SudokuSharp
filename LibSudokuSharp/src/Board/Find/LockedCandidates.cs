using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class _FindClass
        {
            /// <summary>
            /// Looks for candidates in zones which are restricted to a specific column or row.
            /// Removes those candidates from that column or row in other zones, and checks for cells which may be solved
            /// </summary>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<Location, int>> LockedCandidates()
            {
                var possible = AllCandidates();

                for (int number = 1; number < 9; number++)
                {
                    var numberLocations = from item in possible
                                          where item.Value.Contains(number)
                                          select item.Key;

                    for (int zone = 0; zone < 9; zone++)
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
                                    possible[loc].Remove(number);
                            }
                            if (lockedColumn != -1)
                            {
                                foreach (var loc in
                                    (from item in numberLocations
                                     where item.Zone != zone
                                     where item.Column == lockedColumn
                                     select item))
                                    possible[loc].Remove(number);
                            }
                        }
                    }
                }

                return AllSingles(possible);
            }
        }
    }
}
