using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        private _CutClass _cutClass;

        /// <summary>
        /// Hack to allow a namespace inside a class. Allows access to the Cut functions.
        /// </summary>
        public _CutClass Cut
        {
            get
            {
                if (_cutClass == null)
                    _cutClass = new _CutClass(this);

                return _cutClass;
            }
        }

        /// <summary>
        /// Hack to allow a namespace inside a class.
        /// </summary>
        public partial class _CutClass
        {
            Board _parent;

            /// <summary>
            /// Hack to allow a namespace inside a class.
            /// </summary>
            public _CutClass(Board Parent)
            {
                _parent = Parent;
            }

            /// <summary>
            /// Attempts to cut 4 locations from the current board, mirrored about both horizontal and vertical axes.
            /// </summary>
            /// <param name="Stream">An existing <see cref="Random"/> number generator</param>
            /// <returns>If the result has a unique solution, then the new <see cref="Board"/>. Otherwise, the original</returns>
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

                    if (result.ExistsUniqueSolution())
                        return result;
                }
                return _parent;
            }

            /// <summary>
            /// Attempts to cut 2 locations from the current board, mirrored about a randomly chosen axis.
            /// </summary>
            /// <param name="Stream">An existing <see cref="Random"/> number generator</param>
            /// <returns>If the result has a unique solution, then the new <see cref="Board"/>. Otherwise, the original</returns>
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

                    if (result.ExistsUniqueSolution())
                        return result;
                }
                return _parent;
            }

            /// <summary>
            /// Attempts to cut a single location
            /// </summary>
            /// <param name="Stream">An existing <see cref="Random"/> number generator</param>
            /// <returns>If the result has a unique solution, then the new <see cref="Board"/>. Otherwise, the original</returns>
            public Board Single(Random Stream)
            {
                var result = new Board(_parent);

                var Filled = _parent.Find.FilledLocations().ToList();

                if (Filled.Count > 0)
                {
                    result[Filled[Stream.Next(Filled.Count)]] = 0;

                    if (result.ExistsUniqueSolution())
                        return result;
                }
                return _parent;
            }

            /// <summary>
            /// Tries each filled location, in order. If the result has more than one solution, it is filled back in
            /// </summary>
            /// <returns></returns>
            public Board AllSingles()
            {
                var result = new Board(_parent);

                var Filled = _parent.Find.FilledLocations();

                foreach (var loc in Filled)
                {
                    int test = result[loc];
                    result[loc] = 0;

                    var possible = result.Find.Candidates(loc);

                    if (possible.Count > 1)
                    {
                        int count = 0;
                        foreach (var item in possible)
                        {
                            result[loc] = item;
                            if (result.Fill.Sequential() != null)
                                count++;
                        }

                        if (count > 1)
                            result[loc] = test;
                        else
                            result[loc] = 0;
                    }
                }

                return result;
            }

            /// <summary>
            /// Tries to cut each filled location, in random order. If the result has more than one solution, it is filled back in
            /// </summary>
            /// <param name="Stream"></param>
            /// <returns></returns>
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

                    var possible = result.Find.Candidates(loc);

                    if (possible.Count > 1)
                    {
                        int count = 0;
                        foreach (var item in possible)
                        {
                            result[loc] = item;
                            if (result.Fill.Sequential() != null)
                                count++;
                        }

                        if (count > 1)
                            result[loc] = test;
                        else
                            result[loc] = 0;
                    }
                }

                return result;
            }
        }
    }
}
