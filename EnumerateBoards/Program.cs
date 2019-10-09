using System;
using SudokuSharp;

namespace EnumerateBoards
{
    class Program
    {
        static void WriteBoard(Board b)
        {
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    Console.Write(b[r * 9 + c].ToString());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main()
        {
            var blank = new Board(2);

            foreach (var b in blank.Fill())
            {
                WriteBoard(b);
                Console.WriteLine("Press any key to search for the next board...");
                Console.ReadKey();
            }
        }
    }
}
