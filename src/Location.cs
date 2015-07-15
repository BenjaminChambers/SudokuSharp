using System;
using System.Collections.ObjectModel;

namespace SudokuSharp
{
    /// <summary>
    /// The Location class is a reference to a specific cell on a Sudoku board.
    /// It is internally represented as an integer for performance, but contains many useful methods
    /// </summary>
    public class Location
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="idx">The cell <see cref="Index"/>.</param>
        public Location(int idx)
        {
            Index = idx;
        }
        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="Location"/>.
        /// </summary>
        /// <param name="index">The cell <see cref="Index"/>.</param>
        /// <returns>
        /// A new <see cref="Location"/>.
        /// </returns>
        public static implicit operator Location(int index)
        {
            return new Location(index);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class. Instead of specifying the index of the cell, you may specify the row and column to use.
        /// The parameters are specified in Column, Row order to match the X,Y convention.
        /// </summary>
        /// <param name="Column">The column.</param>
        /// <param name="Row">The row.</param>
        public Location(int Column, int Row)
        {
            Index = ListOfIndices[Column, Row];
        }
        #endregion

        #region Location Aids
        /// <summary>
        /// Returns the Row of the specified <see cref="Location"/>.
        /// </summary>
        /// <value>
        /// The row of the specified <see cref="Location"/>.
        /// </value>
        public int Row { get { return ListOfRows[Index]; } }
        /// <summary>
        /// Gets the column of the specified <see cref="Location"/>.
        /// </summary>
        /// <value>
        /// The column of the specified <see cref="Location"/>.
        /// </value>
        public int Column { get { return ListOfColumns[Index]; } }
        /// <summary>
        /// Gets the zone of the specified <see cref="Location"/>.
        /// </summary>
        /// <value>
        /// The zone of the specified <see cref="Location"/>.
        /// The Zone is the 3x3 block to which the location belongs; there are nine of them on the Sudoku board and, together with each Row and Column, the Zone when solved will contain a single instance of each digit.
        /// </value>
        public int Zone { get { return ListOfZones[Index]; } }
        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>
        /// Indices are ordered beginning at 0 (the upper left corner), and running horizontally, and continuing on the following line.
        /// For example, the first row is numbered 0,1,2... while the second row is numbered 9,10,11...
        /// The lower right-hand corner is numbered 80.
        /// </value>
        public readonly int Index;

        /// <summary>
        /// Performs an implicit conversion from <see cref="Location"/> to <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="Where">The <see cref="Location"/> to be cast.</param>
        /// <returns>
        /// The location Index, as an integer
        /// </returns>
        public static implicit operator int (Location Where)
        {
            return Where.Index;
        }
        #endregion

        #region Lists of indices
        public static ReadOnlyCollection<int> All { get { return _all; } }

        // I'm currently marking these as Private, since they aren't ever used.
        // If I ever find a use for them, then I will mark them public

        // If these ever get utilized more I will consider precalculating them as well; as it is, the only one I actually call is GetConflicting, and it's not the bottleneck, so I'm not worried.
        private int[] GetRowIndices()
        {
            int[] result = new int[9];
            int start = Index - (Index % 9);

            for (int i = 0; i < 9; i++)
                result[i] = start + i;

            return result;
        }
        private int[] GetColumnIndices()
        {
            int[] result = new int[9];
            int start = Column;

            for (int i = 0; i < 9; i++)
                result[i] = start + i * 9;

            return result;
        }
        private int[] GetZoneIndices()
        {
            int[] result = new int[9];

            for (int i = 0; i < 3; i++)
            {
                result[i] = ZoneIndices[Zone] + i;
                result[i + 3] = ZoneIndices[Zone] + 9 + i;
                result[i + 6] = ZoneIndices[Zone] + 18 + i;
            }

            return result;
        }
        /// <summary>
        /// Gets the cells which may cause conflicts with this one.
        /// </summary>
        /// <returns>An <see cref="int"/>[] array of all the indices in the current row, column, or zone. These are the only cells which may conflict with this cell.</returns>
        public int[] GetConflictingIndices()
        {
            int[] result = new int[20];
            Array.Copy(ConflictingIndices[Index], result, 20);
            return result;
        }
        #endregion

        #region Conflict verification
        /// <summary>
        /// Determines whether [is same row] [the specified <see cref="Location"/> CompareTo].
        /// </summary>
        /// <param name="CompareTo">The <see cref="Location"/> to be compared.</param>
        /// <returns>True or False</returns>
        public bool IsSameRow(Location CompareTo) { return Row == CompareTo.Row; }
        /// <summary>
        /// Determines whether [is same Column] [the specified <see cref="Location"/> CompareTo].
        /// </summary>
        /// <param name="CompareTo">The <see cref="Location"/> to be compared.</param>
        /// <returns>True or False</returns>
        public bool IsSameColumn(Location CompareTo) { return Column == CompareTo.Column; }
        /// <summary>
        /// Determines whether [is same Zone] [the specified <see cref="Location"/> CompareTo].
        /// </summary>
        /// <param name="CompareTo">The <see cref="Location"/> to be compared.</param>
        /// <returns>True or False</returns>
        public bool IsSameZone(Location CompareTo) { return Zone == CompareTo.Zone; }
        /// <summary>
        /// Determines whether [the specified <see cref="Location"/> CompareTo] is either of the same row, column or zone as the calling instance.
        /// </summary>
        /// <param name="CompareTo">The <see cref="Location"/> to be compared.</param>
        /// <returns>True or False</returns>
        public bool IsBlockedBy(Location CompareTo) { return (IsSameRow(CompareTo) || IsSameColumn(CompareTo) || IsSameZone(CompareTo)); }
        #endregion

        #region Internal predeclarations
        public static ReadOnlyCollection<int> _all = new ReadOnlyCollection<int>(new int[81]
        {
            00, 1, 2, 3, 4, 5, 6, 7, 8,
            09,10,11,12,13,14,15,16,17,
            18,19,20,21,22,23,24,25,26,
            27,28,29,30,31,32,33,34,35,
            36,37,38,39,40,41,42,43,44,
            45,46,47,48,49,50,51,52,53,
            54,55,56,57,58,59,60,61,62,
            63,64,65,66,67,68,69,70,71,
            72,73,74,75,76,77,78,79,80
        });

        private static int[] ListOfRows =
        {
                0,0,0,0,0,0,0,0,0,
                1,1,1,1,1,1,1,1,1,
                2,2,2,2,2,2,2,2,2,
                3,3,3,3,3,3,3,3,3,
                4,4,4,4,4,4,4,4,4,
                5,5,5,5,5,5,5,5,5,
                6,6,6,6,6,6,6,6,6,
                7,7,7,7,7,7,7,7,7,
                8,8,8,8,8,8,8,8,8
        };
        private static int[] ListOfColumns =
        {
            0,1,2,3,4,5,6,7,8,
            0,1,2,3,4,5,6,7,8,
            0,1,2,3,4,5,6,7,8,
            0,1,2,3,4,5,6,7,8,
            0,1,2,3,4,5,6,7,8,
            0,1,2,3,4,5,6,7,8,
            0,1,2,3,4,5,6,7,8,
            0,1,2,3,4,5,6,7,8,
            0,1,2,3,4,5,6,7,8
        };
        private static int[] ListOfZones =
        {
            0,0,0,1,1,1,2,2,2,
            0,0,0,1,1,1,2,2,2,
            0,0,0,1,1,1,2,2,2,
            3,3,3,4,4,4,5,5,5,
            3,3,3,4,4,4,5,5,5,
            3,3,3,4,4,4,5,5,5,
            6,6,6,7,7,7,8,8,8,
            6,6,6,7,7,7,8,8,8,
            6,6,6,7,7,7,8,8,8
        };
        private static int[,] ListOfIndices =
        {
            { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
            { 9,10,11,12,13,14,15,16,17 },
            {18,19,20,21,22,23,24,25,26 },
            {27,28,29,30,31,32,33,34,35 },
            {36,37,38,39,40,41,42,43,44 },
            {45,46,47,48,49,50,51,52,53 },
            {54,55,56,57,58,59,60,61,62 },
            {63,64,65,66,67,68,69,70,71 },
            {72,73,74,75,76,77,78,79,80 }
        };
        private static int[] ZoneIndices = { 0, 3, 6, 27, 30, 33, 54, 57, 60 };

        private static int[][] ConflictingIndices = new int[][] {
    new int[] {1,2,3,4,5,6,7,8,9,10,11,18,19,20,27,36,45,54,63,72},
    new int[] {0,2,3,4,5,6,7,8,9,10,11,18,19,20,28,37,46,55,64,73},
    new int[] {0,1,3,4,5,6,7,8,9,10,11,18,19,20,29,38,47,56,65,74},
    new int[] {0,1,2,4,5,6,7,8,12,13,14,21,22,23,30,39,48,57,66,75},
    new int[] {0,1,2,3,5,6,7,8,12,13,14,21,22,23,31,40,49,58,67,76},
    new int[] {0,1,2,3,4,6,7,8,12,13,14,21,22,23,32,41,50,59,68,77},
    new int[] {0,1,2,3,4,5,7,8,15,16,17,24,25,26,33,42,51,60,69,78},
    new int[] {0,1,2,3,4,5,6,8,15,16,17,24,25,26,34,43,52,61,70,79},
    new int[] {0,1,2,3,4,5,6,7,15,16,17,24,25,26,35,44,53,62,71,80},
    new int[] {0,1,2,10,11,12,13,14,15,16,17,18,19,20,27,36,45,54,63,72},
    new int[] {0,1,2,9,11,12,13,14,15,16,17,18,19,20,28,37,46,55,64,73},
    new int[] {0,1,2,9,10,12,13,14,15,16,17,18,19,20,29,38,47,56,65,74},
    new int[] {3,4,5,9,10,11,13,14,15,16,17,21,22,23,30,39,48,57,66,75},
    new int[] {3,4,5,9,10,11,12,14,15,16,17,21,22,23,31,40,49,58,67,76},
    new int[] {3,4,5,9,10,11,12,13,15,16,17,21,22,23,32,41,50,59,68,77},
    new int[] {6,7,8,9,10,11,12,13,14,16,17,24,25,26,33,42,51,60,69,78},
    new int[] {6,7,8,9,10,11,12,13,14,15,17,24,25,26,34,43,52,61,70,79},
    new int[] {6,7,8,9,10,11,12,13,14,15,16,24,25,26,35,44,53,62,71,80},
    new int[] {0,1,2,9,10,11,19,20,21,22,23,24,25,26,27,36,45,54,63,72},
    new int[] {0,1,2,9,10,11,18,20,21,22,23,24,25,26,28,37,46,55,64,73},
    new int[] {0,1,2,9,10,11,18,19,21,22,23,24,25,26,29,38,47,56,65,74},
    new int[] {3,4,5,12,13,14,18,19,20,22,23,24,25,26,30,39,48,57,66,75},
    new int[] {3,4,5,12,13,14,18,19,20,21,23,24,25,26,31,40,49,58,67,76},
    new int[] {3,4,5,12,13,14,18,19,20,21,22,24,25,26,32,41,50,59,68,77},
    new int[] {6,7,8,15,16,17,18,19,20,21,22,23,25,26,33,42,51,60,69,78},
    new int[] {6,7,8,15,16,17,18,19,20,21,22,23,24,26,34,43,52,61,70,79},
    new int[] {6,7,8,15,16,17,18,19,20,21,22,23,24,25,35,44,53,62,71,80},
    new int[] {0,9,18,28,29,30,31,32,33,34,35,36,37,38,45,46,47,54,63,72},
    new int[] {1,10,19,27,29,30,31,32,33,34,35,36,37,38,45,46,47,55,64,73},
    new int[] {2,11,20,27,28,30,31,32,33,34,35,36,37,38,45,46,47,56,65,74},
    new int[] {3,12,21,27,28,29,31,32,33,34,35,39,40,41,48,49,50,57,66,75},
    new int[] {4,13,22,27,28,29,30,32,33,34,35,39,40,41,48,49,50,58,67,76},
    new int[] {5,14,23,27,28,29,30,31,33,34,35,39,40,41,48,49,50,59,68,77},
    new int[] {6,15,24,27,28,29,30,31,32,34,35,42,43,44,51,52,53,60,69,78},
    new int[] {7,16,25,27,28,29,30,31,32,33,35,42,43,44,51,52,53,61,70,79},
    new int[] {8,17,26,27,28,29,30,31,32,33,34,42,43,44,51,52,53,62,71,80},
    new int[] {0,9,18,27,28,29,37,38,39,40,41,42,43,44,45,46,47,54,63,72},
    new int[] {1,10,19,27,28,29,36,38,39,40,41,42,43,44,45,46,47,55,64,73},
    new int[] {2,11,20,27,28,29,36,37,39,40,41,42,43,44,45,46,47,56,65,74},
    new int[] {3,12,21,30,31,32,36,37,38,40,41,42,43,44,48,49,50,57,66,75},
    new int[] {4,13,22,30,31,32,36,37,38,39,41,42,43,44,48,49,50,58,67,76},
    new int[] {5,14,23,30,31,32,36,37,38,39,40,42,43,44,48,49,50,59,68,77},
    new int[] {6,15,24,33,34,35,36,37,38,39,40,41,43,44,51,52,53,60,69,78},
    new int[] {7,16,25,33,34,35,36,37,38,39,40,41,42,44,51,52,53,61,70,79},
    new int[] {8,17,26,33,34,35,36,37,38,39,40,41,42,43,51,52,53,62,71,80},
    new int[] {0,9,18,27,28,29,36,37,38,46,47,48,49,50,51,52,53,54,63,72},
    new int[] {1,10,19,27,28,29,36,37,38,45,47,48,49,50,51,52,53,55,64,73},
    new int[] {2,11,20,27,28,29,36,37,38,45,46,48,49,50,51,52,53,56,65,74},
    new int[] {3,12,21,30,31,32,39,40,41,45,46,47,49,50,51,52,53,57,66,75},
    new int[] {4,13,22,30,31,32,39,40,41,45,46,47,48,50,51,52,53,58,67,76},
    new int[] {5,14,23,30,31,32,39,40,41,45,46,47,48,49,51,52,53,59,68,77},
    new int[] {6,15,24,33,34,35,42,43,44,45,46,47,48,49,50,52,53,60,69,78},
    new int[] {7,16,25,33,34,35,42,43,44,45,46,47,48,49,50,51,53,61,70,79},
    new int[] {8,17,26,33,34,35,42,43,44,45,46,47,48,49,50,51,52,62,71,80},
    new int[] {0,9,18,27,36,45,55,56,57,58,59,60,61,62,63,64,65,72,73,74},
    new int[] {1,10,19,28,37,46,54,56,57,58,59,60,61,62,63,64,65,72,73,74},
    new int[] {2,11,20,29,38,47,54,55,57,58,59,60,61,62,63,64,65,72,73,74},
    new int[] {3,12,21,30,39,48,54,55,56,58,59,60,61,62,66,67,68,75,76,77},
    new int[] {4,13,22,31,40,49,54,55,56,57,59,60,61,62,66,67,68,75,76,77},
    new int[] {5,14,23,32,41,50,54,55,56,57,58,60,61,62,66,67,68,75,76,77},
    new int[] {6,15,24,33,42,51,54,55,56,57,58,59,61,62,69,70,71,78,79,80},
    new int[] {7,16,25,34,43,52,54,55,56,57,58,59,60,62,69,70,71,78,79,80},
    new int[] {8,17,26,35,44,53,54,55,56,57,58,59,60,61,69,70,71,78,79,80},
    new int[] {0,9,18,27,36,45,54,55,56,64,65,66,67,68,69,70,71,72,73,74},
    new int[] {1,10,19,28,37,46,54,55,56,63,65,66,67,68,69,70,71,72,73,74},
    new int[] {2,11,20,29,38,47,54,55,56,63,64,66,67,68,69,70,71,72,73,74},
    new int[] {3,12,21,30,39,48,57,58,59,63,64,65,67,68,69,70,71,75,76,77},
    new int[] {4,13,22,31,40,49,57,58,59,63,64,65,66,68,69,70,71,75,76,77},
    new int[] {5,14,23,32,41,50,57,58,59,63,64,65,66,67,69,70,71,75,76,77},
    new int[] {6,15,24,33,42,51,60,61,62,63,64,65,66,67,68,70,71,78,79,80},
    new int[] {7,16,25,34,43,52,60,61,62,63,64,65,66,67,68,69,71,78,79,80},
    new int[] {8,17,26,35,44,53,60,61,62,63,64,65,66,67,68,69,70,78,79,80},
    new int[] {0,9,18,27,36,45,54,55,56,63,64,65,73,74,75,76,77,78,79,80},
    new int[] {1,10,19,28,37,46,54,55,56,63,64,65,72,74,75,76,77,78,79,80},
    new int[] {2,11,20,29,38,47,54,55,56,63,64,65,72,73,75,76,77,78,79,80},
    new int[] {3,12,21,30,39,48,57,58,59,66,67,68,72,73,74,76,77,78,79,80},
    new int[] {4,13,22,31,40,49,57,58,59,66,67,68,72,73,74,75,77,78,79,80},
    new int[] {5,14,23,32,41,50,57,58,59,66,67,68,72,73,74,75,76,78,79,80},
    new int[] {6,15,24,33,42,51,60,61,62,69,70,71,72,73,74,75,76,77,79,80},
    new int[] {7,16,25,34,43,52,60,61,62,69,70,71,72,73,74,75,76,77,78,80},
    new int[] {8,17,26,35,44,53,60,61,62,69,70,71,72,73,74,75,76,77,78,79}};
        #endregion
    }
}
