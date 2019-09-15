using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        #region Is Solved
        /// <summary>
        /// Returns whether or not the particular puzzle is solved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is solved; otherwise, <c>false</c>.
        /// </value>
        public bool IsSolved
        {
            get
            {
                const int checksum = (1 + 4 + 9 + 16 + 25 + 36 + 49 + 64 + 81);

                for (int area = 0; area < 9; area++)
                {
                    int sum = 0;
                    foreach (int check in GetRow(area)) sum += (check * check);
                    if (sum != checksum) return false;

                    sum = 0;
                    foreach (int check in GetColumn(area)) sum += (check * check);
                    if (sum != checksum) return false;

                    sum = 0;
                    foreach (int check in GetZone(area)) sum += (check * check);
                    if (sum != checksum) return false;

                }
                return true;
            }
        }
        #endregion

        #region Is Valid
        /// <summary>
        /// Returns whether this particular puzzle MAY be solved.
        /// 
        /// Certain conditions are known to invalidate a puzzle, preventing the existence of a unique solution. This checks for those conditions.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                if (DuplicateValuesPresent) return false;
                if (!AreEnoughDigitsPresent) return false;
                if (CanSwapColumnsOrRows) return false;

                return true;
            }
        }

        private bool DuplicateValuesPresent
        {
            get
            {
                int[,] CountByRow = new int[9, 10];
                int[,] CountByColumn = new int[9, 10];
                int[,] CountByZone = new int[9, 10];

                for (int i = 0; i < 81; i++)
                {
                    Location loc = i;
                    int value = GetCell(i);

                    CountByRow[loc.Row, value]++;
                    CountByColumn[loc.Column, value]++;
                    CountByZone[loc.Zone, value]++;
                }

                for (int area = 0; area < 9; area++)
                {
                    for (int digit = 1; digit < 10; digit++)
                    {
                        if ((CountByRow[area, digit] > 1) || (CountByColumn[area, digit] > 1) || (CountByZone[area, digit] > 1))
                            return true;
                    }
                }

                return false;
            }
        }

        private bool AreEnoughDigitsPresent
        {
            get
            {
                // Easy check: every 8 of the 9 digits must be present on the board
                // If two digits are completely missing, then those two digits may be swapped and you have two
                // valid solutions.
                // Also checking for at least 16 clues total... just so it's not completely insane

                bool[] present = new bool[10];
                for (int i = 0; i < 81; i++)
                {
                    present[data[i]] = true;
                }

                int presentCount = 0;
                for (int i = 1; i < 10; i++)
                    if (present[i])
                        presentCount++;

                if (presentCount > 7)
                    return true;

                return false;
            }
        }
        private bool CanSwapColumnsOrRows
        {
            get
            {
                // If two columns within the same zone are both completely empty, they may be swapped and you have
                // two unique solutions
                bool[] isColPopulated = new bool[9];
                bool[] isRowPopulated = new bool[9];

                for (int i = 0; i < 81; i++)
                {
                    if (data[i] > 0)
                    {
                        Location loc = i;
                        isColPopulated[loc.Column] = true;
                        isRowPopulated[loc.Row] = true;
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    int count = 0;
                    if (isColPopulated[i * 3]) count++;
                    if (isColPopulated[i * 3 + 1]) count++;
                    if (isColPopulated[i * 3 + 2]) count++;
                    if (count < 2) return true;
                    count = 0;
                    if (isRowPopulated[i * 3]) count++;
                    if (isRowPopulated[i * 3 + 1]) count++;
                    if (isRowPopulated[i * 3 + 2]) count++;
                    if (count < 2) return true;
                }
                return false;
            }
        }
        #endregion

        #region Unique Solution
        /// <summary>
        /// Verifies the existance of a unique solution
        /// </summary>
        /// <value>
        /// <c>true</c> if [a unique solution exists]; otherwise, <c>false</c>.
        /// </value>
        public bool ExistsUniqueSolution()
        {
            if (IsSolved) return true;
            if (!IsValid) return false;

            for (int i = 0; i < 81; i++)
            {
                if (GetCell(i) == 0)
                { // Only test against empty cells
                    var Candidates = Find.Candidates(i);

                    if (Candidates.Count > 1)
                    { // Only test where there's more than one option
                        bool foundSolution = false;
                        var working = new Board(this);

                        foreach (int test in Candidates)
                        {
                            working[i] = test;

                            if (working.Fill.Sequential() != null)
                            {
                                // We just found a solution. If we have already found a solution, then multiple exist and we may quit.
                                if (foundSolution)
                                    return false;

                                foundSolution = true;
                            }
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region CountSolutions
        /// <summary>
        /// Attempts to count all possible solutions
        /// </summary>
        /// <returns></returns>
        public int CountSolutions()
        {
            Board work = new Board(this);

            // fill everything that has definite answers
            var mustFill = work.Find.AllSingles().Union(work.Find.LockedCandidates());
            while (mustFill.Count() > 0)
            {
                foreach (var item in mustFill)
                    work[item.Key] = item.Value;

                mustFill = work.Find.LockedCandidates();
            }

            if (IsSolved)
                return 1;

            return CountRecursion(work, 0);
        }

        private static int CountRecursion(Board work, int idx)
        {
            if (idx == 81) // using int instead of Location because Location CAN'T have a value of 81
                return 1;

            if (work[idx] > 0)
                return CountRecursion(work, idx + 1);

            var possible = work.Find.Candidates(idx);
            if (possible.Count == 0)
                return 0;

            int count = 0;
            foreach (var item in possible)
            {
                work[idx] = item;
                count += CountRecursion(work, idx + 1);
            }
            work[idx] = 0;

            return count;
        }
        #endregion
    }
}

