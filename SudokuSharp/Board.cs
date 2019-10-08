using System;
using System.Collections.Generic;

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
        public int this[int Location] => throw new NotImplementedException();
        public int GetCell(int Location) => throw new NotImplementedException();
        public void PutCell(int Location, int Value) => throw new NotImplementedException();
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
        public bool Solved { get => throw new NotImplementedException(); }
        public bool Valid { get => throw new NotImplementedException(); }
        public int Order => _order;
        public int Size => _order * _order;
        #endregion



        public IEnumerable<Board> Solve() => throw new NotImplementedException();


        private void createBoard(int Order)
        {
            _order = Order;
            _data = new int[Size * Size];
        }
        private int[] _data;
        int _order;
    }
}
