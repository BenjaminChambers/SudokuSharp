using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Output on my laptop configured for Any CPU:
15 Puzzles created in 0.5496921 seconds.
22 Puzzles created in 1.0943824 seconds.
33 Puzzles created in 1.2442839 seconds.
49 Puzzles created in 2.3656245 seconds.
73 Puzzles created in 5.4038927 seconds.

Puzzles per second: 18.0148471614737
Press any key to continue . . .


 * Output on my laptop configured for x64:
15 Puzzles created in 0.3168167 seconds.
22 Puzzles created in 0.6826054 seconds.
33 Puzzles created in 0.7215857 seconds.
49 Puzzles created in 1.3152413 seconds.
73 Puzzles created in 2.9723058 seconds.
109 Puzzles created in 4.0476545 seconds.

Puzzles per second: 29.9317553988086
Press any key to continue . . .


 * Output on my laptop configured for x86:
15 Puzzles created in 0.5546807 seconds.
22 Puzzles created in 1.1423399 seconds.
33 Puzzles created in 1.4761508 seconds.
49 Puzzles created in 2.8223899 seconds.
73 Puzzles created in 5.4428703 seconds.

Puzzles per second: 16.785518042526
Press any key to continue . . .

 */




namespace PuzzleGeneration
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
