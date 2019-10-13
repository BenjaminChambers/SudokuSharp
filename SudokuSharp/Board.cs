using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SudokuSharp
{
    public class Board
    {
        #region Constructors
        // Order: 2 = 4x4, 3=9x9, 4=16x16, 5=25x25
        public Board(int Order, IList<int> SourceData)
        {
            // TODO: possibly increase the limit on Order later, but for now I just want to get things working
            if (Order < 2 || Order > 5)
                throw new ArgumentOutOfRangeException("Order must be between 2 and 5 (inclusive)");

            this.Order = Order;
            Size = Order * Order;
            Cells = new ReadOnlyCollection<int>(SourceData.Take(Size * Size).ToList());
        }

        public Board(int Order = 3)
            : this(Order, new int[Order * Order * Order * Order]) { }

        public Board(Board Source)
            : this(Source.Order, Source.Cells) { }

        public Board(Board Source, IEnumerable<(int Index, int Value)> Changes)
        {
            this.Order = Source.Order;
            Size = Order * Order;

            var work = Source.Cells.ToArray();
            foreach (var (Index, Value) in Changes)
                work[Index] = Value;

            Cells = new ReadOnlyCollection<int>(work);
        }
        #endregion



        #region Accessors
        public int this[int Index]
            => Cells[Index];
        #endregion



        #region Transform
        public Board PutCell(int Index, int Value)
            => new Board(this, new[] { (Index, Value) });
        public Board PutCells(IEnumerable<(int Index, int Value)> Changes)
            => new Board(this, Changes);
        public Board SwapColumns(int First, int Second)
        {
            if (First % Order != Second % Order)
                throw new ArgumentException($"Columns {First} and {Second} are not in the same block.");

            var changes = new List<(int Index, int Value)>();

            for (int i = 0; i < Size; i++)
            {
                var a = Location.Index(Order, i, First);
                var b = Location.Index(Order, i, Second);
                changes.Add((a, Cells[b]));
                changes.Add((b, Cells[a]));
            }

            return new Board(this, changes);
        }
        public Board SwapRows(int First, int Second)
        {
            if (First % Order != Second % Order)
                throw new ArgumentException($"Rows {First} and {Second} are not in the same block.");

            var changes = new List<(int Index, int Value)>();

            for (int i = 0; i < Size; i++)
            {
                var a = Location.Index(Order, First, i);
                var b = Location.Index(Order, Second, i);
                changes.Add((a, Cells[b]));
                changes.Add((b, Cells[a]));
            }

            return new Board(this, changes);
        }
        public Board SwapDigits(int First, int Second)
        {
            if (First == Second)
                throw new ArgumentException($"{First} and {Second} are not separate digits");

            return new Board(this, from i in Enumerable.Range(0, Size * Size)
                                   where Cells[i] == First || Cells[i] == Second
                                   select (i, Cells[i] == First ? Second : First));
        }
        public Board Rotate(int NumberOfTimesClockwise)
        {
            var result = new List<int>();

            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    (int Row, int Col) = (NumberOfTimesClockwise % 4) switch
                    {
                        0 => (r, c),
                        1 => (Size - c - 1, r),
                        2 => (Size - r - 1, Size - c - r),
                        3 => (c, Size - r - 1)
                    };

                    result.Add(Cells[Location.Index(Order, Row, Column)]);
                }
            }

            return new Board(Order, result);
        }
        public Board Flip(bool Vertical, bool Horizontal)
        {
            var result = new List<int>();

            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    var nc = Horizontal ? Size - c - 1 : c;
                    var nr = Vertical ? Size - r - 1 : r;
                    result.Add(Cells[Location.Index(Order, nr, nc)]);
                }
            }

            return new Board(Order, result);
        }
        #endregion



        #region Find
        // These functions work on the board itself
        public HashSet<int> FindCandidates(int Index)
        {
            var Result = new HashSet<int>(Enumerable.Range(1, Size));

            foreach (var test in Location.BlockingIndices(Order, Index))
            {
                if (Result.Contains(Cells[test]))
                    Result.Remove(test);
            }

            return Result;
        }

        public IEnumerable<Util.PossibilitySet> FindNakedSets(int SetSize) => throw new NotImplementedException();
        public IEnumerable<Util.PossibilitySet> FindHiddenSets(int SetSize) => throw new NotImplementedException();

        // These functions require a Scratchpad and will alert you to values which may be eliminated from it
        public IEnumerable<Util.PossibilitySet> FindLockedCandidates(Scratchpad Work) => throw new NotImplementedException();
        public IEnumerable<Util.PossibilitySet> FindXWing(Scratchpad Work) => throw new NotImplementedException();
        public IEnumerable<Util.PossibilitySet> FindXYWing(Scratchpad Work) => throw new NotImplementedException();
        public IEnumerable<Util.PossibilitySet> FindSwordfish(Scratchpad Work) => throw new NotImplementedException();
        public IEnumerable<Util.PossibilitySet> FindColorChain(int MaxSteps) => throw new NotImplementedException();
        #endregion



        #region Meta info about the board
        public bool Full
            => !(from c in Cells where c == 0 select c).Any();
        public bool Solved
        {
            get
            {
                int sum = (from i in Enumerable.Range(1, Size) select i * i).Sum();

                for (int i = 0; i < Size; i++)
                {
                    if ((from x in Location.RowIndices(Order, i) select Cells[x] * Cells[x]).Sum() != sum) return false;
                    if ((from x in Location.ColumnIndices(Order, i) select Cells[x] * Cells[x]).Sum() != sum) return false;
                    if ((from x in Location.ZoneIndices(Order, i) select Cells[x] * Cells[x]).Sum() != sum) return false;
                }

                return true;
            }
        }

        public bool Valid { get => throw new NotImplementedException(); }

        public readonly int Order;
        public readonly int Size;
        public ReadOnlyCollection<int> Cells;
        #endregion



        public IEnumerable<Board> Fill()
        {
            if (Full)
            {
                if (Solved)
                    yield return this;
            }
            else
            {
                var loc = Enumerable.Range(0, Cells.Count).Where(x => Cells[x] == 0).First();
                var candidates = FindCandidates(loc);
                foreach (var attempt in candidates)
                {
                    foreach (var result in PutCell(loc, attempt).Fill())
                        yield return result;
                }
            }
        }

        public IEnumerable<Board> Fill(Random Rnd) => throw new NotImplementedException();
    }
}
