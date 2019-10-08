using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public class Board
    {
        #region Constructors
        // Order: 2 = 4x4, 3=9x9, 4=16x16, 5=25x25
        public Board(int Order = 3)
        {
            if (Order < 2 || Order > 5)
                throw new ArgumentOutOfRangeException("Order must be between 2 and 5 (inclusive)");

            createBoard(Order);
        }

        public Board(Board Source)
            : this(Source.Order)
        {
            Array.Copy(Source._data, 0, _data, 0, Size * Size);
        }

        public Board(int Order, IEnumerable<int> Source)
            : this(Order)
        {
            var idx = 0;
            foreach (var x in Source)
            {
                if (x < 0 || x > Size)
                    throw new ArgumentOutOfRangeException($"Encountered value {x} which is outside the range of 1-{Size}");
                _data[idx++] = x;
            }
        }
        #endregion



        #region Accessors
        public int this[int Location]
        {
            get => GetCell(Location);
            set => PutCell(Location, value);
        }
        public int GetCell(int Location)
            => _data[Location];
        public void PutCell(int Location, int Value)
        {
            if (Value < 0 || Value > Size)
                throw new ArgumentOutOfRangeException($"Value {Value} is out of range of [0..{Size - 1}]");

            _data[Location] = Value;
        }

        public int[] GetRow(int Row)
        {
            if (Row < 0 || Row >= Size)
                throw new ArgumentOutOfRangeException($"Row {Row} is out of range of [0..{Size - 1}]");

            var result = new int[Size];
            for (int i = 0; i < Size; i++)
                result[i] = _data[Row * Size + i];
            return result;
        }

        public int[] GetColumn(int Column)
        {
            if (Column < 0 || Column >= Size)
                throw new ArgumentOutOfRangeException($"Column {Column} is out of range of [0..{Size - 1}]");

            var result = new int[Size];
            for (int i = 0; i < Size; i++)
                result[i] = _data[Column + i * Size];

            return result;
        }

        public int[] GetZone(int Zone)
        {
            if (Zone < 0 || Zone >= Size)
                throw new ArgumentOutOfRangeException($"Zone {Zone} is out of range of [0..{Size - 1}]");

            var zRow = Zone - (Zone % Order);
            var zCol = (Zone % Order) * Order;
            var zIndex = zRow * Order + zCol;

            var result = new int[Size];
            for (int iR = 0; iR < Order; iR++)
            {
                for (int iC = 0; iC < Order; iC++)
                {
                    result[iR * Order + iC] = _data[zIndex + iR * Size + iC];
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

        public int Order => _order;
        public int Size => _order * _order;
        #endregion



        public IEnumerable<Board> Fill()
        {


            foreach (var result in Fill(new Board(this), 0))
                yield return result;
        }

        public IEnumerable<Board> Fill(Random Rnd) => throw new NotImplementedException();


        private void createBoard(int Order)
        {
            _order = Order;
            _data = new int[Size * Size];
        }
        private int[] _data;
        int _order;
    }
}
