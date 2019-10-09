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

        public Board(Board Source, IEnumerable<(int Location, int Value)> Changes)
        {
            this.Order = Source.Order;
            Size = Order * Order;

            var work = Source.Cells.ToArray();
            foreach (var change in Changes)
                work[change.Location] = change.Value;

            Cells = new ReadOnlyCollection<int>(work);
        }
        #endregion



        #region Accessors
        public int this[int Location]
            => Cells[Location];

        public int[] GetRow(int Row)
        {
            if (Row < 0 || Row >= Size)
                throw new ArgumentOutOfRangeException($"Row {Row} is out of range of [0..{Size - 1}]");

            var result = new int[Size];
            for (int i = 0; i < Size; i++)
                result[i] = Cells[Row * Size + i];
            return result;
        }

        public int[] GetColumn(int Column)
        {
            if (Column < 0 || Column >= Size)
                throw new ArgumentOutOfRangeException($"Column {Column} is out of range of [0..{Size - 1}]");

            var result = new int[Size];
            for (int i = 0; i < Size; i++)
                result[i] = Cells[Column + i * Size];

            return result;
        }

        public int[] GetZone(int Zone)
        {
            if (Zone < 0 || Zone >= Size)
                throw new ArgumentOutOfRangeException($"Zone {Zone} is out of range of [0..{Size - 1}]");

            var zRow = Zone - (Zone % Order);
            var zCol = (Zone % Order) * Order;

            var result = new int[Size];
            for (int iR = 0; iR < Order; iR++)
            {
                for (int iC = 0; iC < Order; iC++)
                {
                    result[iR * Order + iC] = Cells[(zRow + iR) * Size + zCol + iC];
                }
            }

            return result;
        }

        public (int Row, int Column, int Zone) GetCoordinates(int Index)
        {
            var r = Index / Size;
            var c = Index % Size;
            var z = r - (r % Order) + (c / Order);

            return (r, c, z);
        }
        #endregion



        #region Transform
        public Board PutCell(int Location, int Value)
            => new Board(this, new[] { (Location, Value) });
        public Board PutCells(IEnumerable<(int Location, int Value)> Changes)
            => new Board(this, Changes);
        public Board SwapColumns(int First, int Second) => throw new NotImplementedException();
        public Board SwapRows(int First, int Second) => throw new NotImplementedException();
        public Board SwapDigits(int First, int Second) => throw new NotImplementedException();
        public Board Rotate(int NumberOfTimesClockwise) => throw new NotImplementedException();
        public Board Flip(bool Vertical, bool Horizontal) => throw new NotImplementedException();
        #endregion



        #region Find
        // These functions work on the board itself
        public HashSet<int> FindCandidates(int Location)
        {
            var Result = new HashSet<int>(Enumerable.Range(1,Size));
            var (r, c, z) = GetCoordinates(Location);

            foreach (var test in GetRow(r).Concat(GetColumn(c)).Concat(GetZone(z)))
            {
                if (Result.Contains(test))
                    Result.Remove(test);
            }

            return Result;
        }

        public IEnumerable<PossibilitySet> FindNakedSets(int SetSize) => throw new NotImplementedException();
        public IEnumerable<PossibilitySet> FindHiddenSets(int SetSize) => throw new NotImplementedException();

        // These functions require a Scratchpad and will alert you to values which may be eliminated from it
        public IEnumerable<PossibilitySet> FindLockedCandidates(Scratchpad Work) => throw new NotImplementedException();
        public IEnumerable<PossibilitySet> FindXWing(Scratchpad Work) => throw new NotImplementedException();
        public IEnumerable<PossibilitySet> FindXYWing(Scratchpad Work) => throw new NotImplementedException();
        public IEnumerable<PossibilitySet> FindSwordfish(Scratchpad Work) => throw new NotImplementedException();
        public IEnumerable<PossibilitySet> FindColorChain(int MaxSteps) => throw new NotImplementedException();
        #endregion



        #region Meta info about the board
        public bool Solved
        {
            get
            {
                bool testFail(IEnumerable<int> src)
                {
                    var present = new HashSet<int>();
                    foreach (var x in src)
                    {
                        if (x < 1 || x >= Size)
                            return true;

                        if (present.Contains(x))
                            return true;

                        present.Add(x);
                    }
                    return false;
                }

                for (int i = 0; i < Size; i++)
                {
                    if (testFail(GetRow(i)))
                        return false;
                    if (testFail(GetColumn(i)))
                        return false;
                    if (testFail(GetZone(i)))
                        return false;
                }

                return true;
            }
        }

        public bool Valid { get => throw new NotImplementedException(); }

        public readonly int Order;
        public readonly int Size;
        public ReadOnlyCollection<int> Cells;
        #endregion



        public IEnumerable<Board> Fill() => throw new NotImplementedException();
        public IEnumerable<Board> Fill(Random Rnd) => throw new NotImplementedException();
    }
}
