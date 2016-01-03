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

                List<int> result = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                foreach (var loc in Where.Blocking())
                    result.Remove(_parent[loc]);

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
