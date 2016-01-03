using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class _FillClass
        {
            public Board Randomized(int Seed)
            {
                return Randomized(new Random(Seed));
            }

            public Board Randomized(Random Stream)
            {
                var result = new Board(_parent);

                if (RandomRecursion(result, Stream, 0))
                    return result;

                return null;
            }

            private static bool RandomRecursion(Board work, Random stream, int Index)
            {
                if (Index == 81)
                    return true;

                if (work[Index] > 0)
                    return RandomRecursion(work, stream, Index + 1);
                else
                {
                    var possible = new List<int>();

                    foreach (int digit in work.Find.Candidates(Index))
                        possible.Insert(stream.Next(possible.Count), digit);

                    foreach (int test in possible)
                    {
                        work[Index] = test;
                        if (RandomRecursion(work, stream, Index + 1))
                            return true;
                    }
                    work[Index] = 0;

                    return false;
                }
            }
        }
    }
}

