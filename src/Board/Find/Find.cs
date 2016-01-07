using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        private _FindClass _findClass;

        /// <summary>
        /// Hack to allow a namespace inside a class. Allows access to the Find functions.
        /// </summary>
        public _FindClass Find
        {
            get
            {
                if (_findClass == null)
                    _findClass = new _FindClass(this);

                return _findClass;
            }
        }

        /// <summary>
        /// Hack to allow a namespace inside a class.
        /// </summary>
        public partial class _FindClass
        {
            /// <summary>
            /// Hack to allow a namespace inside a class.
            /// </summary>
            public _FindClass(Board Parent)
            {
                _parent = Parent;
            }

            /// <summary>
            /// Every location of the calling <see cref="Board"/> instance with no cell value
            /// </summary>
            /// <returns><see cref="IEnumerable{Location}"/></returns>
            public IEnumerable<Location> EmptyLocations()
            {
                return from loc in Location.All
                       where _parent[loc] == 0
                       select loc;
            }

            /// <summary>
            /// Every location of the calling <see cref="Board"/> instance with a cell value filled
            /// </summary>
            /// <returns><see cref="IEnumerable{Location}"/></returns>
            public IEnumerable<Location> FilledLocations()
            {
                return from loc in Location.All
                       where _parent[loc] > 0
                       select loc;
            }

            /// <summary>
            /// Returns every digit which may be placed in the calling <see cref="Board"/> instance at the specified <see cref="Location"/>
            /// </summary>
            /// <param name="Where">The <see cref="Location"/> to check</param>
            /// <returns><see cref="List{Int32}"/></returns>
            public List<int> Candidates(Location Where)
            {
                if (_parent[Where] > 0)
                    return new List<int>();
                
                bool[] present = new bool[10];
                foreach (var loc in Where.Blocking)
                    present[_parent[loc]] = true;

                List<int> result = new List<int>();
                for (int i = 1; i < 10; i++)
                    if (!present[i])
                        result.Add(i);

                return result;
            }

            /// <summary>
            /// For convenience, returns a container of every empty location with the candidates for that location
            /// </summary>
            /// <returns><see cref="Dictionary{Location, List}"/></returns>
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
