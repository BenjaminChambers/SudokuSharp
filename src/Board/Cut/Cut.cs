using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        private _CutClass _cutClass;

        public _CutClass Cut
        {
            get
            {
                if (_cutClass == null)
                    _cutClass = new _CutClass(this);

                return _cutClass;
            }
        }


        public partial class _CutClass
        {
            Board _parent;

            public _CutClass(Board Parent)
            {
                _parent = Parent;
            }

            public Board Quad(Random Stream)
            {
                var result = new Board(_parent);

                var Filled = _parent.Find.FilledLocations().ToList();
                Location loc = new Location(Filled[Stream.Next(Filled.Count)]);

                int x = loc.Column;
                int y = loc.Row;

                result[new Location(x, y)] = 0;
                result[new Location(8 - x, y)] = 0;
                result[new Location(x, 8 - y)] = 0;
                result[new Location(8 - x, 8 - y)] = 0;

                if (result.ExistsUniqueSolution)
                    return result;
                else
                    return _parent;
            }

            public Board Pair(Random Stream)
            {
                var result = new Board(_parent);

                var Filled = _parent.Find.FilledLocations().ToList();
                Location loc = new Location(Filled[Stream.Next(Filled.Count)]);

                int x = loc.Column;
                int y = loc.Row;

                result[new Location(x, y)] = 0;

                if (Stream.Next(2) == 1)
                    result[new Location(8 - x, y)] = 0;
                else
                    result[new Location(x, 8 - y)] = 0;

                if (result.ExistsUniqueSolution)
                    return result;
                else
                    return _parent;
            }

            public Board Singles(Random Stream, int NumberToCut)
            {
                var result = new Board(_parent);

                List<Location> Randomized = new List<Location>();

                foreach (var loc in _parent.Find.FilledLocations())
                    Randomized.Insert(Stream.Next(Randomized.Count), loc);

                for (int i = 0; i < Math.Min(Randomized.Count, NumberToCut); i++)
                    result[Randomized[i]] = 0;

                if (result.ExistsUniqueSolution)
                    return result;
                else
                    return _parent;
            }
        }
    }
}
