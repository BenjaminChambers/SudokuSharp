using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        /// <summary>
        /// Checks all empty locations for those with only one possible candidate.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/>, where T is <see cref="KeyValuePair{Location, Int}"/></returns>
        public IEnumerable<KeyValuePair<Location, int>> FindNakedSingles()
        {
            return from item in FindCandidatesForAllEmptyCells()
                where item.Value.Count == 0
                select new KeyValuePair<Location, int>(item.Key, item.Value.First());
        }

        /// <summary>
        /// Static version of <seealso cref="FindNakedSingles()"/>, intended to operate on a <see cref="Dictionary{Location, List}"/> returned by <seealso cref="FindCandidatesForAllEmptyCells"/> after it has been modified
        /// Checks all empty locations for those with only one possible candidate.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/>, where T is <see cref="KeyValuePair{Location, Int}"/></returns>
        public static IEnumerable<KeyValuePair<Location, int>> FindNakedSingles(Dictionary<Location, List<int>> Candidates)
        {
            return from item in Candidates
                where item.Value.Count == 0
                select new KeyValuePair<Location, int>(item.Key, item.Value.First());
        }

        /// <summary>
        /// For every row, column, and zone, checks to see if there is only one location within that area which may hold each number
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/>, where T is <see cref="KeyValuePair{Location, Int}"/></returns>
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
        /// <returns><see cref="IEnumerable{T}"/>, where T is <see cref="KeyValuePair{Location, Int}"/></returns>
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

        /// <summary>
        /// For convenience, returns the result a set of all singles, whether naked or hidden.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/>, where T is <see cref="KeyValuePair{Location, Int}"/></returns>
        public IEnumerable<KeyValuePair<Location, int>> FindAllSingles()
        {
            return FindNakedSingles().Union(FindHiddenSingles());
        }

        /// <summary>
        /// Static version of <seealso cref="FindAllSingles()"/>, intended to operate on a <see cref="Dictionary{Location, List}"/> returned by <seealso cref="FindCandidatesForAllEmptyCells"/> after it has been modified
        /// For convenience, returns the result a set of all singles, whether naked or hidden.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/>, where T is <see cref="KeyValuePair{Location, Int}"/></returns>
        public static IEnumerable<KeyValuePair<Location, int>> FindAllSingles(Dictionary<Location, List<int>> Candidates)
        {
            return FindNakedSingles(Candidates).Union(FindHiddenSingles(Candidates));
        }
    }
}
