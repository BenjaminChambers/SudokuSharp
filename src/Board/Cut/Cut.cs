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

                if (Filled.Count > 0)
                {
                    Location loc = new Location(Filled[Stream.Next(Filled.Count)]);

                    result[loc] = 0;
                    result[loc.FlipHorizontal()] = 0;
                    result[loc.FlipVertical()] = 0;
                    result[loc.FlipHorizontal().FlipVertical()] = 0;

                    if (result.ExistsUniqueSolution)
                        return result;
                }
                return _parent;
            }

            public Board Pair(Random Stream)
            {
                var result = new Board(_parent);

                var Filled = _parent.Find.FilledLocations().ToList();

                if (Filled.Count > 0)
                {
                    Location loc = new Location(Filled[Stream.Next(Filled.Count)]);

                    result[loc] = 0;

                    if (Stream.Next(2) == 1)
                        result[loc.FlipHorizontal()] = 0;
                    else
                        result[loc.FlipVertical()] = 0;

                    if (result.ExistsUniqueSolution)
                        return result;
                }
                return _parent;
            }

            public Board Single(Random Stream)
            {
                var result = new Board(_parent);

                var Filled = _parent.Find.FilledLocations().ToList();

                if (Filled.Count > 0)
                {
                    result[Filled[Stream.Next(Filled.Count)]] = 0;

                    if (result.ExistsUniqueSolution)
                        return result;
                }
                return _parent;
            }

            public Board AllSingles()
            {
                var result = new Board(_parent);

                var Filled = _parent.Find.FilledLocations();

                foreach (var loc in Filled)
                {
                    int test = result[loc];
                    result[loc] = 0;
                    if (!result.ExistsUniqueSolution)
                        result[loc] = test;
                }

                return result;
            }

            public Board AllSingles(Random Stream)
            {
                var result = new Board(_parent);

                List<Location> Randomized = new List<Location>();

                var Filled = _parent.Find.FilledLocations();
                foreach (var loc in Filled)
                    Randomized.Insert(Stream.Next(Randomized.Count), loc);

                foreach (var loc in Randomized)
                {
                    int test = result[loc];
                    result[loc] = 0;
                    if (!result.ExistsUniqueSolution)
                        result[loc] = test;
                }

                return result;
            }
        }
    }
}
