using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class _FindClass
        {
            public IEnumerable<KeyValuePair<Location, int>> NakedSingles()
            {
                return NakedSingles(AllCandidates());
            }

            public IEnumerable<KeyValuePair<Location, int>> NakedSingles(IEnumerable<KeyValuePair<Location, List<int>>> Possibilities)
            {
                return from item in Possibilities
                       where item.Value.Count == 1
                       select new KeyValuePair<Location, int>(item.Key, item.Value.First());
            }

            public IEnumerable<KeyValuePair<Location, int>> HiddenSingles()
            {
                return HiddenSingles(AllCandidates());
            }

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

            public IEnumerable<KeyValuePair<Location,int>> AllSingles()
            {
                return HiddenSingles().Union(NakedSingles());
            }

            public IEnumerable<KeyValuePair<Location, int>> AllSingles(IEnumerable<KeyValuePair<Location, List<int>>> Possibilities)
            {
                return HiddenSingles(Possibilities).Union(NakedSingles(Possibilities));
            }
        }
    }
}
