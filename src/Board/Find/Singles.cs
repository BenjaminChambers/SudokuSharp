using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        /// <summary>
        /// Checks all empty locations for those with only one possible candidate.
        /// </summary>
        /// <returns><see cref="Dictionary{Location, Int}"/></returns>
        public Dictionary<Location, int> FindNakedSingles()
        {
            return (Dictionary < Location, int> )
                from item in FindCandidatesForAllEmptyCells()
                where item.Value.Count == 0
                select new KeyValuePair<Location, int>(item.Key, item.Value.First());
        }

        /// <summary>
        /// Static version of <seealso cref="FindNakedSingles()"/>, intended to operate on a <see cref="Dictionary{Location, List}"/> returned by <seealso cref="FindCandidatesForAllEmptyCells"/> after it has been modified
        /// Checks all empty locations for those with only one possible candidate.
        /// </summary>
        /// <returns><see cref="Dictionary{Location, Int}"/></returns>
        public static Dictionary<Location, int> FindNakedSingles(Dictionary<Location, List<int>> Candidates)
        {
            return (Dictionary<Location, int>)
                from item in Candidates
                where item.Value.Count == 0
                select new KeyValuePair<Location, int>(item.Key, item.Value.First());
        }

        /// <summary>
        /// For every row, column, and zone, checks to see if there is only one location within that area which may hold each number
        /// </summary>
        /// <returns><see cref="Dictionary{Location, Int}"/></returns>
        public Dictionary<Location, int> FindHiddenSingles()
        {
            Dictionary<Location, int> results = new Dictionary<Location, int>();

            var candidates = FindCandidatesForAllEmptyCells();

            for (int number = 1; number < 10; number++)
            {
                var locationsForThisNumber = from item in candidates
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
        /// Static version of <seealso cref="FindHiddenSingles()"/>, intended to operate on a <see cref="Dictionary{Location, List}"/> returned by <seealso cref="FindCandidatesForAllEmptyCells"/> after it has been modified
        /// For every row, column, and zone, checks to see if there is only one location within that area which may hold each number
        /// </summary>
        /// <returns><see cref="Dictionary{Location, Int}"/></returns>
        public static Dictionary<Location, int> FindHiddenSingles(Dictionary<Location, List<int>> Candidates)
        {
            Dictionary<Location, int> results = new Dictionary<Location, int>();

            for (int number = 1; number < 10; number++)
            {
                var locationsForThisNumber = from item in Candidates
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
    }
}
