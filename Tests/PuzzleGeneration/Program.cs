using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * On my laptop running 10x50:
Timing the creation of 500 puzzles.
50 puzzles created in 3.03 seconds for 17 puzzles per second.
50 puzzles created in 3.03 seconds for 16 puzzles per second.
50 puzzles created in 4.31 seconds for 12 puzzles per second.
50 puzzles created in 5.49 seconds for 9 puzzles per second.
50 puzzles created in 5.10 seconds for 10 puzzles per second.
50 puzzles created in 5.56 seconds for 9 puzzles per second.
50 puzzles created in 7.40 seconds for 7 puzzles per second.
50 puzzles created in 4.92 seconds for 10 puzzles per second.
50 puzzles created in 7.64 seconds for 7 puzzles per second.
50 puzzles created in 4.78 seconds for 10 puzzles per second.
500 puzzles created in 51.26 seconds for 10 puzzles per second.
Press any key to continue . . .
*/

namespace PuzzleGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            int Batches = 10;
            int BatchSize = 50;

            Console.WriteLine("Timing the creation of {0:N0} puzzles.", Batches*BatchSize);
            var rnd = new Random(0);
            var brd = SudokuSharp.Factory.Solution(rnd);

            TimeSpan elapsed;
            var start = DateTime.Now;
            for (int i = 0; i < Batches; i++)
            {
                var bStart = DateTime.Now;
                for (int j = 0; j < BatchSize; j++)
                {
                    SudokuSharp.Factory.Puzzle(brd, rnd, 10, 10, 10);
                }
                elapsed = DateTime.Now - bStart;
                Console.WriteLine("{0:N0} puzzles created in {1:0.00} seconds for {2:N0} puzzles per second.", BatchSize, elapsed.TotalSeconds, BatchSize / elapsed.TotalSeconds);
            }
            elapsed = DateTime.Now - start;
            Console.WriteLine("{0:N0} puzzles created in {1:0.00} seconds for {2:N0} puzzles per second.", BatchSize * Batches, elapsed.TotalSeconds, (BatchSize*Batches) / elapsed.TotalSeconds);
        }
    }
}
