using System;
using System.Collections.Generic;
using System.Linq;

namespace TimePuzzleGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random(0);
            var brd = SudokuSharp.Factory.Solution(rnd);

            int BatchSize = 10;

            var results = new List<(int Batch, double Seconds)>();

            TimeSpan Elapsed;
            do
            {
                BatchSize = (3 * BatchSize) / 2;
                var start = DateTime.Now;
                for (int i = 0; i < BatchSize; i++)
                    SudokuSharp.Factory.Puzzle(brd, rnd, 10, 10, 10);
                Elapsed = DateTime.Now - start;
                Console.WriteLine($"{BatchSize} Puzzles created in {Elapsed.TotalSeconds} seconds.");
                results.Add((BatchSize, Elapsed.TotalSeconds));
            } while (Elapsed.TotalSeconds < 3.0f);

            Console.WriteLine();
            Console.WriteLine($"Puzzles per second: {results.Sum(x => x.Batch) / results.Sum(x => x.Seconds)}");
        }
    }
}
