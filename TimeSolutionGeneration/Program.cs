using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeSolutionGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random(0);

            int BatchSize = 1_000;

            var results = new List<(int Batch, double Seconds)>();

            TimeSpan Elapsed;
            do
            {
                BatchSize = (3 * BatchSize) / 2;
                var start = DateTime.Now;
                for (int i = 0; i < BatchSize; i++)
                    SudokuSharp.Factory.Solution(rnd);
                Elapsed = DateTime.Now - start;
                Console.WriteLine($"{BatchSize} Solutions created in {Elapsed.TotalSeconds} seconds.");
                results.Add((BatchSize, Elapsed.TotalSeconds));
            } while (Elapsed.TotalSeconds < 3.0f);

            Console.WriteLine();
            Console.WriteLine($"Solutions per second: {results.Sum(x => x.Batch) / results.Sum(x => x.Seconds)}");
        }
    }
}
