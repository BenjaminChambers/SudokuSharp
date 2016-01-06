using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        private _FindClass _findClass;

        public _FindClass Find
        {
            get
            {
                if (_findClass == null)
                    _findClass = new _FindClass(this);

                return _findClass;
            }
        }

        public partial class _FindClass
        {
            public _FindClass(Board Parent)
            {
                _parent = Parent;
            }

            public IEnumerable<Location> EmptyLocations()
            {
                return from loc in Location.All
                       where _parent[loc] == 0
                       select loc;
            }

            public IEnumerable<Location> FilledLocations()
            {
                return from loc in Location.All
                       where _parent[loc] > 0
                       select loc;
            }

            public List<int> Candidates(Location Where)
            {
                if (_parent[Where] > 0)
                    return new List<int>();
                
                bool[] present = new bool[10];
                foreach (var loc in Where.Blocking())
                    present[_parent[loc]] = true;

                List<int> result = new List<int>();
                for (int i = 1; i < 10; i++)
                    if (!present[i])
                        result.Add(i);

                return result;
            }

            public Dictionary<Location, List<int>> AllCandidates()
            {
                Dictionary<Location, List<int>> result = new Dictionary<Location, List<int>>();

                foreach (var loc in EmptyLocations())
                    result[loc] = Candidates(loc);

                return result;
            }

            Board _parent;
        }
    }
}
