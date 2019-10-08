using SudokuSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    public class Common
    {
        static public int[] Raw2 = new int[] {
                1,2, 3,4,
                3,4, 1,2,

                2,3, 4,1,
                4,1, 2,3
            };
        static public Board Constructed2 = new Board(2, Raw2);
        static public Board Copied2 = new Board(Constructed2);

        static public int[] Raw3 = new int[]
        {
            1,2,3, 4,5,6, 7,8,9,
            4,5,6, 7,8,9, 1,2,3,
            7,8,9, 1,2,3, 4,5,6,

            2,3,4, 5,6,7, 8,9,1,
            5,6,7, 8,9,1, 2,3,4,
            8,9,1, 9,1,2, 3,4,5,

            3,4,5, 6,7,8, 9,1,2,
            6,7,8, 9,1,2, 3,4,5,
            9,1,2, 3,4,5, 6,7,8
        };
        static public Board Constructed3 = new Board(3, Raw3);
        static public Board Copied3 = new Board(Constructed3);

        static public bool CompareArrays(int[] First, int[] Second)
        {
            if (First.Length != Second.Length)
                return false;

            for (int i = 0; i < First.Length; i++)
                if (First[i] != Second[i])
                    return false;

            return true;
        }
    }
}
