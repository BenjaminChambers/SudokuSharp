using System;
using System.Collections.Generic;

namespace SudokuSharp
{
    public class Board
    {
        #region Constructors
        // Order: 2 = 4x4, 3=9x9, 4=16x16...
        public Board(int Order = 3) => throw new NotImplementedException();
        public Board(Board Source) => throw new NotImplementedException();
        public Board(IEnumerable<int> Source) => throw new NotImplementedException();
        public Board(string Source) => throw new NotImplementedException();
        #endregion



        #region Accessors
        public int this[int Location] => throw new NotImplementedException();
        public int GetCell(int Location) => throw new NotImplementedException();
        public void PutCell(int Location, int Value) => throw new NotImplementedException();
        #endregion



        #region Transform
        public Board SwapColumns(int First, int Second) => throw new NotImplementedException();
        public Board SwapRows(int First, int Second) => throw new NotImplementedException();
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
        #endregion



        #region Meta info about the board
        public bool Solved { get => throw new NotImplementedException(); }
        public bool Valid { get => throw new NotImplementedException(); }
        public int Order => _order;
        public int Size => _order * _order;
        #endregion



        public IEnumerable<Board> Solve() => throw new NotImplementedException();

        private int[] _data;
        int _order;
    }
}
