using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class _FindClass
        {
            /// <summary>
            /// Looks for Naked Singles. These are cells with only a single candidate
            /// </summary>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<Location, int>> NakedSingles()
            {
                return NakedSingles(AllCandidates());
            }

            /// <summary>
            /// Looks for Naked Singles. These are cells with only a single candidate.
            /// This version is intended to be called by other reducing functions (such as LockedCandidates)
            /// </summary>
            /// <param name="Possibilities">A set of candidates</param>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<Location, int>> NakedSingles(IEnumerable<KeyValuePair<Location, List<int>>> Possibilities)
            {
                return from item in Possibilities
                       where item.Value.Count == 1
                       select new KeyValuePair<Location, int>(item.Key, item.Value.First());
            }

            /// <summary>
            /// Looks for Hidden Singles. These are digits which may only be placed in one cell within a row, column, or zone
            /// </summary>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<Location, int>> HiddenSingles()
            {
                return HiddenSingles(AllCandidates());
            }

            /// <summary>
            /// Looks for Hidden Singles. These are digits which may only be placed in one cell within a row, column, or zone
            /// This version is intended to be called by other reducing functions (such as LockedCandidates)
            /// </summary>
            /// <param name="Possibilities">A set of candidates</param>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<Location, int>> HiddenSingles(IEnumerable<KeyValuePair<Location, List<int>>> Possibilities)
            {
                Dictionary<Location, int> results = new Dictionary<Location, int>();

                for (int number = 1; number < 10; number++)
                {
                    var locationsForThisNumber = from item in Possibilities
                                                 where item.Value.Contains(number)
                                                 select item.Key;

                    for (int test = 0; test < 9; test++)
                    {
                        var possible = from item in locationsForThisNumber where item.Zone == test select item;
                        if (possible.Count() == 1) results[possible.First()] = number;
                        possible = from item in locationsForThisNumber where item.Row == test select item;
                        if (possible.Count() == 1) results[possible.First()] = number;
                        possible = from item in locationsForThisNumber where item.Column == test select item;
                        if (possible.Count() == 1) results[possible.First()] = number;
                    }
                }

                return results;
            }

            /// <summary>
            /// Returns a the results of both Naked Singles and Hidden Singles
            /// </summary>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<Location,int>> AllSingles()
            {
                return HiddenSingles().Union(NakedSingles());
            }

            /// <summary>
            /// Returns a the results of both Naked Singles and Hidden Singles
            /// This version is intended to be called by other reducing functions (such as LockedCandidates)
            /// </summary>
            /// <param name="Possibilities">A set of candidates</param>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<Location, int>> AllSingles(IEnumerable<KeyValuePair<Location, List<int>>> Possibilities)
            {
                return HiddenSingles(Possibilities).Union(NakedSingles(Possibilities));
            }
        }
    }
}
